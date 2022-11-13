namespace Tweaks.Tweaks
{
	using System;
	using System.Linq;
	using System.Reflection;
	using Settings;
	using TaleWorlds.CampaignSystem;
	using TaleWorlds.CampaignSystem.Actions;
	using TaleWorlds.CampaignSystem.CampaignBehaviors;
	using Utils;

	internal class PrisonerImprisonmentTweak
	{

		public static void Apply(Campaign campaign)
		{
			if (campaign == null)
			{
				throw new ArgumentNullException(nameof(campaign));
			}

			var escapeBehaviour = campaign.GetCampaignBehavior<PrisonerReleaseCampaignBehavior>();
			if (escapeBehaviour != null && CampaignEvents.DailyTickHeroEvent != null)
			{
				CampaignEvents.DailyTickHeroEvent.ClearListeners(escapeBehaviour);
				CampaignEvents.DailyTickHeroEvent.AddNonSerializedListener(escapeBehaviour, (Hero h) => Check(escapeBehaviour, h));
			}
		}

		private static void Check(PrisonerReleaseCampaignBehavior escapeBehaviour, Hero hero)
		{
			if (escapeBehaviour == null || !(Statics.GetSettingsOrThrow() is { } settings) || !hero.IsPrisoner)
			{
				return;
			}

			if (hero.PartyBelongedToAsPrisoner != null && (hero.PartyBelongedToAsPrisoner.MapFaction != null
				|| hero.PartyBelongedToAsPrisoner.LeaderHero?.Clan == Hero.MainHero.Clan))
			{
				var flag = hero.PartyBelongedToAsPrisoner.MapFaction == Hero.MainHero.MapFaction
					|| (hero.PartyBelongedToAsPrisoner.IsSettlement && hero.PartyBelongedToAsPrisoner.Settlement.OwnerClan == Clan.PlayerClan);

				if ((settings.PrisonerImprisonmentPlayerOnly && flag)
					|| (settings.PrisonerImprisonmentPlayerOnly == false && (Kingdom.All.Contains(hero.PartyBelongedToAsPrisoner.MapFaction)
					|| hero.PartyBelongedToAsPrisoner.IsSettlement)))
				{
					flag = true;
				}

				if (flag == true)
				{
					if ((hero.PartyBelongedToAsPrisoner.NumberOfHealthyMembers < hero.PartyBelongedToAsPrisoner.NumberOfPrisoners && !hero.PartyBelongedToAsPrisoner.IsSettlement) ||
						hero.PartyBelongedToAsPrisoner.IsStarving ||
						(hero.MapFaction != null && FactionManager.IsNeutralWithFaction(hero.MapFaction, hero.PartyBelongedToAsPrisoner.MapFaction)) ||
						(int)hero.CaptivityStartTime.ElapsedDaysUntilNow > settings.MinimumDaysOfImprisonment)
					{

						if (Statics.GetSettingsOrThrow().PrisonersDebug)
						{
							MessageUtil.MessageDebug("Prisoner release: elapsed >" + hero.CaptivityStartTime.ElapsedDaysUntilNow.ToString() + "\r\n"
								+ "MinimumDaysOfImprisonment: " + settings.MinimumDaysOfImprisonment.ToString() + "\r\n"
								);
						}
						typeof(PrisonerReleaseCampaignBehavior).GetMethod("DailyHeroTick", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(escapeBehaviour, new object[] { hero });
						return;
					}
					return;
				}

				else
				{
					typeof(PrisonerReleaseCampaignBehavior).GetMethod("DailyHeroTick", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(escapeBehaviour, new object[] { hero });
				}
				return;

			}
			else
			{
				typeof(PrisonerReleaseCampaignBehavior).GetMethod("DailyHeroTick", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(escapeBehaviour, new object[] { hero });
			}
		}

		public static void DailyTick()
		{
			foreach (var hero in Hero.AllAliveHeroes)
			{
				if (hero == null)
				{
					return;
				}

				if (hero.PartyBelongedToAsPrisoner == null && hero.IsPrisoner && hero.IsAlive && !hero.IsActive && !hero.IsNotSpawned && !hero.IsReleased)
				{
					var days = hero.CaptivityStartTime.ElapsedDaysUntilNow;
					if (Statics.GetSettingsOrThrow() is { } settings && (days > (settings.MinimumDaysOfImprisonment + 3)))
					{
						MessageUtil.ColorGreenMessage("Releasing " + hero.Name + " due to Missing Hero Bug. (" + (int)days + " days)");
						MessageUtil.QuickInformationMessage("Releasing " + hero.Name + " due to Missing Hero Bug. (" + (int)days + " days)");
						EndCaptivityAction.ApplyByReleasedByChoice(hero);
					}
				}
			}
		}
	}
}
