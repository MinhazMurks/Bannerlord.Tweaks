using HarmonyLib;
using KaosesTweaks.Settings;
using KaosesTweaks.Utils;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameComponents;

namespace KaosesTweaks.Patches
{

    [HarmonyPatch(typeof(DefaultRansomValueCalculationModel), "PrisonerRansomValue")]
    class PrisonerRansomValuePatch
    {
        private static void Postfix(CharacterObject prisoner, Hero sellerHero, ref int __result)
        {
            if (KaosesMCMSettings.Instance.PrisonerPriceTweaksEnabled)
            {
                float tmp = __result * KaosesMCMSettings.Instance.PrisonerPriceMultiplier;
                __result = (int)tmp;
            }
        }

        static bool Prepare() => KaosesMCMSettings.Instance is { } settings && settings.PrisonerPriceTweaksEnabled;
    }

}
