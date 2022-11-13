namespace Tweaks.Objects
{
	using TaleWorlds.Core;
	using Utils;

	public class MeleeWeapons : ItemModifiersBase
	{
		public MeleeWeapons(ItemObject itemObject) :
			base(itemObject)
		{
			if (Statics.GetSettingsOrThrow().ItemDebugMode)
			{
				//IM.MessageDebug("MeleeWeapons : ObjectsBase");
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
			if (Statics.GetSettingsOrThrow().ItemMeleeWeaponValueModifiers && Statics.GetSettingsOrThrow().MCMMeleeWeaponModifiers)
			{
				this.SetItemsValue((int)(this.Item.Value * this.MultiplierPrice), this.MultiplierPrice);
			}
			if (Statics.GetSettingsOrThrow().ItemMeleeWeaponWeightModifiers && Statics.GetSettingsOrThrow().MCMMeleeWeaponModifiers)
			{
				//SetItemsWeight(_item.Weight * multiplerWeight, multiplerWeight);
			}
		}

		private void GetMultiplierValues()
		{

			if (this.Item.Tier == ItemObject.ItemTiers.Tier1)
			{
				this.MultiplierPrice = Statics.GetSettingsOrThrow().ItemMeleeWeaponTier1PriceMultiplier;
				this.MultiplierWeight = Statics.GetSettingsOrThrow().ItemMeleeWeaponTier1WeightMultiplier;
			}
			else if (this.Item.Tier == ItemObject.ItemTiers.Tier2)
			{
				this.MultiplierPrice = Statics.GetSettingsOrThrow().ItemMeleeWeaponTier2PriceMultiplier;
				this.MultiplierWeight = Statics.GetSettingsOrThrow().ItemMeleeWeaponTier2WeightMultiplier;
			}
			else if (this.Item.Tier == ItemObject.ItemTiers.Tier3)
			{
				this.MultiplierPrice = Statics.GetSettingsOrThrow().ItemMeleeWeaponTier3PriceMultiplier;
				this.MultiplierWeight = Statics.GetSettingsOrThrow().ItemMeleeWeaponTier3WeightMultiplier;
			}
			else if (this.Item.Tier == ItemObject.ItemTiers.Tier4)
			{
				this.MultiplierPrice = Statics.GetSettingsOrThrow().ItemMeleeWeaponTier4PriceMultiplier;
				this.MultiplierWeight = Statics.GetSettingsOrThrow().ItemMeleeWeaponTier4WeightMultiplier;
			}
			else if (this.Item.Tier == ItemObject.ItemTiers.Tier5)
			{
				this.MultiplierPrice = Statics.GetSettingsOrThrow().ItemMeleeWeaponTier5PriceMultiplier;
				this.MultiplierWeight = Statics.GetSettingsOrThrow().ItemMeleeWeaponTier5WeightMultiplier;
			}
			else if (this.Item.Tier == ItemObject.ItemTiers.Tier6)
			{
				this.MultiplierPrice = Statics.GetSettingsOrThrow().ItemMeleeWeaponTier6PriceMultiplier;
				this.MultiplierWeight = Statics.GetSettingsOrThrow().ItemMeleeWeaponTier6WeightMultiplier;
			}
		}
	}
}
