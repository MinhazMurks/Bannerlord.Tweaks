namespace Tweaks.Patches
{
	using System;
	using HarmonyLib;
	using SandBox.GameComponents;
	using Settings;

	[HarmonyPatch(typeof(SandboxBattleMoraleModel), "CalculateMaxMoraleChangeDueToAgentIncapacitated")]
	internal class CalculateMaxMoraleChangeDueToAgentIncapacitatedPatch
	{
		private static void Postfix(ref ValueTuple<float, float> __result)
		{
			if (TweaksMCMSettings.Instance is not null)
			{

				__result = new ValueTuple<float, float>(__result.Item1 * Statics._settings.BattleMoralTweaksMultiplier, __result.Item2 * Statics._settings.BattleMoralTweaksMultiplier);
			}
		}

		private static bool Prepare() => TweaksMCMSettings.Instance is { } settings && settings.BattleMoralTweaksEnabled;
	}

	[HarmonyPatch(typeof(SandboxBattleMoraleModel), "CalculateMaxMoraleChangeDueToAgentPanicked")]
	internal class CalculateMaxMoraleChangeDueToAgentPanickedPatch
	{
		private static void Postfix(ref ValueTuple<float, float> __result)
		{
			if (TweaksMCMSettings.Instance is not null)
			{

				__result = new ValueTuple<float, float>(__result.Item1 * Statics._settings.BattleMoralTweaksMultiplier, __result.Item2 * Statics._settings.BattleMoralTweaksMultiplier);
			}
		}

		private static bool Prepare() => TweaksMCMSettings.Instance is { } settings && settings.BattleMoralTweaksEnabled;
	}

	[HarmonyPatch(typeof(SandboxBattleMoraleModel), "CalculateMoraleChangeToCharacter")]
	internal class CalculateMoraleChangeToCharacterPatch
	{
		private static void Postfix(ref float __result)
		{
			if (TweaksMCMSettings.Instance is not null)
			{

				__result *= Statics._settings.BattleMoralTweaksMultiplier;
			}
		}

		private static bool Prepare() => TweaksMCMSettings.Instance is { } settings && settings.BattleMoralTweaksEnabled;
	}
}
