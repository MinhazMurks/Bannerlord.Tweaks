﻿using Bannerlord.BUTR.Shared.Helpers;
using MCM.Abstractions.Attributes;
using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Dropdown;
using MCM.Abstractions.Settings.Base;
using MCM.Abstractions.Settings.Base.Global;
using System;
//using MCM.Abstractions.Settings.Base.PerSave;
using System.Collections.Generic;
using TaleWorlds.Localization;

namespace KaosesTweaks.Settings
{
    //public class MCMSettings : AttributePerSaveSettings<MCMSettings>, ISettingsProviderInterface
    //public class MCMSettings : AttributeGlobalSettings<MCMSettings>, ISettingsProviderInterface 
    public class MCMSettings : AttributeGlobalSettings<MCMSettings>, ISettingsProviderInterface
    {
        public override string Id => Statics.InstanceID;

        // Build mod display name with name and version form the project properties version
#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
        string modName = Statics.DisplayName;
        public override string DisplayName => TextObjectHelper.Create("{=testModDisplayName}" + modName + " {VERSION}", new Dictionary<string, TextObject>()
        {
            { "VERSION", TextObjectHelper.Create(typeof(MCMSettings).Assembly.GetName().Version?.ToString(3) ?? "")! }
        })!.ToString();
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.

        public override string FolderName => Statics.ModuleFolder;
        public override string FormatType => Statics.FormatType;

        //[SettingPropertyBool("{=debug}Debug", RequireRestart = false, HintText = "{=debug_desc}Displays mod developer debug information and logs them to the file")]
        public bool Debug { get; set; } = false;

        //[SettingPropertyBool("{=debuglog}Log to file", RequireRestart = false, HintText = "{=debuglog_desc}Log information messages to the log file as well as errors and debug")]
        public bool LogToFile { get; set; } = false;

        public bool LoadMCMConfigFile { get; set; } = false;
        public string ModDisplayName { get { return DisplayName; } }



        ///~ Mod Specific settings 


        [SettingPropertyBool("Item Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying Item weight and price.")]
        [SettingPropertyGroup("Items")]
        public bool MCMItemModifiers { get; set; } = true;

        #region Items

        [SettingPropertyBool("BodyArmor Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying BodyArmor weight and price.")]
        [SettingPropertyGroup("Items/BodyArmor")]
        public bool MCMBodyArmorModifiers { get; set; } = true;

        #region Item BodyArmor
        [SettingPropertyBool("BodyArmor Weight", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying BodyArmor weight.")]
        [SettingPropertyGroup("Items/BodyArmor/weight")]
        public bool BodyArmorWeightModifiers { get; set; } = true;


        [SettingPropertyFloatingInteger("Weight Tier1 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply BodyArmor Tier1 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/BodyArmor/weight")]
        public float BodyArmorTier1WeightMultiplier { get; set; } = 0.5f;

        [SettingPropertyFloatingInteger("Weight Tier2 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply BodyArmor Tier2 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/BodyArmor/weight")]
        public float BodyArmorTier2WeightMultiplier { get; set; } = 0.5f;

        [SettingPropertyFloatingInteger("Weight Tier3 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply BodyArmor Tier3 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/BodyArmor/weight")]
        public float BodyArmorTier3WeightMultiplier { get; set; } = 0.5f;

        [SettingPropertyFloatingInteger("Weight Tier4 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply BodyArmor Tier4 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/BodyArmor/weight")]
        public float BodyArmorTier4WeightMultiplier { get; set; } = 0.5f;

        [SettingPropertyFloatingInteger("Weight Tier5 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply BodyArmor Tier5 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/BodyArmor/weight")]
        public float BodyArmorTier5WeightMultiplier { get; set; } = 0.5f;

        [SettingPropertyFloatingInteger("Weight Tier6 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply BodyArmor Tier6 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/BodyArmor/weight")]
        public float BodyArmorTier6WeightMultiplier { get; set; } = 0.5f;

        [SettingPropertyBool("BodyArmor Price", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying BodyArmor price.")]
        [SettingPropertyGroup("Items/BodyArmor/Price")]
        public bool BodyArmorValueModifiers { get; set; } = true;


        [SettingPropertyFloatingInteger("Price Tier1 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply BodyArmor Tier1 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/BodyArmor/Price")]
        public float BodyArmorTier1PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier2 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply BodyArmor Tier2 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/BodyArmor/Price")]
        public float BodyArmorTier2PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier3 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply BodyArmor Tier3 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/BodyArmor/Price")]
        public float BodyArmorTier3PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier4 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply BodyArmor Tier4 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/BodyArmor/Price")]
        public float BodyArmorTier4PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier5 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply BodyArmor Tier5 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/BodyArmor/Price")]
        public float BodyArmorTier5PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier6 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply BodyArmor Tier6 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/BodyArmor/Price")]
        public float BodyArmorTier6PriceMultiplier { get; set; } = 1.0f;

        #endregion Item BodyArmor

        [SettingPropertyBool("Bow Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying Bow weight and price.")]
        [SettingPropertyGroup("Items/Bow")]
        public bool MCMBowModifiers { get; set; } = true;

        #region Item Bow
        [SettingPropertyBool("Bow Weight", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying Bow weight.")]
        [SettingPropertyGroup("Items/Bow/weight")]
        public bool BowWeightModifiers { get; set; } = true;

        [SettingPropertyFloatingInteger("Weight Tier1 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Bow Tier1 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Bow/weight")]
        public float BowTier1WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier2 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Bow Tier2 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Bow/weight")]
        public float BowTier2WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier3 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Bow Tier3 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Bow/weight")]
        public float BowTier3WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier4 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Bow Tier4 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Bow/weight")]
        public float BowTier4WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier5 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Bow Tier5 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Bow/weight")]
        public float BowTier5WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier6 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Bow Tier6 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Bow/weight")]
        public float BowTier6WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyBool("Bow Price", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying Bow price.")]
        [SettingPropertyGroup("Items/Bow/Price")]
        public bool BowValueModifiers { get; set; } = true;

        [SettingPropertyFloatingInteger("Price Tier1 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Bow Tier1 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Bow/Price")]
        public float BowTier1PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier2 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Bow Tier2 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Bow/Price")]
        public float BowTier2PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier3 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Bow Tier3 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Bow/Price")]
        public float BowTier3PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier4 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Bow Tier4 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Bow/Price")]
        public float BowTier4PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier5 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Bow Tier5 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Bow/Price")]
        public float BowTier5PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier6 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Bow Tier6 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Bow/Price")]
        public float BowTier6PriceMultiplier { get; set; } = 1.0f;
        #endregion Item Bow



        [SettingPropertyBool("Cape Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying Cape weight and price.")]
        [SettingPropertyGroup("Items/Cape")]
        public bool MCMCapeModifiers { get; set; } = true;

        #region Item Cape
        [SettingPropertyBool("Cape Weight", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying Cape weight.")]
        [SettingPropertyGroup("Items/Cape/weight")]
        public bool CapeWeightModifiers { get; set; } = true;

        [SettingPropertyFloatingInteger("Weight Tier1 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Cape Tier1 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Cape/weight")]
        public float CapeTier1WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier2 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Cape Tier2 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Cape/weight")]
        public float CapeTier2WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier3 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Cape Tier3 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Cape/weight")]
        public float CapeTier3WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier4 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Cape Tier4 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Cape/weight")]
        public float CapeTier4WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier5 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Cape Tier5 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Cape/weight")]
        public float CapeTier5WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier6 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Cape Tier6 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Cape/weight")]
        public float CapeTier6WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyBool("Cape Price", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying Cape price.")]
        [SettingPropertyGroup("Items/Cape/Price")]
        public bool CapeValueModifiers { get; set; } = true;

        [SettingPropertyFloatingInteger("Price Tier1 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Cape Tier1 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Cape/Price")]
        public float CapeTier1PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier2 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Cape Tier2 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Cape/Price")]
        public float CapeTier2PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier3 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Cape Tier3 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Cape/Price")]
        public float CapeTier3PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier4 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Cape Tier4 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Cape/Price")]
        public float CapeTier4PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier5 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Cape Tier5 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Cape/Price")]
        public float CapeTier5PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier6 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Cape Tier6 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Cape/Price")]
        public float CapeTier6PriceMultiplier { get; set; } = 1.0f;
        #endregion Item Cape

        [SettingPropertyBool("ChestArmor Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying ChestArmor weight and price.")]
        [SettingPropertyGroup("Items/ChestArmor")]
        public bool MCMChestArmorModifiers { get; set; } = true;

        #region Item ChestArmor
        [SettingPropertyBool("ChestArmor Weight", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying ChestArmor weight.")]
        [SettingPropertyGroup("Items/ChestArmor/weight")]
        public bool ChestArmorWeightModifiers { get; set; } = true;

        [SettingPropertyFloatingInteger("Weight Tier1 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply ChestArmor Tier1 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/ChestArmor/weight")]
        public float ChestArmorTier1WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier2 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply ChestArmor Tier2 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/ChestArmor/weight")]
        public float ChestArmorTier2WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier3 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply ChestArmor Tier3 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/ChestArmor/weight")]
        public float ChestArmorTier3WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier4 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply ChestArmor Tier4 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/ChestArmor/weight")]
        public float ChestArmorTier4WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier5 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply ChestArmor Tier5 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/ChestArmor/weight")]
        public float ChestArmorTier5WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier6 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply ChestArmor Tier6 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/ChestArmor/weight")]
        public float ChestArmorTier6WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyBool("ChestArmor Price", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying ChestArmor price.")]
        [SettingPropertyGroup("Items/ChestArmor/Price")]
        public bool ChestArmorValueModifiers { get; set; } = true;

        [SettingPropertyFloatingInteger("Price Tier1 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply ChestArmor Tier1 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/ChestArmor/Price")]
        public float ChestArmorTier1PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier2 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply ChestArmor Tier2 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/ChestArmor/Price")]
        public float ChestArmorTier2PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier3 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply ChestArmor Tier3 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/ChestArmor/Price")]
        public float ChestArmorTier3PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier4 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply ChestArmor Tier4 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/ChestArmor/Price")]
        public float ChestArmorTier4PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier5 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply ChestArmor Tier5 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/ChestArmor/Price")]
        public float ChestArmorTier5PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier6 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply ChestArmor Tier6 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/ChestArmor/Price")]
        public float ChestArmorTier6PriceMultiplier { get; set; } = 1.0f;
        #endregion Item ChestArmor

        [SettingPropertyBool("Crossbow Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying Crossbow weight and price.")]
        [SettingPropertyGroup("Items/Crossbow")]
        public bool MCMCrossbowModifiers { get; set; } = true;

        #region Item Crossbow
        [SettingPropertyBool("Crossbow Weight", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying Crossbow weight.")]
        [SettingPropertyGroup("Items/Crossbow/weight")]
        public bool CrossbowWeightModifiers { get; set; } = true;

        [SettingPropertyFloatingInteger("Weight Tier1 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Crossbow Tier1 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Crossbow/weight")]
        public float CrossbowTier1WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier2 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Crossbow Tier2 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Crossbow/weight")]
        public float CrossbowTier2WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier3 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Crossbow Tier3 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Crossbow/weight")]
        public float CrossbowTier3WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier4 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Crossbow Tier4 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Crossbow/weight")]
        public float CrossbowTier4WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier5 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Crossbow Tier5 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Crossbow/weight")]
        public float CrossbowTier5WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier6 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Crossbow Tier6 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Crossbow/weight")]
        public float CrossbowTier6WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyBool("Crossbow Price", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying Crossbow price.")]
        [SettingPropertyGroup("Items/Crossbow/Price")]
        public bool CrossbowValueModifiers { get; set; } = true;

        [SettingPropertyFloatingInteger("Price Tier1 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Crossbow Tier1 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Crossbow/Price")]
        public float CrossbowTier1PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier2 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Crossbow Tier2 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Crossbow/Price")]
        public float CrossbowTier2PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier3 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Crossbow Tier3 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Crossbow/Price")]
        public float CrossbowTier3PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier4 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Crossbow Tier4 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Crossbow/Price")]
        public float CrossbowTier4PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier5 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Crossbow Tier5 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Crossbow/Price")]
        public float CrossbowTier5PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier6 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Crossbow Tier6 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Crossbow/Price")]
        public float CrossbowTier6PriceMultiplier { get; set; } = 1.0f;
        #endregion Item Crossbow


        [SettingPropertyBool("HandArmor Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying HandArmor weight and price.")]
        [SettingPropertyGroup("Items/HandArmor")]
        public bool MCMHandArmorModifiers { get; set; } = true;

        #region Item HandArmor
        [SettingPropertyBool("HandArmor Weight", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying HandArmor weight.")]
        [SettingPropertyGroup("Items/HandArmor/weight")]
        public bool HandArmorWeightModifiers { get; set; } = true;

        [SettingPropertyFloatingInteger("Weight Tier1 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply HandArmor Tier1 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/HandArmor/weight")]
        public float HandArmorTier1WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier2 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply HandArmor Tier2 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/HandArmor/weight")]
        public float HandArmorTier2WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier3 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply HandArmor Tier3 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/HandArmor/weight")]
        public float HandArmorTier3WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier4 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply HandArmor Tier4 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/HandArmor/weight")]
        public float HandArmorTier4WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier5 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply HandArmor Tier5 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/HandArmor/weight")]
        public float HandArmorTier5WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier6 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply HandArmor Tier6 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/HandArmor/weight")]
        public float HandArmorTier6WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyBool("HandArmor Price", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying HandArmor price.")]
        [SettingPropertyGroup("Items/HandArmor/Price")]
        public bool HandArmorValueModifiers { get; set; } = true;

        [SettingPropertyFloatingInteger("Price Tier1 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply HandArmor Tier1 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/HandArmor/Price")]
        public float HandArmorTier1PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier2 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply HandArmor Tier2 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/HandArmor/Price")]
        public float HandArmorTier2PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier3 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply HandArmor Tier3 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/HandArmor/Price")]
        public float HandArmorTier3PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier4 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply HandArmor Tier4 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/HandArmor/Price")]
        public float HandArmorTier4PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier5 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply HandArmor Tier5 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/HandArmor/Price")]
        public float HandArmorTier5PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier6 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply HandArmor Tier6 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/HandArmor/Price")]
        public float HandArmorTier6PriceMultiplier { get; set; } = 1.0f;
        #endregion Item HandArmor


        [SettingPropertyBool("HeadArmor Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying HeadArmor weight and price.")]
        [SettingPropertyGroup("Items/HeadArmor")]
        public bool MCMHeadArmorModifiers { get; set; } = true;

        #region Item HeadArmor
        [SettingPropertyBool("HeadArmor Weight", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying HeadArmor weight.")]
        [SettingPropertyGroup("Items/HeadArmor/weight")]
        public bool HeadArmorWeightModifiers { get; set; } = true;

        [SettingPropertyFloatingInteger("Weight Tier1 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply HeadArmor Tier1 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/HeadArmor/weight")]
        public float HeadArmorTier1WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier2 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply HeadArmor Tier2 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/HeadArmor/weight")]
        public float HeadArmorTier2WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier3 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply HeadArmor Tier3 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/HeadArmor/weight")]
        public float HeadArmorTier3WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier4 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply HeadArmor Tier4 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/HeadArmor/weight")]
        public float HeadArmorTier4WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier5 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply HeadArmor Tier5 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/HeadArmor/weight")]
        public float HeadArmorTier5WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier6 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply HeadArmor Tier6 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/HeadArmor/weight")]
        public float HeadArmorTier6WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyBool("HeadArmor Price", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying HeadArmor price.")]
        [SettingPropertyGroup("Items/HeadArmor/Price")]
        public bool HeadArmorValueModifiers { get; set; } = true;

        [SettingPropertyFloatingInteger("Price Tier1 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply HeadArmor Tier1 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/HeadArmor/Price")]
        public float HeadArmorTier1PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier2 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply HeadArmor Tier2 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/HeadArmor/Price")]
        public float HeadArmorTier2PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier3 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply HeadArmor Tier3 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/HeadArmor/Price")]
        public float HeadArmorTier3PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier4 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply HeadArmor Tier4 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/HeadArmor/Price")]
        public float HeadArmorTier4PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier5 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply HeadArmor Tier5 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/HeadArmor/Price")]
        public float HeadArmorTier5PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier6 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply HeadArmor Tier6 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/HeadArmor/Price")]
        public float HeadArmorTier6PriceMultiplier { get; set; } = 1.0f;
        #endregion Item HeadArmor


        [SettingPropertyBool("Horse Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying Horse weight and price.")]
        [SettingPropertyGroup("Items/Horse")]
        public bool MCMHorseModifiers { get; set; } = true;

        #region Item Horse
        [SettingPropertyBool("Horse Weight", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying Horse weight.")]
        [SettingPropertyGroup("Items/Horse/weight")]
        public bool HorseWeightModifiers { get; set; } = true;

        [SettingPropertyFloatingInteger("Weight Tier1 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Horse Tier1 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Horse/weight")]
        public float HorseTier1WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier2 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Horse Tier2 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Horse/weight")]
        public float HorseTier2WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier3 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Horse Tier3 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Horse/weight")]
        public float HorseTier3WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier4 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Horse Tier4 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Horse/weight")]
        public float HorseTier4WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier5 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Horse Tier5 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Horse/weight")]
        public float HorseTier5WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier6 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Horse Tier6 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Horse/weight")]
        public float HorseTier6WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyBool("Horse Price", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying Horse price.")]
        [SettingPropertyGroup("Items/Horse/Price")]
        public bool HorseValueModifiers { get; set; } = true;

        [SettingPropertyFloatingInteger("Price Tier1 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Horse Tier1 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Horse/Price")]
        public float HorseTier1PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier2 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Horse Tier2 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Horse/Price")]
        public float HorseTier2PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier3 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Horse Tier3 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Horse/Price")]
        public float HorseTier3PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier4 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Horse Tier4 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Horse/Price")]
        public float HorseTier4PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier5 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Horse Tier5 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Horse/Price")]
        public float HorseTier5PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier6 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Horse Tier6 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Horse/Price")]
        public float HorseTier6PriceMultiplier { get; set; } = 1.0f;
        #endregion Item Horse


        [SettingPropertyBool("HorseHarness Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying HorseHarness weight and price.")]
        [SettingPropertyGroup("Items/HorseHarness")]
        public bool MCMHorseHarnessModifiers { get; set; } = true;

        #region Item HorseHarness
        [SettingPropertyBool("HorseHarness Weight", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying HorseHarness weight.")]
        [SettingPropertyGroup("Items/HorseHarness/weight")]
        public bool HorseHarnessWeightModifiers { get; set; } = true;

        [SettingPropertyFloatingInteger("Weight Tier1 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply HorseHarness Tier1 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/HorseHarness/weight")]
        public float HorseHarnessTier1WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier2 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply HorseHarness Tier2 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/HorseHarness/weight")]
        public float HorseHarnessTier2WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier3 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply HorseHarness Tier3 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/HorseHarness/weight")]
        public float HorseHarnessTier3WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier4 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply HorseHarness Tier4 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/HorseHarness/weight")]
        public float HorseHarnessTier4WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier5 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply HorseHarness Tier5 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/HorseHarness/weight")]
        public float HorseHarnessTier5WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier6 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply HorseHarness Tier6 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/HorseHarness/weight")]
        public float HorseHarnessTier6WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyBool("HorseHarness Price", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying HorseHarness price.")]
        [SettingPropertyGroup("Items/HorseHarness/Price")]
        public bool HorseHarnessValueModifiers { get; set; } = true;

        [SettingPropertyFloatingInteger("Price Tier1 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply HorseHarness Tier1 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/HorseHarness/Price")]
        public float HorseHarnessTier1PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier2 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply HorseHarness Tier2 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/HorseHarness/Price")]
        public float HorseHarnessTier2PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier3 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply HorseHarness Tier3 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/HorseHarness/Price")]
        public float HorseHarnessTier3PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier4 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply HorseHarness Tier4 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/HorseHarness/Price")]
        public float HorseHarnessTier4PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier5 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply HorseHarness Tier5 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/HorseHarness/Price")]
        public float HorseHarnessTier5PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier6 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply HorseHarness Tier6 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/HorseHarness/Price")]
        public float HorseHarnessTier6PriceMultiplier { get; set; } = 1.0f;
        #endregion Item HorseHarness


        [SettingPropertyBool("LegArmor Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying LegArmor weight and price.")]
        [SettingPropertyGroup("Items/LegArmor")]
        public bool MCMLegArmorModifiers { get; set; } = true;

        #region Item LegArmor
        [SettingPropertyBool("LegArmor Weight", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying LegArmor weight.")]
        [SettingPropertyGroup("Items/LegArmor/weight")]
        public bool LegArmorWeightModifiers { get; set; } = true;

        [SettingPropertyFloatingInteger("Weight Tier1 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply LegArmor Tier1 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/LegArmor/weight")]
        public float LegArmorTier1WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier2 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply LegArmor Tier2 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/LegArmor/weight")]
        public float LegArmorTier2WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier3 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply LegArmor Tier3 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/LegArmor/weight")]
        public float LegArmorTier3WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier4 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply LegArmor Tier4 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/LegArmor/weight")]
        public float LegArmorTier4WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier5 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply LegArmor Tier5 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/LegArmor/weight")]
        public float LegArmorTier5WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier6 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply LegArmor Tier6 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/LegArmor/weight")]
        public float LegArmorTier6WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyBool("LegArmor Price", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying LegArmor price.")]
        [SettingPropertyGroup("Items/LegArmor/Price")]
        public bool LegArmorValueModifiers { get; set; } = true;

        [SettingPropertyFloatingInteger("Price Tier1 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply LegArmor Tier1 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/LegArmor/Price")]
        public float LegArmorTier1PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier2 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply LegArmor Tier2 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/LegArmor/Price")]
        public float LegArmorTier2PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier3 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply LegArmor Tier3 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/LegArmor/Price")]
        public float LegArmorTier3PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier4 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply LegArmor Tier4 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/LegArmor/Price")]
        public float LegArmorTier4PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier5 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply LegArmor Tier5 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/LegArmor/Price")]
        public float LegArmorTier5PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier6 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply LegArmor Tier6 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/LegArmor/Price")]
        public float LegArmorTier6PriceMultiplier { get; set; } = 1.0f;
        #endregion Item LegArmor


        [SettingPropertyBool("Musket Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying Musket weight and price.")]
        [SettingPropertyGroup("Items/Musket")]
        public bool MCMMusketModifiers { get; set; } = true;

        #region Item Musket
        [SettingPropertyBool("Musket Weight", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying Musket weight.")]
        [SettingPropertyGroup("Items/Musket/weight")]
        public bool MusketWeightModifiers { get; set; } = true;

        [SettingPropertyFloatingInteger("Weight Tier1 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Musket Tier1 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Musket/weight")]
        public float MusketTier1WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier2 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Musket Tier2 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Musket/weight")]
        public float MusketTier2WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier3 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Musket Tier3 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Musket/weight")]
        public float MusketTier3WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier4 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Musket Tier4 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Musket/weight")]
        public float MusketTier4WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier5 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Musket Tier5 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Musket/weight")]
        public float MusketTier5WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier6 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Musket Tier6 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Musket/weight")]
        public float MusketTier6WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyBool("Musket Price", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying Musket price.")]
        [SettingPropertyGroup("Items/Musket/Price")]
        public bool MusketValueModifiers { get; set; } = true;
        [SettingPropertyFloatingInteger("Price Tier1 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Musket Tier1 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Musket/Price")]
        public float MusketTier1PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier2 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Musket Tier2 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Musket/Price")]
        public float MusketTier2PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier3 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Musket Tier3 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Musket/Price")]
        public float MusketTier3PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier4 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Musket Tier4 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Musket/Price")]
        public float MusketTier4PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier5 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Musket Tier5 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Musket/Price")]
        public float MusketTier5PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier6 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Musket Tier6 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Musket/Price")]
        public float MusketTier6PriceMultiplier { get; set; } = 1.0f;
        #endregion Item Musket


        [SettingPropertyBool("OneHandedWeapon Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying OneHandedWeapon weight and price.")]
        [SettingPropertyGroup("Items/OneHandedWeapon")]
        public bool MCMOneHandedWeaponModifiers { get; set; } = true;

        #region Item OneHandedWeapon
        [SettingPropertyBool("OneHandedWeapon Weight", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying OneHandedWeapon weight.")]
        [SettingPropertyGroup("Items/OneHandedWeapon/weight")]
        public bool OneHandedWeaponWeightModifiers { get; set; } = true;
        [SettingPropertyFloatingInteger("Weight Tier1 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply OneHandedWeapon Tier1 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/OneHandedWeapon/weight")]
        public float OneHandedWeaponTier1WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier2 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply OneHandedWeapon Tier2 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/OneHandedWeapon/weight")]
        public float OneHandedWeaponTier2WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier3 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply OneHandedWeapon Tier3 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/OneHandedWeapon/weight")]
        public float OneHandedWeaponTier3WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier4 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply OneHandedWeapon Tier4 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/OneHandedWeapon/weight")]
        public float OneHandedWeaponTier4WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier5 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply OneHandedWeapon Tier5 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/OneHandedWeapon/weight")]
        public float OneHandedWeaponTier5WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier6 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply OneHandedWeapon Tier6 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/OneHandedWeapon/weight")]
        public float OneHandedWeaponTier6WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyBool("OneHandedWeapon Price", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying OneHandedWeapon price.")]
        [SettingPropertyGroup("Items/OneHandedWeapon/Price")]
        public bool OneHandedWeaponValueModifiers { get; set; } = true;

        [SettingPropertyFloatingInteger("Price Tier1 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply OneHandedWeapon Tier1 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/OneHandedWeapon/Price")]
        public float OneHandedWeaponTier1PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier2 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply OneHandedWeapon Tier2 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/OneHandedWeapon/Price")]
        public float OneHandedWeaponTier2PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier3 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply OneHandedWeapon Tier3 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/OneHandedWeapon/Price")]
        public float OneHandedWeaponTier3PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier4 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply OneHandedWeapon Tier4 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/OneHandedWeapon/Price")]
        public float OneHandedWeaponTier4PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier5 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply OneHandedWeapon Tier5 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/OneHandedWeapon/Price")]
        public float OneHandedWeaponTier5PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier6 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply OneHandedWeapon Tier6 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/OneHandedWeapon/Price")]
        public float OneHandedWeaponTier6PriceMultiplier { get; set; } = 1.0f;
        #endregion Item OneHandedWeapon

        [SettingPropertyBool("Pistol Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying Pistol weight and price.")]
        [SettingPropertyGroup("Items/Pistol")]
        public bool MCMPistolModifiers { get; set; } = true;

        #region Item Pistol
        [SettingPropertyBool("Pistol Weight", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying Pistol weight.")]
        [SettingPropertyGroup("Items/Pistol/weight")]
        public bool PistolWeightModifiers { get; set; } = true;

        [SettingPropertyFloatingInteger("Weight Tier1 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Pistol Tier1 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Pistol/weight")]
        public float PistolTier1WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier2 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Pistol Tier2 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Pistol/weight")]
        public float PistolTier2WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier3 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Pistol Tier3 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Pistol/weight")]
        public float PistolTier3WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier4 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Pistol Tier4 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Pistol/weight")]
        public float PistolTier4WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier5 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Pistol Tier5 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Pistol/weight")]
        public float PistolTier5WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier6 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Pistol Tier6 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Pistol/weight")]
        public float PistolTier6WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyBool("Pistol Price", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying Pistol price.")]
        [SettingPropertyGroup("Items/Pistol/Price")]
        public bool PistolValueModifiers { get; set; } = true;

        [SettingPropertyFloatingInteger("Price Tier1 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Pistol Tier1 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Pistol/Price")]
        public float PistolTier1PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier2 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Pistol Tier2 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Pistol/Price")]
        public float PistolTier2PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier3 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Pistol Tier3 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Pistol/Price")]
        public float PistolTier3PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier4 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Pistol Tier4 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Pistol/Price")]
        public float PistolTier4PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier5 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Pistol Tier5 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Pistol/Price")]
        public float PistolTier5PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier6 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Pistol Tier6 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Pistol/Price")]
        public float PistolTier6PriceMultiplier { get; set; } = 1.0f;
        #endregion Item Pistol

        [SettingPropertyBool("Polearm Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying Polearm weight and price.")]
        [SettingPropertyGroup("Items/Polearm")]
        public bool MCMPolearmModifiers { get; set; } = true;

        #region Item Polearm
        [SettingPropertyBool("Polearm Weight", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying Polearm weight.")]
        [SettingPropertyGroup("Items/Polearm/weight")]
        public bool PolearmWeightModifiers { get; set; } = true;

        [SettingPropertyFloatingInteger("Weight Tier1 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Polearm Tier1 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Polearm/weight")]
        public float PolearmTier1WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier2 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Polearm Tier2 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Polearm/weight")]
        public float PolearmTier2WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier3 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Polearm Tier3 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Polearm/weight")]
        public float PolearmTier3WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier4 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Polearm Tier4 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Polearm/weight")]
        public float PolearmTier4WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier5 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Polearm Tier5 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Polearm/weight")]
        public float PolearmTier5WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier6 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Polearm Tier6 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Polearm/weight")]
        public float PolearmTier6WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyBool("Polearm Price", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying Polearm price.")]
        [SettingPropertyGroup("Items/Polearm/Price")]
        public bool PolearmValueModifiers { get; set; } = true;

        [SettingPropertyFloatingInteger("Price Tier1 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Polearm Tier1 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Polearm/Price")]
        public float PolearmTier1PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier2 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Polearm Tier2 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Polearm/Price")]
        public float PolearmTier2PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier3 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Polearm Tier3 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Polearm/Price")]
        public float PolearmTier3PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier4 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Polearm Tier4 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Polearm/Price")]
        public float PolearmTier4PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier5 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Polearm Tier5 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Polearm/Price")]
        public float PolearmTier5PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier6 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Polearm Tier6 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Polearm/Price")]
        public float PolearmTier6PriceMultiplier { get; set; } = 1.0f;
        #endregion Item Polearm

        [SettingPropertyBool("Shield Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying Shield weight and price.")]
        [SettingPropertyGroup("Items/Shield")]
        public bool MCMShieldModifiers { get; set; } = true;

        #region Item Shield
        [SettingPropertyBool("Shield Weight", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying Shield weight.")]
        [SettingPropertyGroup("Items/Shield/weight")]
        public bool ShieldWeightModifiers { get; set; } = true;

        [SettingPropertyFloatingInteger("Weight Tier1 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Shield Tier1 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Shield/weight")]
        public float ShieldTier1WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier2 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Shield Tier2 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Shield/weight")]
        public float ShieldTier2WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier3 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Shield Tier3 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Shield/weight")]
        public float ShieldTier3WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier4 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Shield Tier4 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Shield/weight")]
        public float ShieldTier4WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier5 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Shield Tier5 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Shield/weight")]
        public float ShieldTier5WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier6 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Shield Tier6 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Shield/weight")]
        public float ShieldTier6WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyBool("Shield Price", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying Shield price.")]
        [SettingPropertyGroup("Items/Shield/Price")]
        public bool ShieldValueModifiers { get; set; } = true;

        [SettingPropertyFloatingInteger("Price Tier1 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Shield Tier1 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Shield/Price")]
        public float ShieldTier1PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier2 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Shield Tier2 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Shield/Price")]
        public float ShieldTier2PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier3 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Shield Tier3 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Shield/Price")]
        public float ShieldTier3PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier4 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Shield Tier4 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Shield/Price")]
        public float ShieldTier4PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier5 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Shield Tier5 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Shield/Price")]
        public float ShieldTier5PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier6 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Shield Tier6 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/Shield/Price")]
        public float ShieldTier6PriceMultiplier { get; set; } = 1.0f;
        #endregion Item Shield


        [SettingPropertyBool("TwoHandedWeapon Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying TwoHandedWeapon weight and price.")]
        [SettingPropertyGroup("Items/TwoHandedWeapon")]
        public bool MCMTwoHandedWeaponModifiers { get; set; } = true;

        #region Item TwoHandedWeapon
        [SettingPropertyBool("TwoHandedWeapon Weight", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying TwoHandedWeapon weight.")]
        [SettingPropertyGroup("Items/TwoHandedWeapon/weight")]
        public bool TwoHandedWeaponWeightModifiers { get; set; } = true;

        [SettingPropertyFloatingInteger("Weight Tier1 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply TwoHandedWeapon Tier1 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/TwoHandedWeapon/weight")]
        public float TwoHandedWeaponTier1WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier2 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply TwoHandedWeapon Tier2 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/TwoHandedWeapon/weight")]
        public float TwoHandedWeaponTier2WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier3 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply TwoHandedWeapon Tier3 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/TwoHandedWeapon/weight")]
        public float TwoHandedWeaponTier3WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier4 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply TwoHandedWeapon Tier4 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/TwoHandedWeapon/weight")]
        public float TwoHandedWeaponTier4WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier5 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply TwoHandedWeapon Tier5 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/TwoHandedWeapon/weight")]
        public float TwoHandedWeaponTier5WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Weight Tier6 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply TwoHandedWeapon Tier6 weight by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/TwoHandedWeapon/weight")]
        public float TwoHandedWeaponTier6WeightMultiplier { get; set; } = 1.0f;

        [SettingPropertyBool("TwoHandedWeapon Price", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying TwoHandedWeapon price.")]
        [SettingPropertyGroup("Items/TwoHandedWeapon/Price")]
        public bool TwoHandedWeaponValueModifiers { get; set; } = true;

        [SettingPropertyFloatingInteger("Price Tier1 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply TwoHandedWeapon Tier1 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/TwoHandedWeapon/Price")]
        public float TwoHandedWeaponTier1PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier2 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply TwoHandedWeapon Tier2 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/TwoHandedWeapon/Price")]
        public float TwoHandedWeaponTier2PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier3 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply TwoHandedWeapon Tier3 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/TwoHandedWeapon/Price")]
        public float TwoHandedWeaponTier3PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier4 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply TwoHandedWeapon Tier4 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/TwoHandedWeapon/Price")]
        public float TwoHandedWeaponTier4PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier5 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply TwoHandedWeapon Tier5 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/TwoHandedWeapon/Price")]
        public float TwoHandedWeaponTier5PriceMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Price Tier6 Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply TwoHandedWeapon Tier6 price by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Items/TwoHandedWeapon/Price")]
        public float TwoHandedWeaponTier6PriceMultiplier { get; set; } = 1.0f;
        #endregion Item TwoHandedWeapon



        #endregion Items


        [SettingPropertyBool("TwoHandedWeapon Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying TwoHandedWeapon weight and price.")]
        [SettingPropertyGroup("Battle Rewards")]
        public bool MCMBattleRewardModifiers { get; set; } = true;
        #region Battle Rewards

        #region  Battle Rewards RelationshipGain

        [SettingPropertyBool("Relationship Gain", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying Relationship gain.")]
        [SettingPropertyGroup("Battle Rewards/RelationShip Gain")]
        public bool BattleRewardsRelationShipGainModifiers { get; set; } = true;

        [SettingPropertyFloatingInteger("Relationship gain Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Relationship gain  by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Battle Rewards/RelationShip Gain")]
        public float BattleRewardsRelationShipGainMultiplier { get; set; } = 1.0f;
        #endregion  Battle Rewards RelationshipGain

        #region  Battle Rewards Renown
        [SettingPropertyBool("Renown Gain", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying Renown gain.")]
        [SettingPropertyGroup("Battle Rewards/Renown Gain")]
        public bool BattleRewardsRenownGainModifiers { get; set; } = true;

        [SettingPropertyFloatingInteger("Renown gain Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Renown gain  by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Battle Rewards/Renown Gain")]
        public float BattleRewardsRenownGainMultiplier { get; set; } = 1.0f;
        #endregion  Battle Rewards Renown

        #region  Battle Rewards InfluenceGain
        [SettingPropertyBool("Influence Gain", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying Influence gain.")]
        [SettingPropertyGroup("Battle Rewards/Influence Gain")]
        public bool BattleRewardsInfluenceGainModifiers { get; set; } = true;

        [SettingPropertyFloatingInteger("Influence gain Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Influence gain  by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Battle Rewards/Influence Gain")]
        public float BattleRewardsInfluenceGainMultiplier { get; set; } = 1.0f;
        #endregion  Battle Rewards InfluenceGain

        #region  Battle Rewards MoraleGain
        [SettingPropertyBool("Morale Gain", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying Morale gain.")]
        [SettingPropertyGroup("Battle Rewards/Morale Gain")]
        public bool BattleRewardsMoraleGainModifiers { get; set; } = true;

        [SettingPropertyFloatingInteger("Morale gain Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Morale gain  by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Battle Rewards/Morale Gain")]
        public float BattleRewardsMoraleGainMultiplier { get; set; } = 1.0f;
        #endregion  Battle Rewards MoraleGain

        #region  Battle Rewards GoldLossAfterDefeat
        [SettingPropertyBool("Gold Loss", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying Gold Loss on defeat .")]
        [SettingPropertyGroup("Battle Rewards/Gold Loss")]
        public bool BattleRewardsGoldLossModifiers { get; set; } = true;

        [SettingPropertyFloatingInteger("Gold Loss Multiplier", 0.1f, 10.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Gold Loss on defeat by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Battle Rewards/Gold Loss")]
        public float BattleRewardsGoldLossMultiplier { get; set; } = 1.0f;
        #endregion  Battle Rewards GoldLossAfterDefeat


        #endregion  Battle Rewards


        [SettingPropertyBool("Clan Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying clan variables.")]
        [SettingPropertyGroup("Clan")]
        public bool MCMClanModifiers { get; set; } = false;
        #region Clan


        #region Clan Party Limit
        [SettingPropertyBool("Bonus Party Limit", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables additional Party limit per clan tier .")]
        [SettingPropertyGroup("Clan/Party Limit")]
        public bool ClanAdditionalPartyLimitForTierModifiers { get; set; } = false;

        [SettingPropertyInteger("Bonus Party Limit", 0, 10, "0 Parties", Order = 0, RequireRestart = false, 
            HintText = "Additional Party limit per clan tier [Native: 0].")]
        [SettingPropertyGroup("Clan/Party Limit")]
        public int ClanAdditionalPartyLimitForTierValue { get; set; } = 0;
        #endregion Clan Party Limit


        #region Clan Companion Limit
        [SettingPropertyBool("Bonus Companion Limit", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables additional Companion limit per clan tier .")]
        [SettingPropertyGroup("Clan/Companion Limit")]
        public bool ClanAdditionaCompanionLimitForTierModifiers { get; set; } = false;

        [SettingPropertyInteger("Bonus Companion Limit", 0, 10, "0 Companions", Order = 0, RequireRestart = false,
            HintText = "Additional Companion limit per clan tier [Native: 0].")]
        [SettingPropertyGroup("Clan/Companion Limit")]
        public int ClanAdditionalCompanionLimitForTierValue { get; set; } = 0;
        #endregion Clan Companion Limit


        #endregion Clan


        [SettingPropertyBool("WorkShop Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying WorkShop variables.")]
        [SettingPropertyGroup("WorkShop")]
        public bool MCMWorkShopModifiers { get; set; } = false;
        #region Workshops

        #region Workshops Save Bankruptcy

        [SettingPropertyBool("Bankruptcy Modifier", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables Bankruptcy Modifiers.")]
        [SettingPropertyGroup("WorkShop/Bankruptcy")]
        public bool WorkShopBankruptcyModifiers { get; set; } = false;

        [SettingPropertyInteger("Days to save", 1, 10, "0 Days", Order = 0, RequireRestart = false,
            HintText = "Days For Player to Save Workshop From Bankruptcy [Native : 3].")]
        [SettingPropertyGroup("WorkShop/Bankruptcy")]
        public int WorkShopBankruptcyValue { get; set; } = 3;
        #endregion Save

        #region WorkShop Limit Player

        [SettingPropertyBool("Enable Limit Modifier", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enable Limit Modifier for workshop limit for player.")]
        [SettingPropertyGroup("WorkShop/Max Limit")]
        public bool WorkShopMaxWorkshopCountForPlayerModifiers { get; set; } = false;

        [SettingPropertyInteger("Max number of Limit", 1, 10, Order = 0, RequireRestart = false,
            HintText = "Maximum number of workshops for player native is the multiplier number times clan tier [Native : 1].")]
        [SettingPropertyGroup("WorkShop/Max Limit")]
        public int WorkShopMaxWorkshopCountForPlayerValue { get; set; } = 1;

        #endregion WorkShop Limit Player

        #endregion Workshops



        [SettingPropertyBool("Character Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying Character variables.")]
        [SettingPropertyGroup("Character")]
        public bool MCMCharacterDevlopmentModifiers { get; set; } = false;
        #region Character Development

        #region Character Development LevelsPerAttributePoint
        [SettingPropertyBool("Levels Per Attribute modifier", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enable Number of Levels Per Attribute modifier")]
        [SettingPropertyGroup("Character/Attributes/Level")]
        public bool CharacterLevelsPerAttributeModifiers { get; set; } = false;

        [SettingPropertyInteger("Levels for Attribute", 1, 20, "0 Levels", Order = 0, RequireRestart = false,
            HintText = "Number of Levels to acquire an attribute point [Native : 4].")]
        [SettingPropertyGroup("Character/Attributes/Level")]
        public int CharacterLevelsPerAttributeValue { get; set; } = 4;
        #endregion Character Development LevelsPerAttributePoint


        #region Character Development FocusPointsPerLevel
        [SettingPropertyBool("Focus points modifier", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables Focus points per level modifier.")]
        [SettingPropertyGroup("Character/Focus/Level")]
        public bool CharacterFocusPerLevelModifiers { get; set; } = false;

        [SettingPropertyInteger("Focus points per level", 1, 20, "0 Focus", Order = 0, RequireRestart = false,
            HintText = "Number of Focus points per level [Native : 1].")]
        [SettingPropertyGroup("Character/Focus/Level")]
        public int CharacterFocusPerLevelValue { get; set; } = 1;
        #endregion Character Development FocusPointsPerLevel

        #endregion Character Development




        [SettingPropertyBool("Pregnancy Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying Pregnancy variables.")]
        [SettingPropertyGroup("Pregnancy")]
        public bool MCMPregnancyModifiers { get; set; } = false;
        #region Pregnancy

        #region Pregnancy Duration
        [SettingPropertyBool("Pregnancy Duration Modifier", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables Pregnancy Duration modifier.")]
        [SettingPropertyGroup("Pregnancy/Duration")]
        public bool PregnancyDurationModifiers { get; set; } = false;

        [SettingPropertyInteger("Pregnancy Duration", 1, 300, "0 Days", Order = 0, RequireRestart = false,
            HintText = "Pregnancy Duration in days [Native : 36].")]
        [SettingPropertyGroup("Pregnancy/Duration")]
        public int PregnancyDurationValue { get; set; } = 36;
        #endregion Pregnancy Duration

        #region Pregnancy MortalityProbabilityInLabor
        [SettingPropertyBool("Labor Mortality Chance Modifier", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables Mortality In Labor Chance modifier.")]
        [SettingPropertyGroup("Pregnancy/Labor Mortality")]
        public bool PregnancyLaborMortalityChanceModifiers { get; set; } = false;

        [SettingPropertyFloatingInteger("Labor Mortality Chance", 0.000f, 1.000f, Order = 2, RequireRestart = false, 
            HintText = "Mortality In Labor Chance [Native : 0.015].")]
        [SettingPropertyGroup("Pregnancy/Labor Mortality")]
        public float PregnancyLaborMortalityChanceValue { get; set; } = 0.015f;
        #endregion Pregnancy MortalityProbabilityInLabor

        #region Pregnancy StillbirthProbability
        [SettingPropertyBool("Stillbirth Chance Modifier", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables Stillbirth Chance modifier.")]
        [SettingPropertyGroup("Pregnancy/Stillbirth")]
        public bool PregnancyStillbirthChanceModifiers { get; set; } = false;

        [SettingPropertyFloatingInteger("Stillbirth Chance", 0.01f, 1.00f, Order = 2, RequireRestart = false,
            HintText = "Stillbirth Chance  [Native : 0.01].")]
        [SettingPropertyGroup("Pregnancy/Stillbirth")]
        public float PregnancyStillbirthChanceValue { get; set; } = 0.01f;
        #endregion PregnancyStillbirthProbability

        #region Pregnancy DeliveringFemaleOffspringProbability
        [SettingPropertyBool("Female Child Chance Modifier", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables Female Child Chance modifier.")]
        [SettingPropertyGroup("Pregnancy/Female Child")]
        public bool PregnancyFemaleOffspringChanceModifiers { get; set; } = false;

        [SettingPropertyFloatingInteger("Female Child Chance", 0.00f, 1.00f, Order = 2, RequireRestart = false,
            HintText = "Female Child Chance  [Native : 0.51].")]
        [SettingPropertyGroup("Pregnancy/Female Child")]
        public float PregnancyFemaleOffspringChanceValue { get; set; } = 0.51f;
        #endregion PregnancyDeliveringFemaleOffspringProbability

        #region Pregnancy DeliveringTwinsProbability
        [SettingPropertyBool("Twins Chance Modifier", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables Twins Chance modifier.")]
        [SettingPropertyGroup("Pregnancy/Twins")]
        public bool PregnancyTwinsChanceModifiers { get; set; } = false;

        [SettingPropertyFloatingInteger("Twins Chance", 0.00f, 1.00f, Order = 2, RequireRestart = false,//, "#0%"
            HintText = "Twins Chance  [Native : 0.03].")]
        [SettingPropertyGroup("Pregnancy/Twins")]
        public float PregnancyTwinsChanceValue { get; set; } = 0.03f;
        #endregion PregnancyDeliveringTwinsProbability


        #endregion Pregnancy





        [SettingPropertyBool("Smithing Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enables modifying Smithing variables.")]
        [SettingPropertyGroup("Smithing")]
        public bool MCMSmithingModifiers { get; set; } = false;
        #region Smithing


        #region  Smithing RefiningXp
        [SettingPropertyBool("Refining Xp", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enable Refining Xp multiplier.")]
        [SettingPropertyGroup("Smithing/XP/Refining")]
        public bool SmithingRefiningXpModifiers { get; set; } = false;

        [SettingPropertyFloatingInteger("Refining Xp", 0.1f, 5.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Refining Xp by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Smithing/XP/Refining")]
        public float SmithingRefiningXpValue { get; set; } = 1.0f;
        #endregion  Smithing RefiningXp 

        #region  Smithing  XpForSmelting
        [SettingPropertyBool("Smelting Xp", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enable Smelting Xp multiplier.")]
        [SettingPropertyGroup("Smithing/XP/Smelting")]
        public bool SmithingSmeltingXpModifiers { get; set; } = false;

        [SettingPropertyFloatingInteger("Smelting Xp", 0.1f, 5.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Smelting Xp by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Smithing/XP/Smelting")]
        public float SmithingSmeltingXpValue { get; set; } = 1.0f;
        #endregion  Smithing XpForSmelting

        #region  Smithing XpForSmithing
        [SettingPropertyBool("Smithing Xp", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enable Smithing Xp multiplier.")]
        [SettingPropertyGroup("Smithing/XP/Smithing")]
        public bool SmithingSmithingXpModifiers { get; set; } = false;

        [SettingPropertyFloatingInteger("Smithing Xp", 0.1f, 5.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply Smithing Xp by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Smithing/XP/Smithing")]
        public float SmithingSmithingXpValue { get; set; } = 1.0f;
        #endregion  Smithing XpForSmithing

        #region  Smithing Energy Disable
        [SettingPropertyBool("Energy Disable", Order = 0, RequireRestart = false,
            HintText = "Disable the energy for crafting and refining tasks [Native : false]")]
        [SettingPropertyGroup("Smithing/Energy/Disable")]
        public bool SmithingEnergyDisable { get; set; } = false;

        #endregion  Smithing Energy Disable


        #region  Smithing EnergyCostForRefining
        [SettingPropertyBool("Energy Refining", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enable modify the energy used for refining.")]
        [SettingPropertyGroup("Smithing/Energy/Refining")]
        public bool SmithingEnergyRefiningModifiers { get; set; } = false;

        [SettingPropertyFloatingInteger("Energy Refining", 0.1f, 1.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply the energy used for refining by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Smithing/Energy/Refining")]
        public float SmithingEnergyRefiningValue { get; set; } = 1.0f;
        #endregion  Smithing EnergyCostForRefining

        #region  Smithing EnergyCostForSmithing
        [SettingPropertyBool("Energy Crafting", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enable modify the energy used for Crafting.")]
        [SettingPropertyGroup("Smithing/Energy/Crafting")]
        public bool SmithingEnergySmithingModifiers { get; set; } = false;

        [SettingPropertyFloatingInteger("Energy Crafting", 0.1f, 1.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply the energy used for Crafting by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Smithing/Energy/Crafting")]
        public float SmithingEnergySmithingValue { get; set; } = 1.0f;
        #endregion  Smithing EnergyCostForSmithing

        #region  Smithing EnergyCostForSmelting
        [SettingPropertyBool("Energy Smelting", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enable modify the energy used for Smelting.")]
        [SettingPropertyGroup("Smithing/Energy/Smelting")]
        public bool SmithingEnergySmeltingModifiers { get; set; } = false;

        [SettingPropertyFloatingInteger("Energy Smithing", 0.1f, 1.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply the energy used for Smelting by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Smithing/Energy/Smelting")]
        public float SmithingEnergySmeltingValue { get; set; } = 1.0f;
        #endregion  Smithing EnergyCostForSmelting


        #endregion  Smithing



        [SettingPropertyBool("Item Auto Locks", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Allows for auto locking horses, food , and smithing materials.")]
        [SettingPropertyGroup("Auto Locks")]
        public bool MCMAutoLocks { get; set; } = false;
        #region ItemLocks

        [SettingPropertyBool("Horses", Order = 0, RequireRestart = false, //, IsToggle = true
            HintText = "Auto lock horses except lame horses.")]
        [SettingPropertyGroup("Auto Locks")]
        public bool autoLockHorses { get; set; } = false;

        [SettingPropertyBool("Food", Order = 0, RequireRestart = false, //, IsToggle = true
            HintText = "Auto lock all food.")]
        [SettingPropertyGroup("Auto Locks")]
        public bool autoLockFood { get; set; } = false;

        [SettingPropertyBool("Crude Iron", Order = 0, RequireRestart = false, //, IsToggle = true
            HintText = "Auto lock Crude Iron.")] 
        [SettingPropertyGroup("Auto Locks")]
        public bool autoLockIronBar1 { get; set; } = false;

        [SettingPropertyBool("Wrought Iron", Order = 0, RequireRestart = false, //, IsToggle = true
            HintText = "Auto lock Wrought Iron.")]
        [SettingPropertyGroup("Auto Locks")]
        public bool autoLockIronBar2 { get; set; } = false;

        [SettingPropertyBool("Iron", Order = 0, RequireRestart = false, //, IsToggle = true
            HintText = "Auto lock Iron.")]
        [SettingPropertyGroup("Auto Locks")]
        public bool autoLockIronBar3 { get; set; } = false;

        [SettingPropertyBool("Steel", Order = 0, RequireRestart = false, //, IsToggle = true
            HintText = "Auto lock Steel.")]
        [SettingPropertyGroup("Auto Locks")]
        public bool autoLockIronBar4 { get; set; } = false;

        [SettingPropertyBool("Fine Steel", Order = 0, RequireRestart = false, //, IsToggle = true
            HintText = "Auto lock Fine Steel.")]
        [SettingPropertyGroup("Auto Locks")]
        public bool autoLockIronBar5 { get; set; } = false;

        [SettingPropertyBool("Thamaskene Steel", Order = 0, RequireRestart = false, //, IsToggle = true
            HintText = "Auto lock Thamaskene Steel.")]
        [SettingPropertyGroup("Auto Locks")]
        public bool autoLockIronBar6 { get; set; } = false;

        [SettingPropertyBool("Iron Ore ", Order = 0, RequireRestart = false, //, IsToggle = true
            HintText = "Auto lock Iron Ore .")]
        [SettingPropertyGroup("Auto Locks")]
        public bool autoLockIronOre { get; set; } = false;

        [SettingPropertyBool("Silver Ore", Order = 0, RequireRestart = false, //, IsToggle = true
            HintText = "Auto lock Silver Ore.")]
        [SettingPropertyGroup("Auto Locks")]
        public bool autoLockSilverOre { get; set; } = false;

        [SettingPropertyBool("Hardwood", Order = 0, RequireRestart = false, //, IsToggle = true
            HintText = "Auto lock Hardwood.")]
        [SettingPropertyGroup("Auto Locks")]
        public bool autoLockHardwood { get; set; } = false;

        [SettingPropertyBool("Charcoal", Order = 0, RequireRestart = false, //, IsToggle = true
            HintText = "Auto lock Charcoal.")]
        [SettingPropertyGroup("Auto Locks")]
        public bool autoLockCharcol { get; set; } = false;


        #endregion ItemLocks




        [SettingPropertyBool("Army Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Allows army modifiers.")]
        [SettingPropertyGroup("Army")]
        public bool MCMArmy { get; set; } = false;
        #region ArmyManagement

        #region ArmyManagement Cohesion

        [SettingPropertyBool("Cohesion Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Allows Cohesion modifiers.")]
        [SettingPropertyGroup("Army/Cohesion")]
        public bool armyCohesionMultipliers { get; set; } = false;

        [SettingPropertyInteger("Base Cohesion Change", -5, 5, "0 Cohesion", Order = 0, RequireRestart = false,
            HintText = "Base Cohesion Change for armies [Native : -2].")]
        [SettingPropertyGroup("Army/Cohesion")]
        public int armyCohesionBaseChange { get; set; } = -2;

        [SettingPropertyBool("Disable Clan Only other modifiers", Order = 0, RequireRestart = false, //, IsToggle = true
            HintText = "Disable other cohesion loss except base for armies that are made up of a single clans parties only.")]
        [SettingPropertyGroup("Army/Cohesion")]
        public bool armyDisableCohesionLossClanOnlyParties { get; set; } = false;

        [SettingPropertyBool("Clan Only Use Multiplier", Order = 0, RequireRestart = false, //, IsToggle = true
            HintText = "Apply the cohesion multiplier to armies that are made up of a single clans parties only.")]
        [SettingPropertyGroup("Army/Cohesion")]
        public bool armyApplyMultiplerToClanOnlyParties { get; set; } = false;

        [SettingPropertyFloatingInteger("Cohesion Multiplier", 0.1f, 3.0f, "#0%", Order = 2, RequireRestart = false,
            HintText = "Multiply the cohesion loss by this multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Army/Cohesion")]
        public float armyCohesionLossMultiplier { get; set; } = 1.0f;

        #endregion ArmyManagement Cohesion


        #endregion ArmyManagement




        [SettingPropertyBool("Relation Building Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Relation Building modifiers.")]
        [SettingPropertyGroup("Relation Building")]
        public bool MCMRelationBuilding { get; set; } = false;
        #region Relations


        #region Relations KillingBandits
        [SettingPropertyBool("Killing Bandits Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Relation Building Killing Bandits modifiers.")]
        [SettingPropertyGroup("Relation Building/Bandits")]
        public bool relationsKillingBanditsEnabled { get; set; } = false;

        [SettingPropertyInteger("Groups for bonus", 1, 50, "0 Parties", Order = 0, RequireRestart = false,
            HintText = "Number of bandit groups you must destroy before you gain relation.")]
        [SettingPropertyGroup("Relation Building/Bandits")]
        public int GroupsOfBandits { get; set; } = 1;

        [SettingPropertyInteger("Relationship Increase", 1, 50, "0 Points", Order = 0, RequireRestart = false,
            HintText = "The base value that your relationship will increase by when it increases.")]
        [SettingPropertyGroup("Relation Building/Bandits")]
        public int RelationshipIncrease { get; set; } = 1;

        [SettingPropertyInteger("Radius", 500, 5000, Order = 0, RequireRestart = false,
            HintText = "This is the size of the radius inside which villages and towns will be affected by the relationship increase.")]
        [SettingPropertyGroup("Relation Building/Bandits")]
        public int Radius { get; set; } = 1000;

        [SettingPropertyBool("Size Bonus", Order = 0, RequireRestart = false,
            HintText = "Enable Size bonus modifiers.")]
        [SettingPropertyGroup("Relation Building/Bandits")]
        public bool SizeBonusEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("Size bonus", 0.1f, 1.0f, Order = 2, RequireRestart = false,
            HintText = "Multiply the Size Bonus by the number of bandits you have killed since you last gained relationship. this will then be multiplied by the base Relationship Increase to give your final increase value.")]
        [SettingPropertyGroup("Relation Building/Bandits")]
        public float SizeBonus { get; set; } = 0.05f;

        [SettingPropertyBool("Only Bandits with Prisoners", Order = 0, RequireRestart = false,
            HintText = "Enable relationship bonuses only for bandit parties with prisoners.")]
        [SettingPropertyGroup("Relation Building/Bandits")]
        public bool PrisonersOnly { get; set; } = false;

        [SettingPropertyBool("Bandits Parties", Order = 0, RequireRestart = false,
            HintText = "Enable relationship bonuses for bandit parties.")]
        [SettingPropertyGroup("Relation Building/Bandits")]
        public bool IncludeBandits { get; set; } = false;

        [SettingPropertyBool("Outlaw Parties", Order = 0, RequireRestart = false,
            HintText = "Enable relationship bonuses for outlaw parties.")]
        [SettingPropertyGroup("Relation Building/Bandits")]
        public bool IncludeOutlaws { get; set; } = false;

        [SettingPropertyBool("Mafia Parties", Order = 0, RequireRestart = false,
            HintText = "Enable relationship bonuses for mafia parties.")]
        [SettingPropertyGroup("Relation Building/Bandits")]
        public bool IncludeMafia { get; set; } = false;

        #endregion Relations KillingBandits


        #endregion Relations








        [SettingPropertyBool("Xp Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enable Xp modifiers.")]
        [SettingPropertyGroup("Xp Modifiers")]
        public bool MCMSkillsXp { get; set; } = false;

        #region SkillsXpMultipliers
        [SettingPropertyBool("Skill Xp Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enable Skill Xp modifiers.")]
        [SettingPropertyGroup("Xp Modifiers/Skills")]
        public bool SkillXpEnabled { get; set; } = false;


        [SettingPropertyBool("Player Skill Modifiers", Order = 0, RequireRestart = false,
            HintText = "Enable Player to use Skill Xp modifiers")]
        [SettingPropertyGroup("Xp Modifiers/Skills")]
        public bool SkillXpUseForPlayer { get; set; } = false;

        [SettingPropertyBool("Player Clan Skill Modifiers", Order = 0, RequireRestart = false,
            HintText = "Enable Player Clan to use Skill Xp modifiers")]
        [SettingPropertyGroup("Xp Modifiers/Skills")]
        public bool SkillXpUseForPlayerClan { get; set; } = false;

        [SettingPropertyBool("AI Skill Modifiers", Order = 0, RequireRestart = false,
            HintText = "Enable AI to use Xp modifiers. Use this or global they don't work at same time")]
        [SettingPropertyGroup("Xp Modifiers/Skills")]
        public bool SkillXpUseForAI { get; set; } = false;



        [SettingPropertyBool("Individual Skill Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enable Individual Skill Xp modifiers. Use this or global they don't work at same time")]
        [SettingPropertyGroup("Xp Modifiers/Skills/Individual")]
        public bool SkillXpUseIndividualMultiplers { get; set; } = false;
        #region SkillsXpMultipliers SpecificMultipliers
        [SettingPropertyFloatingInteger("Athletics Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false, //, Order = 0
            HintText = "Multiply Athletics skills exp gains by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Xp Modifiers/Skills/Individual")]
        public float SkillsXPMultiplierAthletics { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Bow Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false, //, Order = 0
            HintText = "Multiply Bow skills exp gains by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Xp Modifiers/Skills/Individual")]
        public float SkillsXPMultiplierBow { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Charm Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false, //, Order = 0
            HintText = "Multiply Charm skills exp gains by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Xp Modifiers/Skills/Individual")]
        public float SkillsXPMultiplierCharm { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Crafting Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false, //, Order = 0
            HintText = "Multiply Crafting skills exp gains by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Xp Modifiers/Skills/Individual")]
        public float SkillsXPMultiplierCrafting { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Crossbow Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false, //, Order = 0
            HintText = "Multiply Crossbow skills exp gains by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Xp Modifiers/Skills/Individual")]
        public float SkillsXPMultiplierCrossbow { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Engineering Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false, //, Order = 0
            HintText = "Multiply Engineering skills exp gains by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Xp Modifiers/Skills/Individual")]
        public float SkillsXPMultiplierEngineering { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Leadership Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false, //, Order = 0
            HintText = "Multiply Leadership skills exp gains by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Xp Modifiers/Skills/Individual")]
        public float SkillsXPMultiplierLeadership { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Medicine Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false, //, Order = 0
            HintText = "Multiply Medicine skills exp gains by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Xp Modifiers/Skills/Individual")]
        public float SkillsXPMultiplierMedicine { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("OneHanded Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false, //, Order = 0
            HintText = "Multiply OneHanded skills exp gains by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Xp Modifiers/Skills/Individual")]
        public float SkillsXPMultiplierOneHanded { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Polearm Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false, //, Order = 0
            HintText = "Multiply Polearm skills exp gains by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Xp Modifiers/Skills/Individual")]
        public float SkillsXPMultiplierPolearm { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Riding Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false, //, Order = 0
            HintText = "Multiply Riding skills exp gains by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Xp Modifiers/Skills/Individual")]
        public float SkillsXPMultiplierRiding { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Roguery Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false, //, Order = 0
            HintText = "Multiply Roguery skills exp gains by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Xp Modifiers/Skills/Individual")]
        public float SkillsXPMultiplierRoguery { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Scouting Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false, //, Order = 0
            HintText = "Multiply Scouting skills exp gains by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Xp Modifiers/Skills/Individual")]
        public float SkillsXPMultiplierScouting { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Steward Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false, //, Order = 0
            HintText = "Multiply Steward skills exp gains by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Xp Modifiers/Skills/Individual")]
        public float SkillsXPMultiplierSteward { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Tactics Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false, //, Order = 0
            HintText = "Multiply Tactics skills exp gains by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Xp Modifiers/Skills/Individual")]
        public float SkillsXPMultiplierTactics { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Throwing Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false, //, Order = 0
            HintText = "Multiply Throwing skills exp gains by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Xp Modifiers/Skills/Individual")]
        public float SkillsXPMultiplierThrowing { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("Trade Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false, //, Order = 0
            HintText = "Multiply Trade skills exp gains by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Xp Modifiers/Skills/Individual")]
        public float SkillsXPMultiplierTrade { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("TwoHanded Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false, //, Order = 0
            HintText = "Multiply TwoHanded skills exp gains by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Xp Modifiers/Skills/Individual")]
        public float SkillsXPMultiplierTwoHanded { get; set; } = 1.0f;

        #endregion SkillsXpMultipliers SpecificMultipliers

        [SettingPropertyBool("Global Skill Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enable Global Skill Xp modifiers. Use this or global they don't work at same time")]
        [SettingPropertyGroup("Xp Modifiers/Skills/Global")]
        public bool SkillXpUseGlobalMultipler { get; set; } = false;
        #region SkillsXpMultipliers Global

        [SettingPropertyFloatingInteger("Global Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false, //, Order = 0
            HintText = "Multiply skills exp gains by the Global multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Xp Modifiers/Skills/Global")]
        public float SkillsXpGlobalMultiplier { get; set; } = 1.0f;
        #endregion SkillsXpMultipliers Global

        #endregion SkillsXpMultipliers




/*

        [SettingPropertyBool("Learning Rate Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enable Learning Rate modifiers.")]
        [SettingPropertyGroup("Learning Rate")]
        public bool MCMLearningRate { get; set; } = false;
*/

        #region LearningRateMultipliers
        [SettingPropertyBool("Learning Rate Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enable Learning Rate modifiers.")]
        [SettingPropertyGroup("Learning Rate")]
        public bool LearningRateEnabled { get; set; } = false;


        [SettingPropertyFloatingInteger("Global Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false, //, Order = 0
            HintText = "Multiply Learning Rate by the Global multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Learning Rate")]
        public float LearningRateMultiplier { get; set; } = 1.0f;

        #endregion LearningRateMultipliers







        #region MobileParty Food Consumption
        [SettingPropertyBool("Party Food Modifiers", IsToggle = true, Order = 0, RequireRestart = false,
            HintText = "Enable Party food consumption modifiers.")]
        [SettingPropertyGroup("Party Food")]
        public bool PartyFoodConsumptionEnabled { get; set; } = false;

        [SettingPropertyFloatingInteger("Party Food Multiplier", 0.1f, 10.0f, "#0%", RequireRestart = false, //, Order = 0
            HintText = "Multiply Party food consumption by the multiplier [Native : 1.0[100%]].")]
        [SettingPropertyGroup("Party Food")]
        public float PartyFoodConsumptionMultiplier { get; set; } = 1.0f;
        #endregion  MobileParty Food Consumption













        public override IDictionary<string, Func<BaseSettings>> GetAvailablePresets()
        {
            var basePresets = base.GetAvailablePresets(); // include the 'Default' preset that MCM provides

            basePresets.Add("native", () => new MCMSettings()
            {
                Debug = false,
                LoadMCMConfigFile = false,
                LogToFile = false,
                MCMItemModifiers = false,

                MCMBodyArmorModifiers = false,
                BodyArmorWeightModifiers = false,
                BodyArmorTier1WeightMultiplier = 1.0f,
                BodyArmorTier2WeightMultiplier = 1.0f,
                BodyArmorTier3WeightMultiplier = 1.0f,
                BodyArmorTier4WeightMultiplier = 1.0f,
                BodyArmorTier5WeightMultiplier = 1.0f,
                BodyArmorTier6WeightMultiplier = 1.0f,
                BodyArmorValueModifiers = false,
                BodyArmorTier1PriceMultiplier = 1.0f,
                BodyArmorTier2PriceMultiplier = 1.0f,
                BodyArmorTier3PriceMultiplier = 1.0f,
                BodyArmorTier4PriceMultiplier = 1.0f,
                BodyArmorTier5PriceMultiplier = 1.0f,
                BodyArmorTier6PriceMultiplier = 1.0f,

                MCMBowModifiers = false,
                BowWeightModifiers = false,
                BowTier1WeightMultiplier = 1.0f,
                BowTier2WeightMultiplier = 1.0f,
                BowTier3WeightMultiplier = 1.0f,
                BowTier4WeightMultiplier = 1.0f,
                BowTier5WeightMultiplier = 1.0f,
                BowTier6WeightMultiplier = 1.0f,
                BowValueModifiers = false,
                BowTier1PriceMultiplier = 1.0f,
                BowTier2PriceMultiplier = 1.0f,
                BowTier3PriceMultiplier = 1.0f,
                BowTier4PriceMultiplier = 1.0f,
                BowTier5PriceMultiplier = 1.0f,
                BowTier6PriceMultiplier = 1.0f,

                MCMCapeModifiers = false,
                CapeWeightModifiers = false,
                CapeTier1WeightMultiplier = 1.0f,
                CapeTier2WeightMultiplier = 1.0f,
                CapeTier3WeightMultiplier = 1.0f,
                CapeTier4WeightMultiplier = 1.0f,
                CapeTier5WeightMultiplier = 1.0f,
                CapeTier6WeightMultiplier = 1.0f,
                CapeValueModifiers = false,
                CapeTier1PriceMultiplier = 1.0f,
                CapeTier2PriceMultiplier = 1.0f,
                CapeTier3PriceMultiplier = 1.0f,
                CapeTier4PriceMultiplier = 1.0f,
                CapeTier5PriceMultiplier = 1.0f,
                CapeTier6PriceMultiplier = 1.0f,

                MCMChestArmorModifiers = false,
                ChestArmorWeightModifiers = false,
                ChestArmorTier1WeightMultiplier = 1.0f,
                ChestArmorTier2WeightMultiplier = 1.0f,
                ChestArmorTier3WeightMultiplier = 1.0f,
                ChestArmorTier4WeightMultiplier = 1.0f,
                ChestArmorTier5WeightMultiplier = 1.0f,
                ChestArmorTier6WeightMultiplier = 1.0f,
                ChestArmorValueModifiers = false,
                ChestArmorTier1PriceMultiplier = 1.0f,
                ChestArmorTier2PriceMultiplier = 1.0f,
                ChestArmorTier3PriceMultiplier = 1.0f,
                ChestArmorTier4PriceMultiplier = 1.0f,
                ChestArmorTier5PriceMultiplier = 1.0f,
                ChestArmorTier6PriceMultiplier = 1.0f,

                MCMCrossbowModifiers = false,
                CrossbowWeightModifiers = false,
                CrossbowTier1WeightMultiplier = 1.0f,
                CrossbowTier2WeightMultiplier = 1.0f,
                CrossbowTier3WeightMultiplier = 1.0f,
                CrossbowTier4WeightMultiplier = 1.0f,
                CrossbowTier5WeightMultiplier = 1.0f,
                CrossbowTier6WeightMultiplier = 1.0f,
                CrossbowValueModifiers = false,
                CrossbowTier1PriceMultiplier = 1.0f,
                CrossbowTier2PriceMultiplier = 1.0f,
                CrossbowTier3PriceMultiplier = 1.0f,
                CrossbowTier4PriceMultiplier = 1.0f,
                CrossbowTier5PriceMultiplier = 1.0f,
                CrossbowTier6PriceMultiplier = 1.0f,

                MCMHandArmorModifiers = false,
                HandArmorWeightModifiers = false,
                HandArmorTier1WeightMultiplier = 1.0f,
                HandArmorTier2WeightMultiplier = 1.0f,
                HandArmorTier3WeightMultiplier = 1.0f,
                HandArmorTier4WeightMultiplier = 1.0f,
                HandArmorTier5WeightMultiplier = 1.0f,
                HandArmorTier6WeightMultiplier = 1.0f,
                HandArmorValueModifiers = false,
                HandArmorTier1PriceMultiplier = 1.0f,
                HandArmorTier2PriceMultiplier = 1.0f,
                HandArmorTier3PriceMultiplier = 1.0f,
                HandArmorTier4PriceMultiplier = 1.0f,
                HandArmorTier5PriceMultiplier = 1.0f,
                HandArmorTier6PriceMultiplier = 1.0f,

                MCMHeadArmorModifiers = false,
                HeadArmorWeightModifiers = false,
                HeadArmorTier1WeightMultiplier = 1.0f,
                HeadArmorTier2WeightMultiplier = 1.0f,
                HeadArmorTier3WeightMultiplier = 1.0f,
                HeadArmorTier4WeightMultiplier = 1.0f,
                HeadArmorTier5WeightMultiplier = 1.0f,
                HeadArmorTier6WeightMultiplier = 1.0f,
                HeadArmorValueModifiers = false,
                HeadArmorTier1PriceMultiplier = 1.0f,
                HeadArmorTier2PriceMultiplier = 1.0f,
                HeadArmorTier3PriceMultiplier = 1.0f,
                HeadArmorTier4PriceMultiplier = 1.0f,
                HeadArmorTier5PriceMultiplier = 1.0f,
                HeadArmorTier6PriceMultiplier = 1.0f,

                MCMHorseModifiers = false,
                HorseWeightModifiers = false,
                HorseTier1WeightMultiplier = 1.0f,
                HorseTier2WeightMultiplier = 1.0f,
                HorseTier3WeightMultiplier = 1.0f,
                HorseTier4WeightMultiplier = 1.0f,
                HorseTier5WeightMultiplier = 1.0f,
                HorseTier6WeightMultiplier = 1.0f,
                HorseValueModifiers = false,
                HorseTier1PriceMultiplier = 1.0f,
                HorseTier2PriceMultiplier = 1.0f,
                HorseTier3PriceMultiplier = 1.0f,
                HorseTier4PriceMultiplier = 1.0f,
                HorseTier5PriceMultiplier = 1.0f,
                HorseTier6PriceMultiplier = 1.0f,

                MCMHorseHarnessModifiers = false,
                HorseHarnessWeightModifiers = false,
                HorseHarnessTier1WeightMultiplier = 1.0f,
                HorseHarnessTier2WeightMultiplier = 1.0f,
                HorseHarnessTier3WeightMultiplier = 1.0f,
                HorseHarnessTier4WeightMultiplier = 1.0f,
                HorseHarnessTier5WeightMultiplier = 1.0f,
                HorseHarnessTier6WeightMultiplier = 1.0f,
                HorseHarnessValueModifiers = false,
                HorseHarnessTier1PriceMultiplier = 1.0f,
                HorseHarnessTier2PriceMultiplier = 1.0f,
                HorseHarnessTier3PriceMultiplier = 1.0f,
                HorseHarnessTier4PriceMultiplier = 1.0f,
                HorseHarnessTier5PriceMultiplier = 1.0f,
                HorseHarnessTier6PriceMultiplier = 1.0f,

                MCMLegArmorModifiers = false,
                LegArmorWeightModifiers = false,
                LegArmorTier1WeightMultiplier = 1.0f,
                LegArmorTier2WeightMultiplier = 1.0f,
                LegArmorTier3WeightMultiplier = 1.0f,
                LegArmorTier4WeightMultiplier = 1.0f,
                LegArmorTier5WeightMultiplier = 1.0f,
                LegArmorTier6WeightMultiplier = 1.0f,
                LegArmorValueModifiers = false,
                LegArmorTier1PriceMultiplier = 1.0f,
                LegArmorTier2PriceMultiplier = 1.0f,
                LegArmorTier3PriceMultiplier = 1.0f,
                LegArmorTier4PriceMultiplier = 1.0f,
                LegArmorTier5PriceMultiplier = 1.0f,
                LegArmorTier6PriceMultiplier = 1.0f,

                MCMMusketModifiers = false,
                MusketWeightModifiers = false,
                MusketTier1WeightMultiplier = 1.0f,
                MusketTier2WeightMultiplier = 1.0f,
                MusketTier3WeightMultiplier = 1.0f,
                MusketTier4WeightMultiplier = 1.0f,
                MusketTier5WeightMultiplier = 1.0f,
                MusketTier6WeightMultiplier = 1.0f,
                MusketValueModifiers = false,
                MusketTier1PriceMultiplier = 1.0f,
                MusketTier2PriceMultiplier = 1.0f,
                MusketTier3PriceMultiplier = 1.0f,
                MusketTier4PriceMultiplier = 1.0f,
                MusketTier5PriceMultiplier = 1.0f,
                MusketTier6PriceMultiplier = 1.0f,


                MCMOneHandedWeaponModifiers = false,
                OneHandedWeaponWeightModifiers = false,
                OneHandedWeaponTier1WeightMultiplier = 1.0f,
                OneHandedWeaponTier2WeightMultiplier = 1.0f,
                OneHandedWeaponTier3WeightMultiplier = 1.0f,
                OneHandedWeaponTier4WeightMultiplier = 1.0f,
                OneHandedWeaponTier5WeightMultiplier = 1.0f,
                OneHandedWeaponTier6WeightMultiplier = 1.0f,
                OneHandedWeaponValueModifiers = false,
                OneHandedWeaponTier1PriceMultiplier = 1.0f,
                OneHandedWeaponTier2PriceMultiplier = 1.0f,
                OneHandedWeaponTier3PriceMultiplier = 1.0f,
                OneHandedWeaponTier4PriceMultiplier = 1.0f,
                OneHandedWeaponTier5PriceMultiplier = 1.0f,
                OneHandedWeaponTier6PriceMultiplier = 1.0f,

                MCMPistolModifiers = false,
                PistolWeightModifiers = false,
                PistolTier1WeightMultiplier = 1.0f,
                PistolTier2WeightMultiplier = 1.0f,
                PistolTier3WeightMultiplier = 1.0f,
                PistolTier4WeightMultiplier = 1.0f,
                PistolTier5WeightMultiplier = 1.0f,
                PistolTier6WeightMultiplier = 1.0f,
                PistolValueModifiers = false,
                PistolTier1PriceMultiplier = 1.0f,
                PistolTier2PriceMultiplier = 1.0f,
                PistolTier3PriceMultiplier = 1.0f,
                PistolTier4PriceMultiplier = 1.0f,
                PistolTier5PriceMultiplier = 1.0f,
                PistolTier6PriceMultiplier = 1.0f,

                MCMPolearmModifiers = false,
                PolearmWeightModifiers = false,
                PolearmTier1WeightMultiplier = 1.0f,
                PolearmTier2WeightMultiplier = 1.0f,
                PolearmTier3WeightMultiplier = 1.0f,
                PolearmTier4WeightMultiplier = 1.0f,
                PolearmTier5WeightMultiplier = 1.0f,
                PolearmTier6WeightMultiplier = 1.0f,
                PolearmValueModifiers = false,
                PolearmTier1PriceMultiplier = 1.0f,
                PolearmTier2PriceMultiplier = 1.0f,
                PolearmTier3PriceMultiplier = 1.0f,
                PolearmTier4PriceMultiplier = 1.0f,
                PolearmTier5PriceMultiplier = 1.0f,
                PolearmTier6PriceMultiplier = 1.0f,

                MCMShieldModifiers = false,
                ShieldWeightModifiers = false,
                ShieldTier1WeightMultiplier = 1.0f,
                ShieldTier2WeightMultiplier = 1.0f,
                ShieldTier3WeightMultiplier = 1.0f,
                ShieldTier4WeightMultiplier = 1.0f,
                ShieldTier5WeightMultiplier = 1.0f,
                ShieldTier6WeightMultiplier = 1.0f,
                ShieldValueModifiers = false,

                ShieldTier1PriceMultiplier = 1.0f,
                ShieldTier2PriceMultiplier = 1.0f,
                ShieldTier3PriceMultiplier = 1.0f,
                ShieldTier4PriceMultiplier = 1.0f,
                ShieldTier5PriceMultiplier = 1.0f,
                ShieldTier6PriceMultiplier = 1.0f,

                MCMTwoHandedWeaponModifiers = false,
                TwoHandedWeaponWeightModifiers = false,
                TwoHandedWeaponTier1WeightMultiplier = 1.0f,
                TwoHandedWeaponTier2WeightMultiplier = 1.0f,
                TwoHandedWeaponTier3WeightMultiplier = 1.0f,
                TwoHandedWeaponTier4WeightMultiplier = 1.0f,
                TwoHandedWeaponTier5WeightMultiplier = 1.0f,
                TwoHandedWeaponTier6WeightMultiplier = 1.0f,
                TwoHandedWeaponValueModifiers = false,
                TwoHandedWeaponTier1PriceMultiplier = 1.0f,
                TwoHandedWeaponTier2PriceMultiplier = 1.0f,
                TwoHandedWeaponTier3PriceMultiplier = 1.0f,
                TwoHandedWeaponTier4PriceMultiplier = 1.0f,
                TwoHandedWeaponTier5PriceMultiplier = 1.0f,
                TwoHandedWeaponTier6PriceMultiplier = 1.0f,


                MCMBattleRewardModifiers = false,
                BattleRewardsRelationShipGainModifiers = false,
                BattleRewardsRelationShipGainMultiplier = 1.0f,
                BattleRewardsRenownGainModifiers = false,
                BattleRewardsRenownGainMultiplier = 1.0f,
                BattleRewardsInfluenceGainModifiers = false,
                BattleRewardsInfluenceGainMultiplier = 1.0f,
                BattleRewardsMoraleGainModifiers = false,
                BattleRewardsMoraleGainMultiplier = 1.0f,
                BattleRewardsGoldLossModifiers = false,
                BattleRewardsGoldLossMultiplier = 1.0f,


                MCMClanModifiers = false,
                ClanAdditionalPartyLimitForTierModifiers = false,
                ClanAdditionalPartyLimitForTierValue = 0,
                ClanAdditionaCompanionLimitForTierModifiers = false,
                ClanAdditionalCompanionLimitForTierValue = 0,

                MCMWorkShopModifiers = false,
                WorkShopBankruptcyModifiers = false,
                WorkShopBankruptcyValue = 3,
                WorkShopMaxWorkshopCountForPlayerModifiers = false,
                WorkShopMaxWorkshopCountForPlayerValue = 1,

                MCMCharacterDevlopmentModifiers = false,
                //CharacterAttributesAtStartModifiers = false,
                //CharacterAttributesAtStartValue = 15,
                CharacterLevelsPerAttributeModifiers = false,
                CharacterLevelsPerAttributeValue = 4,
                CharacterFocusPerLevelModifiers = false,
                CharacterFocusPerLevelValue = 4,
                //CharacterFocusAtStartModifiers = false,
                //CharacterFocusAtStartValue = 5,


                MCMPregnancyModifiers = false,
                PregnancyDurationModifiers = false,
                PregnancyDurationValue = 36,
                PregnancyLaborMortalityChanceModifiers = false,
                PregnancyLaborMortalityChanceValue = 0.015f,
                PregnancyFemaleOffspringChanceModifiers = false,
                PregnancyFemaleOffspringChanceValue = 0.01f,

                MCMSmithingModifiers = false,
                SmithingRefiningXpModifiers = false,
                SmithingRefiningXpValue = 1.0f,
                SmithingSmeltingXpModifiers = false,
                SmithingSmeltingXpValue = 1.0f,
                SmithingSmithingXpModifiers = false,
                SmithingSmithingXpValue = 1.0f,
                SmithingEnergyDisable = false,
                SmithingEnergyRefiningModifiers = false,
                SmithingEnergyRefiningValue = 1.0f,
                SmithingEnergySmithingModifiers = false,
                SmithingEnergySmithingValue = 1.0f,
                SmithingEnergySmeltingModifiers = false,
                SmithingEnergySmeltingValue = 1.0f,


                MCMAutoLocks = false,
                autoLockHorses = false,
                autoLockFood = false,
                autoLockIronBar1 = false,
                autoLockIronBar2 = false,
                autoLockIronBar3 = false,
                autoLockIronBar4 = false,
                autoLockIronBar5 = false,
                autoLockIronBar6 = false,
                autoLockIronOre = false,
                autoLockSilverOre = false,
                autoLockHardwood = false,
                autoLockCharcol = false,


                MCMArmy = false,
                armyCohesionMultipliers = false,
                armyCohesionBaseChange = -2,
                armyDisableCohesionLossClanOnlyParties = false,
                armyApplyMultiplerToClanOnlyParties = false,
                armyCohesionLossMultiplier = 1.0f,


                MCMRelationBuilding = false,
                relationsKillingBanditsEnabled = false,
                GroupsOfBandits = 1,
                RelationshipIncrease = 1,
                Radius = 1000,
                SizeBonusEnabled = false,
                SizeBonus = 0.05f,
                PrisonersOnly = false,
                IncludeBandits = false,
                IncludeOutlaws = false,
                IncludeMafia = false,




                MCMSkillsXp = false,
                SkillXpEnabled = false,

                SkillXpUseForPlayer = false,
                SkillXpUseForPlayerClan = false,
                SkillXpUseForAI = false,

                SkillXpUseIndividualMultiplers = false,

                SkillsXPMultiplierAthletics = 1.0f,
                SkillsXPMultiplierBow = 1.0f,
                SkillsXPMultiplierCharm = 1.0f,
                SkillsXPMultiplierCrafting = 1.0f,
                SkillsXPMultiplierCrossbow = 1.0f,
                SkillsXPMultiplierEngineering = 1.0f,
                SkillsXPMultiplierLeadership = 1.0f,
                SkillsXPMultiplierMedicine = 1.0f,
                SkillsXPMultiplierOneHanded = 1.0f,
                SkillsXPMultiplierPolearm = 1.0f,
                SkillsXPMultiplierRiding = 1.0f,
                SkillsXPMultiplierRoguery = 1.0f,
                SkillsXPMultiplierScouting = 1.0f,
                SkillsXPMultiplierSteward = 1.0f,
                SkillsXPMultiplierTactics = 1.0f,
                SkillsXPMultiplierThrowing = 1.0f,
                SkillsXPMultiplierTrade = 1.0f,
                SkillsXPMultiplierTwoHanded = 1.0f,

                SkillXpUseGlobalMultipler = false,
                SkillsXpGlobalMultiplier = 1.0f,

        

                LearningRateEnabled = false,
                LearningRateMultiplier = 1.0f,


                PartyFoodConsumptionEnabled = false,
                PartyFoodConsumptionMultiplier = 1.0f,





            });

            basePresets.Add("Kaoses", () => new MCMSettings()
            {
                Debug = false,
                LoadMCMConfigFile = false,
                LogToFile = false,
                MCMItemModifiers = true,

                MCMBodyArmorModifiers = true,
                BodyArmorWeightModifiers = false,
                BodyArmorValueModifiers = true,
                BodyArmorTier1PriceMultiplier = 0.3f,
                BodyArmorTier2PriceMultiplier = 0.4f,
                BodyArmorTier3PriceMultiplier = 0.5f,
                BodyArmorTier4PriceMultiplier = 0.6f,
                BodyArmorTier5PriceMultiplier = 0.7f,
                BodyArmorTier6PriceMultiplier = 0.8f,

                MCMBowModifiers = true,
                BowWeightModifiers = false,
                BowValueModifiers = true,
                BowTier1PriceMultiplier = 0.3f,
                BowTier2PriceMultiplier = 0.4f,
                BowTier3PriceMultiplier = 0.5f,
                BowTier4PriceMultiplier = 0.6f,
                BowTier5PriceMultiplier = 0.7f,
                BowTier6PriceMultiplier = 0.8f,

                MCMCapeModifiers = true,
                CapeWeightModifiers = false,
                CapeValueModifiers = true,
                CapeTier1PriceMultiplier = 0.3f,
                CapeTier2PriceMultiplier = 0.4f,
                CapeTier3PriceMultiplier = 0.5f,
                CapeTier4PriceMultiplier = 0.6f,
                CapeTier5PriceMultiplier = 0.7f,
                CapeTier6PriceMultiplier = 0.8f,

                MCMChestArmorModifiers = true,
                ChestArmorWeightModifiers = false,
                ChestArmorValueModifiers = true,
                ChestArmorTier1PriceMultiplier = 0.3f,
                ChestArmorTier2PriceMultiplier = 0.4f,
                ChestArmorTier3PriceMultiplier = 0.5f,
                ChestArmorTier4PriceMultiplier = 0.6f,
                ChestArmorTier5PriceMultiplier = 0.7f,
                ChestArmorTier6PriceMultiplier = 0.8f,

                MCMCrossbowModifiers = true,
                CrossbowWeightModifiers = false,
                CrossbowValueModifiers = true,
                CrossbowTier1PriceMultiplier = 0.3f,
                CrossbowTier2PriceMultiplier = 0.4f,
                CrossbowTier3PriceMultiplier = 0.5f,
                CrossbowTier4PriceMultiplier = 0.6f,
                CrossbowTier5PriceMultiplier = 0.7f,
                CrossbowTier6PriceMultiplier = 0.8f,

                MCMHandArmorModifiers = true,
                HandArmorWeightModifiers = false,
                HandArmorValueModifiers = true,
                HandArmorTier1PriceMultiplier = 0.3f,
                HandArmorTier2PriceMultiplier = 0.4f,
                HandArmorTier3PriceMultiplier = 0.5f,
                HandArmorTier4PriceMultiplier = 0.6f,
                HandArmorTier5PriceMultiplier = 0.7f,
                HandArmorTier6PriceMultiplier = 0.8f,

                MCMHeadArmorModifiers = true,
                HeadArmorWeightModifiers = false,
                HeadArmorValueModifiers = true,
                HeadArmorTier1PriceMultiplier = 0.3f,
                HeadArmorTier2PriceMultiplier = 0.4f,
                HeadArmorTier3PriceMultiplier = 0.5f,
                HeadArmorTier4PriceMultiplier = 0.6f,
                HeadArmorTier5PriceMultiplier = 0.7f,
                HeadArmorTier6PriceMultiplier = 0.8f,

                MCMHorseModifiers = true,
                HorseWeightModifiers = false,
                HorseValueModifiers = true,
                HorseTier1PriceMultiplier = 0.3f,
                HorseTier2PriceMultiplier = 0.4f,
                HorseTier3PriceMultiplier = 0.5f,
                HorseTier4PriceMultiplier = 0.6f,
                HorseTier5PriceMultiplier = 0.7f,
                HorseTier6PriceMultiplier = 0.8f,

                MCMHorseHarnessModifiers = true,
                HorseHarnessWeightModifiers = false,
                HorseHarnessValueModifiers = true,
                HorseHarnessTier1PriceMultiplier = 0.3f,
                HorseHarnessTier2PriceMultiplier = 0.4f,
                HorseHarnessTier3PriceMultiplier = 0.5f,
                HorseHarnessTier4PriceMultiplier = 0.6f,
                HorseHarnessTier5PriceMultiplier = 0.7f,
                HorseHarnessTier6PriceMultiplier = 0.8f,

                MCMLegArmorModifiers = true,
                LegArmorWeightModifiers = false,
                LegArmorValueModifiers = true,
                LegArmorTier1PriceMultiplier = 0.3f,
                LegArmorTier2PriceMultiplier = 0.4f,
                LegArmorTier3PriceMultiplier = 0.5f,
                LegArmorTier4PriceMultiplier = 0.6f,
                LegArmorTier5PriceMultiplier = 0.7f,
                LegArmorTier6PriceMultiplier = 0.8f,

                MCMMusketModifiers = true,
                MusketWeightModifiers = false,
                MusketValueModifiers = true,
                MusketTier1PriceMultiplier = 0.4f,
                MusketTier2PriceMultiplier = 0.5f,
                MusketTier3PriceMultiplier = 0.6f,
                MusketTier4PriceMultiplier = 0.7f,
                MusketTier5PriceMultiplier = 0.8f,
                MusketTier6PriceMultiplier = 0.9f,

                MCMOneHandedWeaponModifiers = true,
                OneHandedWeaponWeightModifiers = false,
                OneHandedWeaponValueModifiers = true,
                OneHandedWeaponTier1PriceMultiplier = 0.3f,
                OneHandedWeaponTier2PriceMultiplier = 0.4f,
                OneHandedWeaponTier3PriceMultiplier = 0.5f,
                OneHandedWeaponTier4PriceMultiplier = 0.6f,
                OneHandedWeaponTier5PriceMultiplier = 0.7f,
                OneHandedWeaponTier6PriceMultiplier = 0.8f,

                MCMPistolModifiers = true,
                PistolWeightModifiers = false,
                PistolValueModifiers = true,
                PistolTier1PriceMultiplier = 0.4f,
                PistolTier2PriceMultiplier = 0.5f,
                PistolTier3PriceMultiplier = 0.6f,
                PistolTier4PriceMultiplier = 0.7f,
                PistolTier5PriceMultiplier = 0.8f,
                PistolTier6PriceMultiplier = 0.9f,

                MCMPolearmModifiers = true,
                PolearmWeightModifiers = false,
                PolearmValueModifiers = true,
                PolearmTier1PriceMultiplier = 0.3f,
                PolearmTier2PriceMultiplier = 0.4f,
                PolearmTier3PriceMultiplier = 0.5f,
                PolearmTier4PriceMultiplier = 0.6f,
                PolearmTier5PriceMultiplier = 0.7f,
                PolearmTier6PriceMultiplier = 0.8f,

                MCMShieldModifiers = true,
                ShieldWeightModifiers = false,
                ShieldValueModifiers = true,
                ShieldTier1PriceMultiplier = 0.3f,
                ShieldTier2PriceMultiplier = 0.4f,
                ShieldTier3PriceMultiplier = 0.5f,
                ShieldTier4PriceMultiplier = 0.6f,
                ShieldTier5PriceMultiplier = 0.7f,
                ShieldTier6PriceMultiplier = 0.8f,

                MCMTwoHandedWeaponModifiers = true,
                TwoHandedWeaponWeightModifiers = false,
                TwoHandedWeaponValueModifiers = true,
                TwoHandedWeaponTier1PriceMultiplier = 0.4f,
                TwoHandedWeaponTier2PriceMultiplier = 0.5f,
                TwoHandedWeaponTier3PriceMultiplier = 0.6f,
                TwoHandedWeaponTier4PriceMultiplier = 0.7f,
                TwoHandedWeaponTier5PriceMultiplier = 0.8f,
                TwoHandedWeaponTier6PriceMultiplier = 0.9f,

                MCMBattleRewardModifiers = true,
                BattleRewardsRelationShipGainModifiers = true,
                BattleRewardsRelationShipGainMultiplier = 1.5f,
                BattleRewardsRenownGainModifiers = true,
                BattleRewardsRenownGainMultiplier = 1.5f,
                BattleRewardsInfluenceGainModifiers = true,
                BattleRewardsInfluenceGainMultiplier = 1.7f,
                BattleRewardsMoraleGainModifiers = true,
                BattleRewardsMoraleGainMultiplier = 0.9f,
                BattleRewardsGoldLossModifiers = true,
                BattleRewardsGoldLossMultiplier = 1.4f,


                MCMClanModifiers = true,
                ClanAdditionalPartyLimitForTierModifiers = true,
                ClanAdditionalPartyLimitForTierValue = 2,
                ClanAdditionaCompanionLimitForTierModifiers = true,
                ClanAdditionalCompanionLimitForTierValue = 3,

                MCMWorkShopModifiers = true,
                WorkShopBankruptcyModifiers = true,
                WorkShopBankruptcyValue = 6,
                WorkShopMaxWorkshopCountForPlayerModifiers = true,
                WorkShopMaxWorkshopCountForPlayerValue = 2,

                MCMCharacterDevlopmentModifiers = true,
                //CharacterAttributesAtStartModifiers = false,
                //CharacterAttributesAtStartValue = 15,
                CharacterLevelsPerAttributeModifiers = true,
                CharacterLevelsPerAttributeValue = 3,
                CharacterFocusPerLevelModifiers = true,
                CharacterFocusPerLevelValue = 2,
                //CharacterFocusAtStartModifiers = false,
                //CharacterFocusAtStartValue = 5,


                MCMPregnancyModifiers = true,
                PregnancyDurationModifiers = true,
                PregnancyDurationValue = 36,
                PregnancyLaborMortalityChanceModifiers = true,
                PregnancyLaborMortalityChanceValue = 0.010f,
                PregnancyFemaleOffspringChanceModifiers = true,
                PregnancyFemaleOffspringChanceValue = 0.60f,

                MCMSmithingModifiers = true,
                SmithingRefiningXpModifiers = true,
                SmithingRefiningXpValue = 1.5f,
                SmithingSmeltingXpModifiers = true,
                SmithingSmeltingXpValue = 1.5f,
                SmithingSmithingXpModifiers = true,
                SmithingSmithingXpValue = 2.5f,
                SmithingEnergyDisable = true,
                SmithingEnergyRefiningModifiers = true,
                SmithingEnergyRefiningValue = 0.5f,
                SmithingEnergySmithingModifiers = true,
                SmithingEnergySmithingValue = 0.5f,
                SmithingEnergySmeltingModifiers = true,
                SmithingEnergySmeltingValue = 0.5f,



                MCMAutoLocks = true,
                autoLockHorses = true,
                autoLockFood = true,
                autoLockIronBar1 = true,
                autoLockIronBar2 = true,
                autoLockIronBar3 = true,
                autoLockIronBar4 = true,
                autoLockIronBar5 = true,
                autoLockIronBar6 = true,
                autoLockIronOre = true,
                autoLockSilverOre = true,
                autoLockHardwood = true,
                autoLockCharcol = true,


                MCMArmy = true,
                armyCohesionMultipliers = true,
                armyCohesionBaseChange = -2,
                armyDisableCohesionLossClanOnlyParties = false,
                armyApplyMultiplerToClanOnlyParties = true,
                armyCohesionLossMultiplier = 0.75f,


                MCMRelationBuilding = true,
                relationsKillingBanditsEnabled = true,
                GroupsOfBandits = 1,
                RelationshipIncrease = 1,
                Radius = 1500,
                SizeBonusEnabled = false,
                SizeBonus = 0.05f,
                PrisonersOnly = false,
                IncludeBandits = true,
                IncludeOutlaws = false,
                IncludeMafia = false,



                MCMSkillsXp = false,
                SkillXpEnabled = false,

                SkillXpUseForPlayer = false,
                SkillXpUseForPlayerClan = false,
                SkillXpUseForAI = false,

                SkillXpUseIndividualMultiplers = false,

                SkillsXPMultiplierAthletics = 1.0f,
                SkillsXPMultiplierBow = 1.0f,
                SkillsXPMultiplierCharm = 1.0f,
                SkillsXPMultiplierCrafting = 1.0f,
                SkillsXPMultiplierCrossbow = 1.0f,
                SkillsXPMultiplierEngineering = 1.0f,
                SkillsXPMultiplierLeadership = 1.0f,
                SkillsXPMultiplierMedicine = 1.0f,
                SkillsXPMultiplierOneHanded = 1.0f,
                SkillsXPMultiplierPolearm = 1.0f,
                SkillsXPMultiplierRiding = 1.0f,
                SkillsXPMultiplierRoguery = 1.0f,
                SkillsXPMultiplierScouting = 1.0f,
                SkillsXPMultiplierSteward = 1.0f,
                SkillsXPMultiplierTactics = 1.0f,
                SkillsXPMultiplierThrowing = 1.0f,
                SkillsXPMultiplierTrade = 1.0f,
                SkillsXPMultiplierTwoHanded = 1.0f,

                SkillXpUseGlobalMultipler = false,
                SkillsXpGlobalMultiplier = 1.0f,



                LearningRateEnabled = false,
                LearningRateMultiplier = 1.0f,


                PartyFoodConsumptionEnabled = false,
                PartyFoodConsumptionMultiplier = 1.0f,



            });
            /*
            basePresets.Add("True", () => new MCMSettings()
            {
                Property1 = true,
                Property2 = true
            });
            */

            return basePresets;
        }

    }
}