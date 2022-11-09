using KaosesTweaks.Utils;
using TaleWorlds.Core;

namespace KaosesTweaks.Objects.Items
{
    public class Food : ItemModifiersBase
    {

        public Food(ItemObject itemObject) :
            base(itemObject)
        {
            if (_settings.ItemDebugMode)
            {
                //IM.MessageDebug("Food : ObjectsBase");
            }
            TweakValues();
        }

        protected void TweakValues()
        {
            if (_settings.ItemDebugMode)
            {
                IM.MessageDebug("String ID: " + _item.StringId + "  Tier: " + _item.Tier + "  IsCivilian: " + _item.IsCivilian + "  ");
            }
            float multiplierPrice = 1.0f;
            float multiplierWeight = 1.0f;
            GetMultiplierValues(ref multiplierPrice, ref multiplierWeight);
            if (_settings.MCMFoodModifiers)
            {
                SetItemsValue((int)(_item.Value * multiplierPrice), multiplierPrice);
                SetItemsWeight(_item.Weight * multiplierWeight, multiplierWeight);
            }
        }

        protected void GetMultiplierValues(ref float multiplierPrice, ref float multiplierWeight)
        {
            if (Statics._settings!= null && _item.HasFoodComponent)
            {
                TradeItemComponent tc = _item.FoodComponent;
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
