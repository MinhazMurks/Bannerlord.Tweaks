using HarmonyLib;
using TaleWorlds.CampaignSystem.GameComponents;
using Tweaks.Settings;
using Tweaks.Utils;

namespace Tweaks.Patches
{
    [HarmonyPatch(typeof(DefaultTournamentModel), "GetRenownReward")]
    class DefaultTournamentModelPatch
    {
        static bool Prefix(ref int __result)
        {
            if (!(TweaksMCMSettings.Instance is null))
            {
                __result = TweaksMCMSettings.Instance.TournamentRenownAmount;
                if (Statics._settings.TournamentDebug)
                {
                    IM.MessageDebug("Patches TournamentRenownAmount Tweak: " + TweaksMCMSettings.Instance.TournamentRenownAmount.ToString());
                }
                return false;
            }
            return true;
        }

        static bool Prepare() => TweaksMCMSettings.Instance is { } settings && settings.TournamentRenownIncreaseEnabled;
    }
}
