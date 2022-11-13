namespace Tweaks.Patches
{
	using System;
	using System.Diagnostics.CodeAnalysis;
	using HarmonyLib;
	using Settings;
	using TaleWorlds.CampaignSystem;
	using TaleWorlds.CampaignSystem.GameComponents;
	using TaleWorlds.Localization;

	[HarmonyPatch(typeof(DefaultCharacterDevelopmentModel), "CalculateLearningRate", new Type[] { typeof(int), typeof(int), typeof(int), typeof(int), typeof(TextObject), typeof(bool) })]
	[SuppressMessage("ReSharper", "UnusedType.Global")]
	[SuppressMessage("ReSharper", "UnusedMember.Local")]
	public class LearningRatePatches
	{
		private static void Postfix(ref ExplainedNumber __result) => __result.LimitMin(Statics.GetSettingsOrThrow().MinimumLearningRate);

		private static bool Prepare() => Statics.GetSettingsOrThrow() is { } settings && settings.MinimumLearningRate != 0.0f;
	}
}
