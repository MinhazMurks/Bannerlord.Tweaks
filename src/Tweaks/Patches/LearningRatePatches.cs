namespace Tweaks.Patches
{
	using System;
	using HarmonyLib;
	using TaleWorlds.CampaignSystem;
	using TaleWorlds.CampaignSystem.GameComponents;
	using TaleWorlds.Localization;
	using Tweaks.Settings;

	[HarmonyPatch(typeof(DefaultCharacterDevelopmentModel), "CalculateLearningRate", new Type[] { typeof(int), typeof(int), typeof(int), typeof(int), typeof(TextObject), typeof(bool) })]
	public class LearningRatePatches
	{
		private static void Postfix(ref ExplainedNumber __result) => __result.LimitMin(TweaksMCMSettings.Instance.MinimumLearningRate);

		private static bool Prepare() => TweaksMCMSettings.Instance is { } settings && settings.MinimumLearningRate != 0.0f;
	}
}
