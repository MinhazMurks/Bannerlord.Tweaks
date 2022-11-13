namespace Tweaks.Patches
{
	using System;
	using System.Diagnostics.CodeAnalysis;
	using System.Reflection;
	using HarmonyLib;
	using Settings;
	using TaleWorlds.CampaignSystem;
	using TaleWorlds.CampaignSystem.Party;
	using TaleWorlds.CampaignSystem.Settlements;
	using Utils;

	[HarmonyPatch]
	[SuppressMessage("ReSharper", "UnusedType.Global")]
	[SuppressMessage("ReSharper", "UnusedMember.Local")]
	internal class DefaultSettlementGarrisonModelPatch
	{
		private static MethodBase TargetMethod() => AccessTools.Method(AccessTools.TypeByName("DefaultSettlementGarrisonModel"), "FindNumberOfTroopsToLeaveToGarrison", new[]
			{
				typeof(MobileParty),
				typeof(Settlement)
			});

		private static void Postfix(MobileParty? mobileParty, Settlement? settlement, ref int __result)
		{
			if (settlement == null || mobileParty == null)
			{
				return;
			}

			if (Statics.GetSettingsOrThrow() is { } settings && mobileParty.LeaderHero.Clan == Clan.PlayerClan)
			{
				var DisableDonationClan = settlement.OwnerClan == Clan.PlayerClan && settings.DisableTroopDonationPatchEnabled;
				var DisableForAnySettlement = settings.DisableTroopDonationAnyEnabled;

				if (DisableDonationClan || DisableForAnySettlement)
				{
					if (Statics.GetSettingsOrThrow().SettlementsDebug)
					{
						MessageUtil.MessageDebug("FindNumberOfTroopsToLeaveToGarrison: IS DISABLED");
					}
					__result = 0;
				}
			}
		}

		private static bool Prepare() => Statics.GetSettingsOrThrow() is { } settings && settings.DisableTroopDonationPatchEnabled;
	}
}
