namespace Tweaks.Objects.Experience
{
	using Common;
	using Settings;
	using TaleWorlds.CampaignSystem;
	using TaleWorlds.Core;
	using Utils;

	public class TweaksAddSkillXp
	{
		protected TweaksMCMSettings? _settings;
		protected Hero _hero;
		protected bool _isPlayerClan = false;
		protected bool _isPlayer = false;
		protected bool _isAILord = false;
		protected bool _ValidHeroForUse = false;
		protected bool _ValidSkillForUse = false;
		protected SkillObject _skill;
		protected float _xpAmount;
		protected float _skillMultiplier = 1.0f;
		protected int _boundAttributeLevel = 0;

		public TweaksAddSkillXp(Hero hero, SkillObject skill, float xpAmount)
		{
			this._settings = Statics._settings;
			this._hero = hero;
			this._skill = skill;
			this._xpAmount = xpAmount;
			this.BuildHeroVeriables();
			this.CheckHeroUseModifier();
			this.CheckSkill();
		}

		protected void BuildHeroVeriables()
		{
			if (this._hero.IsActive && this._hero.IsAlive && !this._hero.IsDead && this._hero != null)
			{
				if (this._hero.IsHumanPlayerCharacter)
				{
					this._isPlayer = this._hero.IsHumanPlayerCharacter;
				}
				else if (Kaoses.IsLord(this._hero) && Kaoses.IsPlayerClan(this._hero))
				{
					this._isPlayerClan = Kaoses.IsPlayerClan(this._hero);
				}
				if (Kaoses.IsLord(this._hero))
				{

					this._isAILord = this._hero.CharacterObject.IsHero;
				}
			}
		}

		protected void CheckHeroUseModifier()
		{
			if (this._settings.SkillXpEnabled)
			{
				if (this._settings.SkillXpUseForPlayer && this._isPlayer)
				{
					this._ValidHeroForUse = true;
				}
				else if (this._settings.SkillXpUseForPlayerClan && this._isPlayerClan)
				{
					this._ValidHeroForUse = true;
				}
				else if (this._settings.SkillXpUseForAI && this._isAILord)
				{
					this._ValidHeroForUse = true;
				}
			}
		}

		protected void CheckSkill()
		{
			if (this._settings.SkillXpEnabled && this._ValidHeroForUse)
			{
				if (this._settings.SkillXpUseGlobalMultipler)
				{
					this._ValidSkillForUse = true;
					this._skillMultiplier = this._settings.SkillsXpGlobalMultiplier;
				}
				else if (this._settings.SkillXpUseIndividualMultiplers)
				{
					this._ValidSkillForUse = true;
					this.GetSkillModifier();
				}
			}
		}

		protected void GetSkillModifier()
		{
			if (this._skill.GetName().Equals(DefaultSkills.Athletics.GetName()))
			{
				//endurance END
				this._skillMultiplier = this._settings.SkillsXPMultiplierAthletics;
			}
			else if (this._skill.GetName().Equals(DefaultSkills.Bow.GetName()))
			{
				//control CTR
				this._skillMultiplier = this._settings.SkillsXPMultiplierBow;
			}
			else if (this._skill.GetName().Equals(DefaultSkills.Charm.GetName()))
			{
				//social  SOC
				this._skillMultiplier = this._settings.SkillsXPMultiplierCharm;
			}
			else if (this._skill.GetName().Equals(DefaultSkills.Crafting.GetName()))
			{
				//endurance END
				this._skillMultiplier = this._settings.SkillsXPMultiplierCrafting;
			}
			else if (this._skill.GetName().Equals(DefaultSkills.Crossbow.GetName()))
			{
				//control CTR
				this._skillMultiplier = this._settings.SkillsXPMultiplierCrossbow;
			}
			else if (this._skill.GetName().Equals(DefaultSkills.Engineering.GetName()))
			{
				//intelligence  INT
				this._skillMultiplier = this._settings.SkillsXPMultiplierEngineering;
			}
			else if (this._skill.GetName().Equals(DefaultSkills.Leadership.GetName()))
			{
				//social  SOC
				this._skillMultiplier = this._settings.SkillsXPMultiplierLeadership;
			}
			else if (this._skill.GetName().Equals(DefaultSkills.Medicine.GetName()))
			{
				//intelligence  INT
				this._skillMultiplier = this._settings.SkillsXPMultiplierMedicine;
			}
			else if (this._skill.GetName().Equals(DefaultSkills.OneHanded.GetName()))
			{
				//vigor  VIG
				this._skillMultiplier = this._settings.SkillsXPMultiplierOneHanded;
			}
			else if (this._skill.GetName().Equals(DefaultSkills.Polearm.GetName()))
			{
				//vigor  VIG
				this._skillMultiplier = this._settings.SkillsXPMultiplierPolearm;
			}
			else if (this._skill.GetName().Equals(DefaultSkills.Riding.GetName()))
			{
				//endurance END
				this._skillMultiplier = this._settings.SkillsXPMultiplierRiding;
			}
			else if (this._skill.GetName().Equals(DefaultSkills.Roguery.GetName()))
			{
				//cunning CNG
				this._skillMultiplier = this._settings.SkillsXPMultiplierRoguery;
			}
			else if (this._skill.GetName().Equals(DefaultSkills.Scouting.GetName()))
			{
				//cunning CNG
				this._skillMultiplier = this._settings.SkillsXPMultiplierScouting;
			}
			else if (this._skill.GetName().Equals(DefaultSkills.Steward.GetName()))
			{
				//intelligence  INT
				this._skillMultiplier = this._settings.SkillsXPMultiplierSteward;
			}
			else if (this._skill.GetName().Equals(DefaultSkills.Tactics.GetName()))
			{
				//cunning CNG
				this._skillMultiplier = this._settings.SkillsXPMultiplierTactics;
			}
			else if (this._skill.GetName().Equals(DefaultSkills.Throwing.GetName()))
			{
				//control CTR
				this._skillMultiplier = this._settings.SkillsXPMultiplierThrowing;
			}
			else if (this._skill.GetName().Equals(DefaultSkills.Trade.GetName()))
			{
				//social  SOC
				this._skillMultiplier = this._settings.SkillsXPMultiplierTrade;
			}
			else if (this._skill.GetName().Equals(DefaultSkills.TwoHanded.GetName()))
			{
				//vigor  VIG
				this._skillMultiplier = this._settings.SkillsXPMultiplierTwoHanded;
			}
		}

		public bool HasModifiedXP()
		{
			var HasModifiedValues = false;
			if (this._ValidHeroForUse && this._ValidSkillForUse && this._settings.SkillXpEnabled)
			{
				HasModifiedValues = true;
			}
			return HasModifiedValues;
		}

		public float GetNewSkillXp()
		{
			var newXp = 0.0f;
			if (Statics._settings.XpModifiersDebug && this.HasModifiedXP())
			{
				this.DebugDump();
			}
			if (this.HasModifiedXP())
			{
				newXp = this._xpAmount * this._skillMultiplier;
			}
			return newXp;
		}

		public void DebugDump() => MessageUtil.MessageDebug("KaosesAddSkillXp: "
				+ " StringId: " + this._hero.StringId.ToString() + "\r\n"
				+ " Name: " + this._hero.CharacterObject.Name.ToString() + "\r\n"
				+ "  Skill Name: " + this._skill.GetName().ToString() + "\r\n"
				+ "  _isPlayerClan: " + this._isPlayerClan.ToString() + "\r\n"
				+ "  SkillXpUseForPlayerClan: " + Statics._settings.SkillXpUseForPlayerClan.ToString() + "\r\n"
				+ "  _isPlayer: " + this._isPlayer.ToString() + "\r\n"
				+ "  SkillXpUseForPlayer: " + Statics._settings.SkillXpUseForPlayer.ToString() + "\r\n"
				+ "  _isAILord: " + this._isAILord.ToString() + "\r\n"
				+ "  SkillXpUseForAI: " + Statics._settings.SkillXpUseForAI.ToString() + "\r\n"
				+ "  _ValidHeroForUse: " + this._ValidHeroForUse.ToString() + "\r\n"
				+ "  _ValidSkillForUse: " + this._ValidSkillForUse.ToString() + "\r\n"
				+ "  _skillMultiplier: " + this._skillMultiplier.ToString() + "\r\n"
				+ "  _xpAmount: " + this._xpAmount.ToString() + "\r\n"
				+ "  new xpAmount: " + (this._xpAmount * this._skillMultiplier).ToString() + "\r\n"
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
