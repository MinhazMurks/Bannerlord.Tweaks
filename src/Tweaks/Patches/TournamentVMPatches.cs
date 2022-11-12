namespace Tweaks.Patches
{
	using System;
	using System.Linq;
	using System.Reflection;
	using System.Windows.Forms;
	using HarmonyLib;
	using SandBox.ViewModelCollection.Tournament;
	using Settings;
	using TaleWorlds.CampaignSystem;
	using TaleWorlds.CampaignSystem.CharacterDevelopment;
	using TaleWorlds.CampaignSystem.TournamentGames;
	using TaleWorlds.Core;
	using Utils;

	[HarmonyPatch(typeof(TournamentVM), "RefreshBetProperties")]
	public class RefreshBetPropertiesPatch
	{
		private static FieldInfo? bettedAmountFieldInfo = null;

		private static void Postfix(TournamentVM __instance)
		{
			if (TweaksMCMSettings.Instance is not { } settings)
			{
				return;
			}

			if (bettedAmountFieldInfo == null)
			{
				GetFieldInfo();
			}

			var thisRoundBettedAmount = bettedAmountFieldInfo is not null ? (int)bettedAmountFieldInfo.GetValue(__instance) : 0;
			var num = settings.TournamentMaxBetAmount;
			if (Hero.MainHero.GetPerkValue(DefaultPerks.Roguery.DeepPockets))
			{
				num *= (int)DefaultPerks.Roguery.DeepPockets.PrimaryBonus;
			}
			__instance.MaximumBetValue = Math.Min(num - thisRoundBettedAmount, Hero.MainHero.Gold);
		}

		private static bool Prepare()
		{
			if (TweaksMCMSettings.Instance is { } settings && settings.TournamentMaxBetAmountTweakEnabled)
			{
				GetFieldInfo();
				return true;
			}
			return false;
		}

		private static void GetFieldInfo() => bettedAmountFieldInfo = typeof(TournamentVM).GetField("_thisRoundBettedAmount", BindingFlags.Instance | BindingFlags.NonPublic);
	}


	[HarmonyPatch(typeof(TournamentVM), "RefreshValues")]
	public class RefreshValuesPatch
	{
		private static void Postfix(TournamentVM __instance)
		{
			var num = TweaksMCMSettings.Instance is { } settings ? settings.TournamentMaxBetAmount : __instance.MaximumBetValue;
			if (Hero.MainHero.GetPerkValue(DefaultPerks.Roguery.DeepPockets))
			{
				num *= (int)DefaultPerks.Roguery.DeepPockets.PrimaryBonus;
			}
			GameTexts.SetVariable("MAX_AMOUNT", num);
			__instance.BetDescriptionText = GameTexts.FindText("str_tournament_bet_description").ToString();
		}

		private static bool Prepare() => TweaksMCMSettings.Instance is { } settings && settings.TournamentMaxBetAmountTweakEnabled;
	}


	[HarmonyPatch(typeof(TournamentVM), "get_IsBetButtonEnabled")]
	public class IsBetButtonEnabledPatch
	{
		private static FieldInfo? bettedAmountFieldInfo = null;

		private static bool Prefix(TournamentVM __instance, ref bool __result)
		{
			var failed = false;
			try
			{
				if (bettedAmountFieldInfo == null)
				{
					GetFieldInfo();
				}

				var result = false;
				if (__instance.IsTournamentIncomplete)
				{
					var thisRoundBettedAmount = bettedAmountFieldInfo is not null ? (int)bettedAmountFieldInfo.GetValue(__instance) : 0;
					var flag = __instance.Tournament.CurrentMatch.Participants.Any((TournamentParticipant x) => x.Character == CharacterObject.PlayerCharacter);
					var num = TweaksMCMSettings.Instance is { } settings ? settings.TournamentMaxBetAmount : __instance.MaximumBetValue;
					if (Hero.MainHero.GetPerkValue(DefaultPerks.Roguery.DeepPockets))
					{
						num *= (int)DefaultPerks.Roguery.DeepPockets.PrimaryBonus;
					}
					if (flag && thisRoundBettedAmount < num)
					{
						result = Hero.MainHero.Gold > 0;
					}
				}
				__result = result;
			}
			catch (Exception ex)
			{
				failed = true;
				MessageBox.Show($"An error occurred while trying to get IsBetButtonEnabled. Reverting to original...\n\n{ex.ToStringFull()}");
			}
			return failed;
		}

		private static bool Prepare()
		{
			if (TweaksMCMSettings.Instance is { } settings && settings.TournamentMaxBetAmountTweakEnabled)
			{
				GetFieldInfo();
				return true;
			}
			return false;
		}

		private static void GetFieldInfo() => bettedAmountFieldInfo = typeof(TournamentVM).GetField("_thisRoundBettedAmount", BindingFlags.Instance | BindingFlags.NonPublic);
	}
}
