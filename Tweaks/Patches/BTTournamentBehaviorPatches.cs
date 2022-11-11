using HarmonyLib;
using SandBox.Tournaments.MissionLogics;
using System;
using System.Reflection;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.Library;
using Tweaks.Settings;

namespace Tweaks.Patches
{
    [HarmonyPatch(typeof(TournamentBehavior), "OnPlayerWinTournament")]
    public class OnPlayerWinTournamentPatch
    {
        static void Prefix(TournamentBehavior __instance)
        {
            if (TweaksMCMSettings.Instance is { } settings)
            {
                typeof(TournamentBehavior).GetProperty("OverallExpectedDenars").SetValue(__instance, __instance.OverallExpectedDenars + settings.TournamentGoldRewardAmount);
            }
        }

        static bool Prepare() => TweaksMCMSettings.Instance is { } settings && settings.TournamentGoldRewardEnabled;
    }

    [HarmonyPatch(typeof(TournamentBehavior), "CalculateBet")]
    public class CalculateBetPatch
    {
        private static PropertyInfo? betOdd = null;

        static void Postfix(TournamentBehavior __instance)
        {
            if (TweaksMCMSettings.Instance is { } settings)
            {
                betOdd?.SetValue(__instance, MathF.Max((float)betOdd.GetValue(__instance), settings.MinimumBettingOdds, 0));
            }
        }

        static bool Prepare()
        {
            if (TweaksMCMSettings.Instance is { } settings && settings.MinimumBettingOddsTweakEnabled)
            {
                betOdd = typeof(TournamentBehavior).GetProperty(nameof(TournamentBehavior.BetOdd), BindingFlags.Public | BindingFlags.Instance);
                return true;
            }
            return false;
        }
    }

    [HarmonyPatch(typeof(TournamentBehavior), "GetMaximumBet")]
    public class GetMaximumBetPatch
    {
        static void Postfix(TournamentBehavior __instance, ref int __result)
        {
            if (TweaksMCMSettings.Instance is { } settings)
            {
                int num = settings.TournamentMaxBetAmount;
                if (Hero.MainHero.GetPerkValue(DefaultPerks.Roguery.DeepPockets))
                {
                    num *= (int)DefaultPerks.Roguery.DeepPockets.PrimaryBonus;
                }
                __result = Math.Min(num, Hero.MainHero.Gold);
            }
        }

        static bool Prepare()
        {
            return TweaksMCMSettings.Instance is { } settings && settings.TournamentMaxBetAmountTweakEnabled;
        }
    }
}
