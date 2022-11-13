namespace Tweaks.Objects
{
	using TaleWorlds.Core;
	using Utils;

	internal class Bolts : ItemModifiersBase
	{

		public Bolts(ItemObject itemObject) :
			base(itemObject)
		{
			if (Statics.GetSettingsOrThrow().ItemDebugMode)
			{
				MessageUtil.MessageDebug("Bolts : ObjectsBase");
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
			if (Statics.GetSettingsOrThrow().BoltsMultipliersEnabled)
			{
				this.SetItemsValue((int)(this.Item.Value * this.MultiplierPrice), this.MultiplierPrice);
				//SetItemsWeight((int)(_item.Value * multiplerPrice), multiplerPrice);
				this.SetItemsStack(this.MultiplierStack);
			}
		}

		private void GetMultiplierValues()
		{
			this.MultiplierPrice = Statics.GetSettingsOrThrow().BoltsValueMultiplier;
			//multiplierWeight = Statics.GetSettingsOrThrow().BoltsWeightMultiplier;
			this.MultiplierStack = Statics.GetSettingsOrThrow().BoltsMultiplier;
		}
	}
}
