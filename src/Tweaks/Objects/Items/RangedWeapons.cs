namespace Tweaks.Objects
{
	using TaleWorlds.Core;
	using Utils;

	public class RangedWeapons : ItemModifiersBase
	{

		public RangedWeapons(ItemObject itemObject) :
			base(itemObject)
		{
			if (Statics.GetSettingsOrThrow().ItemDebugMode)
			{
				//IM.MessageDebug("RangedWeapons : ObjectsBase");
			}
			this.TweakValues();
		}

		private void TweakValues()
		{
			if (Statics.GetSettingsOrThrow().ItemDebugMode)
			{
				MessageUtil.MessageDebug("String ID: " + this.Item.StringId + "  Tier: " + this.Item.Tier + "  IsCivilian: " + this.Item.IsCivilian + "  ");
			}
			this.GetMultiplierValues();
			if (Statics.GetSettingsOrThrow().ItemRangedWeaponsValueModifiers && Statics.GetSettingsOrThrow().MCMRagedWeaponsModifiers)
			{
				this.SetItemsValue((int)(this.Item.Value * this.MultiplierPrice), this.MultiplierPrice);
			}
			if (Statics.GetSettingsOrThrow().ItemRangedWeaponsWeightModifiers && Statics.GetSettingsOrThrow().MCMRagedWeaponsModifiers)
			{
				if (this.Item.Type != ItemObject.ItemTypeEnum.Thrown)
				{
					//SetItemsWeight(_item.Weight * multiplerWeight, multiplerWeight);
				}

			}
		}

		private void GetMultiplierValues()
		{

			if (this.Item.Tier == ItemObject.ItemTiers.Tier1)
			{
				this.MultiplierPrice = Statics.GetSettingsOrThrow().ItemRangedWeaponsTier1PriceMultiplier;
				this.MultiplierWeight = Statics.GetSettingsOrThrow().ItemRangedWeaponsTier1WeightMultiplier;
			}
			else if (this.Item.Tier == ItemObject.ItemTiers.Tier2)
			{
				this.MultiplierPrice = Statics.GetSettingsOrThrow().ItemRangedWeaponsTier2PriceMultiplier;
				this.MultiplierWeight = Statics.GetSettingsOrThrow().ItemRangedWeaponsTier2WeightMultiplier;
			}
			else if (this.Item.Tier == ItemObject.ItemTiers.Tier3)
			{
				this.MultiplierPrice = Statics.GetSettingsOrThrow().ItemRangedWeaponsTier3PriceMultiplier;
				this.MultiplierWeight = Statics.GetSettingsOrThrow().ItemRangedWeaponsTier3WeightMultiplier;
			}
			else if (this.Item.Tier == ItemObject.ItemTiers.Tier4)
			{
				this.MultiplierPrice = Statics.GetSettingsOrThrow().ItemRangedWeaponsTier4PriceMultiplier;
				this.MultiplierWeight = Statics.GetSettingsOrThrow().ItemRangedWeaponsTier4WeightMultiplier;
			}
			else if (this.Item.Tier == ItemObject.ItemTiers.Tier5)
			{
				this.MultiplierPrice = Statics.GetSettingsOrThrow().ItemRangedWeaponsTier5PriceMultiplier;
				this.MultiplierWeight = Statics.GetSettingsOrThrow().ItemRangedWeaponsTier5WeightMultiplier;
			}
			else if (this.Item.Tier == ItemObject.ItemTiers.Tier6)
			{
				this.MultiplierPrice = Statics.GetSettingsOrThrow().ItemRangedWeaponsTier6PriceMultiplier;
				this.MultiplierWeight = Statics.GetSettingsOrThrow().ItemRangedWeaponsTier6WeightMultiplier;
			}
		}
	}
}
