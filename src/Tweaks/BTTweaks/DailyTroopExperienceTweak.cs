using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.Core;
using TaleWorlds.Library;
using Tweaks.Settings;
using Tweaks.Utils;

namespace Tweaks.BTTweaks
{
    class DailyTroopExperienceTweak
    {
        public static void Apply(Campaign campaign)
        {
            DailyTroopExperienceTweak? obj = new DailyTroopExperienceTweak();
            CampaignEvents.DailyTickPartyEvent.AddNonSerializedListener(obj, (MobileParty mp) => { obj.DailyTick(mp); });
        }

        private void DailyTick(MobileParty party)
        {
            if (party.LeaderHero != null)
            {
                int count = party.MemberRoster.TotalManCount;
                if (party.LeaderHero == Hero.MainHero || TweaksMCMSettings.Instance is { } settings && settings.DailyTroopExperienceApplyToAllNPC
                    || TweaksMCMSettings.Instance is { } settings2 && settings2.DailyTroopExperienceApplyToPlayerClanMembers && party.LeaderHero.Clan == Clan.PlayerClan)
                {
                    int experienceAmount = ExperienceAmount(party.LeaderHero);
                    if (experienceAmount > 0)
                    {
                        int num = 0;
                        foreach (TroopRosterElement troop in party.MemberRoster.GetTroopRoster())
                        {
                            if (!troop.Character.IsHero)
                            {
                                party.MemberRoster.AddXpToTroop(experienceAmount, troop.Character);
                                num++;
                            }
                        }

                        if (TweaksMCMSettings.Instance is { } settings3 && settings3.DisplayMessageDailyExperienceGain)
                        {
                            string troops = count == 1 ? "troop roster (stack)" : "troop rosters (stacks)";
                            if (party.LeaderHero == Hero.MainHero && num > 0)
                                InformationManager.DisplayMessage(new InformationMessage($"Granted {experienceAmount} experience to {num} {troops}."));
                        }
                    }
                }
            }
        }

        private static int ExperienceAmount(Hero h)
        {
            int leadership = h.GetSkillValue(DefaultSkills.Leadership);
            if (TweaksMCMSettings.Instance != null)
            {
                if (TweaksMCMSettings.Instance.XpModifiersDebug)
                {
                    IM.MessageDebug("leadership: " + leadership.ToString() + " RequiredLeadershipLevel: " + TweaksMCMSettings.Instance.DailyTroopExperienceRequiredLeadershipLevel.ToString());
                }
                if (leadership >= TweaksMCMSettings.Instance.DailyTroopExperienceRequiredLeadershipLevel)
                {
                    if (TweaksMCMSettings.Instance.XpModifiersDebug)
                    {
                        IM.MessageDebug("DailyExperienceGain : " + (TweaksMCMSettings.Instance.LeadershipPercentageForDailyExperienceGain * leadership).ToString());
                    }
                    return (int)(TweaksMCMSettings.Instance.LeadershipPercentageForDailyExperienceGain * leadership);
                }
            }
            return 0;
        }
    }
}
