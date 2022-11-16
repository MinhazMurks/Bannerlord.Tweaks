﻿namespace Tweaks.Patches
{
	using System;
	using System.Diagnostics.CodeAnalysis;
	using HarmonyLib;
	using TaleWorlds.CampaignSystem;
	using TaleWorlds.CampaignSystem.BarterSystem.Barterables;

	[HarmonyPatch(typeof(JoinKingdomAsClanBarterable), "GetUnitValueForFaction")]
	[SuppressMessage("ReSharper", "UnusedType.Global")]
	[SuppressMessage("ReSharper", "UnusedMember.Local")]
	internal class BarterablePatch
	{
		private static void Postfix(ref int __result, IFaction factionForEvaluation, Kingdom ___TargetKingdom)
		{
			if (Statics.GetSettingsOrThrow() is not { } settings)
			{
				return;
			}

			var factionLeader = factionForEvaluation.Leader;

			if (___TargetKingdom.MapFaction == factionForEvaluation.MapFaction || ___TargetKingdom.MapFaction != Hero.MainHero.MapFaction || ___TargetKingdom.Leader != Hero.MainHero)
			{
				return;
			}

			if (factionLeader == null || factionLeader.IsFactionLeader)
			{
				return;
			}

			if (settings.BarterablesTweaksEnabled)
			{
				double cost = __result * settings.BarterablesJoinKingdomAsClanAdjustment;

				__result = (int)Math.Round(cost);
			}

			if (settings.BarterablesJoinKingdomAsClanAltFormulaEnabled)
			{
				__result /= 10;

				var relations = Hero.MainHero.GetRelation(factionLeader);
				if (relations > 100)
				{
					relations = 99;
				}

				var percent = Math.Abs(((double)relations / 100) - 1);

				var num2 = (relations > -1) ? (__result * percent) : __result * percent * 100;

				__result = (int)Math.Round(num2);
			}
		}

		private static bool Prepare => Statics.GetSettingsOrThrow() is {BarterablesTweaksEnabled: true};
	}
}