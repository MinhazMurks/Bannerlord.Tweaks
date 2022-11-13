﻿namespace Tweaks.Behaviors
{
	using System;
	using Objects;
	using TaleWorlds.CampaignSystem;
	using TaleWorlds.Core;
	using Utils;
	using static TaleWorlds.Core.ItemObject;

	internal class TweaksCraftingCampaignBehaviors : CampaignBehaviorBase
	{
		//private readonly MbEvent<ItemObject, Crafting.OverrideData> _onNewItemCraftedEvent = new MbEvent<ItemObject, Crafting.OverrideData>();
		public override void RegisterEvents()
		{
			try
			{
				CampaignEvents.OnNewItemCraftedEvent.AddNonSerializedListener(this, this.OnNewItemCraftedEvent);
			}
			catch (Exception ex)
			{
				MessageUtil.MessageError("Kaoses Projectiles Fatal error on RegisterEvents" + ex.ToString());
			}
		}

		public override void SyncData(IDataStore dataStore)
		{

		}

		//private void OnNewItemCraftedEvent(ItemObject itemObject, Crafting.OverrideData overRideData)
		private void OnNewItemCraftedEvent(ItemObject itemObject, Crafting.OverrideData overrideData, bool isCraftingOrderItem)
		{
			if (itemObject != null)
			{
				if (itemObject.ItemType is ItemTypeEnum.Bow or ItemTypeEnum.Crossbow or ItemTypeEnum.Musket
				   or ItemTypeEnum.Pistol or ItemTypeEnum.Thrown)
				{
					new RangedWeapons(itemObject);
				}
				else if (itemObject.ItemType is ItemTypeEnum.OneHandedWeapon or ItemTypeEnum.Polearm
					or ItemTypeEnum.TwoHandedWeapon)
				{
					MessageUtil.MessageDebug($"IS MELEE WEAPON DO NEW MELEE ITEM MODIFICATION");
					new MeleeWeapons(itemObject);
				}
				else if (itemObject.ItemType == ItemTypeEnum.Thrown)
				{
					MessageUtil.MessageDebug($"IS Thrown WEAPON DO NEW thrown ITEM MODIFICATION");
					new Thrown(itemObject);
				}
			}
		}

	}
}
