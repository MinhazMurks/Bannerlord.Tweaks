﻿namespace Tweaks.Models
{
	using System;
	using TaleWorlds.CampaignSystem;
	using TaleWorlds.CampaignSystem.CharacterDevelopment;
	using TaleWorlds.CampaignSystem.GameComponents;
	using TaleWorlds.Core;
	using TaleWorlds.Library;
	using TaleWorlds.Localization;

	internal class TweaksClanTierModel : DefaultClanTierModel
	{
		// Token: 0x06002C15 RID: 11285 RVA: 0x000AAEB8 File Offset: 0x000A90B8
		public override int GetPartyLimitForTier(Clan clan, int clanTierToCheck)
		{
			var result = new ExplainedNumber();
			if (Statics.GetSettingsOrThrow().ClanAdditionalPartyLimitEnabled && clan == Clan.PlayerClan && Statics.GetSettingsOrThrow().ClanPlayerPartiesLimitEnabled)
			{
				result.Add((float)(Statics.GetSettingsOrThrow().ClanPlayerBasePartiesLimit + Math.Floor(clanTierToCheck * Statics.GetSettingsOrThrow().ClanPlayerPartiesBonusPerClanTier)), new TextObject("KT Player Clan Parties Tweak"));
			}
			else if (Statics.GetSettingsOrThrow().ClanAIPartiesLimitTweakEnabled && clan.IsClan && !clan.StringId.Contains("_deserters"))
			{

				if (Statics.GetSettingsOrThrow().AICustomSpawnPartiesLimitTweakEnabled && clan.StringId.StartsWith("cs_"))
				{
					result.Add((float)(Statics.GetSettingsOrThrow().BaseAICustomSpawnPartiesLimit + Math.Floor(clanTierToCheck * Statics.GetSettingsOrThrow().ClanCSPartiesBonusPerClanTier)), new TextObject("KT Custom Spawn Parties Tweak"));

				}
				else if (Statics.GetSettingsOrThrow().ClanAIMinorClanPartiesLimitTweakEnabled && clan.IsMinorFaction && !clan.StringId.StartsWith("cs_"))
				{
					result.Add(base.GetPartyLimitForTier(clan, clanTierToCheck) + Statics.GetSettingsOrThrow().ClanAIBaseClanPartiesLimit, new TextObject("KT Minor Clan Parties Tweak"));
				}
				else if (clan.IsClan)
				{
					result.Add((float)(Statics.GetSettingsOrThrow().ClanAIBaseClanPartiesLimit + Math.Floor(clanTierToCheck * Statics.GetSettingsOrThrow().ClanAIPartiesBonusPerClanTier)), new TextObject("KT AI Clan Parties Tweak"));
				}
			}
			else if (!clan.IsMinorFaction)
			{
				if (clanTierToCheck < 3)
				{
					result.Add(1f);
				}
				else if (clanTierToCheck < 5)
				{
					result.Add(2f);
				}
				else
				{
					result.Add(3f);
				}
			}
			else
			{
				result.Add(MathF.Clamp(clanTierToCheck, 1f, 4f));
			}

			this.AddPartyLimitPerkEffects(clan, ref result);
			return MathF.Round(result.ResultNumber);
		}

		// Token: 0x06002C17 RID: 11287 RVA: 0x000AAF7C File Offset: 0x000A917C
		public override int GetCompanionLimit(Clan clan)
		{
			var num = this.GetCompanionLimitFromTier(clan.Tier);
			if (Statics.GetSettingsOrThrow().ClanCompanionLimitEnabled)
			{
				num += Statics.GetSettingsOrThrow().ClanAdditionalCompanionLimit * clan.Tier;
			}
			if (clan.Leader.GetPerkValue(DefaultPerks.Leadership.WePledgeOurSwords))
			{
				num += (int)DefaultPerks.Leadership.WePledgeOurSwords.PrimaryBonus;
			}
			return num;
		}

		// Token: 0x06002C18 RID: 11288 RVA: 0x000AAFB7 File Offset: 0x000A91B7
		private int GetCompanionLimitFromTier(int clanTier)
		{
			if (Statics.GetSettingsOrThrow().ClanCompanionLimitEnabled)
			{
				return clanTier + Statics.GetSettingsOrThrow().ClanCompanionBaseLimit;
			}
			return clanTier + 3;
		}


		//~ KaosesClanTierModel Copied Private Methods to make Clan Model Work
		#region Copied Private Methods to make Clan Model Work
		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x06002C0F RID: 11279 RVA: 0x000AAC6F File Offset: 0x000A8E6F
		private int KingdomEligibleTier => Campaign.Current.Models.KingdomCreationModel.MinimumClanTierToCreateKingdom;

		// Token: 0x06002C16 RID: 11286 RVA: 0x000AAF42 File Offset: 0x000A9142
		private void AddPartyLimitPerkEffects(Clan clan, ref ExplainedNumber result)
		{
			if (clan.Leader != null && clan.Leader.GetPerkValue(DefaultPerks.Leadership.TalentMagnet))
			{
				result.Add(DefaultPerks.Leadership.TalentMagnet.SecondaryBonus, DefaultPerks.Leadership.TalentMagnet.Name);
			}
		}

		// Token: 0x04000EC5 RID: 3781
		private readonly TextObject partyLimitBonusText = GameTexts.FindText("str_clan_tier_party_limit_bonus");

		// Token: 0x04000EC6 RID: 3782
		private readonly TextObject companionLimitBonusText = GameTexts.FindText("str_clan_tier_companion_limit_bonus");

		// Token: 0x04000EC7 RID: 3783
		private readonly TextObject mercenaryEligibleText = GameTexts.FindText("str_clan_tier_mercenary_eligible");

		// Token: 0x04000EC8 RID: 3784
		private readonly TextObject vassalEligibleText = GameTexts.FindText("str_clan_tier_vassal_eligible");

		// Token: 0x04000EC9 RID: 3785
		private readonly TextObject additionalCurrentPartySizeBonus = GameTexts.FindText("str_clan_tier_party_size_bonus");

		// Token: 0x04000ECA RID: 3786
		private readonly TextObject kingdomEligibleText = GameTexts.FindText("str_clan_tier_kingdom_eligible");

		// Token: 0x06002C13 RID: 11283 RVA: 0x000AAD60 File Offset: 0x000A8F60
		public override ValueTuple<ExplainedNumber, bool> HasUpcomingTier(Clan clan, out TextObject extraExplanation, bool includeDescriptions = false)
		{
			var flag = clan.Tier < this.MaxClanTier;
			var item = new ExplainedNumber(0f, includeDescriptions);
			extraExplanation = TextObject.Empty;
			if (flag)
			{
				var num = this.GetPartyLimitForTier(clan, clan.Tier + 1) - this.GetPartyLimitForTier(clan, clan.Tier);
				if (num != 0)
				{
					item.Add(num, this.partyLimitBonusText);
				}
				var num2 = this.GetCompanionLimitFromTier(clan.Tier + 1) - this.GetCompanionLimitFromTier(clan.Tier);
				if (num2 != 0)
				{
					item.Add(num2, this.companionLimitBonusText);
				}
				var num3 = Campaign.Current.Models.PartySizeLimitModel.GetTierPartySizeEffect(clan.Tier + 1) - Campaign.Current.Models.PartySizeLimitModel.GetTierPartySizeEffect(clan.Tier);
				if (num3 > 0)
				{
					item.Add(num3, this.additionalCurrentPartySizeBonus);
				}
				if (clan.Tier + 1 == this.MercenaryEligibleTier)
				{
					item.Add(1f, this.mercenaryEligibleText);
				}
				if (clan.Tier + 1 == this.VassalEligibleTier)
				{
					item.Add(1f, this.vassalEligibleText);
				}
				if (clan.Tier + 1 == this.KingdomEligibleTier)
				{
					item.Add(1f, this.kingdomEligibleText);
				}
			}
			return new ValueTuple<ExplainedNumber, bool>(item, flag);
		}
		#endregion














	}
}
