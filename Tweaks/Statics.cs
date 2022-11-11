using Bannerlord.BUTR.Shared.Helpers;
using Tweaks.Utils;
using System.Linq;
using System.Reflection;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Engine;
using Tweaks.Settings;

namespace Tweaks
{
    public static class Statics
    {
        public static TweaksMCMSettings? _settings;
        public const string ModuleFolder = "Tweaks";
        public const string InstanceId = ModuleFolder;
        public const string DisplayName = "Bannerlord Tweaks";
        public const string FormatType = "json";
        public const string LogPath = @"..\\..\\Modules\\" + ModuleFolder + "\\TweaksLog.txt";
        public const string ConfigFilePath = @"..\\..\\Modules\\" + ModuleFolder + "\\config.json";
        public static string PrePrend { get; set; } = DisplayName;
        public const string HarmonyId = ModuleFolder + ".harmony";
        public static string ModVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

        #region MCMConfigValues
        public static string? MCMConfigFolder { get; set; }
        public static bool MCMModuleLoaded { get; set; } = false;
        public static bool ModConfigFileExists { get; set; } = false;
        #endregion
    }
}