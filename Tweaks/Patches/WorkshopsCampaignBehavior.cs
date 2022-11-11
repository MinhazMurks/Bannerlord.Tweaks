using HarmonyLib;
using System;
using System.Runtime.ExceptionServices;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Workshops;
using TaleWorlds.Core;
using TaleWorlds.Localization;
using Tweaks.Settings;
using Tweaks.Utils;


namespace Tweaks.Patches
{

    [HarmonyPatch(typeof(WorkshopsCampaignBehavior), "ProduceOutput", new Type[] { typeof(EquipmentElement), typeof(Town), typeof(Workshop), typeof(int), typeof(bool) })]
    public class ProductionOutputPatch
    {
        private static void Postfix(EquipmentElement outputItem, Town town, Workshop workshop, int count, bool doNotEffectCapital)
        {
            if (Campaign.Current.GameStarted && !doNotEffectCapital && TweaksMCMSettings.Instance is { } settings && settings.EnableWorkshopSellTweak)
            {
                int __state;
                try
                {
                    __state = town.GetItemPrice(outputItem);

                }
                catch (Exception)
                {
                    __state = 0;
                }
                float num = Math.Min(1000, __state) * count * (settings.WorkshopSellTweak - 1f);
                workshop.ChangeGold((int)num);
                town.ChangeGold((int)-num);
                if (Statics._settings.WorkshopsDebug)
                {
                    IM.MessageDebug("Patches WorkshopsCampaignBehavior Workshops are selling: " + num.ToString() + "  Tweak : " + settings.WorkshopSellTweak.ToString());
                }
            }
        }
        static bool Prepare() => TweaksMCMSettings.Instance is { } settings && settings.EnableWorkshopSellTweak;
    }


    [HarmonyPatch(typeof(WorkshopsCampaignBehavior), "ConsumeInput", new Type[] { typeof(ItemCategory), typeof(Town), typeof(Workshop), typeof(bool) })]
    public class ConsumeInputPatch
    {
        private static void Postfix(ItemCategory productionInput, Town town, Workshop workshop, bool doNotEffectCapital)
        {

            if (Campaign.Current.GameStarted && !doNotEffectCapital && TweaksMCMSettings.Instance is { } settings && settings.EnableWorkshopBuyTweak)
            {
                int __state;
                ItemRoster itemRoster = town.Owner.ItemRoster;
                int num2 = itemRoster.FindIndex((ItemObject x) => x.ItemCategory == productionInput);
                if (num2 >= 0)
                {
                    try
                    {
                        ItemObject itemAtIndex = itemRoster.GetItemAtIndex(num2);
                        __state = town.GetItemPrice(itemAtIndex);
                    }
                    catch (Exception)
                    {
                        __state = 0;
                    }
                }
                else
                {
                    __state = 0;
                }
                float num = __state * (settings.WorkshopBuyTweak - 1f);
                if (Statics._settings.WorkshopsDebug)
                {
                    IM.MessageDebug("Patches WorkshopsCampaignBehavior Workshop are paying: " + num.ToString() + "  Tweak : " + settings.WorkshopBuyTweak.ToString());
                }
                workshop.ChangeGold((int)-num);
                town.ChangeGold((int)num);
            }
        }
        static bool Prepare() => TweaksMCMSettings.Instance is { } settings && settings.EnableWorkshopBuyTweak;
    }

    [HarmonyPatch(typeof(ChangeOwnerOfWorkshopAction), "ApplyByWarDeclaration")]
    class KeepWorkshopsOnWarDeclarationPatch
    {
        private static bool Prefix(Workshop workshop, Hero newOwner, WorkshopType workshopType, int capital, bool upgradable, TextObject customName = null)
        {
            if (TweaksMCMSettings.Instance is { } settings && settings.KeepWorkshopsOnWarDeclaration)
                return false;

            return true;
        }
        static bool Prepare() => TweaksMCMSettings.Instance is { } settings && settings.KeepWorkshopsOnWarDeclaration;
    }

    [HarmonyPatch(typeof(ChangeOwnerOfWorkshopAction), "ApplyByBankruptcy")]
    class KeepWorkshopsOnBankruptcyPatch
    {
        private static bool Prefix(Workshop workshop, Hero newOwner, WorkshopType workshopType, int capital, bool upgradable, TextObject customName = null)
        {
            if (TweaksMCMSettings.Instance is { } settings && settings.KeepWorkshopsOnBankruptcy)
                return false;

            return true;
        }
        static bool Prepare() => TweaksMCMSettings.Instance is { } settings && settings.KeepWorkshopsOnBankruptcy;
    }

    /*
        [HarmonyPatch(typeof(DefaultWorkshopModel), "DaysForPlayerSaveWorkshopFromBankruptcy")]
        public class DaysForPlayerSaveWorkshopFromBankruptcyPatch
        {
            private static void Postfix(ref int __result)
            {
                if (MCMSettings.Instance is { } settings && settings.WorkShopBankruptcyModifiers)
                {
                    __result = settings.WorkShopBankruptcyValue;
                }
            }
            static bool Prepare() => MCMSettings.Instance is { } settings && settings.WorkShopBankruptcyModifiers;
        }*/
}
