namespace Tweaks.Patches
{
	using System.Diagnostics.CodeAnalysis;
	using HarmonyLib;
	using TaleWorlds.CampaignSystem;
	using TaleWorlds.CampaignSystem.GameComponents;
	using TaleWorlds.CampaignSystem.Party;

	[HarmonyPatch(typeof(DefaultMapVisibilityModel), "GetPartySpottingRange")]
	[SuppressMessage("ReSharper", "UnusedType.Global")]
	[SuppressMessage("ReSharper", "UnusedMember.Local")]
	internal class GetPartySpottingRangePatch
	{
		private static void Postfix(MobileParty party, bool includeDescriptions, ref ExplainedNumber __result)
		{
			/*
						Logging.Lm($"GetPartySpottingRange Postfix Called \n__result.ResultNumber: {__result.ResultNumber}");
						   float existingView = __result.ResultNumber;
						existingView *= Statics.GetSettingsOrThrow().MobilePartyViewDistanceMultiplier;
						__result.Add(existingView - __result.ResultNumber);
						Logging.Lm(
							$"\nStatics.GetSettingsOrThrow().MobilePartyViewDistanceMultiplier: {Statics.GetSettingsOrThrow().MobilePartyViewDistanceMultiplier}" +
							$"existingView: {existingView}\n" +
							$"existingView - __result.ResultNumber: {existingView - __result.ResultNumber}\n" +
							$"__result.ResultNumber: {__result.ResultNumber}\n"
							);*/
		}

		private static bool Prepare() => Statics.GetSettingsOrThrow() is {MobilePartyViewDistanceEnabled: true};
	}

}
