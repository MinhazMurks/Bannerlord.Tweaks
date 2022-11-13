namespace Tweaks.Objects
{
	using TaleWorlds.Core;
	using Utils;

	public class Armor : ItemModifiersBase
	{

		public Armor(ItemObject itemObject) :
			base(itemObject)
		{
			if (Statics.GetSettingsOrThrow().ItemDebugMode)
			{
				//IM.MessageDebug("Armor : ObjectsBase");
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
			if (Statics.GetSettingsOrThrow().ItemArmorValueModifiers && Statics.GetSettingsOrThrow().MCMArmorModifiers)
			{
				this.SetItemsValue((int)(this.Item.Value * this.MultiplierPrice), this.MultiplierPrice);
			}
			if (Statics.GetSettingsOrThrow().ItemArmorWeightModifiers && Statics.GetSettingsOrThrow().MCMArmorModifiers)
			{
				this.SetItemsWeight(this.Item.Weight * this.MultiplierWeight, this.MultiplierWeight);
			}
		}

		private void GetMultiplierValues()
		{

			if (this.Item.Tier == ItemObject.ItemTiers.Tier1)
			{
				this.MultiplierPrice = Statics.GetSettingsOrThrow().ItemArmorTier1PriceMultiplier;
				this.MultiplierWeight = Statics.GetSettingsOrThrow().ItemArmorTier1WeightMultiplier;
			}
			else if (this.Item.Tier == ItemObject.ItemTiers.Tier2)
			{
				this.MultiplierPrice = Statics.GetSettingsOrThrow().ItemArmorTier2PriceMultiplier;
				this.MultiplierWeight = Statics.GetSettingsOrThrow().ItemArmorTier2WeightMultiplier;
			}
			else if (this.Item.Tier == ItemObject.ItemTiers.Tier3)
			{
				this.MultiplierPrice = Statics.GetSettingsOrThrow().ItemArmorTier3PriceMultiplier;
				this.MultiplierWeight = Statics.GetSettingsOrThrow().ItemArmorTier3WeightMultiplier;
			}
			else if (this.Item.Tier == ItemObject.ItemTiers.Tier4)
			{
				this.MultiplierPrice = Statics.GetSettingsOrThrow().ItemArmorTier4PriceMultiplier;
				this.MultiplierWeight = Statics.GetSettingsOrThrow().ItemArmorTier4WeightMultiplier;
			}
			else if (this.Item.Tier == ItemObject.ItemTiers.Tier5)
			{
				this.MultiplierPrice = Statics.GetSettingsOrThrow().ItemArmorTier5PriceMultiplier;
				this.MultiplierWeight = Statics.GetSettingsOrThrow().ItemArmorTier5WeightMultiplier;
			}
			else if (this.Item.Tier == ItemObject.ItemTiers.Tier6)
			{
				this.MultiplierPrice = Statics.GetSettingsOrThrow().ItemArmorTier6PriceMultiplier;
				this.MultiplierWeight = Statics.GetSettingsOrThrow().ItemArmorTier6WeightMultiplier;
			}
		}
	}
}
