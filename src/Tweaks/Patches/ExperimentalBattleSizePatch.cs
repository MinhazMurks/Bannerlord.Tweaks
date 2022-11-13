namespace Tweaks.Patches
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics.CodeAnalysis;
	using System.Linq;
	using System.Reflection.Emit;
	using HarmonyLib;
	using Settings;
	using TaleWorlds.CampaignSystem.MapEvents;
	using TaleWorlds.CampaignSystem.Roster;
	using TaleWorlds.CampaignSystem.TroopSuppliers;
	using TaleWorlds.Core;
	using TaleWorlds.MountAndBlade;
	using Utils;

	[HarmonyPatch(typeof(BannerlordConfig), "GetRealBattleSize")]
	[SuppressMessage("ReSharper", "UnusedType.Global")]
	[SuppressMessage("ReSharper", "UnusedMember.Local")]
	public class BattleSizePatchExGetRealBattleSize
	{
		private static void Postfix(ref int __result)
		{
			if (Statics.GetSettingsOrThrow() is { } settings)
			{
				if (BattleSizePatchExPartyGroupTroopSupplier.mountfootratio * settings.BattleSizeEx * (1f + (Statics.GetSettingsOrThrow().ReinforcementQuota * Statics.GetSettingsOrThrow().SlotsForReinforcements * 0.01f)) > 2048)
				{
					__result = (int)(2048 / BattleSizePatchExPartyGroupTroopSupplier.mountfootratio / (1f + (Statics.GetSettingsOrThrow().ReinforcementQuota * Statics.GetSettingsOrThrow().SlotsForReinforcements * 0.01f)));
					MessageUtil.ColorRedMessage("Battle size was adjusted to prevent crashing and ensure reinforcements.");
					if (Statics.GetSettingsOrThrow().BattleSizeDebug)
					{
						var SlotsForMounts = (int)(2048 - (2048 / BattleSizePatchExPartyGroupTroopSupplier.mountfootratio));
						var SlotsForReinforcements = (int)(2048 - SlotsForMounts - ((2048 - SlotsForMounts) / (1f + (Statics.GetSettingsOrThrow().ReinforcementQuota * Statics.GetSettingsOrThrow().SlotsForReinforcements * 0.01f))));
						MessageUtil.ColorRedMessage("MountedRatio: " + BattleSizePatchExPartyGroupTroopSupplier.mountfootratio + " | Reserved: " + (1f + (Statics.GetSettingsOrThrow().ReinforcementQuota * Statics.GetSettingsOrThrow().SlotsForReinforcements * 0.01f)));
						MessageUtil.ColorRedMessage("2048 - Slots mounts (" + SlotsForMounts + ") - Slots reinforcements(" + SlotsForReinforcements + ")");
					}
				}
				else
				{
					__result = settings.BattleSizeEx;
				}

				MessageUtil.ColorRedMessage("Battle size was set to " + __result + ".");
			}
		}

		private static bool Prepare() => Statics.GetSettingsOrThrow() is {BattleSizeTweakExEnabled: true};
	}


	[HarmonyPatch(typeof(MissionAgentSpawnLogic), "get_MaxNumberOfTroopsForMission")]
	[SuppressMessage("ReSharper", "UnusedType.Global")]
	[SuppressMessage("ReSharper", "UnusedMember.Local")]
	internal class BattleSizePatchExGetMaxNumberOfTroopsForMission
	{
		private static void Postfix(ref int __result)
		{
			if (Statics.GetSettingsOrThrow() is { } settings)
			{
				if (BattleSizePatchExPartyGroupTroopSupplier.mountfootratio * settings.BattleSizeEx * (1f + (Statics.GetSettingsOrThrow().ReinforcementQuota * Statics.GetSettingsOrThrow().SlotsForReinforcements * 0.01f)) > 2048)
				{
					__result = (int)(2048 / BattleSizePatchExPartyGroupTroopSupplier.mountfootratio / (1f + (Statics.GetSettingsOrThrow().ReinforcementQuota * Statics.GetSettingsOrThrow().SlotsForReinforcements * 0.01f)));
				}
				else
				{
					__result = settings.BattleSizeEx;
				}
			}
		}

		private static bool Prepare() => Statics.GetSettingsOrThrow() is { } settings && settings.BattleSizeTweakExEnabled;
	}


	[HarmonyPatch(typeof(PartyGroupTroopSupplier), MethodType.Constructor, new Type[] { typeof(MapEvent), typeof(BattleSideEnum), typeof(FlattenedTroopRoster), typeof(Func<UniqueTroopDescriptor, MapEventParty, bool>) })]
	[SuppressMessage("ReSharper", "UnusedType.Global")]
	[SuppressMessage("ReSharper", "UnusedMember.Local")]
	public static class BattleSizePatchExPartyGroupTroopSupplier
	{
		private static void Postfix(MapEvent mapEvent, BattleSideEnum side, FlattenedTroopRoster priorTroops)
		{
			var settings = Statics.GetSettingsOrThrow();

			BattleSizePatchExBattleSizeSpawnTick.AgentTrackerTroop.Clear();
			BattleSizePatchExBattleSizeSpawnTick.AgentTrackerMount.Clear();
			BattleSizePatchExBattleSizeSpawnTick.NumAttackers = 0;
			BattleSizePatchExBattleSizeSpawnTick.NumDefenders = 0;

			if (troops == 0 && mounts == 0)
			{
				foreach (var party in MapEvent.PlayerMapEvent.AttackerSide.Parties)
				{
					foreach (var troop in party.Troops.Troops)
					{
						if (troop.IsMounted)
						{
							mounts += 1f;
							troops += 1f;
						}
						else
						{
							troops += 1f;
						}
					}
				}
				foreach (var party in MapEvent.PlayerMapEvent.DefenderSide.Parties)
				{
					foreach (var troop in party.Troops.Troops)
					{
						if (troop.IsMounted)
						{
							mounts += 1f;
							troops += 1f;
						}
						else
						{
							troops += 1f;
						}
					}
				}
			}
			mountfootratio = 1f + (mounts / troops * settings.BattleSizeExSafePuffer);
			if (MapEvent.PlayerMapEvent.EventType == MapEvent.BattleTypes.Siege)
			{
				mountfootratio = 1f;
				isSiege = true;
			}
		}

		private static bool Prepare() => Statics.GetSettingsOrThrow() is {BattleSizeTweakExEnabled: true};
		public static float mountfootratio;
		public static float troops;
		public static float mounts;
		public static bool isSiege;
	}


	[HarmonyPatch(typeof(MissionAgentSpawnLogic), "PhaseTick")]
	[SuppressMessage("ReSharper", "UnusedType.Global")]
	[SuppressMessage("ReSharper", "UnusedMember.Local")]
	internal static class BattleSizePatchExBattleSizeSpawnTick
	{
		private static bool Prepare() => Statics.GetSettingsOrThrow() is {BattleSizeTweakExEnabled: true};
		private static int runs;
		private static int numberOfTroopsCanBeSpawned;
		public static readonly List<Agent> AgentTrackerTroop = new();
		public static readonly List<Agent> AgentTrackerMount = new();
		public static int NumAttackers;
		public static int NumDefenders;
		public static int removed = 0;

		public static float GetMountRatio() => BattleSizePatchExPartyGroupTroopSupplier.mountfootratio;

		private static bool Prefix(MissionAgentSpawnLogic __instance)
		{
			if (numberOfTroopsCanBeSpawned > MissionAgentSpawnLogic.MaxNumberOfAgentsForMission)
			{
				var NumAttackersNew = __instance.NumberOfActiveAttackerTroops;
				var NumDefendersNew = __instance.NumberOfActiveDefenderTroops;
				if (NumAttackers != 0 && NumDefenders != 0 && (NumAttackers < NumAttackersNew || NumDefenders < NumDefendersNew))
				{
					MessageUtil.ColorRedMessage("Attackers got " + (NumAttackersNew - NumAttackers) + " reinforcements!");
					MessageUtil.ColorRedMessage("Defenders got " + (NumDefendersNew - NumDefenders) + " reinforcements!");
					MessageUtil.ColorRedMessage("Slots were: " + numberOfTroopsCanBeSpawned + ".");
				}
			}
			NumAttackers = __instance.NumberOfActiveAttackerTroops;
			NumDefenders = __instance.NumberOfActiveDefenderTroops;
			runs += 1;
			var mountAgents = 0;
			var mountNoRider = 0;
			numberOfTroopsCanBeSpawned = MissionAgentSpawnLogic.MaxNumberOfAgentsForMission;

			foreach (var agent in __instance.Mission.AllAgents)
			{
				if (agent.IsMount)
				{
					if (!AgentTrackerMount.Contains(agent))
					{
						AgentTrackerMount.Add(agent);
					}

					mountAgents += 1;
					if (agent.RiderAgent == null || agent.RiderAgent.State != AgentState.Active)
					{
						mountNoRider += 1;
						if (mountNoRider > Statics.GetSettingsOrThrow().RetreatHorses)
						{
							agent.Retreat(__instance.Mission.GetClosestFleePositionForAgent(agent)); //Mounts retreat more, freeing up agents
						}
					}
				}
				else if (agent.IsHuman)
				{
					if (!AgentTrackerTroop.Contains(agent))
					{
						AgentTrackerTroop.Add(agent);
					}
				}
			}
			BattleSizePatchExPartyGroupTroopSupplier.mountfootratio = (BattleSizePatchExPartyGroupTroopSupplier.troops == AgentTrackerTroop.Count) ? 1f : 1f + ((BattleSizePatchExPartyGroupTroopSupplier.mounts - AgentTrackerMount.Count) / (BattleSizePatchExPartyGroupTroopSupplier.troops - AgentTrackerTroop.Count) * Statics.GetSettingsOrThrow().BattleSizeExSafePuffer);
			if (BattleSizePatchExPartyGroupTroopSupplier.isSiege)
			{
				BattleSizePatchExPartyGroupTroopSupplier.mountfootratio = 1f;
			}

			if (runs > 200)
			{
				if (Statics.GetSettingsOrThrow().BattleSizeDebug)
				{
					MessageUtil.ColorGreenMessage("---------REPORT START------------");
					MessageUtil.ColorGreenMessage("Mounts: " + mountAgents + " | Troops: " + __instance.GetNumberOfPlayerControllableTroops() + " | Agents: " + __instance.Mission.AllAgents.Count);
					MessageUtil.ColorGreenMessage("To be spawned: " + __instance.NumberOfRemainingTroops + " | Slots available: " + MissionAgentSpawnLogic.MaxNumberOfAgentsForMission);
					MessageUtil.ColorGreenMessage("Reinforcements mounted agent ratio: " + Math.Round(BattleSizePatchExPartyGroupTroopSupplier.mountfootratio, 2));
				}
				runs = 0;
				return true;
			}
			return true;
		}


		private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
		{

			var list = new List<CodeInstruction>(instructions);

			if (list.Count == 250)
			{
				list.Insert(72, new CodeInstruction(OpCodes.Conv_R4, null)); //  (MissionAgentSpawnLogic.MaxNumberOfAgentsForMission - base.Mission.AllAgents.Count) --> float
				list.Insert(74, new CodeInstruction(OpCodes.Conv_R4, null)); // >= numberOfTroopsCanBeSpawned --> float
				list[75] = new CodeInstruction(OpCodes.Call, SymbolExtensions.GetMethodInfo(() => GetMountRatio())); // >= numberOfTroopsCanBeSpawned * mountfootratio
				list[83].operand = Statics.GetSettingsOrThrow().ReinforcementQuota * 0.01f; //>= (float)this._battleSize * _ReinforcementQuota * 0.01f_ --> Percentage of Battlesize each reinforcement
				list.RemoveRange(85, 6); // remove  _ || num4 >= 0.5f || num5 >= 0.5f_
			}

			return list.AsEnumerable();
		}

		/*
		int numberOfTroopsCanBeSpawned = this.NumberOfTroopsCanBeSpawned;
		if (this.NumberOfRemainingTroops > 0 && numberOfTroopsCanBeSpawned > 0)
		{
		AllSpawned					int num = this.DefenderActivePhase.TotalSpawnNumber + this.AttackerActivePhase.TotalSpawnNumber;
		DefenderCanSpawnAmount		int num2 = MBMath.Round((float)numberOfTroopsCanBeSpawned * (float)this.DefenderActivePhase.TotalSpawnNumber / (float)num);
		AttackerCanSpawnAmount		int num3 = numberOfTroopsCanBeSpawned - num2;
		Def % lost					float num4 = (float)(this.DefenderActivePhase.InitialSpawnedNumber - this._missionSides[0].NumberOfActiveTroops) / (float)this.DefenderActivePhase.InitialSpawnedNumber;
		Att % lost					float num5 = (float)(this.AttackerActivePhase.InitialSpawnedNumber - this._missionSides[1].NumberOfActiveTroops) / (float)this.AttackerActivePhase.InitialSpawnedNumber;
									if (MissionAgentSpawnLogic.MaxNumberOfAgentsForMission - base.Mission.AllAgents.Count >= numberOfTroopsCanBeSpawned * 2 && ((float)numberOfTroopsCanBeSpawned >= (float)this._battleSize * 0.1f || num4 >= 0.5f || num5 >= 0.5f))
									{
		Def % lost x 2					float num6 = num4 / 0.5f;
		Att % lost x 2					float num7 = num5 / 0.5f;
		Who is doing better				float num8 = MBMath.ClampFloat(num6 - num7, -1f, 1f);
		Attacker is doing better		if (num8 > 0f)
										{
											int num9 = MBMath.Round((float)num3 * num8);
											num3 -= num9;
											num2 += num9;
										}
		Defender is doing better		else if (num8 < 0f)
										{
											num8 = MBMath.Absf(num8);
											int num10 = MBMath.Round((float)num2 * num8);
											num2 -= num10;
											num3 += num10;
										}
										int num11 = Math.Max(num2 - this.DefenderActivePhase.RemainingSpawnNumber, 0);
										int num12 = Math.Max(num3 - this.AttackerActivePhase.RemainingSpawnNumber, 0);
										if (num11 > 0 && num12 > 0)
										{
											num2 = this.DefenderActivePhase.RemainingSpawnNumber;
											num3 = this.AttackerActivePhase.RemainingSpawnNumber;
										}
										else if (num11 > 0)
										{
											num2 = this.DefenderActivePhase.RemainingSpawnNumber;
											num3 = Math.Min(num3 + num11, this.AttackerActivePhase.RemainingSpawnNumber);
										}
										else if (num12 > 0)
										{
											num3 = this.AttackerActivePhase.RemainingSpawnNumber;
											num2 = Math.Min(num2 + num12, this.DefenderActivePhase.RemainingSpawnNumber);
										}
										if (this._missionSides[0].TroopSpawningActive && num2 > 0)
										{
											this.DefenderActivePhase.RemainingSpawnNumber -= this._missionSides[0].SpawnTroops(num2, true, true);
										}
										if (this._missionSides[1].TroopSpawningActive && num3 > 0)
										{
											this.AttackerActivePhase.RemainingSpawnNumber -= this._missionSides[1].SpawnTroops(num3, true, true);
										}
									}
								}*/
	}
}
