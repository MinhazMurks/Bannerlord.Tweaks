namespace Tweaks.Objects
{
	using TaleWorlds.Core;
	using Utils;

	public class TradeGoods : ItemModifiersBase
	{

		public TradeGoods(ItemObject itemObject) :
			base(itemObject)
		{
			if (this._settings.ItemDebugMode)
			{
				//IM.MessageDebug("TradeGoods : ObjectsBase");
			}
			this.TweakValues();
		}

		protected void TweakValues()
		{
			if (this._settings.ItemDebugMode)
			{
				MessageUtil.MessageDebug("String ID: " + this._item.StringId.ToString() + "  Tier: " + this._item.Tier.ToString() + "  IsCivilian: " + this._item.IsCivilian.ToString() + "  ");
			}
			if (this._settings.MCMTradeGoodsModifiers)
			{
				this.SetItemsValue((int)(this._item.Value * this._settings.ItemTradeGoodsPriceMultiplier), this._settings.ItemTradeGoodsPriceMultiplier);
				this.SetItemsWeight(this._item.Weight * this._settings.ItemTradeGoodsWeightMultiplier, this._settings.ItemTradeGoodsWeightMultiplier);
			}
		}
	}
}
