namespace Tweaks.Patches
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Diagnostics.CodeAnalysis;
	using System.Linq;
	using System.Reflection;
	using HarmonyLib;
	using TaleWorlds.CampaignSystem;
	using TaleWorlds.CampaignSystem.CampaignBehaviors;
	using TaleWorlds.CampaignSystem.CharacterDevelopment;
	using TaleWorlds.CampaignSystem.GameComponents;
	using TaleWorlds.CampaignSystem.Roster;
	using TaleWorlds.CampaignSystem.ViewModelCollection.WeaponCrafting;
	using TaleWorlds.CampaignSystem.ViewModelCollection.WeaponCrafting.Smelting;
	using TaleWorlds.Core;
	using TaleWorlds.Library;
	using TaleWorlds.Localization;
	using Tweaks;
	using Utils;

	//~ BT Tweaks
	[HarmonyPatch(typeof(CraftingCampaignBehavior), "DoSmelting")]
	[SuppressMessage("ReSharper", "UnusedType.Global")]
	[SuppressMessage("ReSharper", "UnusedMember.Local")]
	public class DoSmeltingPatch
	{
		private static MethodInfo? openPartMethodInfo;

		private static void Postfix(CraftingCampaignBehavior __instance, EquipmentElement equipmentElement)
		{
			var item = equipmentElement.Item;
			if (item == null)
			{
				return;
			}

			if (__instance == null)
			{
				throw new ArgumentNullException(nameof(__instance), $"Tried to run postfix for {nameof(CraftingCampaignBehavior)}.DoSmelting but the instance was null.");
			}

			if (openPartMethodInfo == null)
			{
				GetMethodInfo();
			}

			foreach (var piece in SmeltingHelper.GetNewPartsFromSmelting(item))
			{
				if (piece != null && piece.Name != null && openPartMethodInfo != null)
				{
					openPartMethodInfo.Invoke(__instance, new object[] { piece, item.WeaponDesign.Template, true });
				}
			}
		}

		private static bool Prepare()
		{
			if (Statics.GetSettingsOrThrow() is { } settings)
			{
				if (settings.AutoLearnSmeltedParts)
				{
					GetMethodInfo();
				}

				return settings.AutoLearnSmeltedParts;
			}
			else
			{
				return false;
			}
		}

		private static void GetMethodInfo() => openPartMethodInfo = typeof(CraftingCampaignBehavior).GetMethod("OpenPart", BindingFlags.NonPublic | BindingFlags.Instance);
	}

	[HarmonyPatch(typeof(CraftingCampaignBehavior), "OnSessionLaunched")]
	[SuppressMessage("ReSharper", "UnusedType.Global")]
	[SuppressMessage("ReSharper", "UnusedMember.Local")]
	public class OnSessionLaunchedPatch
	{
		//static void Postfix(CraftingCampaignBehavior __instance, CraftingPiece[] ____allCraftingParts, List<CraftingPiece> ____openedParts)
		private static void Postfix(CraftingCampaignBehavior __instance)
		{

			if (Statics.GetSettingsOrThrow()!.craftingUnlockAllParts)
			{
				var ____openedPartsDictionary =
					(Dictionary<CraftingTemplate, List<CraftingPiece>>)AccessTools.Field(typeof(CraftingCampaignBehavior), "_openedPartsDictionary").GetValue(__instance);
				var ____allCraftingTemplates = CraftingTemplate.All;
				List<CraftingPiece> ____openedParts = new();

				foreach (var template in ____allCraftingTemplates)
				{
					____openedParts.AddRange(____openedPartsDictionary[template]);
				}

				var ____allCraftingParts = (from x in CraftingPiece.All
											orderby x.Id
											select x).ToArray<CraftingPiece>();

				var num = ____allCraftingParts.Length;
				var count = ____openedParts.Count;
				var smithingModel = Campaign.Current.Models.SmithingModel;
				var array = (from x in ____allCraftingParts
							 where !____openedParts.Contains(x)
							 select x).ToArray<CraftingPiece>();
				if (Statics.GetSettingsOrThrow().craftingUnlockAllParts)
				{
					if (array.Length != 0 && count < num)
					{
						foreach (var craftingPiece in array)
						{
							if (!____openedParts.Contains(craftingPiece))
							{
								____openedParts.Add(craftingPiece);
							}
						}
						InformationManager.DisplayMessage(new InformationMessage(new TextObject("{=p9F90bc0}KT All Smithing Parts Unlocked:", null).ToString()));

					}
				}
			}
		}

		private static bool Prepare() => Statics.GetSettingsOrThrow() is { } settings && settings.craftingUnlockAllParts;
	}

	[HarmonyPatch(typeof(CraftingCampaignBehavior), "GetMaxHeroCraftingStamina")]
	[SuppressMessage("ReSharper", "UnusedType.Global")]
	[SuppressMessage("ReSharper", "UnusedMember.Local")]
	public class GetMaxHeroCraftingStaminaPatch
	{
		private static void Postfix(CraftingCampaignBehavior __instance, ref int __result) => __result = Statics.GetSettingsOrThrow() is { } settings ? MathF.Round(settings.MaxCraftingStaminaMultiplier * __result) : __result;

		private static bool Prepare() => Statics.GetSettingsOrThrow() is { } settings && settings.CraftingStaminaTweakEnabled;
	}

	[HarmonyPatch(typeof(CraftingCampaignBehavior), "HourlyTick")]
	[SuppressMessage("ReSharper", "UnusedType.Global")]
	[SuppressMessage("ReSharper", "UnusedMember.Local")]
	public class HourlyTickPatch
	{
		private static FieldInfo? recordsInfo;

		private static bool Prefix(CraftingCampaignBehavior __instance)
		{
			if (recordsInfo == null)
			{
				GetRecordsInfo();
			}

			if (recordsInfo == null || __instance == null)
			{
				throw new ArgumentNullException(nameof(__instance), $"Tried to run postfix for {nameof(CraftingCampaignBehavior)}.HourlyTickPatch but the recordsInfo or __instance was null.");
			}

			var records = (IDictionary)recordsInfo.GetValue(__instance);

			foreach (Hero hero in records.Keys)
			{
				var curCraftingStamina = __instance.GetHeroCraftingStamina(hero);

				var settings = Statics.GetSettingsOrThrow();
				if (curCraftingStamina < __instance.GetMaxHeroCraftingStamina(hero))
				{
					var staminaGainAmount = settings.CraftingStaminaGainAmount;

					if (settings.CraftingStaminaGainOutsideSettlementMultiplier < 1 && hero.PartyBelongedTo?.CurrentSettlement == null)
					{
						staminaGainAmount = (int)Math.Ceiling(staminaGainAmount * settings.CraftingStaminaGainOutsideSettlementMultiplier);
					}

					var diff = __instance.GetMaxHeroCraftingStamina(hero) - curCraftingStamina;
					if (diff < staminaGainAmount)
					{
						staminaGainAmount = diff;
					}

					__instance.SetHeroCraftingStamina(hero, Math.Min(__instance.GetMaxHeroCraftingStamina(hero), curCraftingStamina + staminaGainAmount));
				}
			}
			return false;
		}

		private static bool Prepare()
		{
			if (Statics.GetSettingsOrThrow() is { } settings)
			{
				if (settings.CraftingStaminaTweakEnabled)
				{
					GetRecordsInfo();
				}

				return settings.CraftingStaminaTweakEnabled;
			}
			else
			{
				return false;
			}
		}

		private static void GetRecordsInfo() => recordsInfo = typeof(CraftingCampaignBehavior).GetField("_heroCraftingRecords", BindingFlags.Instance | BindingFlags.NonPublic);
	}


	[SuppressMessage("ReSharper", "UnusedType.Global")]
	[SuppressMessage("ReSharper", "UnusedMember.Local")]
	internal class CraftingVMPatch
	{
		[HarmonyPatch(typeof(CraftingVM), "HaveEnergy")]
		private static bool Prefix(ref bool __result)
		{
			__result = true;
			return false;
		}

		private static bool Prepare() => Statics.GetSettingsOrThrow() is { } settings && settings.SmithingEnergyDisable;
	}

	[HarmonyPatch(typeof(SmeltingVM), "RefreshList")]
	[SuppressMessage("ReSharper", "UnusedType.Global")]
	[SuppressMessage("ReSharper", "UnusedMember.Local")]
	internal class RefreshListPatch
	{
		private static void Postfix(SmeltingVM __instance, ItemRoster ____playerItemRoster, Action ____updateValuesOnSelectItemAction)
		{

			if (Statics.GetSettingsOrThrow() is {PreventSmeltingLockedItems: true})
			{
				var locked_items = Campaign.Current.GetCampaignBehavior<ViewDataTrackerCampaignBehavior>().GetInventoryLocks().ToList<string>();

				bool IsLocked(ItemRosterElement item)
				{
					var text = item.EquipmentElement.Item.StringId;
					if (item.EquipmentElement.ItemModifier != null)
					{
						text += item.EquipmentElement.ItemModifier.StringId;
					}
					return locked_items.Contains(text);
				}
				var filteredList = new MBBindingList<SmeltingItemVM>();
				foreach (var sItem in __instance.SmeltableItemList)
				{
					if (!____playerItemRoster.Any(rItem =>
						sItem.EquipmentElement.Item == rItem.EquipmentElement.Item && IsLocked(rItem)
					))
					{
						filteredList.Add(sItem);
					}
				}

				__instance.SmeltableItemList = filteredList;
				__instance.SortController.SetListToControl(__instance.SmeltableItemList);

				if (__instance.SmeltableItemList.Count == 0)
				{
					__instance.CurrentSelectedItem = null;
				}
			}
		}

		private static bool Prepare() => Statics.GetSettingsOrThrow() is { } settings && settings.SmeltingTweakEnabled;
	}

	[HarmonyPatch(typeof(SmeltingVM), "RefreshList")]
	[HarmonyPriority(Priority.VeryLow)]
	[SuppressMessage("ReSharper", "UnusedType.Global")]
	[SuppressMessage("ReSharper", "UnusedMember.Local")]
	public class RefreshListRenamePatch
	{
		private static void Postfix(SmeltingVM __instance, ItemRoster ____playerItemRoster)
		{
			if (Statics.GetSettingsOrThrow() is { } settings && settings.AutoLearnSmeltedParts)
			{
				foreach (var item in __instance.SmeltableItemList)
				{
					var count = SmeltingHelper.GetNewPartsFromSmelting(item.EquipmentElement.Item).Count();
					if (count > 0)
					{
						var parts = count == 1 ? "part" : "parts";
						item.Name = $"{item.Name} ({count} new {parts})";
					}
				}
			}
		}

		private static bool Prepare() => Statics.GetSettingsOrThrow() is { } settings && settings.SmeltingTweakEnabled;
	}


	//~ KT Tweaks

	//~ XP Tweaks
	[HarmonyPatch(typeof(DefaultSmithingModel), "GetSkillXpForRefining")]
	[SuppressMessage("ReSharper", "UnusedType.Global")]
	[SuppressMessage("ReSharper", "UnusedMember.Local")]
	public class GetSkillXpForRefiningPatch
	{
		private static bool Prefix(DefaultSmithingModel __instance, ref Crafting.RefiningFormula refineFormula, ref int __result)
		{
			if (Statics.GetSettingsOrThrow().SmithingXpModifiers)
			{
				float baseXp = MathF.Round(0.3f * (__instance.GetCraftingMaterialItem(refineFormula.Output).Value * refineFormula.OutputCount));
				baseXp *= Statics.GetSettingsOrThrow().SmithingRefiningXpValue;
				if (Statics.GetSettingsOrThrow().CraftingDebug)
				{
					MessageUtil.MessageDebug("GetSkillXpForRefining  base: " + MathF.Round(0.3f * (__instance.GetCraftingMaterialItem(refineFormula.Output).Value * refineFormula.OutputCount)).ToString() + "  new :" + baseXp.ToString());
				}
				__result = (int)baseXp;
				return false;
			}
			return true;
		}

		private static bool Prepare() => Statics.GetSettingsOrThrow() != null && Statics.GetSettingsOrThrow().SmithingXpModifiers && Statics.GetSettingsOrThrow().MCMSmithingHarmoneyPatches;
	}

	[HarmonyPatch(typeof(DefaultSmithingModel), "GetSkillXpForSmelting")]
	[SuppressMessage("ReSharper", "UnusedType.Global")]
	[SuppressMessage("ReSharper", "UnusedMember.Local")]
	public class GetSkillXpForSmeltingPatch
	{
		private static bool Prefix(ItemObject item, ref int __result)
		{
			if (Statics.GetSettingsOrThrow().SmithingXpModifiers)
			{
				MessageUtil.MessageDebug("GetSkillXpForSmelting Patch called");
				float baseXp = MathF.Round(0.02f * item.Value);
				baseXp *= Statics.GetSettingsOrThrow().SmithingSmeltingXpValue;
				MessageUtil.MessageDebug("GetSkillXpForSmelting  base: " + MathF.Round(0.02f * item.Value).ToString() + "  new :" + baseXp.ToString());
				__result = (int)baseXp;
				return false;
			}
			return true;
		}

		private static bool Prepare() => Statics.GetSettingsOrThrow() != null && Statics.GetSettingsOrThrow().SmithingXpModifiers && Statics.GetSettingsOrThrow().MCMSmithingHarmoneyPatches;
	}

	[HarmonyPatch(typeof(DefaultSmithingModel), "GetSkillXpForSmithing")]
	[SuppressMessage("ReSharper", "UnusedType.Global")]
	[SuppressMessage("ReSharper", "UnusedMember.Local")]
	public class GetSkillXpForSmithingPatch
	{
		private static bool Prefix(DefaultSmithingModel __instance, ItemObject item, ref int __result)
		{
			if (Statics.GetSettingsOrThrow().SmithingXpModifiers)
			{
				float baseXp = MathF.Round(0.1f * item.Value);
				baseXp *= Statics.GetSettingsOrThrow().SmithingSmithingXpValue;
				if (Statics.GetSettingsOrThrow().CraftingDebug)
				{
					MessageUtil.MessageDebug("GetSkillXpForSmithing  base: " + MathF.Round(0.1f * item.Value).ToString() + "  new :" + baseXp.ToString());
				}
				__result = (int)baseXp;
				return false;
			}
			return true;
		}

		private static bool Prepare() => Statics.GetSettingsOrThrow() != null && Statics.GetSettingsOrThrow().SmithingXpModifiers && Statics.GetSettingsOrThrow().MCMSmithingHarmoneyPatches;
	}

	//~ Energy Tweaks
	[HarmonyPatch(typeof(DefaultSmithingModel), "GetEnergyCostForRefining")]
	[SuppressMessage("ReSharper", "UnusedType.Global")]
	[SuppressMessage("ReSharper", "UnusedMember.Local")]
	public class GetEnergyCostForRefiningPatch
	{
		private static bool Prefix(Hero hero, ref int __result)
		{
			if (Statics.GetSettingsOrThrow().SmithingEnergyDisable || Statics.GetSettingsOrThrow().CraftingStaminaTweakEnabled)
			{
				MessageUtil.MessageDebug("GetEnergyCostForRefining Patch called");
				var num = 6;
				if (Statics.GetSettingsOrThrow().SmithingEnergyDisable)
				{
					MessageUtil.MessageDebug("GetEnergyCostForRefining: DISABLED ");
					__result = 0;
					return false;
				}

				var tmp = num * Statics.GetSettingsOrThrow().SmithingEnergyRefiningValue;
				MessageUtil.MessageDebug("GetEnergyCostForRefining Old : " + num + " New : " + tmp);
				num = (int)tmp;
				if (hero.GetPerkValue(DefaultPerks.Crafting.PracticalRefiner))
				{
					num = (num + 1) / 2;
				}
				__result = num;
				return false;
			}
			return true;
		}

		private static bool Prepare() => Statics.GetSettingsOrThrow() is { } settings && (settings.SmithingEnergyDisable || settings.CraftingStaminaTweakEnabled) && Statics.GetSettingsOrThrow().MCMSmithingHarmoneyPatches;
	}

	[HarmonyPatch(typeof(DefaultSmithingModel), "GetEnergyCostForSmithing")]
	[SuppressMessage("ReSharper", "UnusedType.Global")]
	[SuppressMessage("ReSharper", "UnusedMember.Local")]
	public class GetEnergyCostForSmithingPatch
	{
		private static bool Prefix(ItemObject item, Hero hero, ref int __result)
		{
			if (Statics.GetSettingsOrThrow().SmithingEnergyDisable || Statics.GetSettingsOrThrow().CraftingStaminaTweakEnabled)
			{
				int.TryParse(item.Tier.ToString(), out var itemTier);
				var tier6 = 6;
				var num = 10 + (tier6 * itemTier);
				if (Statics.GetSettingsOrThrow().SmithingEnergyDisable)
				{
					if (Statics.GetSettingsOrThrow().CraftingDebug)
					{
						MessageUtil.MessageDebug("GetEnergyCostForSmithing: DISABLED ");
					}
					__result = 0;
					return false;
				}
				else
				{
					var tmp = num * Statics.GetSettingsOrThrow().SmithingEnergySmithingValue;
					if (Statics.GetSettingsOrThrow().CraftingDebug)
					{
						MessageUtil.MessageDebug("GetEnergyCostForSmithing Old : " + num.ToString() + " New : " + tmp.ToString());
					}
					num = (int)tmp;
					if (hero.GetPerkValue(DefaultPerks.Crafting.PracticalSmith))
					{
						num = (num + 1) / 2;
					}
					__result = num;
					return false;
				}
			}
			return true;
		}

		private static bool Prepare() => Statics.GetSettingsOrThrow() is { } settings && (settings.SmithingEnergyDisable || settings.CraftingStaminaTweakEnabled) && Statics.GetSettingsOrThrow().MCMSmithingHarmoneyPatches;
	}

	[HarmonyPatch(typeof(DefaultSmithingModel), "GetEnergyCostForSmelting")]
	[SuppressMessage("ReSharper", "UnusedType.Global")]
	[SuppressMessage("ReSharper", "UnusedMember.Local")]
	public class GetEnergyCostForSmeltingPatch
	{
		private static bool Prefix(Hero hero, ref int __result)
		{

			if (Statics.GetSettingsOrThrow().SmithingEnergyDisable || Statics.GetSettingsOrThrow().CraftingStaminaTweakEnabled)
			{
				MessageUtil.MessageDebug("GetEnergyCostForSmelting Patch called");
				var num = 10;
				if (Statics.GetSettingsOrThrow().SmithingEnergyDisable)
				{
					if (Statics.GetSettingsOrThrow().CraftingDebug)
					{
						MessageUtil.MessageDebug("GetEnergyCostForSmelting: DISABLED ");
					}
					__result = 0;
					return false;
				}
				else
				{
					var tmp = num * Statics.GetSettingsOrThrow().SmithingEnergySmeltingValue;
					if (Statics.GetSettingsOrThrow().CraftingDebug)
					{
						MessageUtil.MessageDebug("GetEnergyCostForSmelting Old : " + num.ToString() + " New : " + tmp.ToString());
					}
					num = (int)tmp;
					if (hero.GetPerkValue(DefaultPerks.Crafting.PracticalSmelter))
					{
						num = (num + 1) / 2;
					}
					__result = num;
					return false;
				}
			}
			return true;
		}

		private static bool Prepare() => Statics.GetSettingsOrThrow() is { } settings && (settings.SmithingEnergyDisable || settings.CraftingStaminaTweakEnabled) && Statics.GetSettingsOrThrow().MCMSmithingHarmoneyPatches;

	}













}
