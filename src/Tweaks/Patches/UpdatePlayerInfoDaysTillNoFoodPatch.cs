namespace Tweaks.Patches
{
	using System.Diagnostics.CodeAnalysis;
	using HarmonyLib;
	using Settings;
	using TaleWorlds.CampaignSystem.Party;
	using TaleWorlds.CampaignSystem.ViewModelCollection.Map.MapBar;

	[HarmonyPatch(typeof(MapInfoVM), "UpdatePlayerInfo")]
	[SuppressMessage("ReSharper", "UnusedType.Global")]
	[SuppressMessage("ReSharper", "UnusedMember.Local")]
	internal class UpdatePlayerInfoDaysTillNoFoodPatch
	{
		private static void Postfix(MapInfoVM __instance) => __instance.TotalFood = MobileParty.MainParty.GetNumDaysForFoodToLast() + 1;

		private static bool Prepare() => Statics.GetSettingsOrThrow() is {ShowFoodDaysRemaining: true};
	}
}
