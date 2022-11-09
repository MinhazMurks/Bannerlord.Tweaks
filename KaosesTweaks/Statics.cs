using System.Reflection;
using Bannerlord.BUTR.Shared.Helpers;
using KaosesTweaks.Settings;

namespace KaosesTweaks
{
    public static class Statics
    {
        public static KaosesMCMSettings? _settings;
        public const string ModuleFolder = "KaosesTweaks";
        public const string InstanceId = ModuleFolder;
        public const string DisplayName = "Kaoses Tweaks";
        public const string FormatType = "json";
        public const string LogPath = @"..\\..\\Modules\\" + ModuleFolder + "\\KaosLog.txt";
        public const string ConfigFilePath = @"..\\..\\Modules\\" + ModuleFolder + "\\config.json";
        public static string PrePrend { get; set; } = DisplayName;
        public const string HarmonyId = ModuleFolder + ".harmony";
        public static readonly string GameVersion = ApplicationVersionHelper.GameVersionStr();
        public static readonly string ModVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        public const bool UsesHarmony = true;

        #region MCMConfigValues
        public static string? MCMConfigFolder { get; set; }
        public static string? MCMConfigFile { get; set; }
        public static bool MCMConfigFileExists { get; set; } = false;
        public static bool MCMModuleLoaded { get; set; } = false;
        public static bool ModConfigFileExists { get; set; } = false;
        #endregion


        //~DEBUG
        //public static bool Debug { get; set; } = true;
        //public static bool LogToFile { get; set; } = true;
    }
}