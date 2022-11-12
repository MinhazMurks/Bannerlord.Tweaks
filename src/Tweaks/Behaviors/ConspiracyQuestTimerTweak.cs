/*
 1.5.7.2 - Disable until we understand main quest changes.
 */
namespace Tweaks.Behaviors
{
	using System;
	using StoryMode.StoryModePhases;
	using TaleWorlds.CampaignSystem;
	using Utils;

	internal class ConspiracyQuestTimerTweak
	{
		public static void Apply(Campaign campaign)
		{
			var obj = new ConspiracyQuestTimerTweak();
			CampaignEvents.DailyTickEvent.AddNonSerializedListener(obj, new Action(obj.ExtendDeadline));
		}

		private void ExtendDeadline()
		{
			if (Campaign.Current != null && Campaign.Current.QuestManager != null)
			{
				foreach (var questBase in Campaign.Current.QuestManager.Quests)
				{
					var flag2 = questBase.GetName().ToString().StartsWith("stop_conspiracy_") && questBase.QuestDueTime < CampaignTime.DaysFromNow(5f);
					if (flag2)
					{
						MessageUtil.ColorGreenMessage("Extending Stop the Conspiracy quest by 1 year.");
						questBase.ChangeQuestDueTime(CampaignTime.YearsFromNow(1f));
						MessageUtil.ColorGreenMessage("New quest deadline: " + questBase.QuestDueTime.ToString());
					}
					var flag3 = questBase.StringId.StartsWith("conspiracy_quest_") && questBase.QuestDueTime < CampaignTime.DaysFromNow(7f);
					if (flag3)
					{
						questBase.ChangeQuestDueTime(CampaignTime.WeeksFromNow(3f));
						MessageUtil.ColorGreenMessage("BT Extend Conspiracy Tweak: Extended conspiracy quest.");
						var cStrngth = SecondPhase.Instance.ConspiracyStrength;
						if (cStrngth is > 1000 and > 250)
						{
							SecondPhase.Instance.DecreaseConspiracyStrength(150);
							MessageUtil.ColorGreenMessage("BT Extend Conspiracy Tweak: Reduced conspiracy strength.");
						}

					}
				}
			}
		}
	}
}
