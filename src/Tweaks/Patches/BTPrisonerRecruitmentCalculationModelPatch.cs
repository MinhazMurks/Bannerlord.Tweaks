namespace Tweaks.Patches
{
	using HarmonyLib;
	using TaleWorlds.CampaignSystem;
	using TaleWorlds.CampaignSystem.GameComponents;
	using TaleWorlds.CampaignSystem.Party;
	using TaleWorlds.Core;
	using TaleWorlds.Library;
	using Tweaks.Settings;
	using Tweaks.Utils;

	[HarmonyPatch(typeof(DefaultPrisonerRecruitmentCalculationModel), "GetConformityChangePerHour")]
	internal class BTPrisonerRecruitmentCalculationModelPatch
	{
		private static void Postfix(PartyBase party, CharacterObject troopToBoost, ref int __result)
		{
			if (TweaksMCMSettings.Instance is { } settings && settings.PrisonerConformityTweaksEnabled && party.LeaderHero is not null)
			{
				float num;
				if (party.LeaderHero == Hero.MainHero ||
				  (party.Owner is not null && party.Owner.Clan == Hero.MainHero.Clan && settings.PrisonerConformityTweaksApplyToClan) ||
				  settings.PrisonerConformityTweaksApplyToAi)
				{
					if (Statics._settings.PrisonersDebug)
					{
						IM.MessageDebug("Prisoner ConformityTweak: original: " + __result.ToString() + "   Multiplier: " + (1 + settings.PrisonerConformityTweakBonus).ToString());
					}
					num = __result * (1 + settings.PrisonerConformityTweakBonus);
					if (Statics._settings.PrisonersDebug)
					{
						IM.MessageDebug("Prisoner num Final: " + num.ToString());
					}
					party.MobileParty.EffectiveQuartermaster.AddSkillXp(DefaultSkills.Charm, num * .05f);
					__result = MathF.Round(num);
				}
			}

			// Add Tier-Specific Boosts?
		}

		private static bool Prepare() => TweaksMCMSettings.Instance is { } settings && settings.PrisonerConformityTweaksEnabled;
	}
}
