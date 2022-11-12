namespace Tweaks.Patches
{
	using System;
	using HarmonyLib;
	using TaleWorlds.Core;
	using TaleWorlds.MountAndBlade;
	using Tweaks.Settings;
	using Tweaks.Utils;

	[HarmonyPatch(typeof(MissionAgentSpawnLogic), MethodType.Constructor, new Type[] { typeof(IMissionTroopSupplier[]), typeof(BattleSideEnum), typeof(bool) })]
	public class TweakedBattleSizePatch
	{
		private static void Postfix(MissionAgentSpawnLogic __instance, ref int ____battleSize)
		{

			if (TweaksMCMSettings.Instance is { } settings && settings.BattleSize > 0)
			{
				____battleSize = settings.BattleSize;
				if (Statics._settings.BattleSizeDebug)
				{
					IM.ColorGreenMessage("Max Battle Size Modified to: " + settings.BattleSize);
				}

			}

			return;
		}

		private static bool Prepare() => TweaksMCMSettings.Instance is { } settings && settings.BattleSizeTweakEnabled && !settings.BattleSizeTweakExEnabled;
	}
}
