﻿namespace Tweaks.Patches
{
	using System;
	using System.Diagnostics.CodeAnalysis;
	using HarmonyLib;
	using Settings;
	using TaleWorlds.CampaignSystem;
	using TaleWorlds.CampaignSystem.GameComponents;
	using TaleWorlds.CampaignSystem.Settlements;
	using TaleWorlds.Core;
	using TaleWorlds.Localization;

	[HarmonyPatch(typeof(DefaultSettlementFoodModel), "CalculateTownFoodStocksChange")]
	[SuppressMessage("ReSharper", "UnusedType.Global")]
	[SuppressMessage("ReSharper", "UnusedMember.Local")]
	internal class SettlementFoodModelPatch
	{
		private static void Postfix(Town? town, ref ExplainedNumber __result)
		{
			if (Statics.GetSettingsOrThrow() is { } settings && settings.SettlementFoodBonusEnabled && town is not null)
			{
				if (settings.SettlementProsperityFoodMalusTweakEnabled && settings.SettlementProsperityFoodMalusDivisor != 50)
				{
					var malus = town.Owner.Settlement.Prosperity / 50f;
					var prosperityTextObj = GameTexts.FindText("str_prosperity");
					__result.Add(malus, prosperityTextObj);

					malus = -town.Owner.Settlement.Prosperity / settings.SettlementProsperityFoodMalusDivisor;

					//IM.MessageDebug("Patches CalculateTownFoodStocksChange Tweak: " + settings.SettlementProsperityFoodMalusDivisor.ToString());

					__result.Add(malus, prosperityTextObj);
				}
				if (town.IsCastle)
				{
					__result.Add(Math.Abs(__result.ResultNumber) * (settings.CastleFoodBonus - 1), new TextObject("Military rations"));

					//IM.MessageDebug("Patches CastleFoodBonus Tweak: " + settings.CastleFoodBonus.ToString());

				}


				else if (town.IsTown)
				{
					__result.Add(Math.Abs(__result.ResultNumber) * (settings.TownFoodBonus - 1), new TextObject("Citizen food drive"));

					//IM.MessageDebug("Patches TownFoodBonus Tweak: " + settings.TownFoodBonus.ToString());

				}
			}
		}

		private static bool Prepare() => Statics.GetSettingsOrThrow() is {SettlementFoodBonusEnabled: true};
	}
}
