namespace Tweaks.Objects
{
	using TaleWorlds.Core;
	using Utils;

	public class MeleeWeapons : ItemModifiersBase
	{
		public MeleeWeapons(ItemObject itemObject) :
			base(itemObject)
		{
			if (this._settings.ItemDebugMode)
			{
				//IM.MessageDebug("MeleeWeapons : ObjectsBase");
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
			if (this._settings.ItemMeleeWeaponValueModifiers && this._settings.MCMMeleeWeaponModifiers)
			{
				this.SetItemsValue((int)(this._item.Value * multiplerPrice), multiplerPrice);
			}
			if (this._settings.ItemMeleeWeaponWeightModifiers && this._settings.MCMMeleeWeaponModifiers)
			{
				//SetItemsWeight(_item.Weight * multiplerWeight, multiplerWeight);
			}
		}

		protected void GetMultiplierValues(ref float multiplierPrice, ref float multiplierWeight)
		{

			if (this._item.Tier == ItemObject.ItemTiers.Tier1)
			{
				multiplierPrice = this._settings.ItemMeleeWeaponTier1PriceMultiplier;
				multiplierWeight = this._settings.ItemMeleeWeaponTier1WeightMultiplier;
			}
			else if (this._item.Tier == ItemObject.ItemTiers.Tier2)
			{
				multiplierPrice = this._settings.ItemMeleeWeaponTier2PriceMultiplier;
				multiplierWeight = this._settings.ItemMeleeWeaponTier2WeightMultiplier;
			}
			else if (this._item.Tier == ItemObject.ItemTiers.Tier3)
			{
				multiplierPrice = this._settings.ItemMeleeWeaponTier3PriceMultiplier;
				multiplierWeight = this._settings.ItemMeleeWeaponTier3WeightMultiplier;
			}
			else if (this._item.Tier == ItemObject.ItemTiers.Tier4)
			{
				multiplierPrice = this._settings.ItemMeleeWeaponTier4PriceMultiplier;
				multiplierWeight = this._settings.ItemMeleeWeaponTier4WeightMultiplier;
			}
			else if (this._item.Tier == ItemObject.ItemTiers.Tier5)
			{
				multiplierPrice = this._settings.ItemMeleeWeaponTier5PriceMultiplier;
				multiplierWeight = this._settings.ItemMeleeWeaponTier5WeightMultiplier;
			}
			else if (this._item.Tier == ItemObject.ItemTiers.Tier6)
			{
				multiplierPrice = this._settings.ItemMeleeWeaponTier6PriceMultiplier;
				multiplierWeight = this._settings.ItemMeleeWeaponTier6WeightMultiplier;
			}
		}
	}
}
