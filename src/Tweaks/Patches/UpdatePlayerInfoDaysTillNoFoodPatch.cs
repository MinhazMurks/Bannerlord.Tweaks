namespace Tweaks.Patches
{
	using HarmonyLib;
	using Settings;
	using TaleWorlds.CampaignSystem.Party;
	using TaleWorlds.CampaignSystem.ViewModelCollection.Map.MapBar;

	[HarmonyPatch(typeof(MapInfoVM), "UpdatePlayerInfo")]
	internal class BTUpdatePlayerInfoDaysTillNoFoodPatch
	{
		private static void Postfix(MapInfoVM __instance) => __instance.TotalFood = MobileParty.MainParty.GetNumDaysForFoodToLast() + 1;

		private static bool Prepare() => TweaksMCMSettings.Instance is { } settings && settings.ShowFoodDaysRemaining;
	}
}
