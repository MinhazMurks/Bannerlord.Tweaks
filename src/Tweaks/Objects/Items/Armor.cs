namespace Tweaks.Objects
{
	using TaleWorlds.Core;
	using Utils;

	public class Armor : ItemModifiersBase
	{

		public Armor(ItemObject itemObject) :
			base(itemObject)
		{
			if (this._settings.ItemDebugMode)
			{
				//IM.MessageDebug("Armor : ObjectsBase");
			}
			this.TweakValues();
		}

		protected void TweakValues()
		{
			if (this._settings.ItemDebugMode)
			{
				MessageUtil.MessageDebug("String ID: " + this._item.StringId.ToString() + "  Tier: " + this._item.Tier.ToString() + "  IsCivilian: " + this._item.IsCivilian.ToString() + "  ");
			}
			var multiplerPrice = 1.0f;
			var multiplerWeight = 1.0f;
			this.GetMultiplierValues(ref multiplerPrice, ref multiplerWeight);
			if (this._settings.ItemArmorValueModifiers && this._settings.MCMArmorModifiers)
			{
				this.SetItemsValue((int)(this._item.Value * multiplerPrice), multiplerPrice);
			}
			if (this._settings.ItemArmorWeightModifiers && this._settings.MCMArmorModifiers)
			{
				this.SetItemsWeight(this._item.Weight * multiplerWeight, multiplerWeight);
			}
		}

		protected void GetMultiplierValues(ref float multiplierPrice, ref float multiplierWeight)
		{

			if (this._item.Tier == ItemObject.ItemTiers.Tier1)
			{
				multiplierPrice = this._settings.ItemArmorTier1PriceMultiplier;
				multiplierWeight = this._settings.ItemArmorTier1WeightMultiplier;
			}
			else if (this._item.Tier == ItemObject.ItemTiers.Tier2)
			{
				multiplierPrice = this._settings.ItemArmorTier2PriceMultiplier;
				multiplierWeight = this._settings.ItemArmorTier2WeightMultiplier;
			}
			else if (this._item.Tier == ItemObject.ItemTiers.Tier3)
			{
				multiplierPrice = this._settings.ItemArmorTier3PriceMultiplier;
				multiplierWeight = this._settings.ItemArmorTier3WeightMultiplier;
			}
			else if (this._item.Tier == ItemObject.ItemTiers.Tier4)
			{
				multiplierPrice = this._settings.ItemArmorTier4PriceMultiplier;
				multiplierWeight = this._settings.ItemArmorTier4WeightMultiplier;
			}
			else if (this._item.Tier == ItemObject.ItemTiers.Tier5)
			{
				multiplierPrice = this._settings.ItemArmorTier5PriceMultiplier;
				multiplierWeight = this._settings.ItemArmorTier5WeightMultiplier;
			}
			else if (this._item.Tier == ItemObject.ItemTiers.Tier6)
			{
				multiplierPrice = this._settings.ItemArmorTier6PriceMultiplier;
				multiplierWeight = this._settings.ItemArmorTier6WeightMultiplier;
			}
		}
	}
}
