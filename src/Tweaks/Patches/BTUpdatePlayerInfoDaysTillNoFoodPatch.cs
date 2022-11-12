using HarmonyLib;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.ViewModelCollection.Map.MapBar;
using Tweaks.Settings;

namespace Tweaks.Patches
{
    [HarmonyPatch(typeof(MapInfoVM), "UpdatePlayerInfo")]
    class BTUpdatePlayerInfoDaysTillNoFoodPatch
    {
        private static void Postfix(MapInfoVM __instance)
        {
            __instance.TotalFood = MobileParty.MainParty.GetNumDaysForFoodToLast() + 1;
        }

        static bool Prepare() => TweaksMCMSettings.Instance is { } settings && settings.ShowFoodDaysRemaining;
    }
}
