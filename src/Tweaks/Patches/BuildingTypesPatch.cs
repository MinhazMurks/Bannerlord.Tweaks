namespace Tweaks.Patches
{
	using System;
	using System.Diagnostics.CodeAnalysis;
	using HarmonyLib;
	using TaleWorlds.CampaignSystem.Settlements.Buildings;
	using TaleWorlds.Localization;

	[HarmonyPatch(typeof(DefaultBuildingTypes), "InitializeAll")]
	[SuppressMessage("ReSharper", "UnusedType.Global")]
	[SuppressMessage("ReSharper", "UnusedMember.Local")]
	internal class BuildingTypesPatch
	{
		private static void Postfix(BuildingType buildingCastleTrainingFields, BuildingType buildingCastleGranary, BuildingType buildingCastleGardens,
			BuildingType buildingCastleMilitiaBarracks, BuildingType buildingSettlementTrainingFields, BuildingType buildingSettlementGranary,
			BuildingType buildingSettlementOrchard, BuildingType buildingSettlementMilitiaBarracks)
		{
			//Castle
			#region Training Fields
			if (Statics.GetSettingsOrThrow().CastleTrainingFieldsBonusEnabled)
			{
				buildingCastleTrainingFields.Initialize(new TextObject("{=BkTiRPT4}Training Fields"),
					new TextObject("{=otWlERkc}A field for military drills that increase the daily experience gain of all garrisoned units."),
					new[] { 500, 1000, 1500 }, BuildingLocation.Castle,
					new[]
					{
						new Tuple<BuildingEffectEnum, float, float, float>(
							BuildingEffectEnum.Experience,
							Statics.GetSettingsOrThrow().CastleTrainingFieldsXpAmountLevel1,
							Statics.GetSettingsOrThrow().CastleTrainingFieldsXpAmountLevel2,
							Statics.GetSettingsOrThrow().CastleTrainingFieldsXpAmountLevel3
						)
					});
			}
			#endregion
			#region Granary
			if (Statics.GetSettingsOrThrow().CastleGranaryBonusEnabled)
			{
				buildingCastleGranary.Initialize(new TextObject("{=PstO2f5I}Granary"),
					new TextObject("{=iazij7fO}Keeps stockpiles of food so that the settlement has more food supply. Each level increases the local food supply."),
					new[] { 1000, 1500, 2000 }, BuildingLocation.Castle,
					new[]
					{
						new Tuple<BuildingEffectEnum, float, float, float>(
							BuildingEffectEnum.Foodstock,
							Statics.GetSettingsOrThrow().CastleGranaryStorageAmountLevel1,
							Statics.GetSettingsOrThrow().CastleGranaryStorageAmountLevel2,
							Statics.GetSettingsOrThrow().CastleGranaryStorageAmountLevel3
						)
					});
			}
			#endregion
			#region Gardens
			if (Statics.GetSettingsOrThrow().CastleGardensBonusEnabled)
			{
				buildingCastleGardens.Initialize(new TextObject("{=yT6XN4Mr}Gardens"),
					new TextObject("{=ZCLVOXgM}Fruit trees and vegetable gardens outside the walls provide food as long as there is no siege."),
					new[] { 500, 750, 1000 }, BuildingLocation.Castle,
					new[]
					{
						new Tuple<BuildingEffectEnum, float, float, float>(
							BuildingEffectEnum.FoodProduction,
							Statics.GetSettingsOrThrow().CastleGardensFoodProductionAmountLevel1,
							Statics.GetSettingsOrThrow().CastleGardensFoodProductionAmountLevel2,
							Statics.GetSettingsOrThrow().CastleGardensFoodProductionAmountLevel3
						)
					});
			}
			#endregion
			#region Militia Barracks
			if (Statics.GetSettingsOrThrow().CastleMilitiaBarracksBonusEnabled)
			{
				buildingCastleMilitiaBarracks.Initialize(new TextObject("{=l91xAgmU}Militia Grounds"),
					new TextObject("{=YRrx8bAK}Provides battle training for citizens and recruit them into militia, each level increases daily militia recruitment."),
					new[] { 500, 750, 1000 }, BuildingLocation.Castle,
					new[]
					{
						new Tuple<BuildingEffectEnum, float, float, float>(
							BuildingEffectEnum.Militia,
							Statics.GetSettingsOrThrow().CastleMilitiaBarracksAmountLevel1,
							Statics.GetSettingsOrThrow().CastleMilitiaBarracksAmountLevel2,
							Statics.GetSettingsOrThrow().CastleMilitiaBarracksAmountLevel3
						)
					});
			}
			#endregion

			//Town
			#region Training Fields
			if (Statics.GetSettingsOrThrow().TownTrainingFieldsBonusEnabled)
			{
				buildingSettlementTrainingFields.Initialize(new TextObject("{=BkTiRPT4}Training Fields"),
					new TextObject("{=otWlERkc}A field for military drills that increase the daily experience gain of all garrisoned units."),
					new[] { 2000, 3000, 4000 }, BuildingLocation.Settlement,
					new[]
					{
						new Tuple<BuildingEffectEnum, float, float, float>(
						BuildingEffectEnum.Experience,
						Statics.GetSettingsOrThrow().TownTrainingFieldsXpAmountLevel1,
						Statics.GetSettingsOrThrow().TownTrainingFieldsXpAmountLevel2,
						Statics.GetSettingsOrThrow().TownTrainingFieldsXpAmountLevel3)
					});
			}
			#endregion
			#region Granary
			if (Statics.GetSettingsOrThrow().TownGranaryBonusEnabled)
			{
				buildingSettlementGranary.Initialize(new TextObject("{=PstO2f5I}Granary"),
					new TextObject("{=aK23T43P}Keeps stockpiles of food so that the settlement has more food supply. Each level increases the local food supply."),
					new[] { 1000, 1500, 2000 }, BuildingLocation.Settlement,
					new[]
					{
						new Tuple<BuildingEffectEnum,float,float,float>(
							BuildingEffectEnum.Foodstock,
							Statics.GetSettingsOrThrow().TownGranaryStorageAmountLevel1,
							Statics.GetSettingsOrThrow().TownGranaryStorageAmountLevel2,
							Statics.GetSettingsOrThrow().TownGranaryStorageAmountLevel3)
					});
			}
			#endregion
			#region Orchards
			if (Statics.GetSettingsOrThrow().TownOrchardsBonusEnabled)
			{
				buildingSettlementOrchard.Initialize(new TextObject("{=AkbiPIij}Orchards"),
					new TextObject("{=ZCLVOXgM}Fruit trees and vegetable gardens outside the walls provide food as long as there is no siege."),
					new[] { 2000, 3000, 4000 }, BuildingLocation.Settlement,
					new[]
					{
					new Tuple<BuildingEffectEnum,float,float,float>(
						BuildingEffectEnum.FoodProduction,
						Statics.GetSettingsOrThrow().TownOrchardsFoodProductionAmountLevel1,
						Statics.GetSettingsOrThrow().TownOrchardsFoodProductionAmountLevel2,
						Statics.GetSettingsOrThrow().TownOrchardsFoodProductionAmountLevel3)
					});
			}
			#endregion
			#region Militia Barracks
			if (Statics.GetSettingsOrThrow().TownMilitiaBarracksBonusEnabled)
			{
				buildingSettlementMilitiaBarracks.Initialize(new TextObject("{=l91xAgmU}Militia Grounds"),
					new TextObject("{=RliyRJKl}Provides battle training for citizens and recruit them into militia. Increases daily militia recruitment."),
					new[] { 2000, 3000, 4000 }, BuildingLocation.Settlement,
					new[]
					{
						new Tuple<BuildingEffectEnum, float, float, float>(
							BuildingEffectEnum.Militia,
							Statics.GetSettingsOrThrow().TownMilitiaBarracksAmountLevel1,
							Statics.GetSettingsOrThrow().TownMilitiaBarracksAmountLevel2,
							Statics.GetSettingsOrThrow().TownMilitiaBarracksAmountLevel3)
					});
			}
			#endregion
		}

		private static bool Prepare() =>
			Statics.GetSettingsOrThrow().CastleGranaryBonusEnabled || Statics.GetSettingsOrThrow().CastleGardensBonusEnabled ||
			Statics.GetSettingsOrThrow().CastleTrainingFieldsBonusEnabled || Statics.GetSettingsOrThrow().CastleMilitiaBarracksBonusEnabled ||
			Statics.GetSettingsOrThrow().TownGranaryBonusEnabled || Statics.GetSettingsOrThrow().TownOrchardsBonusEnabled ||
			Statics.GetSettingsOrThrow().TownTrainingFieldsBonusEnabled || Statics.GetSettingsOrThrow().TownMilitiaBarracksBonusEnabled;
	}
}
