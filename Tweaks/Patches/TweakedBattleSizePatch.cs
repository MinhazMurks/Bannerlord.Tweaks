using HarmonyLib;
using System;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using Tweaks.Settings;
using Tweaks.Utils;

namespace Tweaks.Patches
{
    [HarmonyPatch(typeof(MissionAgentSpawnLogic), MethodType.Constructor, new Type[] { typeof(IMissionTroopSupplier[]), typeof(BattleSideEnum), typeof(bool) })]
    public class TweakedBattleSizePatch
    {
        static void Postfix(MissionAgentSpawnLogic __instance, ref int ____battleSize)
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

        static bool Prepare() => TweaksMCMSettings.Instance is { } settings && settings.BattleSizeTweakEnabled && !settings.BattleSizeTweakExEnabled;
    }
}
