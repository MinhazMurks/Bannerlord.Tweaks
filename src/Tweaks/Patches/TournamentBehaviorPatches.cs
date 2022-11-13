namespace Tweaks.Patches
{
	using System;
	using System.Diagnostics.CodeAnalysis;
	using System.Reflection;
	using HarmonyLib;
	using SandBox.Tournaments.MissionLogics;
	using Settings;
	using TaleWorlds.CampaignSystem;
	using TaleWorlds.CampaignSystem.CharacterDevelopment;
	using TaleWorlds.Library;

	[HarmonyPatch(typeof(TournamentBehavior), "OnPlayerWinTournament")]
	[SuppressMessage("ReSharper", "UnusedType.Global")]
	[SuppressMessage("ReSharper", "UnusedMember.Local")]
	public class OnPlayerWinTournamentPatch
	{
		private static void Prefix(TournamentBehavior __instance)
		{
			if (Statics.GetSettingsOrThrow() is { } settings)
			{
				typeof(TournamentBehavior).GetProperty("OverallExpectedDenars")?.SetValue(__instance, __instance.OverallExpectedDenars + settings.TournamentGoldRewardAmount);
			}
		}

		private static bool Prepare() => Statics.GetSettingsOrThrow() is {TournamentGoldRewardEnabled: true};
	}

	[HarmonyPatch(typeof(TournamentBehavior), "CalculateBet")]
	[SuppressMessage("ReSharper", "UnusedType.Global")]
	[SuppressMessage("ReSharper", "UnusedMember.Local")]
	public class CalculateBetPatch
	{
		private static PropertyInfo? betOdd = null;

		private static void Postfix(TournamentBehavior __instance)
		{
			if (Statics.GetSettingsOrThrow() is { } settings)
			{
				betOdd?.SetValue(__instance, MathF.Max((float)betOdd.GetValue(__instance), settings.MinimumBettingOdds, 0));
			}
		}

		private static bool Prepare()
		{
			if (Statics.GetSettingsOrThrow() is {MinimumBettingOddsTweakEnabled: true})
			{
				betOdd = typeof(TournamentBehavior).GetProperty(nameof(TournamentBehavior.BetOdd), BindingFlags.Public | BindingFlags.Instance);
				return true;
			}
			return false;
		}
	}

	[HarmonyPatch(typeof(TournamentBehavior), "GetMaximumBet")]
	[SuppressMessage("ReSharper", "UnusedType.Global")]
	[SuppressMessage("ReSharper", "UnusedMember.Local")]
	public class GetMaximumBetPatch
	{
		private static void Postfix(TournamentBehavior __instance, ref int __result)
		{
			if (Statics.GetSettingsOrThrow() is { } settings)
			{
				var num = settings.TournamentMaxBetAmount;
				if (Hero.MainHero.GetPerkValue(DefaultPerks.Roguery.DeepPockets))
				{
					num *= (int)DefaultPerks.Roguery.DeepPockets.PrimaryBonus;
				}
				__result = Math.Min(num, Hero.MainHero.Gold);
			}
		}

		private static bool Prepare() => Statics.GetSettingsOrThrow() is {TournamentMaxBetAmountTweakEnabled: true};
	}
}
