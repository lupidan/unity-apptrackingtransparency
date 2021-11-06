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

        public static AppTrackingTransparencySettings LoadSettings()
        {
            if (!File.Exists(SettingsFilePath))
            {
                var defaultSettings = new AppTrackingTransparencySettings();
                defaultSettings.SettingsFileVersion = 1;
                defaultSettings.AutomaticPostProcessing = true;
                defaultSettings.AutomaticPostProcessingCallbackOrder = 10;
                defaultSettings.AddAppTransparencyTrackingFramework = true;
                defaultSettings.AddUserTrackingUsageDescription = true;
                defaultSettings.UserTrackingUsageDescription = "Your data will be used to deliver personalized ads to you";
                defaultSettings.AutoDetectInfoPlistFilePath = true;
                defaultSettings.MainInfoPlistFilePath = "Info.plist";
                return defaultSettings;
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
