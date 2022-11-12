namespace Tweaks.Patches
{
	using System;
	using System.Reflection;
	using HarmonyLib;
	using Settings;
	using TaleWorlds.CampaignSystem;
	using TaleWorlds.CampaignSystem.Party;
	using TaleWorlds.CampaignSystem.Settlements;
	using Utils;

	[HarmonyPatch]
	internal class DefaultSettlementGarrisonModelPatch
	{
		private static MethodBase TargetMethod() => AccessTools.Method(AccessTools.TypeByName("DefaultSettlementGarrisonModel"), "FindNumberOfTroopsToLeaveToGarrison", new Type[]
			{
				typeof(MobileParty),
				typeof(Settlement)
			}, null);

		private static void Postfix(MobileParty mobileParty, Settlement settlement, ref int __result)
		{
			if (settlement == null || mobileParty == null)
			{
				return;
			}

			if (TweaksMCMSettings.Instance is { } settings && mobileParty.LeaderHero.Clan == Clan.PlayerClan)
			{
				var DisableDonationClan = settlement.OwnerClan == Clan.PlayerClan && settings.DisableTroopDonationPatchEnabled;
				var DisableForAnySettlement = settings.DisableTroopDonationAnyEnabled;

				if (DisableDonationClan || DisableForAnySettlement)
				{
					if (Statics._settings.SettlementsDebug)
					{
						MessageUtil.MessageDebug("FindNumberOfTroopsToLeaveToGarrison: IS DISABLED");
					}
					__result = 0;
				}
			}
		}

		private static bool Prepare() => TweaksMCMSettings.Instance is { } settings && settings.DisableTroopDonationPatchEnabled;
	}
}
