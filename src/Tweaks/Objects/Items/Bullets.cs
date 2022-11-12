namespace Tweaks.Objects.Items
{
	using TaleWorlds.Core;
	using Tweaks.Utils;

	internal class Bullets : ItemModifiersBase
	{
		public Bullets(ItemObject itemObject) :
			base(itemObject)
		{
			if (this._settings.ItemDebugMode)
			{
				IM.MessageDebug("Bullets : ObjectsBase");
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
			var multiplerStack = 1.0f;
			this.GetMultiplierValues(ref multiplerPrice, ref multiplerWeight, ref multiplerStack);
			if (Statics._settings.BulletsMultiplierEnabled)
			{
				this.SetItemsValue((int)(this._item.Value * multiplerPrice), multiplerPrice);
				//SetItemsWeight((int)(_item.Value * multiplerPrice), multiplerPrice);
				this.SetItemsStack(multiplerStack);
			}
		}

		protected void GetMultiplierValues(ref float multiplierPrice, ref float multiplierWeight, ref float multiplierStack)
		{
			multiplierPrice = Statics._settings.BulletsValueMultiplier;
			//multiplierWeight = Statics._settings.BoltsWeightMultiplier;
			multiplierStack = Statics._settings.BulletsMultiplier;
		}
	}
}
