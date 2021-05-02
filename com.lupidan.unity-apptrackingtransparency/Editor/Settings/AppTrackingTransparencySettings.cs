using System;
using UnityEngine;

namespace AppTrackingTransparency.Editor.Settings
{
    [Serializable]
    public class AppTrackingTransparencySettings
    {
        public int SettingsFileVersion
        {
            get => m_SettingsFileVersion;
            set => m_SettingsFileVersion = value;
        }

        public bool AutomaticPostProcessing
        {
            get => m_AutomaticPostProcessing;
            set => m_AutomaticPostProcessing = value;
        }

        public int AutomaticPostProcessingCallbackOrder
        {
            get => m_AutomaticPostProcessingCallbackOrder;
            set => m_AutomaticPostProcessingCallbackOrder = value;
        }

        public bool AddAppTransparencyTrackingFramework
        {
            get => m_AddAppTransparencyTrackingFramework;
            set => m_AddAppTransparencyTrackingFramework = value;
        }

        public bool AutoDetectInfoPlistFilePath
        {
            get => m_AutoDetectInfoPlistFilePath;
            set => m_AutoDetectInfoPlistFilePath = value;
        }

        public string MainInfoPlistFilePath
        {
            get => m_MainInfoPlistFilePath;
            set => m_MainInfoPlistFilePath = value;
        }

        public string UserTrackingUsageDescription
        {
            get => m_UserTrackingUsageDescription;
            set => m_UserTrackingUsageDescription = value;
        }

        [SerializeField] private int m_SettingsFileVersion;
        [SerializeField] private bool m_AutomaticPostProcessing;
        [SerializeField] private int m_AutomaticPostProcessingCallbackOrder;
        [SerializeField] private bool m_AddAppTransparencyTrackingFramework;
        [SerializeField] private bool m_AutoDetectInfoPlistFilePath;
        [SerializeField] private string m_MainInfoPlistFilePath;
        [SerializeField] private string m_UserTrackingUsageDescription;
    }
}
