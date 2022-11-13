namespace Tweaks.Objects
{
	using TaleWorlds.Core;
	using Utils;

	internal class Arrows : ItemModifiersBase
	{
		public Arrows(ItemObject itemObject) :
			base(itemObject)
		{
			if (Statics.GetSettingsOrThrow().ItemDebugMode)
			{
				MessageUtil.MessageDebug("Arrows : ObjectsBase");
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
			if (Statics.GetSettingsOrThrow().ArrowMultipliersEnabled)
			{
				this.SetItemsValue((int)(this.Item.Value * this.MultiplierPrice), this.MultiplierPrice);
				//SetItemsWeight((int)(_item.Value * multiplerPrice), multiplerPrice);
				this.SetItemsStack(this.MultiplierStack);
			}
		}

		private void GetMultiplierValues()
		{
			this.MultiplierPrice = Statics.GetSettingsOrThrow().ArrowValueMultiplier;
			//multiplierWeight = Statics.GetSettingsOrThrow().ArrowWeightMultiplier;
			this.MultiplierStack = Statics.GetSettingsOrThrow().ArrowMultiplier;
		}
	}
}
