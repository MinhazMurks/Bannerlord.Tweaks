namespace Tweaks.Objects
{
	using TaleWorlds.Core;
	using Tweaks.Utils;

	public class Food : ItemModifiersBase
	{

		public Food(ItemObject itemObject) :
			base(itemObject)
		{
			if (this._settings.ItemDebugMode)
			{
				//IM.MessageDebug("Food : ObjectsBase");
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
			if (this._settings.MCMFoodModifiers)
			{
				this.SetItemsValue((int)(this._item.Value * multiplerPrice), multiplerPrice);
				this.SetItemsWeight(this._item.Weight * multiplerWeight, multiplerWeight);
			}
		}

		protected void GetMultiplierValues(ref float multiplierPrice, ref float multiplierWeight)
		{
			if (this._item.HasFoodComponent)
			{
				var tc = this._item.FoodComponent;
				if (tc.MoraleBonus == 0)
				{
					multiplierPrice = Statics._settings.ItemFoodPriceMorale0Multiplier;
					multiplierWeight = Statics._settings.ItemFoodWeightMorale0Multiplier;
				}
				else if (tc.MoraleBonus == 1)
				{
					multiplierPrice = Statics._settings.ItemFoodPriceMorale1Multiplier;
					multiplierWeight = Statics._settings.ItemFoodWeightMorale1Multiplier;
				}
				else if (tc.MoraleBonus == 2)
				{
					multiplierPrice = Statics._settings.ItemFoodPriceMorale2Multiplier;
					multiplierWeight = Statics._settings.ItemFoodWeightMorale2Multiplier;
				}
				else if (tc.MoraleBonus == 3)
				{
					multiplierPrice = Statics._settings.ItemFoodPriceMorale3Multiplier;
					multiplierWeight = Statics._settings.ItemFoodWeightMorale3Multiplier;
				}
			}

		}
	}
}
