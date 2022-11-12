namespace Tweaks.Settings
{
	using Utils;

	internal class ConfigLoader
	{

		public static void LoadConfig()
		{
			SetSettingsInstance();
			if (Statics._settings is null)
			{
				MessageUtil.MessageError("Failed to load any config provider");
			}
			MessageUtil.logToFile = Statics._settings.LogToFile;
			MessageUtil.Debug = Statics._settings.Debug;
			MessageUtil.PrePend = Statics.PrePrend;
		}

		private static void SetSettingsInstance()
		{
			if (TweaksMCMSettings.Instance is not null)
			{
				Statics._settings = TweaksMCMSettings.Instance;
				MessageUtil.MessageDebug("using MCM");
			}
			else
			{
				MessageUtil.MessageError("MCM Module is not loaded");
			}
		}
	}
}
