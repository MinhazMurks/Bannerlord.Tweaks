namespace Tweaks.Event
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using TaleWorlds.CampaignSystem;
	using TaleWorlds.CampaignSystem.Actions;
	using TaleWorlds.CampaignSystem.Encounters;
	using TaleWorlds.CampaignSystem.MapEvents;
	using TaleWorlds.CampaignSystem.Party;
	using TaleWorlds.CampaignSystem.Roster;
	using TaleWorlds.CampaignSystem.Settlements;
	using TaleWorlds.Core;
	using Utils;

	internal class PlayerBattleEndEventListener
	{
		private int BanditGroupCounter { get; set; }
		private int BanditDeathCounter { get; set; }

		public PlayerBattleEndEventListener()
		{
			this.BanditGroupCounter = Statics._settings.GroupsOfBandits;
			this.BanditDeathCounter = 0;
			MessageUtil.logMessage("Killing Bandits : PlayerBattleEndEventListener Called" + "");
		}

		public void IncreaseLocalRelationsAfterBanditFight(MapEvent m)
		{
			MessageUtil.logMessage("Killing Bandits : IncreaseLocalRelationsAfterBanditFight Called" + "");
			TroopRoster rosterReceivingLootShare;
			var mainPartSideInt = (int)PartyBase.MainParty.Side;
			rosterReceivingLootShare = PlayerEncounter.Current.RosterToReceiveLootMembers;
			//PartyBase partyReceivingLootShare = m.GetPartyReceivingLootShare(PartyBase.MainParty);

			MapEventSide banditSide;

			if (m.DefeatedSide == BattleSideEnum.Attacker)
			{
				banditSide = m.AttackerSide;
			}
			else
			{
				banditSide = m.DefenderSide;
			}
			if ((int)m.DefeatedSide is not ((-1) or 2))
			{
				if (this.IsDefeatedBanditLike(m) && (rosterReceivingLootShare.TotalHealthyCount > 0 || !Statics._settings.PrisonersOnly))
				{
					this.BanditDeathCounter += banditSide.Casualties;
					//IM.ColorGreenMessage("BanditDeathCounter: " + BanditDeathCounter.ToString());
					if (this.BanditGroupCounter == 1)
					{
						this.IncreaseLocalRelations(m);
						this.ResetBanditDeathCounter();
					}
					this.BanditGroupCounterUpdate();
				}
			}
		}

		private void IncreaseLocalRelations(MapEvent m)
		{
			float FinalRelationshipIncrease = Statics._settings.RelationshipIncrease;
			if (Statics._settings.SizeBonusEnabled)
			{
				FinalRelationshipIncrease = Statics._settings.RelationshipIncrease * this.BanditDeathCounter * Statics._settings.SizeBonus;
				if (Statics._settings.KillingBanditsDebug)
				{
					MessageUtil.MessageDebug("Killing Bandits: SizeBonusEnabled: " + FinalRelationshipIncrease.ToString());
				}
			}
			var FinalRelationshipIncreaseInt = (int)Math.Floor(FinalRelationshipIncrease);
			if (Statics._settings.KillingBanditsDebug)
			{
				MessageUtil.MessageDebug("Killing Bandits: IncreaseLocalRelations: " + "Base Change: " + Statics._settings.RelationshipIncrease.ToString() + "Final Change: " + FinalRelationshipIncreaseInt.ToString());
			}
			FinalRelationshipIncreaseInt = FinalRelationshipIncreaseInt < 1 ? 1 : FinalRelationshipIncreaseInt;
			if (Statics._settings.KillingBanditsRelationReportEnabled)
			{
				MessageUtil.ColorGreenMessage("Final Relationship Increase: " + FinalRelationshipIncreaseInt.ToString());
			}

			var list = new List<Settlement>();
			foreach (var settlement in Settlement.All)
			{
				if ((settlement.IsVillage || settlement.IsTown) && settlement.Position2D.DistanceSquared(m.Position) <= Statics._settings.Radius)
				{
					list.Add(settlement);
				}
			}
			foreach (var settlement2 in list)
			{
				if (settlement2.Notables.Any<Hero>())
				{
					var h = settlement2.Notables.GetRandomElement<Hero>();
					ChangeRelationAction.ApplyPlayerRelation(h, relation: FinalRelationshipIncreaseInt, affectRelatives: true, showQuickNotification: false);
				}
			}
			if (Statics._settings.KillingBanditsRelationReportEnabled)
			{
				MessageUtil.ColorGreenMessage("Your relationship increased with nearby notables. " + FinalRelationshipIncreaseInt.ToString());
			}
		}

		private void BanditGroupCounterUpdate()
		{
			this.BanditGroupCounter--;
			if (this.BanditGroupCounter == 0)
			{
				this.BanditGroupCounter = Statics._settings.GroupsOfBandits;
			}
			if (Statics._settings.KillingBanditsDebug)
			{
				MessageUtil.MessageDebug("Killing Bandits : BanditGroupCounterUpdate: " + this.BanditGroupCounter.ToString());
			}
		}

		private void ResetBanditDeathCounter() => this.BanditDeathCounter = 0;

		private bool IsDefeatedBanditLike(MapEvent m)
		{
			try
			{
				if (m.GetLeaderParty(m.DefeatedSide).MapFaction.IsBanditFaction && Statics._settings.IncludeBandits)
				{
					return true;
				}

				if (m.GetLeaderParty(m.DefeatedSide).MapFaction.IsOutlaw && Statics._settings.IncludeOutlaws)
				{
					return true;
				}

				if (m.GetLeaderParty(m.DefeatedSide).Owner.Clan.IsMafia && Statics._settings.IncludeMafia)
				{
					return true;
				}
			}

			catch (Exception ex)
			{
				//Avoids crash for parties without an owner set
			}
			return false;
		}
	}
}
