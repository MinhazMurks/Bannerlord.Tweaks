namespace Tweaks
{
	using System;
	using System.Reflection;
	using Settings;

	public static class Statics
	{
		private static TweaksMCMSettings? _settings;
		public const string ModuleFolder = "Tweaks";
		public const string InstanceId = ModuleFolder;
		public const string DisplayName = "Bannerlord Tweaks";
		public const string FormatType = "json";
		public const string LogPath = @"..\\..\\Modules\\" + ModuleFolder + "\\TweaksLog.txt";
		public const string Prefix = DisplayName;
		public const string HarmonyId = ModuleFolder + ".harmony";
		public static readonly string ModVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

		public static TweaksMCMSettings GetSettingsOrThrow()
		{
			if (_settings == null)
			{
				throw new NullReferenceException("Settings Instance should NOT be null!");
			}
			return _settings;
		}

		public static void Init()
		{
			if (Statics.GetSettingsOrThrow() == null)
			{
				throw new NullReferenceException("MCM instance has not been initialized yet");
			}

			_settings = Statics.GetSettingsOrThrow();
		}
	}
}
