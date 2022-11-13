namespace Tweaks.Objects.Experience
{
	using TaleWorlds.CampaignSystem;
	using TaleWorlds.Core;
	using Utils;

	public class TweaksAddSkillXp
	{
		protected Hero Hero { get; set; }
		protected bool IsPlayerClan { get; set; }
		protected bool IsPlayer { get; set; }
		protected bool IsAiLord { get; set; }
		protected bool ValidHeroForUse { get; set; }
		protected bool ValidSkillForUse { get; set; }
		protected SkillObject Skill { get; set; }
		protected float XpAmount { get; set; }
		protected float SkillMultiplier { get; set; } = 1.0f;
		protected int BoundAttributeLevel { get; set; } = 0;

		public TweaksAddSkillXp(Hero hero, SkillObject skill, float xpAmount)
		{
			this.Hero = hero;
			this.Skill = skill;
			this.XpAmount = xpAmount;
			this.BuildHeroVariables();
			this.CheckHeroUseModifier();
			this.CheckSkill();
		}

		protected void BuildHeroVariables()
		{
			if (this.Hero.IsActive && this.Hero.IsAlive && !this.Hero.IsDead)
			{
				if (this.Hero.IsHumanPlayerCharacter)
				{
					this.IsPlayer = this.Hero.IsHumanPlayerCharacter;
				}
				else if (PlayerUtil.IsLord(this.Hero) && PlayerUtil.IsPlayerClan(this.Hero))
				{
					this.IsPlayerClan = PlayerUtil.IsPlayerClan(this.Hero);
				}
				if (PlayerUtil.IsLord(this.Hero))
				{

					this.IsAiLord = this.Hero.CharacterObject.IsHero;
				}
			}
		}

		protected void CheckHeroUseModifier()
		{
			if (Statics.GetSettingsOrThrow().SkillXpEnabled)
			{
				if (Statics.GetSettingsOrThrow().SkillXpUseForPlayer && this.IsPlayer)
				{
					this.ValidHeroForUse = true;
				}
				else if (Statics.GetSettingsOrThrow().SkillXpUseForPlayerClan && this.IsPlayerClan)
				{
					this.ValidHeroForUse = true;
				}
				else if (Statics.GetSettingsOrThrow().SkillXpUseForAI && this.IsAiLord)
				{
					this.ValidHeroForUse = true;
				}
			}
		}

		protected void CheckSkill()
		{
			if (Statics.GetSettingsOrThrow().SkillXpEnabled && this.ValidHeroForUse)
			{
				if (Statics.GetSettingsOrThrow().SkillXpUseGlobalMultipler)
				{
					this.ValidSkillForUse = true;
					this.SkillMultiplier = Statics.GetSettingsOrThrow().SkillsXpGlobalMultiplier;
				}
				else if (Statics.GetSettingsOrThrow().SkillXpUseIndividualMultiplers)
				{
					this.ValidSkillForUse = true;
					this.GetSkillModifier();
				}
			}
		}

		protected void GetSkillModifier()
		{
			if (this.Skill.GetName().Equals(DefaultSkills.Athletics.GetName()))
			{
				//endurance END
				this.SkillMultiplier = Statics.GetSettingsOrThrow().SkillsXPMultiplierAthletics;
			}
			else if (this.Skill.GetName().Equals(DefaultSkills.Bow.GetName()))
			{
				//control CTR
				this.SkillMultiplier = Statics.GetSettingsOrThrow().SkillsXPMultiplierBow;
			}
			else if (this.Skill.GetName().Equals(DefaultSkills.Charm.GetName()))
			{
				//social  SOC
				this.SkillMultiplier = Statics.GetSettingsOrThrow().SkillsXPMultiplierCharm;
			}
			else if (this.Skill.GetName().Equals(DefaultSkills.Crafting.GetName()))
			{
				//endurance END
				this.SkillMultiplier = Statics.GetSettingsOrThrow().SkillsXPMultiplierCrafting;
			}
			else if (this.Skill.GetName().Equals(DefaultSkills.Crossbow.GetName()))
			{
				//control CTR
				this.SkillMultiplier = Statics.GetSettingsOrThrow().SkillsXPMultiplierCrossbow;
			}
			else if (this.Skill.GetName().Equals(DefaultSkills.Engineering.GetName()))
			{
				//intelligence  INT
				this.SkillMultiplier = Statics.GetSettingsOrThrow().SkillsXPMultiplierEngineering;
			}
			else if (this.Skill.GetName().Equals(DefaultSkills.Leadership.GetName()))
			{
				//social  SOC
				this.SkillMultiplier = Statics.GetSettingsOrThrow().SkillsXPMultiplierLeadership;
			}
			else if (this.Skill.GetName().Equals(DefaultSkills.Medicine.GetName()))
			{
				//intelligence  INT
				this.SkillMultiplier = Statics.GetSettingsOrThrow().SkillsXPMultiplierMedicine;
			}
			else if (this.Skill.GetName().Equals(DefaultSkills.OneHanded.GetName()))
			{
				//vigor  VIG
				this.SkillMultiplier = Statics.GetSettingsOrThrow().SkillsXPMultiplierOneHanded;
			}
			else if (this.Skill.GetName().Equals(DefaultSkills.Polearm.GetName()))
			{
				//vigor  VIG
				this.SkillMultiplier = Statics.GetSettingsOrThrow().SkillsXPMultiplierPolearm;
			}
			else if (this.Skill.GetName().Equals(DefaultSkills.Riding.GetName()))
			{
				//endurance END
				this.SkillMultiplier = Statics.GetSettingsOrThrow().SkillsXPMultiplierRiding;
			}
			else if (this.Skill.GetName().Equals(DefaultSkills.Roguery.GetName()))
			{
				//cunning CNG
				this.SkillMultiplier = Statics.GetSettingsOrThrow().SkillsXPMultiplierRoguery;
			}
			else if (this.Skill.GetName().Equals(DefaultSkills.Scouting.GetName()))
			{
				//cunning CNG
				this.SkillMultiplier = Statics.GetSettingsOrThrow().SkillsXPMultiplierScouting;
			}
			else if (this.Skill.GetName().Equals(DefaultSkills.Steward.GetName()))
			{
				//intelligence  INT
				this.SkillMultiplier = Statics.GetSettingsOrThrow().SkillsXPMultiplierSteward;
			}
			else if (this.Skill.GetName().Equals(DefaultSkills.Tactics.GetName()))
			{
				//cunning CNG
				this.SkillMultiplier = Statics.GetSettingsOrThrow().SkillsXPMultiplierTactics;
			}
			else if (this.Skill.GetName().Equals(DefaultSkills.Throwing.GetName()))
			{
				//control CTR
				this.SkillMultiplier = Statics.GetSettingsOrThrow().SkillsXPMultiplierThrowing;
			}
			else if (this.Skill.GetName().Equals(DefaultSkills.Trade.GetName()))
			{
				//social  SOC
				this.SkillMultiplier = Statics.GetSettingsOrThrow().SkillsXPMultiplierTrade;
			}
			else if (this.Skill.GetName().Equals(DefaultSkills.TwoHanded.GetName()))
			{
				//vigor  VIG
				this.SkillMultiplier = Statics.GetSettingsOrThrow().SkillsXPMultiplierTwoHanded;
			}
		}

		public bool HasModifiedXp() => this.ValidHeroForUse && this.ValidSkillForUse && Statics.GetSettingsOrThrow().SkillXpEnabled;

		public float GetNewSkillXp()
		{
			var newXp = 0.0f;
			if (Statics.GetSettingsOrThrow().XpModifiersDebug && this.HasModifiedXp())
			{
				this.DebugDump();
			}
			if (this.HasModifiedXp())
			{
				newXp = this.XpAmount * this.SkillMultiplier;
			}
			return newXp;
		}

		public void DebugDump() => MessageUtil.MessageDebug("KaosesAddSkillXp: "
				+ " StringId: " + this.Hero.StringId + "\r\n"
				+ " Name: " + this.Hero.CharacterObject.Name + "\r\n"
				+ "  Skill Name: " + this.Skill.GetName() + "\r\n"
				+ "  _isPlayerClan: " + this.IsPlayerClan + "\r\n"
				+ "  SkillXpUseForPlayerClan: " + Statics.GetSettingsOrThrow().SkillXpUseForPlayerClan + "\r\n"
				+ "  _isPlayer: " + this.IsPlayer + "\r\n"
				+ "  SkillXpUseForPlayer: " + Statics.GetSettingsOrThrow().SkillXpUseForPlayer + "\r\n"
				+ "  _isAILord: " + this.IsAiLord + "\r\n"
				+ "  SkillXpUseForAI: " + Statics.GetSettingsOrThrow().SkillXpUseForAI + "\r\n"
				+ "  _ValidHeroForUse: " + this.ValidHeroForUse + "\r\n"
				+ "  _ValidSkillForUse: " + this.ValidSkillForUse + "\r\n"
				+ "  _skillMultiplier: " + this.SkillMultiplier + "\r\n"
				+ "  _xpAmount: " + this.XpAmount + "\r\n"
				+ "  new xpAmount: " + (this.XpAmount * this.SkillMultiplier) + "\r\n"
				);


		#region source code
		/*

		// Token: 0x060008BC RID: 2236 RVA: 0x000322EC File Offset: 0x000304EC
		public void AddSkillXp(SkillObject skill, float rawXp, bool isAffectedByFocusFactor = true, bool shouldNotify = true)
		{
			if (rawXp <= 0f)
			{
				return;
			}
			if (isAffectedByFocusFactor)
			{
				this.GainRawXp(rawXp, shouldNotify);
			}
			float num = rawXp * Campaign.Current.Models.GenericXpModel.GetXpMultiplier(this.Hero);
			if (num <= 0f)
			{
				return;
			}
			float propertyValue = base.GetPropertyValue(skill);
			this.GetFocus(skill); //return this._newFocuses.GetPropertyValue(skill);
			float focusFactor = this.GetFocusFactor(skill); //is the learning rate
			float num2 = isAffectedByFocusFactor ? (num * focusFactor) : num;
			float num3 = propertyValue + num2;
			int num4;
			Campaign.Current.Models.CharacterDevelopmentModel.GetSkillLevelChange(this.Hero, skill, num3, out num4);
			base.SetPropertyValue(skill, num3);
			if (num4 > 0)
			{
				this.ChangeSkillLevelFromXpChange(skill, num4, shouldNotify);
			}
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x00032390 File Offset: 0x00030590
		private void GainRawXp(float rawXp, bool shouldNotify)
		{
			if ((long)this._totalXp + (long)MathF.Round(rawXp) < (long)Campaign.Current.Models.CharacterDevelopmentModel.GetMaxSkillPoint())
			{
				this._totalXp += MathF.Round(rawXp);
				this.CheckLevel(shouldNotify);
				return;
			}
			this._totalXp = Campaign.Current.Models.CharacterDevelopmentModel.GetMaxSkillPoint();
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x000323F8 File Offset: 0x000305F8
		public float GetFocusFactor(SkillObject skill)
		{
			return Campaign.Current.Models.CharacterDevelopmentModel.CalculateLearningRate(this.Hero, skill);
		}


	*/
		#endregion







	}
}
