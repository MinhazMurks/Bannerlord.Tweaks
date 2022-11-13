namespace Tweaks.Models
{
	using Settings;
	using TaleWorlds.CampaignSystem.GameComponents;

	internal class TweaksDifficultyModel : DefaultDifficultyModel
	{
		/*
				public override float GetDamageToFriendsMultiplier()
				{
					return MCMSettings.Instance is { } settings && settings.DamageToFriendsTweakEnabled ? settings.DamageToFriendsMultiplier : base.GetDamageToFriendsMultiplier();
				}
		*/

		public override float GetDamageToPlayerMultiplier() => Statics.GetSettingsOrThrow() is
		{
			DamageToPlayerTweakEnabled: true
		} settings ? settings.DamageToPlayerMultiplier : base.GetDamageToPlayerMultiplier();

		public override float GetPlayerTroopsReceivedDamageMultiplier() => Statics.GetSettingsOrThrow() is
		{
			DamageToTroopsTweakEnabled: true
		} settings ? settings.DamageToTroopsMultiplier : base.GetPlayerTroopsReceivedDamageMultiplier();

		public override float GetCombatAIDifficultyMultiplier() => Statics.GetSettingsOrThrow() is
		{
			CombatAIDifficultyTweakEnabled: true
		} settings ? settings.CombatAIDifficultyMultiplier : base.GetCombatAIDifficultyMultiplier();

		public override float GetPlayerMapMovementSpeedBonusMultiplier() => Statics.GetSettingsOrThrow() is
		{
			PlayerMapMovementSpeedBonusTweakEnabled: true
		} settings ? settings.PlayerMapMovementSpeedBonusMultiplier : base.GetPlayerMapMovementSpeedBonusMultiplier();

		public override float GetPersuasionBonusChance() => Statics.GetSettingsOrThrow() is
		{
			PlayerPersuasionBonusChanceTweakEnabled: true
		} settings ? settings.PlayerPersuasionBonusChanceMultiplier : base.GetPersuasionBonusChance();

		public override float GetClanMemberDeathChanceMultiplier() => Statics.GetSettingsOrThrow() is
		{
			ClanMemberDeathChanceReductionTweakEnabled: true
		} settings ? settings.ClanMemberDeathChanceReductionMultiplier : base.GetClanMemberDeathChanceMultiplier();



		// Token: 0x06002C31 RID: 11313 RVA: 0x000AC2F0 File Offset: 0x000AA4F0
		/*
				public override int GetPlayerRecruitSlotBonus()
				{
					switch (CampaignOptions.RecruitmentDifficulty)
					{
						case CampaignOptions.Difficulty.VeryEasy:
							return 2;
						case CampaignOptions.Difficulty.Easy:
							return 1;
						case CampaignOptions.Difficulty.Realistic:
							return 0;
						default:
							return 0;
					}
				}*/





	}
}
