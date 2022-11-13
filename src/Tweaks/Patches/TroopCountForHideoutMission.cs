namespace Tweaks.Patches
{
	using System;
	using System.Diagnostics.CodeAnalysis;
	using HarmonyLib;
	using Settings;
	using TaleWorlds.CampaignSystem.GameComponents;
	using Utils;

	[HarmonyPatch(typeof(DefaultBanditDensityModel), "GetPlayerMaximumTroopCountForHideoutMission")]
	[SuppressMessage("ReSharper", "UnusedType.Global")]
	[SuppressMessage("ReSharper", "UnusedMember.Global")]
	public class TroopCountForHideoutMission
	{
		public static bool Prefix(ref int __result)
		{
			if (Statics.GetSettingsOrThrow().HideoutBattleTroopLimitTweakEnabled)
			{
				if (Statics.GetSettingsOrThrow().BattleSizeDebug)
				{
					MessageUtil.MessageDebug($"Hideout Battle Troop Limit Tweak: original: {__result}");
				}
				__result = Math.Min(Statics.GetSettingsOrThrow().HideoutBattleTroopLimit, 90);
				if (Statics.GetSettingsOrThrow().BattleSizeDebug)
				{
					MessageUtil.MessageDebug($"Hideout Battle Troop Limit Tweak: modified: {__result}");
				}
				return false;
			}
			return true;

		}

		//static bool Prepare() => MCMSettings.Instance is { } settings && settings.HideoutBattleTroopLimitTweakEnabled;
	}
}
