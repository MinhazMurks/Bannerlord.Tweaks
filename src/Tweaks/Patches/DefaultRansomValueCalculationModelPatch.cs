namespace Tweaks.Patches
{
	using System.Diagnostics.CodeAnalysis;
	using HarmonyLib;
	using Settings;
	using TaleWorlds.CampaignSystem;
	using TaleWorlds.CampaignSystem.GameComponents;

	[HarmonyPatch(typeof(DefaultRansomValueCalculationModel), "PrisonerRansomValue")]
	[SuppressMessage("ReSharper", "UnusedType.Global")]
	[SuppressMessage("ReSharper", "UnusedMember.Local")]
	internal class PrisonerRansomValuePatch
	{
		private static void Postfix(CharacterObject prisoner, Hero sellerHero, ref int __result)
		{
			if (Statics.GetSettingsOrThrow().PrisonerPriceTweaksEnabled)
			{
				var tmp = __result * Statics.GetSettingsOrThrow().PrisonerPriceMultiplier;
				__result = (int)tmp;
			}
		}

		private static bool Prepare() => Statics.GetSettingsOrThrow() is {PrisonerPriceTweaksEnabled: true};
	}

}
