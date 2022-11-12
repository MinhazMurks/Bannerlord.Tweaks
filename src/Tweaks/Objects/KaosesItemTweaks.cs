namespace Tweaks.Models
{
	using TaleWorlds.Core;
	using TaleWorlds.Library;
	using Tweaks.Objects;
	using Tweaks.Objects.Items;
	using static TaleWorlds.Core.ItemObject;

	public class KaosesItemTweaks
	{
		private readonly MBReadOnlyList<ItemObject> _ItemsList;

		public KaosesItemTweaks(MBReadOnlyList<ItemObject> ItemsList)
		{
			this._ItemsList = ItemsList;
			this.TweakItemValues();
		}

		/*
			@TODO need to update the sub classed objects to be more like two handed weapons 
		also need to make code us the WeaponComponentData WeaponClass for better detection of weapon type

		WeaponComponentData weaponComponent = item.PrimaryWeapon;
		//weaponComponent.WeaponClass == WeaponClass.
		//IM.MessageDebug("StringId: " + item.StringId.ToString() + "  Type:" + item.ItemType.ToString());
		if (weaponComponent != null)
		{
			//IM.MessageDebug("weaponComponent.WeaponClass:" + weaponComponent.WeaponClass.ToString());
		}                
			*/
		protected void TweakItemValues()
		{
			for (var i = 0; i < this._ItemsList.Count; i++)
			{

				var item = this._ItemsList[i];
				if (item.IsTradeGood && !item.IsFood)
				{
					new TradeGoods(item);
				}
				else if (item.IsFood)
				{
					new Food(item);
				}

				if (item.ItemType is ItemTypeEnum.BodyArmor or ItemTypeEnum.Cape or
				   ItemTypeEnum.ChestArmor or ItemTypeEnum.HandArmor
				   or ItemTypeEnum.HeadArmor or ItemTypeEnum.LegArmor
				   or ItemTypeEnum.Shield or ItemTypeEnum.HorseHarness
				   )
				{
					new Armor(item);
				}
				else if (item.ItemType is ItemTypeEnum.Bow or ItemTypeEnum.Crossbow or ItemTypeEnum.Musket
					or ItemTypeEnum.Pistol)//|| item.ItemType == ItemTypeEnum.Thrown
				{
					new RangedWeapons(item);
				}
				else if (item.ItemType is ItemTypeEnum.OneHandedWeapon or ItemTypeEnum.Polearm
					or ItemTypeEnum.TwoHandedWeapon)
				{
					new MeleeWeapons(item);
				}
				else if (item.ItemType == ItemTypeEnum.Arrows)
				{
					new Arrows(item);
				}
				else if (item.ItemType == ItemTypeEnum.Bolts)
				{
					new Bolts(item);
				}
				else if (item.ItemType == ItemTypeEnum.Thrown)
				{
					new Thrown(item);
				}
				else if (item.ItemType == ItemTypeEnum.Bullets)
				{
					new Bullets(item);
				}

			}
		}

	}
}
