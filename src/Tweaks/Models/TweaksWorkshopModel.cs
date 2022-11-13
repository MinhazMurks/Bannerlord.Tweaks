namespace Tweaks.Models
{
	using Settings;
	using TaleWorlds.CampaignSystem;
	using TaleWorlds.CampaignSystem.GameComponents;
	using TaleWorlds.CampaignSystem.Settlements.Workshops;
	using TaleWorlds.Library;

	internal class TweaksWorkshopModel : DefaultWorkshopModel
	{
		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x06002CBA RID: 11450 RVA: 0x000AECEC File Offset: 0x000ACEEC
		public override int DaysForPlayerSaveWorkshopFromBankruptcy
		{
			get
			{
				if (Statics.GetSettingsOrThrow().WorkShopBankruptcyModifiers)
				{
					return Statics.GetSettingsOrThrow().WorkShopBankruptcyValue;
				}
				return 3;
			}
		}

		public override int GetMaxWorkshopCountForTier(int tier)
		{
			if (Statics.GetSettingsOrThrow() is {MaxWorkshopCountTweakEnabled: true} settings)
			{
				return settings.BaseWorkshopCount + (Clan.PlayerClan.Tier * settings.BonusWorkshopsPerClanTier);
			}
			else
			{
				return base.GetMaxWorkshopCountForTier(tier);
			}
		}

		public override int GetBuyingCostForPlayer(Workshop? workshop)
		{
			if (Statics.GetSettingsOrThrow() is {WorkshopBuyingCostTweakEnabled: true} settings && workshop != null)
			{
				return workshop.WorkshopType.EquipmentCost + ((int)workshop.Settlement.Prosperity / 2) + settings.WorkshopBaseCost;
			}
			else
			{
				return base.GetBuyingCostForPlayer(workshop);
			}
		}
		public override int GetDailyExpense(int level)
		{
			if (Statics.GetSettingsOrThrow() is {WorkshopEffectivnessEnabled: true} settings)
			{
				return MathF.Round(base.GetDailyExpense(level) * settings.WorkshopEffectivnessv2Factor);
			}
			else
			{
				return base.GetDailyExpense(level);
			}
		}
	}
}
