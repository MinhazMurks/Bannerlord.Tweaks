namespace Tweaks.Patches
{
	using System;
	using System.Diagnostics.CodeAnalysis;
	using HarmonyLib;
	using SandBox.GameComponents;
	using Settings;

	[HarmonyPatch(typeof(SandboxBattleMoraleModel), "CalculateMaxMoraleChangeDueToAgentIncapacitated")]
	[SuppressMessage("ReSharper", "UnusedType.Global")]
	[SuppressMessage("ReSharper", "UnusedMember.Local")]
	internal class CalculateMaxMoraleChangeDueToAgentIncapacitatedPatch
	{
		private static void Postfix(ref ValueTuple<float, float> __result)
		{

			var settings = Statics.GetSettingsOrThrow();
			__result = new ValueTuple<float, float>(__result.Item1 * settings.BattleMoralTweaksMultiplier, __result.Item2 * settings.BattleMoralTweaksMultiplier);
		}

		private static bool Prepare() => Statics.GetSettingsOrThrow() is {BattleMoralTweaksEnabled: true};
	}

	[HarmonyPatch(typeof(SandboxBattleMoraleModel), "CalculateMaxMoraleChangeDueToAgentPanicked")]
	[SuppressMessage("ReSharper", "UnusedType.Global")]
	[SuppressMessage("ReSharper", "UnusedMember.Local")]
	internal class CalculateMaxMoraleChangeDueToAgentPanickedPatch
	{
		private static void Postfix(ref ValueTuple<float, float> __result)
		{
			var settings = Statics.GetSettingsOrThrow();

			__result = new ValueTuple<float, float>(__result.Item1 * settings.BattleMoralTweaksMultiplier, __result.Item2 * settings.BattleMoralTweaksMultiplier);
		}

		private static bool Prepare() => Statics.GetSettingsOrThrow() is {BattleMoralTweaksEnabled: true};
	}

	[HarmonyPatch(typeof(SandboxBattleMoraleModel), "CalculateMoraleChangeToCharacter")]
	[SuppressMessage("ReSharper", "UnusedType.Global")]
	[SuppressMessage("ReSharper", "UnusedMember.Local")]
	internal class CalculateMoraleChangeToCharacterPatch
	{
		private static void Postfix(ref float __result)
		{
			__result *= Statics.GetSettingsOrThrow().BattleMoralTweaksMultiplier;
		}

		private static bool Prepare() => Statics.GetSettingsOrThrow() is { } settings && settings.BattleMoralTweaksEnabled;
	}
}
