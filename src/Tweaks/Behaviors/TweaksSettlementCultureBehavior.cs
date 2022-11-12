namespace Tweaks.Behaviors
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using TaleWorlds.CampaignSystem;
	using TaleWorlds.CampaignSystem.Actions;
	using TaleWorlds.CampaignSystem.CampaignBehaviors;
	using TaleWorlds.CampaignSystem.GameMenus;
	using TaleWorlds.CampaignSystem.Party;
	using TaleWorlds.CampaignSystem.Settlements;
	using TaleWorlds.Library;
	using Tweaks.Settings;
	using Tweaks.Utils;

	internal class TweaksSettlementCultureBehavior : CampaignBehaviorBase
	{
		private void OnSessionLaunched(CampaignGameStarter campaignGameStarter) => this.AddGameMenus(campaignGameStarter);

		public override void RegisterEvents()
		{
			CampaignEvents.ClanChangedKingdom.AddNonSerializedListener(this, new Action<Clan, Kingdom, Kingdom, ChangeKingdomAction.ChangeKingdomActionDetail, bool>(this.OnClanChangedKingdom));
			//CampaignEvents.ClanChangedKingdom.AddNonSerializedListener(this, new Action<Clan, Kingdom, Kingdom, bool, bool>(this.OnClanChangedKingdom));
			CampaignEvents.OnGameLoadedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnGameLoaded));
			//CampaignEvents.WeeklyTickSettlementEvent.AddNonSerializedListener(this, new Action<Settlement>(this.OnWeeklyTickSettlement));
			CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnSessionLaunched));
			CampaignEvents.DailyTickSettlementEvent.AddNonSerializedListener(this, new Action<Settlement>(this.OnDailyTickSettlement));
			CampaignEvents.OnNewGameCreatedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnGameLoaded));
			CampaignEvents.OnSiegeAftermathAppliedEvent.AddNonSerializedListener(this, new Action<MobileParty, Settlement, SiegeAftermathCampaignBehavior.SiegeAftermath, Clan, Dictionary<MobileParty, float>>(this.OnSiegeAftermathApplied));
			CampaignEvents.OnSettlementOwnerChangedEvent.AddNonSerializedListener(this, new Action<Settlement, bool, Hero, Hero, Hero, ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail>(this.OnSettlementOwnerChanged));
		}

		private void OnGameLoaded(CampaignGameStarter obj)
		{
			Dictionary<Settlement, CultureObject> startingCultures = new();

			if (TweaksMCMSettings.Instance is { } settings)
			{
				this.UpdatePlayerOverride();
			}

			foreach (var settlement in from settlement in Campaign.Current.Settlements where settlement.IsTown || settlement.IsCastle || settlement.IsVillage select settlement)
			{
				startingCultures.Add(settlement, settlement.Culture);
				if (TweaksMCMSettings.Instance is { } settings2)
				{
					var PlayerOverride = settlement.OwnerClan == Clan.PlayerClan && OverrideCulture != settlement.Culture;
					var KingdomOverride = settlement.OwnerClan != Clan.PlayerClan && settings2.ChangeToKingdomCulture && settlement.OwnerClan.Kingdom != null && settlement.OwnerClan.Kingdom.Culture != settlement.Culture;
					var ClanCulture = settlement.OwnerClan != Clan.PlayerClan && (!settings2.ChangeToKingdomCulture || settlement.OwnerClan.Kingdom == null) && settlement.OwnerClan.Culture != settlement.Culture;

					if ((PlayerOverride || KingdomOverride || ClanCulture) && !WeekCounter.ContainsKey(settlement))
					{
						this.AddCounter(settlement);
					}
					else if ((PlayerOverride || KingdomOverride || ClanCulture) && this.IsSettlementDue(settlement))
					{
						this.Transform(settlement, false);
					}
				}
			}
			initialCultureDictionary = startingCultures;
		}

		private void AddGameMenus(CampaignGameStarter campaignGameStarter)
		{
			campaignGameStarter.AddGameMenuOption("village", "village_culture_changer", "Culture Transformation", new GameMenuOption.OnConditionDelegate(Game_menu_village_change_culture_on_condition), new GameMenuOption.OnConsequenceDelegate(Game_menu_change_culture_on_consequence), false, 5, false);
			campaignGameStarter.AddGameMenuOption("town", "town_culture_changer", "Culture Transformation", new GameMenuOption.OnConditionDelegate(Game_menu_town_change_culture_on_condition), new GameMenuOption.OnConsequenceDelegate(Game_menu_change_culture_on_consequence), false, 5, false);
			campaignGameStarter.AddGameMenuOption("castle", "castle_culture_changer", "Culture Transformation", new GameMenuOption.OnConditionDelegate(Game_menu_castle_change_culture_on_condition), new GameMenuOption.OnConsequenceDelegate(Game_menu_change_culture_on_consequence), false, 5, false);
		}

		private void OnSiegeAftermathApplied(MobileParty arg1, Settlement settlement, SiegeAftermathCampaignBehavior.SiegeAftermath arg3, Clan arg4, Dictionary<MobileParty, float> arg5) => this.AddCounter(settlement);

		private void OnSettlementOwnerChanged(Settlement settlement, bool arg2, Hero arg3, Hero arg4, Hero arg5, ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail detail)
		{

			if (detail != ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail.BySiege)
			{
				if (settlement.OwnerClan == Clan.PlayerClan)
				{
					this.UpdatePlayerOverride();
				}
				this.AddCounter(settlement);
			}
			else
			{
				settlement.Culture = initialCultureDictionary[settlement];
			}
		}


		// Token: 0x06000E45 RID: 3653 RVA: 0x000630F6 File Offset: 0x000612F6
		private void OnClanChangedKingdom(Clan clan, Kingdom oldKingdom, Kingdom newKingdom, ChangeKingdomAction.ChangeKingdomActionDetail detail, bool showNotification = true)
		{
			if (TweaksMCMSettings.Instance is { } settings && settings.ChangeToKingdomCulture)
			{
				if (clan == Clan.PlayerClan)
				{
					this.UpdatePlayerOverride();
				}
				else if (clan.Kingdom == null || clan.Kingdom.Culture != clan.Culture)
				{
					foreach (var settlement in from settlement in clan.Settlements where settlement.IsTown || settlement.IsCastle || settlement.IsVillage select settlement)
					{
						this.AddCounter(settlement);
					}
				}
			}
		}

		public void Transform(Settlement settlement, bool removeTroops)
		{


			if (TweaksMCMSettings.Instance is { } settings && settlement.OwnerClan == Clan.PlayerClan)
			{
				this.UpdatePlayerOverride();
			}
			if (settlement.IsVillage || settlement.IsCastle || settlement.IsTown)
			{
				if (TweaksMCMSettings.Instance is { } settings2)
				{
					var PlayerOverride = settlement.OwnerClan == Clan.PlayerClan && OverrideCulture != settlement.Culture;
					var KingdomOverride = settlement.OwnerClan != Clan.PlayerClan && settings2.ChangeToKingdomCulture && settlement.OwnerClan.Kingdom != null && settlement.OwnerClan.Kingdom.Culture != settlement.Culture;
					var ClanCulture = settlement.OwnerClan != Clan.PlayerClan && (!settings2.ChangeToKingdomCulture || settlement.OwnerClan.Kingdom == null) && settlement.OwnerClan.Culture != settlement.Culture;

					if (PlayerOverride || KingdomOverride || ClanCulture)
					{
						var newculture = (settlement.OwnerClan == Clan.PlayerClan) ? OverrideCulture : (settings2.ChangeToKingdomCulture && settlement.OwnerClan.Kingdom != null) ? settlement.OwnerClan.Kingdom.Culture : settlement.OwnerClan.Culture;
						if (newculture != null)
						{
							//dont switch last town of a culture to prevent bugs in vanilla
							var count = 0;
							if (settlement.IsTown)
							{
								foreach (var Town in Campaign.Current.Settlements)
								{
									if (Town.IsTown && Town.Culture == settlement.Culture)
									{
										count++;
									}
								}
							}
							if (count != 1)
							{
								settlement.Culture = newculture;
								if (removeTroops)
								{
									RemoveTroopsfromNotable(settlement);
								}
								foreach (var boundVillage in settlement.BoundVillages)
								{
									if (removeTroops)
									{
										this.Transform(boundVillage.Settlement, true);
									}
									else
									{
										this.Transform(boundVillage.Settlement, false);
									}
								}
							}
						}
					}
				}
			}
		}

		public void UpdatePlayerOverride()
		{
			if (TweaksMCMSettings.Instance is { } settings)
			{
				OverrideCulture = null;
				foreach (var Culture in from kingdom in Campaign.Current.Kingdoms where settings.PlayerCultureOverride.SelectedValue == kingdom.Culture.StringId || (settings.PlayerCultureOverride.SelectedValue == "khergit" && kingdom.Culture.StringId == "rebkhu") select kingdom.Culture)
				{
					OverrideCulture = Culture;
					break;
				}
				if (OverrideCulture == null && settings.ChangeToKingdomCulture && Clan.PlayerClan.Kingdom != null)
				{
					OverrideCulture = Clan.PlayerClan.Kingdom.Culture;
				}
				else
				{
					OverrideCulture ??= Clan.PlayerClan.Culture;
				}
			}
		}

		public static void RemoveTroopsfromNotable(Settlement settlement)
		{
			if ((settlement.IsTown || settlement.IsVillage) && settlement.Notables != null)
			{
				foreach (var notable in settlement.Notables)
				{
					if (notable.CanHaveRecruits)
					{
						for (var index = 0; index < 6; index++)
						{
							notable.VolunteerTypes[index] = null;
						}
					}
				}
			}
		}

		public void OnDailyTickSettlement(Settlement settlement)
		{
			if (WeekCounter.ContainsKey(settlement))
			{
				var dictionary = WeekCounter;
				if (dictionary[settlement] / 7 <= Statics._settings.TimeToChanceCulture)
				{
					dictionary[settlement]++;
					if (Statics._settings.CultureChangeDebug)
					{
						IM.MessageDebug($"OnDailyTickSettlement : {settlement.Name} counter: {dictionary[settlement]}");
						IM.MessageDebug($"OnDailyTickSettlement condition: {dictionary[settlement] / 7 <= Statics._settings.TimeToChanceCulture} ");
						IM.MessageDebug($"OnDailyTickSettlement (dictionary[settlement] / 7) : {dictionary[settlement] / 7} ");
						IM.MessageDebug($"OnDailyTickSettlement TimeToChanceCulture: {Statics._settings.TimeToChanceCulture} ");
					}

					if (this.IsSettlementDue(settlement))
					{
						this.Transform(settlement, true);
					}
				}

			}
		}

		public void OnWeeklyTickSettlement(Settlement settlement)
		{
			if (WeekCounter.ContainsKey(settlement))
			{
				var dictionary = WeekCounter;
				dictionary[settlement]++;
				if (Statics._settings.CultureChangeDebug)
				{
					IM.MessageDebug($"OnWeeklyTickSettlement : {settlement.Name} Added 1 week : {dictionary[settlement]} ");
				}

				if (this.IsSettlementDue(settlement))
				{
					this.Transform(settlement, true);
				}
			}
		}

		public bool IsSettlementDue(Settlement settlement)
		{
			if (TweaksMCMSettings.Instance is { } settings && settings.TimeToChanceCulture > 0)
			{
				return WeekCounter[settlement] / 7 >= settings.TimeToChanceCulture;
			}
			else
			{
				return false;
			}
		}

		public void AddCounter(Settlement settlement)
		{
			if (settlement.IsVillage || settlement.IsCastle || settlement.IsTown)
			{
				if (WeekCounter.ContainsKey(settlement))
				{
					if (Statics._settings.CultureChangeDebug)
					{
						IM.MessageDebug($"AddCounter : {settlement.Name} set exisiting");
					}
					WeekCounter[settlement] = 0;
				}
				else
				{
					if (Statics._settings.CultureChangeDebug)
					{
						IM.MessageDebug($"AddCounter : {settlement.Name} add new");
					}
					WeekCounter.Add(settlement, 0);
				}
			}
		}

		public override void SyncData(IDataStore dataStore) => dataStore.SyncData("SettlementCultureTransformation", ref WeekCounter);

		public static bool Game_menu_castle_change_culture_on_condition(MenuCallbackArgs args)
		{
			args.optionLeaveType = GameMenuOption.LeaveType.Manage;
			return Settlement.CurrentSettlement.IsCastle;
		}
		public static bool Game_menu_town_change_culture_on_condition(MenuCallbackArgs args)
		{
			args.optionLeaveType = GameMenuOption.LeaveType.Manage;
			return Settlement.CurrentSettlement.IsTown;
		}

		public static bool Game_menu_village_change_culture_on_condition(MenuCallbackArgs args)
		{
			args.optionLeaveType = GameMenuOption.LeaveType.Manage;
			return Settlement.CurrentSettlement.IsVillage;
		}

		public static void Game_menu_change_culture_on_consequence(MenuCallbackArgs args)
		{
			if (TweaksMCMSettings.Instance is { } settings)
			{
				var PlayerOverride = Settlement.CurrentSettlement.OwnerClan == Clan.PlayerClan && OverrideCulture != Settlement.CurrentSettlement.Culture;
				var KingdomOverride = Settlement.CurrentSettlement.OwnerClan != Clan.PlayerClan && settings.ChangeToKingdomCulture && Settlement.CurrentSettlement.OwnerClan.Kingdom != null && Settlement.CurrentSettlement.OwnerClan.Kingdom.Culture != Settlement.CurrentSettlement.Culture;
				var ClanCulture = Settlement.CurrentSettlement.OwnerClan != Clan.PlayerClan && (!settings.ChangeToKingdomCulture || Settlement.CurrentSettlement.OwnerClan.Kingdom == null) && Settlement.CurrentSettlement.OwnerClan.Culture != Settlement.CurrentSettlement.Culture;

				if (!WeekCounter.ContainsKey(Settlement.CurrentSettlement))
				{
					InformationManager.DisplayMessage(new InformationMessage("The people in " + Settlement.CurrentSettlement.Name + " already appraise their owners culture."));
				}
				else if (PlayerOverride || KingdomOverride || ClanCulture)
				{
					InformationManager.DisplayMessage(new InformationMessage("The people in " + Settlement.CurrentSettlement.Name + " seem to adopt their owners culture in " + (settings.TimeToChanceCulture - (WeekCounter.ContainsKey(Settlement.CurrentSettlement) ? (WeekCounter[Settlement.CurrentSettlement] / 7) : 00)) + " weeks."));
				}
				else
				{
					InformationManager.DisplayMessage(new InformationMessage("The people in " + Settlement.CurrentSettlement.Name + " already appraise their owners culture."));
				}
			}
		}

		private static Dictionary<Settlement, CultureObject> initialCultureDictionary = new();
		public static Dictionary<Settlement, int> WeekCounter = new();
		private static CultureObject? OverrideCulture = new();
	}
}
