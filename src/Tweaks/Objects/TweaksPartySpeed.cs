namespace KaosesPartySpeeds.Objects
{
	using TaleWorlds.CampaignSystem;
	using TaleWorlds.CampaignSystem.Party;
	using TaleWorlds.Localization;
	using Tweaks;
	using Tweaks.Common;

	public class TweaksPartySpeed
	{
		protected MobileParty _mobileParty;
		protected bool HasModifiedSpeed = false;
		protected float ModifiedSpeed = 0.0f;
		protected TextObject Message = new TextObject(null, null);

		/* Kaoses Custom Text Explainers*/
		protected readonly TextObject _slowMessage = new TextObject("{=Kaoses1ZiDIanZ}Kaoses Bandits", null);
		protected readonly TextObject _slowPlayerMessage = new TextObject("{=Kaoses1ZiDIa2Z}Kaoses Player", null);
		protected readonly TextObject _slowPlayerClanMessage = new TextObject("{=Kaoses1ZiDIa6Z}Player Clan", null);
		protected readonly TextObject _slowMinorMessage = new TextObject("{=Kaoses1ZiDIa4Z}Kaoses Minor", null);
		protected readonly TextObject _slowKingdomMessage = new TextObject("{=Kaoses1ZiDIa3Z}Kaoses Kingdom", null);
		protected readonly TextObject _slowVillagerMessage = new TextObject("{=Kaoses1ZiDIa4Z}Kaoses Villagers", null);
		protected readonly TextObject _slowCaravansMessage = new TextObject("{=Kaoses1ZiDIa5Z}Kaoses Caravans", null);
		/* Kaoses Custom Text Explainers*/

		public TweaksPartySpeed(MobileParty mobileParty)
		{
			this._mobileParty = mobileParty;
			this.calculatePartySpeed();
		}

		private void calculatePartySpeed()
		{
			if (Statics._settings.KaosesStaticSpeedModifiersEnabled)
			{
				if (this._mobileParty.StringId.Contains("looter") && Statics._settings.LooterSpeedReductionAmount != 0.0f)
				{
					this.ModifiedSpeed = Statics._settings.LooterSpeedReductionAmount;
					this.Message = this._slowMessage;
					this.HasModifiedSpeed = true;
				}
				if (this._mobileParty.StringId.Contains("caravan") && Statics._settings.CaravanSpeedReductiontEnabled)
				{
					if (this._mobileParty.StringId.Contains("elite") && Statics._settings.EliteCaravanSpeedReductionAmount != 0.0f)
					{
						this.ModifiedSpeed = Statics._settings.EliteCaravanSpeedReductionAmount;
						this.Message = this._slowCaravansMessage;
						this.HasModifiedSpeed = true;
					}
					else if (Statics._settings.CaravanSpeedReductionAmount != 0.0f)
					{
						this.ModifiedSpeed = Statics._settings.CaravanSpeedReductionAmount;
						this.Message = this._slowCaravansMessage;
						this.HasModifiedSpeed = true;
					}
				}
				if (this._mobileParty.StringId.Contains("desert") && Statics._settings.DesertSpeedReductionAmount != 0.0f)
				{
					this.ModifiedSpeed = Statics._settings.DesertSpeedReductionAmount;
					this.Message = this._slowMessage;
					this.HasModifiedSpeed = true;
				}
				if (this._mobileParty.StringId.Contains("forest") && Statics._settings.ForestSpeedReductionAmount != 0.0f)
				{
					this.ModifiedSpeed = Statics._settings.ForestSpeedReductionAmount;
					this.Message = this._slowMessage;
					this.HasModifiedSpeed = true;
				}
				if (this._mobileParty.StringId.Contains("mountain") && Statics._settings.MountainSpeedReductionAmount != 0.0f)
				{
					this.ModifiedSpeed = Statics._settings.MountainSpeedReductionAmount;
					this.Message = this._slowMessage;
					this.HasModifiedSpeed = true;
				}
				if (this._mobileParty.StringId.Contains("raider") && Statics._settings.SeaRaiderSpeedReductionAmount != 0.0f)
				{
					this.ModifiedSpeed = Statics._settings.SeaRaiderSpeedReductionAmount;
					this.Message = this._slowMessage;
					this.HasModifiedSpeed = true;
				}
				if (this._mobileParty.StringId.Contains("steppe") && Statics._settings.SteppeSpeedReductionAmount != 0.0f)
				{
					this.ModifiedSpeed = Statics._settings.SteppeSpeedReductionAmount;
					this.Message = this._slowMessage;
					this.HasModifiedSpeed = true;
				}
				if (this._mobileParty.StringId.Contains("villager") && Statics._settings.VillagerSpeedReductiontEnabled
					&& Statics._settings.VillagerSpeedReductionAmount != 0.0f)
				{
					this.ModifiedSpeed = Statics._settings.VillagerSpeedReductionAmount;
					this.Message = this._slowVillagerMessage;
					this.HasModifiedSpeed = true;
				}
				if (this._mobileParty.StringId.Contains("lord_") && Statics._settings.KingdomSpeedReductiontEnabled
					&& Statics._settings.KingdomSpeedReductionAmount != 0.0f)
				{
					this.ModifiedSpeed = Statics._settings.KingdomSpeedReductionAmount;
					this.Message = this._slowKingdomMessage;
					this.HasModifiedSpeed = true;
				}
				if (this._mobileParty.StringId.Contains("troops_of") && Statics._settings.OtherKingdomSpeedReductionAmount != 0.0f)
				{
					this.ModifiedSpeed = Statics._settings.OtherKingdomSpeedReductionAmount;
					this.Message = this._slowMinorMessage;
					this.HasModifiedSpeed = true;
				}

				if (this._mobileParty.IsMainParty && Statics._settings.PlayerSpeedReductiontEnabled && Statics._settings.PlayerSpeedReductionAmount != 0.0f)
				{
					this.ModifiedSpeed = Statics._settings.PlayerSpeedReductionAmount;
					this.Message = this._slowPlayerMessage;
					this.HasModifiedSpeed = true;

				}

				if (!this._mobileParty.IsMainParty && !this._mobileParty.StringId.Contains("player_")
					&& !this._mobileParty.StringId.Contains("militias_") && !this._mobileParty.StringId.Contains("garrison_"))
				{
					if (!this.HasModifiedSpeed && this._mobileParty.LeaderHero != null)
					{
						if (Kaoses.IsPlayerClan(this._mobileParty) && Statics._settings.PlayerSpeedReductiontEnabled && Statics._settings.PlayerClanSpeedReductionAmount != 0.0f)
						{
							this.ModifiedSpeed = Statics._settings.PlayerClanSpeedReductionAmount;
							this.Message = this._slowPlayerClanMessage;
							this.HasModifiedSpeed = true;
							//Logger.Lm("IsPlayerClan new speed:" + finalSpeed.ResultNumber.ToString());
						}
						else if (Statics._settings.OtherKingdomSpeedReductionAmount != 0.0f)
						{
							this.ModifiedSpeed = Statics._settings.OtherKingdomSpeedReductionAmount;
							this.Message = this._slowMinorMessage;
							this.HasModifiedSpeed = true;
						}
					}
				}

			}


		}

		public bool HasPartyModifiedSpeed() => this.HasModifiedSpeed;

		public float ModifiedPartySpeed() => this.ModifiedSpeed;

		public TextObject ExplainationMessage() => this.Message;


		public static void GetDynamicSpeedChange(MobileParty mobileParty, ref ExplainedNumber finalSpeed)
		{

			if (Statics._settings.KaosesDynamicSpeedModifiersEnabled)
			{
				var reduction = 0.0f;
				if (mobileParty.ShortTermBehavior is AiBehavior.FleeToPoint or AiBehavior.FleeToParty or AiBehavior.FleeToGate)
				{
					var oldTime = SubModule.FleeingParties.GetOrAdd(mobileParty, CampaignTime.Now);
					if (CampaignTime.Now.ToHours > oldTime.ToHours)
					{
						var fleeingHours = SubModule.FleeingHours.GetOrAdd(mobileParty, 1);
						var newReduction = Statics._settings.DynamicFleeingSpeedReductionAmount * fleeingHours;
						SubModule.FleeingHours.AddOrUpdate(mobileParty, fleeingHours + 1, (k, v) => v = fleeingHours + 1);
						SubModule.FleeingParties.AddOrUpdate(mobileParty,
							CampaignTime.HoursFromNow(Statics._settings.DynamicFleeingSpeedReductionHours),
							(k, v) => oldTime);
						SubModule.FleeingSpeedReduction.AddOrUpdate(mobileParty, newReduction, (k, v) => v = newReduction);
						reduction = newReduction;
					}
					else
					{
						if (SubModule.FleeingSpeedReduction.ContainsKey(mobileParty))
						{
							var fleeingHours = SubModule.FleeingHours.GetOrAdd(mobileParty, 1);
							reduction = SubModule.FleeingSpeedReduction.GetOrAdd(mobileParty, Statics._settings.DynamicFleeingSpeedReductionAmount);
						}
					}

					if (reduction != 0)
					{
						finalSpeed.Add(reduction, null);
					}
				}
				else
				{
					if (SubModule.FleeingParties.ContainsKey(mobileParty))
					{
						var oldTime = SubModule.FleeingParties.TryGetValue(mobileParty, out var oT) ? oT : CampaignTime.Never;
						var fleeingHours = SubModule.FleeingHours.TryGetValue(mobileParty, out var fH) ? fH : 0;
						var oldReduction = SubModule.FleeingSpeedReduction.TryGetValue(mobileParty, out var oR) ? oR : 0f;
						if (CampaignTime.Now.ToHours > oldTime.ToHours)
						{
							SubModule.FleeingParties.TryRemove(mobileParty, out oldTime);
							if (SubModule.FleeingHours.ContainsKey(mobileParty))
							{
								SubModule.FleeingHours.TryRemove(mobileParty, out fleeingHours);
							}
							if (SubModule.FleeingSpeedReduction.ContainsKey(mobileParty))
							{
								SubModule.FleeingSpeedReduction.TryRemove(mobileParty, out oldReduction);
							}
						}
					}
				}
			}
		}



	}
}
