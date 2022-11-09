using KaosesTweaks.Common;
using KaosesTweaks.Settings;
using KaosesTweaks.Utils;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;

namespace KaosesTweaks.Objects.Experience
{
    public class KaosesAddSkillXp
    {
        private readonly KaosesMCMSettings? _settings;
        private readonly Hero _hero;
        private bool _isPlayerClan;
        private bool _isPlayer;
        private bool _isAiLord;
        private bool _validHeroForUse;
        private bool _validSkillForUse;
        private readonly SkillObject _skill;
        private readonly float _xpAmount;
        private float _skillMultiplier = 1.0f;
        protected int BoundAttributeLevel = 0;

        public KaosesAddSkillXp(Hero hero, SkillObject skill, float xpAmount)
        {
            _settings = Statics._settings;
            _hero = hero;
            _skill = skill;
            _xpAmount = xpAmount;
            BuildHeroVariables();
            CheckHeroUseModifier();
            CheckSkill();
        }

        private void BuildHeroVariables()
        {
            if (_hero.IsActive && _hero.IsAlive && !_hero.IsDead)
            {
                if (_hero.IsHumanPlayerCharacter)
                {
                    _isPlayer = _hero.IsHumanPlayerCharacter;
                }
                else if (Kaoses.IsLord(_hero) && Kaoses.IsPlayerClan(_hero))
                {
                    _isPlayerClan = Kaoses.IsPlayerClan(_hero);
                }
                if (Kaoses.IsLord(_hero))
                {

                    _isAiLord = _hero.CharacterObject.IsHero;
                }
            }
        }

        private void CheckHeroUseModifier()
        {
            if (_settings is {SkillXpEnabled: true})
            {
                if (_settings.SkillXpUseForPlayer && _isPlayer)
                {
                    _validHeroForUse = true;
                }
                else if (_settings.SkillXpUseForPlayerClan && _isPlayerClan)
                {
                    _validHeroForUse = true;
                }
                else if (_settings.SkillXpUseForAI && _isAiLord)
                {
                    _validHeroForUse = true;
                }
            }
        }

        private void CheckSkill()
        {
            if (_settings is {SkillXpEnabled: true} && _validHeroForUse)
            {
                if (_settings.SkillXpUseGlobalMultipler)
                {
                    _validSkillForUse = true;
                    _skillMultiplier = _settings.SkillsXpGlobalMultiplier;
                }
                else if (_settings.SkillXpUseIndividualMultiplers)
                {
                    _validSkillForUse = true;
                    GetSkillModifier();
                }
            }
        }

        private void GetSkillModifier()
        {
            if (_settings == null) return;
            if (_skill.GetName().Equals(DefaultSkills.Athletics.GetName()))
            {
                //endurance END
                _skillMultiplier = _settings.SkillsXPMultiplierAthletics;
            }
            else if (_skill.GetName().Equals(DefaultSkills.Bow.GetName()))
            {
                //control CTR
                _skillMultiplier = _settings.SkillsXPMultiplierBow;
            }
            else if (_skill.GetName().Equals(DefaultSkills.Charm.GetName()))
            {
                //social  SOC
                _skillMultiplier = _settings.SkillsXPMultiplierCharm;
            }
            else if (_skill.GetName().Equals(DefaultSkills.Crafting.GetName()))
            {
                //endurance END
                _skillMultiplier = _settings.SkillsXPMultiplierCrafting;
            }
            else if (_skill.GetName().Equals(DefaultSkills.Crossbow.GetName()))
            {
                //control CTR
                _skillMultiplier = _settings.SkillsXPMultiplierCrossbow;
            }
            else if (_skill.GetName().Equals(DefaultSkills.Engineering.GetName()))
            {
                //intelligence  INT
                _skillMultiplier = _settings.SkillsXPMultiplierEngineering;
            }
            else if (_skill.GetName().Equals(DefaultSkills.Leadership.GetName()))
            {
                //social  SOC
                _skillMultiplier = _settings.SkillsXPMultiplierLeadership;
            }
            else if (_skill.GetName().Equals(DefaultSkills.Medicine.GetName()))
            {
                //intelligence  INT
                _skillMultiplier = _settings.SkillsXPMultiplierMedicine;
            }
            else if (_skill.GetName().Equals(DefaultSkills.OneHanded.GetName()))
            {
                //vigor  VIG
                _skillMultiplier = _settings.SkillsXPMultiplierOneHanded;
            }
            else if (_skill.GetName().Equals(DefaultSkills.Polearm.GetName()))
            {
                //vigor  VIG
                _skillMultiplier = _settings.SkillsXPMultiplierPolearm;
            }
            else if (_skill.GetName().Equals(DefaultSkills.Riding.GetName()))
            {
                //endurance END
                _skillMultiplier = _settings.SkillsXPMultiplierRiding;
            }
            else if (_skill.GetName().Equals(DefaultSkills.Roguery.GetName()))
            {
                //cunning CNG
                _skillMultiplier = _settings.SkillsXPMultiplierRoguery;
            }
            else if (_skill.GetName().Equals(DefaultSkills.Scouting.GetName()))
            {
                //cunning CNG
                _skillMultiplier = _settings.SkillsXPMultiplierScouting;
            }
            else if (_skill.GetName().Equals(DefaultSkills.Steward.GetName()))
            {
                //intelligence  INT
                _skillMultiplier = _settings.SkillsXPMultiplierSteward;
            }
            else if (_skill.GetName().Equals(DefaultSkills.Tactics.GetName()))
            {
                //cunning CNG
                _skillMultiplier = _settings.SkillsXPMultiplierTactics;
            }
            else if (_skill.GetName().Equals(DefaultSkills.Throwing.GetName()))
            {
                //control CTR
                _skillMultiplier = _settings.SkillsXPMultiplierThrowing;
            }
            else if (_skill.GetName().Equals(DefaultSkills.Trade.GetName()))
            {
                //social  SOC
                _skillMultiplier = _settings.SkillsXPMultiplierTrade;
            }
            else if (_skill.GetName().Equals(DefaultSkills.TwoHanded.GetName()))
            {
                //vigor  VIG
                _skillMultiplier = _settings.SkillsXPMultiplierTwoHanded;
            }
        }

        public bool HasModifiedXp()
        {
            return _settings != null && _validHeroForUse && _validSkillForUse && _settings.SkillXpEnabled;
        }

        public float GetNewSkillXp()
        {
            var newXp = 0.0f;
            if (Statics._settings is {XpModifiersDebug: true} && HasModifiedXp())
            {
                DebugDump();
            }
            if (HasModifiedXp())
            {
                newXp = _xpAmount * _skillMultiplier;
            }
            return newXp;
        }

        public void DebugDump()
        {
            if (Statics._settings != null)
            {
                IM.MessageDebug("KaosesAddSkillXp: "
                                + " StringId: " + _hero.StringId + "\r\n"
                                + " Name: " + _hero.CharacterObject.Name + "\r\n"
                                + "  Skill Name: " + _skill.GetName() + "\r\n"
                                + "  _isPlayerClan: " + _isPlayerClan + "\r\n"
                                + "  SkillXpUseForPlayerClan: " + Statics._settings.SkillXpUseForPlayerClan + "\r\n"
                                + "  _isPlayer: " + _isPlayer + "\r\n"
                                + "  SkillXpUseForPlayer: " + Statics._settings.SkillXpUseForPlayer + "\r\n"
                                + "  _isAILord: " + _isAiLord + "\r\n"
                                + "  SkillXpUseForAI: " + Statics._settings.SkillXpUseForAI + "\r\n"
                                + "  _ValidHeroForUse: " + _validHeroForUse + "\r\n"
                                + "  _ValidSkillForUse: " + _validSkillForUse + "\r\n"
                                + "  _skillMultiplier: " + _skillMultiplier + "\r\n"
                                + "  _xpAmount: " + _xpAmount + "\r\n"
                                + "  new xpAmount: " + (_xpAmount * _skillMultiplier) + "\r\n"
                );
            }
        }


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
