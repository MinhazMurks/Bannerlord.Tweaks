namespace Tweaks.Patches
{
	using System.Diagnostics.CodeAnalysis;
	using HarmonyLib;
	using Settings;
	using TaleWorlds.CampaignSystem.GameComponents;
	using Utils;

	[HarmonyPatch(typeof(DefaultTournamentModel), "GetRenownReward")]
	[SuppressMessage("ReSharper", "UnusedType.Global")]
	[SuppressMessage("ReSharper", "UnusedMember.Local")]
	internal class DefaultTournamentModelPatch
	{
		private static bool Prefix(ref int __result)
		{
			var settings = Statics.GetSettingsOrThrow();
			__result = settings.TournamentRenownAmount;
			if (settings.TournamentDebug)
			{
				MessageUtil.MessageDebug("Patches TournamentRenownAmount Tweak: " + settings.TournamentRenownAmount.ToString());
			}
			return false;
		}

		private static bool Prepare() => Statics.GetSettingsOrThrow().TournamentRenownIncreaseEnabled;
	}
}
