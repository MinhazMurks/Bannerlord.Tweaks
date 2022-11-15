namespace Tweaks.Patches
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Diagnostics.CodeAnalysis;
	using System.Linq;
	using System.Reflection;
	using System.Windows.Forms;
	using HarmonyLib;
	using SandBox.Missions.MissionLogics;
	using TaleWorlds.Core;
	using TaleWorlds.Library;
	using TaleWorlds.MountAndBlade;
	using static TaleWorlds.MountAndBlade.Agent;

	[HarmonyPatch(typeof(HideoutMissionController), "IsSideDepleted")]
	[SuppressMessage("ReSharper", "UnusedType.Global")]
	[SuppressMessage("ReSharper", "UnusedMember.Local")]
	internal class IsSideDepletedPatch
	{
		public static bool Notified { get; set; }
		public static bool Yelled { get; set; }
		public static bool Dueled { get; set; }

		private static void Postfix(HideoutMissionController __instance, ref bool __result, BattleSideEnum side, ref int ____hideoutMissionState, Team ____enemyTeam)
		{
			if (__result && side == BattleSideEnum.Attacker)
			{
				try
				{
					if (HasTroopsRemaining(__instance, side))
					{
						if (PlayerIsDead() && Statics.GetSettingsOrThrow() is { } settings)
						{
							if (____hideoutMissionState is 5 or 6)
							{
								if (settings.ContinueHideoutBattleOnPlayerLoseDuel)
								{
									if (!Notified)
									{
										SetTeamsHostile(__instance, ____enemyTeam);
										FreeAgentsToMove(__instance);
										TryAlarmAgents(__instance);
										MakeAgentsYell(__instance);
										TrySetFormationsCharge(__instance, BattleSideEnum.Attacker);
										TrySetFormationsCharge(__instance, BattleSideEnum.Defender);
										InformationManager.DisplayMessage(new InformationMessage("You have lost the duel! Your men are avenging your defeat!"));
										Notified = true;
										Dueled = true;
									}

									____hideoutMissionState = 6;

									__result = false;
								}
							}
							else
							{
								if (settings.ContinueHideoutBattleOnPlayerDeath && !Dueled)
								{
									if (!Notified)
									{
										TrySetFormationsCharge(__instance, BattleSideEnum.Attacker);
										MakeAgentsYell(__instance, BattleSideEnum.Attacker);
										InformationManager.DisplayMessage(new InformationMessage("You have fallen in the attack. Your troops are charging to avenge you!"));
										Notified = true;
									}

									if (____hideoutMissionState is not 1 and not 6)
									{
										____hideoutMissionState = 1;
									}

									__result = false;
								}
							}
						}
					}
				}
				catch (Exception exception)
				{
					MessageBox.Show(exception.Message + "\n" + exception.StackTrace + "\n" + exception.InnerException);
				}
			}
		}

		private static bool Prepare() => Statics.GetSettingsOrThrow() is { } settings && (settings.ContinueHideoutBattleOnPlayerDeath || settings.ContinueHideoutBattleOnPlayerLoseDuel);


		private static bool HasTroopsRemaining(HideoutMissionController controller, BattleSideEnum side)
		{
			var missionSides = (IList)typeof(HideoutMissionController).GetField("_missionSides", BindingFlags.NonPublic | BindingFlags.Instance)
				?.GetValue(controller);
			var mSide = missionSides?[(int)side];
			var numTroops = (int)(typeof(HideoutMissionController).GetNestedType("MissionSide", BindingFlags.NonPublic)
				.GetProperty("NumberOfActiveTroops", BindingFlags.Public | BindingFlags.Instance)
				?.GetValue(mSide) ?? throw new InvalidOperationException());
			return numTroops > 0;
		}

		private static bool PlayerIsDead() => Main == null || !Main.IsActive();

		private static void TrySetFormationsCharge(HideoutMissionController controller, BattleSideEnum side)
		{
			var teams = (from t in controller.Mission.Teams
						 where t.Side == side
						 select t).ToList();
			if (teams.Count > 0)
			{
				foreach (var team in teams)
				{
					foreach (var formation in team.Formations)
					{

						if (formation.GetReadonlyMovementOrderReference().OrderType != OrderType.Charge)
						{
							formation.SetMovementOrder(MovementOrder.MovementOrderCharge);
						}
						/*
			if (formation.MovementOrder.OrderType != OrderType.Charge)
			formation.MovementOrder = MovementOrder.MovementOrderCharge;*/
					}
				}
			}
		}

		private static void TryAlarmAgents(HideoutMissionController controller)
		{
			foreach (var agent in controller.Mission.Agents)
			{

				if (agent.IsAIControlled && agent.CurrentWatchState != WatchState.Alarmed)
				{
					agent.SetWatchState(WatchState.Alarmed);
				}
			}
		}

		private static void MakeAgentsYell(HideoutMissionController controller, BattleSideEnum side)
		{
			foreach (var agent in controller.Mission.Agents)
			{
				if (agent.IsActive() && agent.Team.Side == side)
				{
					agent.SetWantsToYell();
				}
			}
		}

		private static void MakeAgentsYell(HideoutMissionController controller)
		{
			MakeAgentsYell(controller, BattleSideEnum.Attacker);
			MakeAgentsYell(controller, BattleSideEnum.Defender);
		}

		private static void SetTeamsHostile(HideoutMissionController controller, Team enemyTeam)
		{
			var passivePlayerTeam = controller.Mission.Teams.FirstOrDefault(x => x.Side == BattleSideEnum.None && x.Banner == controller.Mission.PlayerTeam.Banner);
			var passiveEnemyTeam = controller.Mission.Teams.FirstOrDefault(x => x.Side == BattleSideEnum.None && x.Banner == enemyTeam.Banner);

			if (passivePlayerTeam != null)
			{
				var list = new List<Agent>(passivePlayerTeam.ActiveAgents);
				foreach (var agent in list)
				{
					agent.SetTeam(controller.Mission.Teams.Attacker, true);
				}
			}
			if (passiveEnemyTeam != null)
			{
				var list = new List<Agent>(passiveEnemyTeam.ActiveAgents);
				foreach (var agent in list)
				{
					agent.SetTeam(controller.Mission.Teams.Defender, true);
				}
			}
			controller.Mission.Teams.Attacker.SetIsEnemyOf(controller.Mission.Teams.Defender, true);
		}

		private static void FreeAgentsToMove(HideoutMissionController controller)
		{
			foreach (var agent in controller.Mission.Agents)
			{
				if (agent.IsActive())
				{
					agent.DisableScriptedMovement();
				}
			}
		}
	}

	[HarmonyPatch(typeof(HideoutMissionController), "InitializeMission")]
	[SuppressMessage("ReSharper", "UnusedType.Global")]
	[SuppressMessage("ReSharper", "UnusedMember.Local")]
	public class InitializeMissionPatch
	{
		private static void Postfix()
		{
			IsSideDepletedPatch.Notified = false;
			IsSideDepletedPatch.Yelled = false;
			IsSideDepletedPatch.Dueled = false;
		}

		private static bool Prepare() => Statics.GetSettingsOrThrow() is { } settings && (settings.ContinueHideoutBattleOnPlayerDeath || settings.ContinueHideoutBattleOnPlayerLoseDuel);
	}
}
