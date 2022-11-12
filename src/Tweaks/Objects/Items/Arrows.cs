namespace Tweaks.Objects.Items
{
	using TaleWorlds.Core;
	using Utils;

	internal class Arrows : ItemModifiersBase
	{
		public Arrows(ItemObject itemObject) :
			base(itemObject)
		{
			if (this._settings.ItemDebugMode)
			{
				MessageUtil.MessageDebug("Arrows : ObjectsBase");
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
			var multiplerStack = 1.0f;
			this.GetMultiplierValues(ref multiplerPrice, ref multiplerWeight, ref multiplerStack);
			if (Statics._settings.ArrowMultipliersEnabled)
			{
				this.SetItemsValue((int)(this._item.Value * multiplerPrice), multiplerPrice);
				//SetItemsWeight((int)(_item.Value * multiplerPrice), multiplerPrice);
				this.SetItemsStack(multiplerStack);
			}
		}

		protected void GetMultiplierValues(ref float multiplierPrice, ref float multiplierWeight, ref float multiplierStack)
		{
			multiplierPrice = Statics._settings.ArrowValueMultiplier;
			//multiplierWeight = Statics._settings.ArrowWeightMultiplier;
			multiplierStack = Statics._settings.ArrowMultiplier;
		}
	}
}
