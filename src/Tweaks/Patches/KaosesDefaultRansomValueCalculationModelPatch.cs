namespace Tweaks.Patches
{
	using HarmonyLib;
	using TaleWorlds.CampaignSystem;
	using TaleWorlds.CampaignSystem.GameComponents;
	using Tweaks.Settings;

	[HarmonyPatch(typeof(DefaultRansomValueCalculationModel), "PrisonerRansomValue")]
	internal class PrisonerRansomValuePatch
	{
		private static void Postfix(CharacterObject prisoner, Hero sellerHero, ref int __result)
		{
			if (TweaksMCMSettings.Instance.PrisonerPriceTweaksEnabled)
			{
				var tmp = __result * TweaksMCMSettings.Instance.PrisonerPriceMultiplier;
				__result = (int)tmp;
			}
		}

		private static bool Prepare() => TweaksMCMSettings.Instance is { } settings && settings.PrisonerPriceTweaksEnabled;
	}

}
