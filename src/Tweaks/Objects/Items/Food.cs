namespace Tweaks.Objects
{
	using TaleWorlds.Core;
	using Utils;

	public class Food : ItemModifiersBase
	{
		private float multiplierPrice = 1.0f;
		private float multiplierWeight = 1.0f;

		public Food(ItemObject itemObject) :
			base(itemObject)
		{
			if (Statics.GetSettingsOrThrow().ItemDebugMode)
			{
				//IM.MessageDebug("Food : ObjectsBase");
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
			if (Statics.GetSettingsOrThrow().MCMFoodModifiers)
			{
				this.SetItemsValue((int)(this.Item.Value * this.multiplierPrice), this.multiplierPrice);
				this.SetItemsWeight(this.Item.Weight * this.multiplierWeight, this.multiplierWeight);
			}
		}

		private void GetMultiplierValues()
		{
			if (this.Item.HasFoodComponent)
			{
				var tc = this.Item.FoodComponent;
				if (tc.MoraleBonus == 0)
				{
					this.multiplierPrice = Statics.GetSettingsOrThrow().ItemFoodPriceMorale0Multiplier;
					this.multiplierWeight = Statics.GetSettingsOrThrow().ItemFoodWeightMorale0Multiplier;
				}
				else if (tc.MoraleBonus == 1)
				{
					this.multiplierPrice = Statics.GetSettingsOrThrow().ItemFoodPriceMorale1Multiplier;
					this.multiplierWeight = Statics.GetSettingsOrThrow().ItemFoodWeightMorale1Multiplier;
				}
				else if (tc.MoraleBonus == 2)
				{
					this.multiplierPrice = Statics.GetSettingsOrThrow().ItemFoodPriceMorale2Multiplier;
					this.multiplierWeight = Statics.GetSettingsOrThrow().ItemFoodWeightMorale2Multiplier;
				}
				else if (tc.MoraleBonus == 3)
				{
					this.multiplierPrice = Statics.GetSettingsOrThrow().ItemFoodPriceMorale3Multiplier;
					this.multiplierWeight = Statics.GetSettingsOrThrow().ItemFoodWeightMorale3Multiplier;
				}
			}

		}
	}
}
