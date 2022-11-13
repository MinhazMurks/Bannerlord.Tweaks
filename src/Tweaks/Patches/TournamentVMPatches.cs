namespace Tweaks.Patches
{
	using System;
	using System.Diagnostics.CodeAnalysis;
	using System.Linq;
	using System.Reflection;
	using System.Windows.Forms;
	using HarmonyLib;
	using SandBox.ViewModelCollection.Tournament;
	using Settings;
	using TaleWorlds.CampaignSystem;
	using TaleWorlds.CampaignSystem.CharacterDevelopment;
	using TaleWorlds.Core;
	using Utils;

	[HarmonyPatch(typeof(TournamentVM), "RefreshBetProperties")]
	[SuppressMessage("ReSharper", "UnusedType.Global")]
	[SuppressMessage("ReSharper", "UnusedMember.Local")]
	public class RefreshBetPropertiesPatch
	{
		private static FieldInfo? betAmountFieldInfo;

		private static void Postfix(TournamentVM __instance)
		{
			if (Statics.GetSettingsOrThrow() is not { } settings)
			{
				return;
			}

			if (betAmountFieldInfo == null)
			{
				GetFieldInfo();
			}

			var thisRoundBetAmount = betAmountFieldInfo is not null ? (int)betAmountFieldInfo.GetValue(__instance) : 0;
			var num = settings.TournamentMaxBetAmount;
			if (Hero.MainHero.GetPerkValue(DefaultPerks.Roguery.DeepPockets))
			{
				num *= (int)DefaultPerks.Roguery.DeepPockets.PrimaryBonus;
			}
			__instance.MaximumBetValue = Math.Min(num - thisRoundBetAmount, Hero.MainHero.Gold);
		}

		private static bool Prepare()
		{
			if (Statics.GetSettingsOrThrow() is { } settings && settings.TournamentMaxBetAmountTweakEnabled)
			{
				GetFieldInfo();
				return true;
			}
			return false;
		}

		private static void GetFieldInfo() => betAmountFieldInfo = typeof(TournamentVM).GetField("_thisRoundBettedAmount", BindingFlags.Instance | BindingFlags.NonPublic);
	}


	[HarmonyPatch(typeof(TournamentVM), "RefreshValues")]
	[SuppressMessage("ReSharper", "UnusedType.Global")]
	[SuppressMessage("ReSharper", "UnusedMember.Local")]
	public class RefreshValuesPatch
	{
		private static void Postfix(TournamentVM __instance)
		{
			var num = Statics.GetSettingsOrThrow() is { } settings ? settings.TournamentMaxBetAmount : __instance.MaximumBetValue;
			if (Hero.MainHero.GetPerkValue(DefaultPerks.Roguery.DeepPockets))
			{
				num *= (int)DefaultPerks.Roguery.DeepPockets.PrimaryBonus;
			}
			GameTexts.SetVariable("MAX_AMOUNT", num);
			__instance.BetDescriptionText = GameTexts.FindText("str_tournament_bet_description").ToString();
		}

		private static bool Prepare() => Statics.GetSettingsOrThrow() is { } settings && settings.TournamentMaxBetAmountTweakEnabled;
	}


	[HarmonyPatch(typeof(TournamentVM), "get_IsBetButtonEnabled")]
	[SuppressMessage("ReSharper", "UnusedType.Global")]
	[SuppressMessage("ReSharper", "UnusedMember.Local")]
	public class IsBetButtonEnabledPatch
	{
		private static FieldInfo? betAmountFieldInfo;

		private static bool Prefix(TournamentVM __instance, ref bool __result)
		{
			var failed = false;
			try
			{
				if (betAmountFieldInfo == null)
				{
					GetFieldInfo();
				}

				var result = false;
				if (__instance.IsTournamentIncomplete)
				{
					var thisRoundBetAmount = betAmountFieldInfo is not null ? (int)betAmountFieldInfo.GetValue(__instance) : 0;
					var flag = __instance.Tournament.CurrentMatch.Participants.Any(x => x.Character == CharacterObject.PlayerCharacter);
					var num = Statics.GetSettingsOrThrow() is { } settings ? settings.TournamentMaxBetAmount : __instance.MaximumBetValue;
					if (Hero.MainHero.GetPerkValue(DefaultPerks.Roguery.DeepPockets))
					{
						num *= (int)DefaultPerks.Roguery.DeepPockets.PrimaryBonus;
					}
					if (flag && thisRoundBetAmount < num)
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
			if (Statics.GetSettingsOrThrow() is {TournamentMaxBetAmountTweakEnabled: true})
			{
				GetFieldInfo();
				return true;
			}
			return false;
		}

		private static void GetFieldInfo() => betAmountFieldInfo = typeof(TournamentVM).GetField("_thisRoundBettedAmount", BindingFlags.Instance | BindingFlags.NonPublic);
	}
}
