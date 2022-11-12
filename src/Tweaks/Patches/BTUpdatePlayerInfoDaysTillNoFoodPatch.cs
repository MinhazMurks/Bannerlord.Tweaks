namespace Tweaks.Patches
{
	using HarmonyLib;
	using TaleWorlds.CampaignSystem.Party;
	using TaleWorlds.CampaignSystem.ViewModelCollection.Map.MapBar;
	using Tweaks.Settings;

	[HarmonyPatch(typeof(MapInfoVM), "UpdatePlayerInfo")]
	internal class BTUpdatePlayerInfoDaysTillNoFoodPatch
	{
		private static void Postfix(MapInfoVM __instance) => __instance.TotalFood = MobileParty.MainParty.GetNumDaysForFoodToLast() + 1;

		private static bool Prepare() => TweaksMCMSettings.Instance is { } settings && settings.ShowFoodDaysRemaining;
	}
}
