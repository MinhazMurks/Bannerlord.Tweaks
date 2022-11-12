using HarmonyLib;
using Tweaks.Utils;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameComponents;
using Tweaks.Settings;

namespace Tweaks.Patches
{

    [HarmonyPatch(typeof(DefaultRansomValueCalculationModel), "PrisonerRansomValue")]
    class PrisonerRansomValuePatch
    {
        private static void Postfix(CharacterObject prisoner, Hero sellerHero, ref int __result)
        {
            if (TweaksMCMSettings.Instance.PrisonerPriceTweaksEnabled)
            {
                float tmp = __result * TweaksMCMSettings.Instance.PrisonerPriceMultiplier;
                __result = (int)tmp;
            }
        }

        static bool Prepare() => TweaksMCMSettings.Instance is { } settings && settings.PrisonerPriceTweaksEnabled;
    }

}
