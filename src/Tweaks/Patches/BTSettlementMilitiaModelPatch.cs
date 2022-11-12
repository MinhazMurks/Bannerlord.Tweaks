namespace Tweaks.Patches
{
	using HarmonyLib;
	using TaleWorlds.CampaignSystem;
	using TaleWorlds.CampaignSystem.GameComponents;
	using TaleWorlds.CampaignSystem.Settlements;
	using TaleWorlds.Localization;
	using Tweaks.Settings;

	[HarmonyPatch(typeof(DefaultSettlementMilitiaModel), "CalculateMilitiaChange")]
	internal class BTSettlementMilitiaModelPatch
	{
		private static void Postfix(Settlement settlement, ref ExplainedNumber __result)
		{
			if (TweaksMCMSettings.Instance is { } settings && settings.SettlementMilitiaBonusEnabled && settlement is not null)
			{
				if (settlement.IsCastle)
				{
					__result.Add(settlement.Militia * 0.025f, new TextObject("{=gHnfFi1s}Retired", null));
					__result.Add(settings.CastleMilitiaRetirementModifier * -settlement.Militia, new TextObject("{=gHnfFi1s}Retired", null));
					__result.Add(settings.CastleMilitiaBonusFlat, new TextObject("Recruitment drive"));
				}
				if (settlement.IsTown)
				{
					__result.Add(settlement.Militia * 0.025f, new TextObject("{=gHnfFi1s}Retired", null));
					__result.Add(settings.TownMilitiaRetirementModifier * -settlement.Militia, new TextObject("{=gHnfFi1s}Retired", null));
					__result.Add(settings.TownMilitiaBonusFlat, new TextObject("Citizen militia"));
				}
				if (settlement.IsVillage)
				{
					__result.Add(settlement.Militia * 0.025f, new TextObject("{=gHnfFi1s}Retired", null));
					__result.Add(settings.VillageMilitiaRetirementModifier * -settlement.Militia, new TextObject("{=gHnfFi1s}Retired", null));
				}
			}
			return;
		}

		private static bool Prepare() => TweaksMCMSettings.Instance is { } settings && settings.SettlementMilitiaBonusEnabled;
	}
}
