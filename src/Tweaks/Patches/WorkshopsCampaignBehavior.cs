﻿namespace Tweaks.Patches
{
	using System;
	using System.Diagnostics.CodeAnalysis;
	using HarmonyLib;
	using Settings;
	using TaleWorlds.CampaignSystem;
	using TaleWorlds.CampaignSystem.Actions;
	using TaleWorlds.CampaignSystem.CampaignBehaviors;
	using TaleWorlds.CampaignSystem.Settlements;
	using TaleWorlds.CampaignSystem.Settlements.Workshops;
	using TaleWorlds.Core;
	using TaleWorlds.Localization;
	using Utils;

	[HarmonyPatch(typeof(WorkshopsCampaignBehavior), "ProduceOutput", new Type[] { typeof(EquipmentElement), typeof(Town), typeof(Workshop), typeof(int), typeof(bool) })]
	[SuppressMessage("ReSharper", "UnusedType.Global")]
	[SuppressMessage("ReSharper", "UnusedMember.Local")]
	public class ProductionOutputPatch
	{
		private static void Postfix(EquipmentElement outputItem, Town town, Workshop workshop, int count, bool doNotEffectCapital)
		{
			if (Campaign.Current.GameStarted && !doNotEffectCapital && Statics.GetSettingsOrThrow() is
			    {
				    EnableWorkshopSellTweak: true
			    } settings)
			{
				int __state;
				try
				{
					__state = town.GetItemPrice(outputItem);

				}
				catch (Exception)
				{
					__state = 0;
				}
				var num = Math.Min(1000, __state) * count * (settings.WorkshopSellTweak - 1f);
				workshop.ChangeGold((int)num);
				town.ChangeGold((int)-num);
				if (Statics.GetSettingsOrThrow().WorkshopsDebug)
				{
					MessageUtil.MessageDebug("Patches WorkshopsCampaignBehavior Workshops are selling: " + num + "  Tweak : " + settings.WorkshopSellTweak);
				}
			}
		}

		private static bool Prepare() => Statics.GetSettingsOrThrow() is { } settings && settings.EnableWorkshopSellTweak;
	}


	[HarmonyPatch(typeof(WorkshopsCampaignBehavior), "ConsumeInput", new Type[] { typeof(ItemCategory), typeof(Town), typeof(Workshop), typeof(bool) })]
	[SuppressMessage("ReSharper", "UnusedType.Global")]
	[SuppressMessage("ReSharper", "UnusedMember.Local")]
	public class ConsumeInputPatch
	{
		private static void Postfix(ItemCategory productionInput, Town town, Workshop workshop, bool doNotEffectCapital)
		{

			if (Campaign.Current.GameStarted && !doNotEffectCapital && Statics.GetSettingsOrThrow() is
			    {
				    EnableWorkshopBuyTweak: true
			    } settings)
			{
				int __state;
				var itemRoster = town.Owner.ItemRoster;
				var num2 = itemRoster.FindIndex(x => x.ItemCategory == productionInput);
				if (num2 >= 0)
				{
					try
					{
						var itemAtIndex = itemRoster.GetItemAtIndex(num2);
						__state = town.GetItemPrice(itemAtIndex);
					}
					catch (Exception)
					{
						__state = 0;
					}
				}
				else
				{
					__state = 0;
				}
				var num = __state * (settings.WorkshopBuyTweak - 1f);
				if (Statics.GetSettingsOrThrow().WorkshopsDebug)
				{
					MessageUtil.MessageDebug("Patches WorkshopsCampaignBehavior Workshop are paying: " + num + "  Tweak : " + settings.WorkshopBuyTweak);
				}
				workshop.ChangeGold((int)-num);
				town.ChangeGold((int)num);
			}
		}

		private static bool Prepare() => Statics.GetSettingsOrThrow() is { } settings && settings.EnableWorkshopBuyTweak;
	}

	[HarmonyPatch(typeof(ChangeOwnerOfWorkshopAction), "ApplyByWarDeclaration")]
	[SuppressMessage("ReSharper", "UnusedType.Global")]
	[SuppressMessage("ReSharper", "UnusedMember.Local")]
	internal class KeepWorkshopsOnWarDeclarationPatch
	{
		private static bool Prefix(Workshop workshop, Hero newOwner, WorkshopType workshopType, int capital, bool upgradable, TextObject? customName = null)
		{
			if (Statics.GetSettingsOrThrow() is {KeepWorkshopsOnWarDeclaration: true})
			{
				return false;
			}

			return true;
		}

		private static bool Prepare() => Statics.GetSettingsOrThrow() is {KeepWorkshopsOnWarDeclaration: true};
	}

	[HarmonyPatch(typeof(ChangeOwnerOfWorkshopAction), "ApplyByBankruptcy")]
	[SuppressMessage("ReSharper", "UnusedType.Global")]
	[SuppressMessage("ReSharper", "UnusedMember.Local")]
	internal class KeepWorkshopsOnBankruptcyPatch
	{
		private static bool Prefix(Workshop workshop, Hero newOwner, WorkshopType workshopType, int capital, bool upgradable, TextObject? customName = null)
		{
			if (Statics.GetSettingsOrThrow() is {KeepWorkshopsOnBankruptcy: true})
			{
				return false;
			}

			return true;
		}

		private static bool Prepare() => Statics.GetSettingsOrThrow() is { } settings && settings.KeepWorkshopsOnBankruptcy;
	}

	/*
		[HarmonyPatch(typeof(DefaultWorkshopModel), "DaysForPlayerSaveWorkshopFromBankruptcy")]
		public class DaysForPlayerSaveWorkshopFromBankruptcyPatch
		{
			private static void Postfix(ref int __result)
			{
				if (MCMSettings.Instance is { } settings && settings.WorkShopBankruptcyModifiers)
				{
					__result = settings.WorkShopBankruptcyValue;
				}
			}
			static bool Prepare() => MCMSettings.Instance is { } settings && settings.WorkShopBankruptcyModifiers;
		}*/
}
