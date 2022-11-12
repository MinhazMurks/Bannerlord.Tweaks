namespace Tweaks.Models
{
	using Settings;
	using TaleWorlds.CampaignSystem.GameComponents;
	using TaleWorlds.CampaignSystem.Party;
	using TaleWorlds.CampaignSystem.Siege;
	using TaleWorlds.Core;

	internal class TweaksSiegeEventModel : DefaultSiegeEventModel
	{
		public override float GetConstructionProgressPerHour(SiegeEngineType type, SiegeEvent siegeEvent, ISiegeEventSide side)
		{
			if (TweaksMCMSettings.Instance is { } settings)
			{
				return base.GetConstructionProgressPerHour(type, siegeEvent, side) * settings.SiegeConstructionProgressPerDayMultiplier;
			}
			else
			{
				return base.GetConstructionProgressPerHour(type, siegeEvent, side);
			}
		}

		public override int GetColleteralDamageCasualties(SiegeEngineType siegeEngineType, MobileParty party)
		{
			if (TweaksMCMSettings.Instance is { } settings)
			{
				return base.GetColleteralDamageCasualties(siegeEngineType, party) + settings.SiegeCollateralDamageCasualties;
			}
			else
			{
				return base.GetColleteralDamageCasualties(siegeEngineType, party);
			}
		}

		public override int GetSiegeEngineDestructionCasualties(SiegeEvent siegeEvent, BattleSideEnum side, SiegeEngineType destroyedSiegeEngine)
		{
			if (TweaksMCMSettings.Instance is { } settings)
			{
				return base.GetSiegeEngineDestructionCasualties(siegeEvent, side, destroyedSiegeEngine) + settings.SiegeDestructionCasualties;
			}
			else
			{
				return base.GetSiegeEngineDestructionCasualties(siegeEvent, side, destroyedSiegeEngine);
			}
		}
	}
}
