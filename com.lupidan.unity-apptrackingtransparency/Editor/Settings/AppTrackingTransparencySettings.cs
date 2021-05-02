using System;
using UnityEngine;

namespace AppTrackingTransparency.Editor.Settings
{
    [Serializable]
    public class AppTrackingTransparencySettings
    {
        public int SettingsFileVersion
        {
            get => this.m_SettingsFileVersion;
            set => this.m_SettingsFileVersion = value;
        }

        public bool AutomaticPostProcessing
        {
            get => this.m_AutomaticPostProcessing;
            set => this.m_AutomaticPostProcessing = value;
        }

        public int AutomaticPostProcessingCallbackOrder
        {
            get => this.m_AutomaticPostProcessingCallbackOrder;
            set => this.m_AutomaticPostProcessingCallbackOrder = value;
        }

        public bool AddAppTransparencyTrackingFramework
        {
            get => this.m_AddAppTransparencyTrackingFramework;
            set => this.m_AddAppTransparencyTrackingFramework = value;
        }

        public bool AddUserTrackingUsageDescription
        {
            get => this.m_AddUserTrackingUsageDescription;
            set => this.m_AddUserTrackingUsageDescription = value;
        }

        public string UserTrackingUsageDescription
        {
            get => this.m_UserTrackingUsageDescription;
            set => this.m_UserTrackingUsageDescription = value;
        }

        public bool AutoDetectInfoPlistFilePath
        {
            get => this.m_AutoDetectInfoPlistFilePath;
            set => this.m_AutoDetectInfoPlistFilePath = value;
        }

        public string MainInfoPlistFilePath
        {
            get => this.m_MainInfoPlistFilePath;
            set => this.m_MainInfoPlistFilePath = value;
        }

        [SerializeField] private int m_SettingsFileVersion;
        [SerializeField] private bool m_AutomaticPostProcessing;
        [SerializeField] private int m_AutomaticPostProcessingCallbackOrder;
        [SerializeField] private bool m_AddAppTransparencyTrackingFramework;
        [SerializeField] private bool m_AddUserTrackingUsageDescription;
        [SerializeField] private string m_UserTrackingUsageDescription;
        [SerializeField] private bool m_AutoDetectInfoPlistFilePath;
        [SerializeField] private string m_MainInfoPlistFilePath;
    }
}
