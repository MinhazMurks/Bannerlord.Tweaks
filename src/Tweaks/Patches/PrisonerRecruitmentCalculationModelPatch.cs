namespace Tweaks.Patches
{
	using System.Diagnostics.CodeAnalysis;
	using HarmonyLib;
	using TaleWorlds.CampaignSystem;
	using TaleWorlds.CampaignSystem.GameComponents;
	using TaleWorlds.CampaignSystem.Party;
	using TaleWorlds.Core;
	using TaleWorlds.Library;
	using Utils;

	[HarmonyPatch(typeof(DefaultPrisonerRecruitmentCalculationModel), "GetConformityChangePerHour")]
	[SuppressMessage("ReSharper", "UnusedType.Global")]
	[SuppressMessage("ReSharper", "UnusedMember.Local")]
	internal class PrisonerRecruitmentCalculationModelPatch
	{
		private static void Postfix(PartyBase party, CharacterObject troopToBoost, ref int __result)
		{
			if (Statics.GetSettingsOrThrow() is {PrisonerConformityTweaksEnabled: true} settings && party.LeaderHero is not null)
			{
				float num;
				if (party.LeaderHero == Hero.MainHero ||
				  (party.Owner is not null && party.Owner.Clan == Hero.MainHero.Clan && settings.PrisonerConformityTweaksApplyToClan) ||
				  settings.PrisonerConformityTweaksApplyToAi)
				{
					if (Statics.GetSettingsOrThrow().PrisonersDebug)
					{
						MessageUtil.MessageDebug("Prisoner ConformityTweak: original: " + __result + "   Multiplier: " + (1 + settings.PrisonerConformityTweakBonus));
					}
					num = __result * (1 + settings.PrisonerConformityTweakBonus);
					if (Statics.GetSettingsOrThrow().PrisonersDebug)
					{
						MessageUtil.MessageDebug("Prisoner num Final: " + num);
					}
					party.MobileParty.EffectiveQuartermaster.AddSkillXp(DefaultSkills.Charm, num * .05f);
					__result = MathF.Round(num);
				}
			}

			// Add Tier-Specific Boosts?
		}

		private static bool Prepare() => Statics.GetSettingsOrThrow() is {PrisonerConformityTweaksEnabled: true};
	}
}
