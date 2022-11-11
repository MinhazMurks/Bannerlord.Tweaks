using Bannerlord.BUTR.Shared.Helpers;
using System.IO;
using System.Linq;
using TaleWorlds.Engine;
using Tweaks.Utils;

namespace Tweaks.Settings
{
    class ConfigLoader
    {

        public static void LoadConfig()
        {
            SetSettingsInstance();
            if (Statics._settings is null)
            {
                IM.MessageError("Failed to load any config provider");
            }
            IM.logToFile = Statics._settings.LogToFile;
            IM.Debug = Statics._settings.Debug;
            IM.PrePrend = Statics.PrePrend;
            Logging.PrePrend = Statics.PrePrend;
        }
        
        private static void SetSettingsInstance()
        {
            if (TweaksMCMSettings.Instance is not null)
            {
                Statics._settings = TweaksMCMSettings.Instance;
                IM.MessageDebug("using MCM");
            }
            else
            {
                IM.MessageError("MCM Module is not loaded");
            }
        }
    }
}
