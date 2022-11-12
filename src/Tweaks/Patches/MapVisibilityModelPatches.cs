namespace Tweaks.Patches
{
	using HarmonyLib;
	using Settings;
	using TaleWorlds.CampaignSystem;
	using TaleWorlds.CampaignSystem.GameComponents;
	using TaleWorlds.CampaignSystem.Party;

	[HarmonyPatch(typeof(DefaultMapVisibilityModel), "GetPartySpottingRange")]
	internal class GetPartySpottingRangePatch
	{
		private static void Postfix(MobileParty party, bool includeDescriptions, ref ExplainedNumber __result)
		{
			/*
						Logging.Lm($"GetPartySpottingRange Postfix Called \n__result.ResultNumber: {__result.ResultNumber}");
						   float existingView = __result.ResultNumber;
						existingView *= Statics._settings.MobilePartyViewDistanceMultiplier;
						__result.Add(existingView - __result.ResultNumber);
						Logging.Lm(
							$"\nStatics._settings.MobilePartyViewDistanceMultiplier: {Statics._settings.MobilePartyViewDistanceMultiplier}" +
							$"existingView: {existingView}\n" +
							$"existingView - __result.ResultNumber: {existingView - __result.ResultNumber}\n" +
							$"__result.ResultNumber: {__result.ResultNumber}\n"
							);*/
		}

		private static bool Prepare() => TweaksMCMSettings.Instance is { } settings && settings.MobilePartyViewDistanceEnabled;
	}

}
