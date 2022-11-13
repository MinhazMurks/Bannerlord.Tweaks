namespace Tweaks.Patches
{
	using System;
	using System.Diagnostics.CodeAnalysis;
	using HarmonyLib;
	using Settings;
	using TaleWorlds.Core;
	using TaleWorlds.MountAndBlade;
	using Utils;

	[HarmonyPatch(typeof(MissionAgentSpawnLogic), MethodType.Constructor, new Type[] { typeof(IMissionTroopSupplier[]), typeof(BattleSideEnum), typeof(bool) })]
	[SuppressMessage("ReSharper", "UnusedType.Global")]
	[SuppressMessage("ReSharper", "UnusedMember.Local")]
	public class TweakedBattleSizePatch
	{
		private static void Postfix(MissionAgentSpawnLogic __instance, ref int ____battleSize)
		{

			if (Statics.GetSettingsOrThrow() is {BattleSize: > 0} settings)
			{
				____battleSize = settings.BattleSize;
				if (Statics.GetSettingsOrThrow().BattleSizeDebug)
				{
					MessageUtil.ColorGreenMessage("Max Battle Size Modified to: " + settings.BattleSize);
				}

			}
		}

		private static bool Prepare() => Statics.GetSettingsOrThrow() is {BattleSizeTweakEnabled: true, BattleSizeTweakExEnabled: false};
	}
}
