using System;
using System.Globalization;
using AppTrackingTransparency.Common;
using AppTrackingTransparency.Editor.Settings;
using UnityEditor;
using UnityEngine;

namespace AppTrackingTransparency.Editor
{
    public static class AppTrackingTransparencyEditorTools
    {
        [MenuItem("Assets/AppTrackingTransparency/Configuration")]
        private static void OpenConfiguration()
        {
            var window = (Window)EditorWindow.GetWindow(typeof(Window));
            window.Show();
        }

        [MenuItem("Assets/AppTrackingTransparency/Reset tracking permission")]
        private static void ResetState()
        {
            AppTrackingTransparencyEditorPrefs.Clear();
        }

        public class Window : EditorWindow
        {
            private static string GetAppTrackingTransparencyHint()
            {
#if UNITY_2019_3_OR_NEWER
                return "You need to add AppTransparencyFramework.framework as Optional to the UnityFramework target";
#else
                return "You need to add AppTransparencyFramework.framework as Optional to the Unity target";
#endif
            }

            private static string GetUserTrackingUsageDescriptionHint()
            {
#if UNITY_2019_3_OR_NEWER
                return "You need to add NSUserTrackingUsageDescription to the Info.plist file of the Main target";
#else
                return "You need to add NSUserTrackingUsageDescription to the Info.plist file of the Unity target";
#endif
            }

            private void OnGUI()
            {
                this.titleContent = new GUIContent("App Tracking Transparency");

                GUILayout.Space(10);
                GUILayout.Label("Editor manager status", EditorStyles.boldLabel);
                GUILayout.Space(10);
                GUILayout.Label("The tracking authorization status in the editor.", EditorStyles.miniLabel);
                var authorizationStatus = (AppTrackingTransparencyAuthorizationStatus)EditorGUILayout.EnumPopup("Authorization Status", AppTrackingTransparencyEditorPrefs.AuthorizationStatus);
                AppTrackingTransparencyEditorPrefs.AuthorizationStatus = authorizationStatus;

                GUILayout.Space(10);
                GUILayout.Label("The IDFA returned by the manager in the editor. You can select your own, or randomize it.", EditorStyles.miniLabel);
                GUI.enabled = authorizationStatus == AppTrackingTransparencyAuthorizationStatus.Authorized;
                var storedIdfa = EditorGUILayout.TextField("IDFA", AppTrackingTransparencyEditorPrefs.Idfa);
                if (GUILayout.Button("Random IDFA", new [] {GUILayout.MaxWidth(150)}))
                {
                    storedIdfa = Guid.NewGuid().ToString("D").ToUpper(CultureInfo.InvariantCulture);
                }

                AppTrackingTransparencyEditorPrefs.Idfa = storedIdfa;
                GUI.enabled = true;

                if (GUILayout.Button("Reset status", new [] {GUILayout.MaxWidth(150)}))
                {
                    AppTrackingTransparencyEditorPrefs.Clear();
                }

                var labelWidth = EditorGUIUtility.labelWidth;

                EditorGUIUtility.labelWidth = 250;
                GUILayout.Space(50);
                GUILayout.Label("iOS Build Settings", EditorStyles.boldLabel);
                GUILayout.Space(10);

                var settings = AppTrackingTransparencySettingsManager.LoadSettings();
                GUILayout.Label("Settings file v" + settings.SettingsFileVersion, EditorStyles.miniLabel);
                GUILayout.Label(AppTrackingTransparencySettingsManager.PrintableProjectSettingsFilePath, EditorStyles.miniLabel);

                GUILayout.Space(10);

                settings.AutomaticPostProcessing = EditorGUILayout.Toggle("Automatic postprocessing", settings.AutomaticPostProcessing);
                if (settings.AutomaticPostProcessing)
                {
                    settings.AutomaticPostProcessingCallbackOrder = EditorGUILayout.IntField("Postprocessing Callback Order", settings.AutomaticPostProcessingCallbackOrder);

                    GUILayout.Space(10);
                    settings.AddAppTransparencyTrackingFramework = EditorGUILayout.Toggle("Add AppTrackingTransparency.framework", settings.AddAppTransparencyTrackingFramework);
                    if (!settings.AddAppTransparencyTrackingFramework)
                    {
                        GUILayout.Label(GetAppTrackingTransparencyHint(), EditorStyles.miniLabel);
                    }

                    GUILayout.Space(10);
                    settings.AddUserTrackingUsageDescription = EditorGUILayout.Toggle("Add NSUserTrackingUsageDescription", settings.AddUserTrackingUsageDescription);
                    if (settings.AddUserTrackingUsageDescription)
                    {
                        settings.UserTrackingUsageDescription = EditorGUILayout.TextField("Tracking Usage Description", settings.UserTrackingUsageDescription);
                        settings.AutoDetectInfoPlistFilePath = EditorGUILayout.Toggle("Auto-detect Info.plist file", settings.AutoDetectInfoPlistFilePath);
                        if (!settings.AutoDetectInfoPlistFilePath)
                        {
                            settings.MainInfoPlistFilePath = EditorGUILayout.TextField("Info.plist file path", settings.MainInfoPlistFilePath);
                        }
                    }
                    else
                    {
                        GUILayout.Label(GetUserTrackingUsageDescriptionHint(), EditorStyles.miniLabel);
                    }
                }
                else
                {
                    GUILayout.Label("By disabling automatic post-processing you will need to handle it on your own", EditorStyles.miniLabel);
                    GUILayout.Label("- " + GetAppTrackingTransparencyHint(), EditorStyles.miniLabel);
                    GUILayout.Label("- " + GetUserTrackingUsageDescriptionHint(), EditorStyles.miniLabel);
                }

                AppTrackingTransparencySettingsManager.WriteSettings(settings);

                GUILayout.Space(10);
                if (GUILayout.Button("Reset to default", new [] {GUILayout.MaxWidth(150)}))
                {
                    AppTrackingTransparencySettingsManager.DeleteSettings();
                }
                EditorGUIUtility.labelWidth = labelWidth;
            }
        }
    }
}
