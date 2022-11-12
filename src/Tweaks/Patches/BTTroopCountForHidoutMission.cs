namespace Tweaks.Patches
{
	using System;
	using HarmonyLib;
	using TaleWorlds.CampaignSystem.GameComponents;
	using Tweaks.Settings;
	using Tweaks.Utils;

	[HarmonyPatch(typeof(DefaultBanditDensityModel), "GetPlayerMaximumTroopCountForHideoutMission")]
	public class BTTroopCountForHidoutMission
	{
		public static bool Prefix(ref int __result)
		{
			if (TweaksMCMSettings.Instance.HideoutBattleTroopLimitTweakEnabled)
			{
				if (TweaksMCMSettings.Instance.BattleSizeDebug)
				{
					IM.MessageDebug($"Hideout Battle Troop Limit Tweak: original: {__result}");
				}
				__result = Math.Min(TweaksMCMSettings.Instance.HideoutBattleTroopLimit, 90);
				if (TweaksMCMSettings.Instance.BattleSizeDebug)
				{
					IM.MessageDebug($"Hideout Battle Troop Limit Tweak: modified: {__result}");
				}
				return false;
			}
			return true;

		}

		//static bool Prepare() => MCMSettings.Instance is { } settings && settings.HideoutBattleTroopLimitTweakEnabled;
	}
}
