namespace Tweaks.Models
{
	using Helpers;
	using TaleWorlds.CampaignSystem;
	using TaleWorlds.CampaignSystem.CharacterDevelopment;
	using TaleWorlds.CampaignSystem.GameComponents;
	using TaleWorlds.CampaignSystem.Party;
	using TaleWorlds.Core;

	internal class TweaksMobilePartyFoodConsumptionModel : DefaultMobilePartyFoodConsumptionModel
	{
		// Token: 0x06002DFD RID: 11773 RVA: 0x000BB4A4 File Offset: 0x000B96A4
		public override ExplainedNumber CalculateDailyFoodConsumptionf(MobileParty party, ExplainedNumber baseConsumption)
		{
			var num = 0;
			for (var i = 0; i < party.MemberRoster.Count; i++)
			{
				if (party.MemberRoster.GetCharacterAtIndex(i).Culture.IsBandit)
				{
					num += party.MemberRoster.GetElementNumber(i);
				}
			}
			for (var j = 0; j < party.PrisonRoster.Count; j++)
			{
				if (party.PrisonRoster.GetCharacterAtIndex(j).Culture.IsBandit)
				{
					num += party.PrisonRoster.GetElementNumber(j);
				}
			}
			var num2 = party.Party.NumberOfAllMembers + (party.Party.NumberOfPrisoners / 2);
			if (party.LeaderHero != null && party.LeaderHero.CharacterObject.GetPerkValue(DefaultPerks.Roguery.Promises) && num != 0)
			{
				num2 += (int)(num * DefaultPerks.Roguery.Promises.PrimaryBonus * 0.01f);
			}
			num2 = (num2 < 1) ? 1 : num2;
			var baseNumber = -num2 / 20f;
			//~ KT
			if (Statics._settings.PartyFoodConsumptionEnabled)
			{
				//IM.MessageDebug("PartyFoodConsumption: original: " + baseNumber.ToString());
				baseNumber *= Statics._settings.PartyFoodConsumptionMultiplier;
				//IM.MessageDebug("PartyFoodConsumption: modified: " + baseNumber.ToString());
			}
			//~ KT
			var result = new ExplainedNumber(baseNumber, false);
			this.CalculatePerkEffects(party, ref result);
			return result;
		}


		// Token: 0x06002DFF RID: 11775 RVA: 0x000BB6C8 File Offset: 0x000B98C8
		public override bool DoesPartyConsumeFood(MobileParty mobileParty) => mobileParty.IsActive && (mobileParty.LeaderHero == null || mobileParty.LeaderHero.IsLord || mobileParty.LeaderHero.Clan == Clan.PlayerClan || mobileParty.LeaderHero.IsMinorFactionHero) && !mobileParty.IsGarrison && !mobileParty.IsCommonAreaParty && !mobileParty.IsCaravan && !mobileParty.IsBandit && !mobileParty.IsMilitia && !mobileParty.IsVillager;

		// Token: 0x06002DFE RID: 11774 RVA: 0x000BB5B0 File Offset: 0x000B97B0
		private void CalculatePerkEffects(MobileParty party, ref ExplainedNumber result)
		{
			PerkHelper.AddPerkBonusForParty(DefaultPerks.Athletics.Spartan, party, false, ref result);
			PerkHelper.AddPerkBonusForParty(DefaultPerks.Steward.Spartan, party, true, ref result);
			if (party.EffectiveQuartermaster != null)
			{
				PerkHelper.AddEpicPerkBonusForCharacter(DefaultPerks.Steward.PriceOfLoyalty, party.EffectiveQuartermaster.CharacterObject, DefaultSkills.Steward, true, ref result, 200);
			}
			var faceTerrainType = Campaign.Current.MapSceneWrapper.GetFaceTerrainType(party.CurrentNavigationFace);
			if (faceTerrainType is TerrainType.Forest or TerrainType.Steppe)
			{
				PerkHelper.AddPerkBonusForParty(DefaultPerks.Scouting.Foragers, party, true, ref result);
			}
			if (party.IsGarrison && party.CurrentSettlement != null && party.CurrentSettlement.Town.IsUnderSiege)
			{
				PerkHelper.AddPerkBonusForTown(DefaultPerks.Athletics.StrongLegs, party.CurrentSettlement.Town, ref result);
			}
			if (party.Army != null)
			{
				PerkHelper.AddPerkBonusForParty(DefaultPerks.Steward.StiffUpperLip, party, true, ref result);
			}
			var siegeEvent = party.SiegeEvent;
			if ((siegeEvent?.BesiegerCamp) != null && party.SiegeEvent.BesiegerCamp.BesiegerParty == party && party.HasPerk(DefaultPerks.Steward.SoundReserves, true))
			{
				PerkHelper.AddPerkBonusForParty(DefaultPerks.Steward.SoundReserves, party, false, ref result);
			}
		}




	}
}
