using HarmonyLib;
using System;
using System.Reflection;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using Tweaks.Settings;
using Tweaks.Utils;

namespace Tweaks.Patches
{

    [HarmonyPatch]
    class DefaultSettlementGarrisonModelPatch
    {
        private static MethodBase TargetMethod()
        {
            return AccessTools.Method(AccessTools.TypeByName("DefaultSettlementGarrisonModel"), "FindNumberOfTroopsToLeaveToGarrison", new Type[]
            {
                typeof(MobileParty),
                typeof(Settlement)
            }, null);
        }

        private static void Postfix(MobileParty mobileParty, Settlement settlement, ref int __result)
        {
            if (settlement == null || mobileParty == null) return;

            if (TweaksMCMSettings.Instance is { } settings && mobileParty.LeaderHero.Clan == Clan.PlayerClan)
            {
                bool DisableDonationClan = settlement.OwnerClan == Clan.PlayerClan && settings.DisableTroopDonationPatchEnabled;
                bool DisableForAnySettlement = settings.DisableTroopDonationAnyEnabled;

                if (DisableDonationClan || DisableForAnySettlement)
                {
                    if (Statics._settings.SettlementsDebug)
                    {
                        IM.MessageDebug("FindNumberOfTroopsToLeaveToGarrison: IS DISABLED");
                    }
                    __result = 0;
                }
            }
        }
        static bool Prepare() => TweaksMCMSettings.Instance is { } settings && settings.DisableTroopDonationPatchEnabled;
    }
}
