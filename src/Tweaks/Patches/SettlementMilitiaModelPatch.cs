namespace Tweaks.Patches
{
	using System.Diagnostics.CodeAnalysis;
	using HarmonyLib;
	using Settings;
	using TaleWorlds.CampaignSystem;
	using TaleWorlds.CampaignSystem.GameComponents;
	using TaleWorlds.CampaignSystem.Settlements;
	using TaleWorlds.Localization;

	[HarmonyPatch(typeof(DefaultSettlementMilitiaModel), "CalculateMilitiaChange")]
	[SuppressMessage("ReSharper", "UnusedType.Global")]
	[SuppressMessage("ReSharper", "UnusedMember.Local")]
	internal class SettlementMilitiaModelPatch
	{
		private static void Postfix(Settlement? settlement, ref ExplainedNumber __result)
		{
			if (Statics.GetSettingsOrThrow() is {SettlementMilitiaBonusEnabled: true} settings && settlement is not null)
			{
				if (settlement.IsCastle)
				{
					__result.Add(settlement.Militia * 0.025f, new TextObject("{=gHnfFi1s}Retired"));
					__result.Add(settings.CastleMilitiaRetirementModifier * -settlement.Militia, new TextObject("{=gHnfFi1s}Retired"));
					__result.Add(settings.CastleMilitiaBonusFlat, new TextObject("Recruitment drive"));
				}
				if (settlement.IsTown)
				{
					__result.Add(settlement.Militia * 0.025f, new TextObject("{=gHnfFi1s}Retired"));
					__result.Add(settings.TownMilitiaRetirementModifier * -settlement.Militia, new TextObject("{=gHnfFi1s}Retired"));
					__result.Add(settings.TownMilitiaBonusFlat, new TextObject("Citizen militia"));
				}
				if (settlement.IsVillage)
				{
					__result.Add(settlement.Militia * 0.025f, new TextObject("{=gHnfFi1s}Retired"));
					__result.Add(settings.VillageMilitiaRetirementModifier * -settlement.Militia, new TextObject("{=gHnfFi1s}Retired"));
				}
			}
		}

		private static bool Prepare() => Statics.GetSettingsOrThrow() is {SettlementMilitiaBonusEnabled: true};
	}
}
