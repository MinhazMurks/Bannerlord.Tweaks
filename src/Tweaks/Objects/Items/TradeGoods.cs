namespace Tweaks.Objects
{
	using TaleWorlds.Core;
	using Utils;

	public class TradeGoods : ItemModifiersBase
	{

		public TradeGoods(ItemObject itemObject) :
			base(itemObject)
		{
			if (Statics.GetSettingsOrThrow().ItemDebugMode)
			{
				//IM.MessageDebug("TradeGoods : ObjectsBase");
			}
			this.TweakValues();
		}

		private void TweakValues()
		{
			if (Statics.GetSettingsOrThrow().ItemDebugMode)
			{
				MessageUtil.MessageDebug("String ID: " + this.Item.StringId + "  Tier: " + this.Item.Tier + "  IsCivilian: " + this.Item.IsCivilian + "  ");
			}
			if (Statics.GetSettingsOrThrow().MCMTradeGoodsModifiers)
			{
				this.SetItemsValue((int)(this.Item.Value * Statics.GetSettingsOrThrow().ItemTradeGoodsPriceMultiplier), Statics.GetSettingsOrThrow().ItemTradeGoodsPriceMultiplier);
				this.SetItemsWeight(this.Item.Weight * Statics.GetSettingsOrThrow().ItemTradeGoodsWeightMultiplier, Statics.GetSettingsOrThrow().ItemTradeGoodsWeightMultiplier);
			}
		}
	}
}
