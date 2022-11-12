namespace Tweaks.Objects
{
	using TaleWorlds.Core;
	using Tweaks.Utils;

	public class RangedWeapons : ItemModifiersBase
	{

		public RangedWeapons(ItemObject itemObject) :
			base(itemObject)
		{
			if (this._settings.ItemDebugMode)
			{
				//IM.MessageDebug("RangedWeapons : ObjectsBase");
			}
			this.TweakValues();
		}

		protected void TweakValues()
		{
			if (this._settings.ItemDebugMode)
			{
				IM.MessageDebug("String ID: " + this._item.StringId.ToString() + "  Tier: " + this._item.Tier.ToString() + "  IsCivilian: " + this._item.IsCivilian.ToString() + "  ");
			}
			var multiplerPrice = 1.0f;
			var multiplerWeight = 1.0f;
			this.GetMultiplierValues(ref multiplerPrice, ref multiplerWeight);
			if (this._settings.ItemRangedWeaponsValueModifiers && this._settings.MCMRagedWeaponsModifiers)
			{
				this.SetItemsValue((int)(this._item.Value * multiplerPrice), multiplerPrice);
			}
			if (this._settings.ItemRangedWeaponsWeightModifiers && this._settings.MCMRagedWeaponsModifiers)
			{
				if (this._item.Type != ItemObject.ItemTypeEnum.Thrown)
				{
					//SetItemsWeight(_item.Weight * multiplerWeight, multiplerWeight);
				}

			}
		}

		protected void GetMultiplierValues(ref float multiplierPrice, ref float multiplierWeight)
		{

			if (this._item.Tier == ItemObject.ItemTiers.Tier1)
			{
				multiplierPrice = this._settings.ItemRangedWeaponsTier1PriceMultiplier;
				multiplierWeight = this._settings.ItemRangedWeaponsTier1WeightMultiplier;
			}
			else if (this._item.Tier == ItemObject.ItemTiers.Tier2)
			{
				multiplierPrice = this._settings.ItemRangedWeaponsTier2PriceMultiplier;
				multiplierWeight = this._settings.ItemRangedWeaponsTier2WeightMultiplier;
			}
			else if (this._item.Tier == ItemObject.ItemTiers.Tier3)
			{
				multiplierPrice = this._settings.ItemRangedWeaponsTier3PriceMultiplier;
				multiplierWeight = this._settings.ItemRangedWeaponsTier3WeightMultiplier;
			}
			else if (this._item.Tier == ItemObject.ItemTiers.Tier4)
			{
				multiplierPrice = this._settings.ItemRangedWeaponsTier4PriceMultiplier;
				multiplierWeight = this._settings.ItemRangedWeaponsTier4WeightMultiplier;
			}
			else if (this._item.Tier == ItemObject.ItemTiers.Tier5)
			{
				multiplierPrice = this._settings.ItemRangedWeaponsTier5PriceMultiplier;
				multiplierWeight = this._settings.ItemRangedWeaponsTier5WeightMultiplier;
			}
			else if (this._item.Tier == ItemObject.ItemTiers.Tier6)
			{
				multiplierPrice = this._settings.ItemRangedWeaponsTier6PriceMultiplier;
				multiplierWeight = this._settings.ItemRangedWeaponsTier6WeightMultiplier;
			}
		}
	}
}
