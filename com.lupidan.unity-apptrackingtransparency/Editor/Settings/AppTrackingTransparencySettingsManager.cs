using System.IO;
using UnityEngine;

namespace AppTrackingTransparency.Editor.Settings
{
    public static class AppTrackingTransparencySettingsManager
    {
        private const string SharedProjectSettingsFolderName = "ProjectSettings";
        private const string DedicatedProjectSettingsFolderName = "com.lupidan.unity-apptrackingtransparency";
        private const string DedicatedSettingsFileName = "AppTrackingTransparencySettings.json";

        public static readonly string PrintableProjectSettingsFilePath = Path.Combine(
            SharedProjectSettingsFolderName,
            DedicatedProjectSettingsFolderName,
            DedicatedSettingsFileName);

        private static readonly string SettingsFolderPath = Path.Combine(
            Application.dataPath,
            "..",
            SharedProjectSettingsFolderName,
            DedicatedProjectSettingsFolderName);

        private static readonly string SettingsFilePath = Path.Combine(
            SettingsFolderPath,
            DedicatedSettingsFileName);

        public static AppTrackingTransparencySettings LoadOrCreateSettings()
        {
            var settings = LoadSettings();
            if (settings == null)
            {
                settings = new AppTrackingTransparencySettings();
                settings.SettingsFileVersion = 1;
                settings.AutomaticPostProcessing = true;
                settings.AutomaticPostProcessingCallbackOrder = 10;
                settings.AddAppTransparencyTrackingFramework = true;
                settings.AddUserTrackingUsageDescription = true;
                settings.UserTrackingUsageDescription = "Your data will be used to deliver personalized ads to you";
                settings.AutoDetectInfoPlistFilePath = true;
                settings.MainInfoPlistFilePath = "Info.plist";
                WriteSettings(settings);
            }

            return settings;
        }

        public static AppTrackingTransparencySettings LoadSettings()
        {
            if (!File.Exists(SettingsFilePath))
            {
                return null;
            }

            return JsonUtility.FromJson<AppTrackingTransparencySettings>(File.ReadAllText(SettingsFilePath));
        }

        public static void WriteSettings(AppTrackingTransparencySettings settings)
        {
            var settingsFolder = SettingsFolderPath;
            if (!Directory.Exists(settingsFolder))
            {
                Directory.CreateDirectory(settingsFolder);
            }

            File.WriteAllText(SettingsFilePath, JsonUtility.ToJson(settings, true));
        }

        public static void DeleteSettings()
        {
            if (File.Exists(SettingsFilePath))
            {
                File.Delete(SettingsFilePath);
            }
        }
    }
}
