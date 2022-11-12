namespace Tweaks.Patches
{
	using System;
	using HarmonyLib;
	using Settings;
	using TaleWorlds.CampaignSystem.GameComponents;
	using Utils;

	[HarmonyPatch(typeof(DefaultBanditDensityModel), "GetPlayerMaximumTroopCountForHideoutMission")]
	public class BTTroopCountForHidoutMission
	{
		public static bool Prefix(ref int __result)
		{
			if (TweaksMCMSettings.Instance.HideoutBattleTroopLimitTweakEnabled)
			{
				if (TweaksMCMSettings.Instance.BattleSizeDebug)
				{
					MessageUtil.MessageDebug($"Hideout Battle Troop Limit Tweak: original: {__result}");
				}
				__result = Math.Min(TweaksMCMSettings.Instance.HideoutBattleTroopLimit, 90);
				if (TweaksMCMSettings.Instance.BattleSizeDebug)
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
