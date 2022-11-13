namespace Tweaks.Objects
{
	using TaleWorlds.Core;
	using Utils;

	internal class Bullets : ItemModifiersBase
	{
		public Bullets(ItemObject itemObject) :
			base(itemObject)
		{
			if (Statics.GetSettingsOrThrow().ItemDebugMode)
			{
				MessageUtil.MessageDebug("Bullets : ObjectsBase");
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
			if (Statics.GetSettingsOrThrow().BulletsMultiplierEnabled)
			{
				this.SetItemsValue((int)(this.Item.Value * this.MultiplierPrice), this.MultiplierPrice);
				//SetItemsWeight((int)(_item.Value * multiplerPrice), multiplerPrice);
				this.SetItemsStack(this.MultiplierStack);
			}
		}

		private void GetMultiplierValues()
		{
			this.MultiplierPrice = Statics.GetSettingsOrThrow().BulletsValueMultiplier;
			//multiplierWeight = Statics.GetSettingsOrThrow().BoltsWeightMultiplier;
			this.MultiplierStack = Statics.GetSettingsOrThrow().BulletsMultiplier;
		}
	}
}
