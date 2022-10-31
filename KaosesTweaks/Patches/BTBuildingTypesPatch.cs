using HarmonyLib;
using KaosesTweaks.Settings;
using System;
using TaleWorlds.CampaignSystem.Settlements.Buildings;
using TaleWorlds.Localization;

namespace KaosesTweaks.Patches
{
    [HarmonyPatch(typeof(DefaultBuildingTypes), "InitializeAll")]
    class BTBuildingTypesPatch
    {
        static void Postfix(BuildingType ____buildingCastleTrainingFields, BuildingType ____buildingCastleGranary, BuildingType ____buildingCastleGardens,
            BuildingType ____buildingCastleMilitiaBarracks, BuildingType ____buildingSettlementTrainingFields, BuildingType ____buildingSettlementGranary,
            BuildingType ____buildingSettlementOrchard, BuildingType ____buildingSettlementMilitiaBarracks)
        {
            if (KaosesMCMSettings.Instance is null) { return; }
            //Castle
            #region Training Fields
            if (KaosesMCMSettings.Instance.CastleTrainingFieldsBonusEnabled)
            {
                ____buildingCastleTrainingFields?.Initialize(new TextObject("{=BkTiRPT4}Training Fields"),
                    new TextObject("{=otWlERkc}A field for military drills that increase the daily experience gain of all garrisoned units."),
                    new int[3] { 500, 1000, 1500 }, BuildingLocation.Castle,
                    new Tuple<BuildingEffectEnum, float, float, float>[]
                    {
                        new Tuple<BuildingEffectEnum, float, float, float>(
                            BuildingEffectEnum.Experience,
                            KaosesMCMSettings.Instance.CastleTrainingFieldsXpAmountLevel1,
                            KaosesMCMSettings.Instance.CastleTrainingFieldsXpAmountLevel2,
                            KaosesMCMSettings.Instance.CastleTrainingFieldsXpAmountLevel3
                        )
                    });
            }
            #endregion
            #region Granary
            if (KaosesMCMSettings.Instance.CastleGranaryBonusEnabled)
            {
                ____buildingCastleGranary?.Initialize(new TextObject("{=PstO2f5I}Granary"),
                    new TextObject("{=iazij7fO}Keeps stockpiles of food so that the settlement has more food supply. Each level increases the local food supply."),
                    new int[3] { 1000, 1500, 2000 }, BuildingLocation.Castle,
                    new Tuple<BuildingEffectEnum, float, float, float>[]
                    {
                        new Tuple<BuildingEffectEnum, float, float, float>(
                            BuildingEffectEnum.Foodstock,
                            KaosesMCMSettings.Instance.CastleGranaryStorageAmountLevel1,
                            KaosesMCMSettings.Instance.CastleGranaryStorageAmountLevel2,
                            KaosesMCMSettings.Instance.CastleGranaryStorageAmountLevel3
                        )
                    });
            }
            #endregion
            #region Gardens
            if (KaosesMCMSettings.Instance.CastleGardensBonusEnabled)
            {
                ____buildingCastleGardens?.Initialize(new TextObject("{=yT6XN4Mr}Gardens"),
                    new TextObject("{=ZCLVOXgM}Fruit trees and vegetable gardens outside the walls provide food as long as there is no siege.", null),
                    new int[] { 500, 750, 1000 }, BuildingLocation.Castle,
                    new Tuple<BuildingEffectEnum, float, float, float>[]
                    {
                        new Tuple<BuildingEffectEnum, float, float, float>(
                            BuildingEffectEnum.FoodProduction,
                            KaosesMCMSettings.Instance.CastleGardensFoodProductionAmountLevel1,
                            KaosesMCMSettings.Instance.CastleGardensFoodProductionAmountLevel2,
                            KaosesMCMSettings.Instance.CastleGardensFoodProductionAmountLevel3
                        )
                    });
            }
            #endregion
            #region Militia Barracks
            if (KaosesMCMSettings.Instance.CastleMilitiaBarracksBonusEnabled)
            {
                ____buildingCastleMilitiaBarracks?.Initialize(new TextObject("{=l91xAgmU}Militia Grounds"),
                    new TextObject("{=YRrx8bAK}Provides battle training for citizens and recruit them into militia, each level increases daily militia recruitment."),
                    new int[3] { 500, 750, 1000 }, BuildingLocation.Castle,
                    new Tuple<BuildingEffectEnum, float, float, float>[]
                    {
                        new Tuple<BuildingEffectEnum, float, float, float>(
                            BuildingEffectEnum.Militia,
                            KaosesMCMSettings.Instance.CastleMilitiaBarracksAmountLevel1,
                            KaosesMCMSettings.Instance.CastleMilitiaBarracksAmountLevel2,
                            KaosesMCMSettings.Instance.CastleMilitiaBarracksAmountLevel3
                        )
                    });
            }
            #endregion

            //Town
            #region Training Fields
            if (KaosesMCMSettings.Instance.TownTrainingFieldsBonusEnabled)
            {
                ____buildingSettlementTrainingFields?.Initialize(new TextObject("{=BkTiRPT4}Training Fields"),
                    new TextObject("{=otWlERkc}A field for military drills that increase the daily experience gain of all garrisoned units."),
                    new int[3] { 2000, 3000, 4000 }, BuildingLocation.Settlement,
                    new Tuple<BuildingEffectEnum, float, float, float>[]
                    {
                        new Tuple<BuildingEffectEnum, float, float, float>(
                        BuildingEffectEnum.Experience,
                        KaosesMCMSettings.Instance.TownTrainingFieldsXpAmountLevel1,
                        KaosesMCMSettings.Instance.TownTrainingFieldsXpAmountLevel2,
                        KaosesMCMSettings.Instance.TownTrainingFieldsXpAmountLevel3)
                    });
            }
            #endregion
            #region Granary
            if (KaosesMCMSettings.Instance.TownGranaryBonusEnabled)
            {
                ____buildingSettlementGranary?.Initialize(new TextObject("{=PstO2f5I}Granary"),
                    new TextObject("{=aK23T43P}Keeps stockpiles of food so that the settlement has more food supply. Each level increases the local food supply."),
                    new int[3] { 1000, 1500, 2000 }, BuildingLocation.Settlement,
                    new Tuple<BuildingEffectEnum, float, float, float>[]
                    {
                        new Tuple<BuildingEffectEnum,float,float,float>(
                            BuildingEffectEnum.Foodstock,
                            KaosesMCMSettings.Instance.TownGranaryStorageAmountLevel1,
                            KaosesMCMSettings.Instance.TownGranaryStorageAmountLevel2,
                            KaosesMCMSettings.Instance.TownGranaryStorageAmountLevel3)
                    });
            }
            #endregion
            #region Orchards
            if (KaosesMCMSettings.Instance.TownOrchardsBonusEnabled)
            {
                ____buildingSettlementOrchard?.Initialize(new TextObject("{=AkbiPIij}Orchards"),
                    new TextObject("{=ZCLVOXgM}Fruit trees and vegetable gardens outside the walls provide food as long as there is no siege."),
                    new int[3] { 2000, 3000, 4000 }, BuildingLocation.Settlement,
                    new Tuple<BuildingEffectEnum, float, float, float>[]
                    {
                    new Tuple<BuildingEffectEnum,float,float,float>(
                        BuildingEffectEnum.FoodProduction,
                        KaosesMCMSettings.Instance.TownOrchardsFoodProductionAmountLevel1,
                        KaosesMCMSettings.Instance.TownOrchardsFoodProductionAmountLevel2,
                        KaosesMCMSettings.Instance.TownOrchardsFoodProductionAmountLevel3)
                    });
            }
            #endregion
            #region Militia Barracks
            if (KaosesMCMSettings.Instance.TownMilitiaBarracksBonusEnabled)
            {
                ____buildingSettlementMilitiaBarracks?.Initialize(new TextObject("{=l91xAgmU}Militia Grounds"),
                    new TextObject("{=RliyRJKl}Provides battle training for citizens and recruit them into militia. Increases daily militia recruitment."),
                    new int[3] { 2000, 3000, 4000 }, BuildingLocation.Settlement,
                    new Tuple<BuildingEffectEnum, float, float, float>[]
                    {
                        new Tuple<BuildingEffectEnum, float, float, float>(
                            BuildingEffectEnum.Militia,
                            KaosesMCMSettings.Instance.TownMilitiaBarracksAmountLevel1,
                            KaosesMCMSettings.Instance.TownMilitiaBarracksAmountLevel2,
                            KaosesMCMSettings.Instance.TownMilitiaBarracksAmountLevel3)
                    });
            }
            #endregion
        }

        static bool Prepare()
        {
            if (KaosesMCMSettings.Instance == null) { return false; }
            return KaosesMCMSettings.Instance.CastleGranaryBonusEnabled || KaosesMCMSettings.Instance.CastleGardensBonusEnabled ||
                KaosesMCMSettings.Instance.CastleTrainingFieldsBonusEnabled || KaosesMCMSettings.Instance.CastleMilitiaBarracksBonusEnabled ||
                KaosesMCMSettings.Instance.TownGranaryBonusEnabled || KaosesMCMSettings.Instance.TownOrchardsBonusEnabled ||
                KaosesMCMSettings.Instance.TownTrainingFieldsBonusEnabled || KaosesMCMSettings.Instance.TownMilitiaBarracksBonusEnabled;
        }
    }
}
