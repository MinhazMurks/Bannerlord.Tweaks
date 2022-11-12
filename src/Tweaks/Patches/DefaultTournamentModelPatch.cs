namespace Tweaks.Patches
{
	using HarmonyLib;
	using Settings;
	using TaleWorlds.CampaignSystem.GameComponents;
	using Utils;

	[HarmonyPatch(typeof(DefaultTournamentModel), "GetRenownReward")]
	internal class DefaultTournamentModelPatch
	{
		private static bool Prefix(ref int __result)
		{
			if (TweaksMCMSettings.Instance is not null)
			{
				__result = TweaksMCMSettings.Instance.TournamentRenownAmount;
				if (Statics._settings.TournamentDebug)
				{
					MessageUtil.MessageDebug("Patches TournamentRenownAmount Tweak: " + TweaksMCMSettings.Instance.TournamentRenownAmount.ToString());
				}
				return false;
			}
			return true;
		}

		private static bool Prepare() => TweaksMCMSettings.Instance is { } settings && settings.TournamentRenownIncreaseEnabled;
	}
}
