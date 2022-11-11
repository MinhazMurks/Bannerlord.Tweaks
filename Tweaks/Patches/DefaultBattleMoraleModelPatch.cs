using HarmonyLib;
using SandBox.GameComponents;
using System;
using Tweaks.Settings;

namespace Tweaks.Patches
{
    [HarmonyPatch(typeof(SandboxBattleMoraleModel), "CalculateMaxMoraleChangeDueToAgentIncapacitated")]
    class CalculateMaxMoraleChangeDueToAgentIncapacitatedPatch
    {
        static void Postfix(ref ValueTuple<float, float> __result)
        {
            if (!(TweaksMCMSettings.Instance is null))
            {

                __result = new ValueTuple<float, float>(__result.Item1 * Statics._settings.BattleMoralTweaksMultiplier, __result.Item2 * Statics._settings.BattleMoralTweaksMultiplier);
            }
        }

        static bool Prepare() => TweaksMCMSettings.Instance is { } settings && settings.BattleMoralTweaksEnabled;
    }

    [HarmonyPatch(typeof(SandboxBattleMoraleModel), "CalculateMaxMoraleChangeDueToAgentPanicked")]
    class CalculateMaxMoraleChangeDueToAgentPanickedPatch
    {
        static void Postfix(ref ValueTuple<float, float> __result)
        {
            if (!(TweaksMCMSettings.Instance is null))
            {

                __result = new ValueTuple<float, float>(__result.Item1 * Statics._settings.BattleMoralTweaksMultiplier, __result.Item2 * Statics._settings.BattleMoralTweaksMultiplier);
            }
        }

        static bool Prepare() => TweaksMCMSettings.Instance is { } settings && settings.BattleMoralTweaksEnabled;
    }

    [HarmonyPatch(typeof(SandboxBattleMoraleModel), "CalculateMoraleChangeToCharacter")]
    class CalculateMoraleChangeToCharacterPatch
    {
        static void Postfix(ref float __result)
        {
            if (!(TweaksMCMSettings.Instance is null))
            {

                __result *= Statics._settings.BattleMoralTweaksMultiplier;
            }
        }

        static bool Prepare() => TweaksMCMSettings.Instance is { } settings && settings.BattleMoralTweaksEnabled;
    }
}
