namespace Tweaks.Patches
{
	using System;
	using System.Diagnostics.CodeAnalysis;
	using HarmonyLib;
	using TaleWorlds.CampaignSystem.Inventory;
	using TaleWorlds.CampaignSystem.ViewModelCollection.Inventory;
	using TaleWorlds.Core;



	// Token: 0x02000003 RID: 3
	[HarmonyPatch(typeof(SPItemVM))]
	[HarmonyPatch(MethodType.Constructor)]
	[HarmonyPatch(new Type[]
	{
		typeof(InventoryLogic),
		typeof(bool),
		typeof(bool),
		typeof(InventoryMode),
		typeof(ItemRosterElement),
		typeof(InventoryLogic.InventorySide),
		typeof(string),
		typeof(string),
		typeof(int),
		typeof(EquipmentIndex?)
	})]
	[SuppressMessage("ReSharper", "UnusedType.Global")]
	[SuppressMessage("ReSharper", "UnusedMember.Global")]
	public class AutoLockItemsPatch
	{
		// Token: 0x06000004 RID: 4 RVA: 0x00002088 File Offset: 0x00000288
		public static void Postfix(SPItemVM __instance)
		{
			if (__instance.InventorySide == InventoryLogic.InventorySide.PlayerInventory && Statics.GetSettingsOrThrow().MCMAutoLocks)
			{
				var isHorse = __instance.ItemType == EquipmentIndex.Horse;
				if (isHorse && !__instance.StringId.Contains("lame") && Statics.GetSettingsOrThrow().autoLockHorses)
				{
					__instance.IsLocked = true;
				}
				var isFood = __instance.ItemRosterElement.EquipmentElement.Item.IsFood;
				if (isFood && Statics.GetSettingsOrThrow().autoLockFood)
				{
					__instance.IsLocked = true;
				}
				if (__instance.IsCivilianItem && !isFood)
				{
					if (__instance.StringId == "ironIngot1" && Statics.GetSettingsOrThrow().autoLockIronBar1)
					{
						__instance.IsLocked = true;
					}

					if (__instance.StringId == "ironIngot2" && Statics.GetSettingsOrThrow().autoLockIronBar2)
					{
						__instance.IsLocked = true;
					}

					if (__instance.StringId == "ironIngot3" && Statics.GetSettingsOrThrow().autoLockIronBar3)
					{
						__instance.IsLocked = true;
					}

					if (__instance.StringId == "ironIngot4" && Statics.GetSettingsOrThrow().autoLockIronBar4)
					{
						__instance.IsLocked = true;
					}

					if (__instance.StringId == "ironIngot5" && Statics.GetSettingsOrThrow().autoLockIronBar5)
					{
						__instance.IsLocked = true;
					}

					if (__instance.StringId == "ironIngot6" && Statics.GetSettingsOrThrow().autoLockIronBar6)
					{
						__instance.IsLocked = true;
					}

					if (__instance.StringId == "iron" && Statics.GetSettingsOrThrow().autoLockIronOre)
					{
						__instance.IsLocked = true;
					}

					if (__instance.StringId == "silver" && Statics.GetSettingsOrThrow().autoLockSilverOre)
					{
						__instance.IsLocked = true;
					}

					if (__instance.StringId == "hardwood" && Statics.GetSettingsOrThrow().autoLockHardwood)
					{
						__instance.IsLocked = true;
					}


					if (__instance.StringId == "charcoal" && Statics.GetSettingsOrThrow().autoLockCharcol)
					{
						__instance.IsLocked = true;
					}
				}
			}
		}
	}
}

