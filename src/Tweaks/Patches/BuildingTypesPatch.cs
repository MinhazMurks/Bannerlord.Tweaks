namespace Tweaks.Patches
{
	using System;
	using HarmonyLib;
	using Settings;
	using TaleWorlds.CampaignSystem.Settlements.Buildings;
	using TaleWorlds.Localization;

	[HarmonyPatch(typeof(DefaultBuildingTypes), "InitializeAll")]
	internal class BTBuildingTypesPatch
	{
		private static void Postfix(BuildingType ____buildingCastleTrainingFields, BuildingType ____buildingCastleGranary, BuildingType ____buildingCastleGardens,
			BuildingType ____buildingCastleMilitiaBarracks, BuildingType ____buildingSettlementTrainingFields, BuildingType ____buildingSettlementGranary,
			BuildingType ____buildingSettlementOrchard, BuildingType ____buildingSettlementMilitiaBarracks)
		{
			if (TweaksMCMSettings.Instance is null)
			{ return; }
			//Castle
			#region Training Fields
			if (TweaksMCMSettings.Instance.CastleTrainingFieldsBonusEnabled)
			{
				____buildingCastleTrainingFields?.Initialize(new TextObject("{=BkTiRPT4}Training Fields"),
					new TextObject("{=otWlERkc}A field for military drills that increase the daily experience gain of all garrisoned units."),
					new int[3] { 500, 1000, 1500 }, BuildingLocation.Castle,
					new Tuple<BuildingEffectEnum, float, float, float>[]
					{
						new Tuple<BuildingEffectEnum, float, float, float>(
							BuildingEffectEnum.Experience,
							TweaksMCMSettings.Instance.CastleTrainingFieldsXpAmountLevel1,
							TweaksMCMSettings.Instance.CastleTrainingFieldsXpAmountLevel2,
							TweaksMCMSettings.Instance.CastleTrainingFieldsXpAmountLevel3
						)
					});
			}
			#endregion
			#region Granary
			if (TweaksMCMSettings.Instance.CastleGranaryBonusEnabled)
			{
				____buildingCastleGranary?.Initialize(new TextObject("{=PstO2f5I}Granary"),
					new TextObject("{=iazij7fO}Keeps stockpiles of food so that the settlement has more food supply. Each level increases the local food supply."),
					new int[3] { 1000, 1500, 2000 }, BuildingLocation.Castle,
					new Tuple<BuildingEffectEnum, float, float, float>[]
					{
						new Tuple<BuildingEffectEnum, float, float, float>(
							BuildingEffectEnum.Foodstock,
							TweaksMCMSettings.Instance.CastleGranaryStorageAmountLevel1,
							TweaksMCMSettings.Instance.CastleGranaryStorageAmountLevel2,
							TweaksMCMSettings.Instance.CastleGranaryStorageAmountLevel3
						)
					});
			}
			#endregion
			#region Gardens
			if (TweaksMCMSettings.Instance.CastleGardensBonusEnabled)
			{
				____buildingCastleGardens?.Initialize(new TextObject("{=yT6XN4Mr}Gardens"),
					new TextObject("{=ZCLVOXgM}Fruit trees and vegetable gardens outside the walls provide food as long as there is no siege.", null),
					new int[] { 500, 750, 1000 }, BuildingLocation.Castle,
					new Tuple<BuildingEffectEnum, float, float, float>[]
					{
						new Tuple<BuildingEffectEnum, float, float, float>(
							BuildingEffectEnum.FoodProduction,
							TweaksMCMSettings.Instance.CastleGardensFoodProductionAmountLevel1,
							TweaksMCMSettings.Instance.CastleGardensFoodProductionAmountLevel2,
							TweaksMCMSettings.Instance.CastleGardensFoodProductionAmountLevel3
						)
					});
			}
			#endregion
			#region Militia Barracks
			if (TweaksMCMSettings.Instance.CastleMilitiaBarracksBonusEnabled)
			{
				____buildingCastleMilitiaBarracks?.Initialize(new TextObject("{=l91xAgmU}Militia Grounds"),
					new TextObject("{=YRrx8bAK}Provides battle training for citizens and recruit them into militia, each level increases daily militia recruitment."),
					new int[3] { 500, 750, 1000 }, BuildingLocation.Castle,
					new Tuple<BuildingEffectEnum, float, float, float>[]
					{
						new Tuple<BuildingEffectEnum, float, float, float>(
							BuildingEffectEnum.Militia,
							TweaksMCMSettings.Instance.CastleMilitiaBarracksAmountLevel1,
							TweaksMCMSettings.Instance.CastleMilitiaBarracksAmountLevel2,
							TweaksMCMSettings.Instance.CastleMilitiaBarracksAmountLevel3
						)
					});
			}
			#endregion

			//Town
			#region Training Fields
			if (TweaksMCMSettings.Instance.TownTrainingFieldsBonusEnabled)
			{
				____buildingSettlementTrainingFields?.Initialize(new TextObject("{=BkTiRPT4}Training Fields"),
					new TextObject("{=otWlERkc}A field for military drills that increase the daily experience gain of all garrisoned units."),
					new int[3] { 2000, 3000, 4000 }, BuildingLocation.Settlement,
					new Tuple<BuildingEffectEnum, float, float, float>[]
					{
						new Tuple<BuildingEffectEnum, float, float, float>(
						BuildingEffectEnum.Experience,
						TweaksMCMSettings.Instance.TownTrainingFieldsXpAmountLevel1,
						TweaksMCMSettings.Instance.TownTrainingFieldsXpAmountLevel2,
						TweaksMCMSettings.Instance.TownTrainingFieldsXpAmountLevel3)
					});
			}
			#endregion
			#region Granary
			if (TweaksMCMSettings.Instance.TownGranaryBonusEnabled)
			{
				____buildingSettlementGranary?.Initialize(new TextObject("{=PstO2f5I}Granary"),
					new TextObject("{=aK23T43P}Keeps stockpiles of food so that the settlement has more food supply. Each level increases the local food supply."),
					new int[3] { 1000, 1500, 2000 }, BuildingLocation.Settlement,
					new Tuple<BuildingEffectEnum, float, float, float>[]
					{
						new Tuple<BuildingEffectEnum,float,float,float>(
							BuildingEffectEnum.Foodstock,
							TweaksMCMSettings.Instance.TownGranaryStorageAmountLevel1,
							TweaksMCMSettings.Instance.TownGranaryStorageAmountLevel2,
							TweaksMCMSettings.Instance.TownGranaryStorageAmountLevel3)
					});
			}
			#endregion
			#region Orchards
			if (TweaksMCMSettings.Instance.TownOrchardsBonusEnabled)
			{
				____buildingSettlementOrchard?.Initialize(new TextObject("{=AkbiPIij}Orchards"),
					new TextObject("{=ZCLVOXgM}Fruit trees and vegetable gardens outside the walls provide food as long as there is no siege."),
					new int[3] { 2000, 3000, 4000 }, BuildingLocation.Settlement,
					new Tuple<BuildingEffectEnum, float, float, float>[]
					{
					new Tuple<BuildingEffectEnum,float,float,float>(
						BuildingEffectEnum.FoodProduction,
						TweaksMCMSettings.Instance.TownOrchardsFoodProductionAmountLevel1,
						TweaksMCMSettings.Instance.TownOrchardsFoodProductionAmountLevel2,
						TweaksMCMSettings.Instance.TownOrchardsFoodProductionAmountLevel3)
					});
			}
			#endregion
			#region Militia Barracks
			if (TweaksMCMSettings.Instance.TownMilitiaBarracksBonusEnabled)
			{
				____buildingSettlementMilitiaBarracks?.Initialize(new TextObject("{=l91xAgmU}Militia Grounds"),
					new TextObject("{=RliyRJKl}Provides battle training for citizens and recruit them into militia. Increases daily militia recruitment."),
					new int[3] { 2000, 3000, 4000 }, BuildingLocation.Settlement,
					new Tuple<BuildingEffectEnum, float, float, float>[]
					{
						new Tuple<BuildingEffectEnum, float, float, float>(
							BuildingEffectEnum.Militia,
							TweaksMCMSettings.Instance.TownMilitiaBarracksAmountLevel1,
							TweaksMCMSettings.Instance.TownMilitiaBarracksAmountLevel2,
							TweaksMCMSettings.Instance.TownMilitiaBarracksAmountLevel3)
					});
			}
			#endregion
		}

		private static bool Prepare()
		{
			if (TweaksMCMSettings.Instance == null)
			{ return false; }
			return TweaksMCMSettings.Instance.CastleGranaryBonusEnabled || TweaksMCMSettings.Instance.CastleGardensBonusEnabled ||
				TweaksMCMSettings.Instance.CastleTrainingFieldsBonusEnabled || TweaksMCMSettings.Instance.CastleMilitiaBarracksBonusEnabled ||
				TweaksMCMSettings.Instance.TownGranaryBonusEnabled || TweaksMCMSettings.Instance.TownOrchardsBonusEnabled ||
				TweaksMCMSettings.Instance.TownTrainingFieldsBonusEnabled || TweaksMCMSettings.Instance.TownMilitiaBarracksBonusEnabled;
		}
	}
}
