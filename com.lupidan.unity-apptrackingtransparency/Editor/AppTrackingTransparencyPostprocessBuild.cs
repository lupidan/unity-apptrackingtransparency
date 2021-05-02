using System.IO;
using AppTrackingTransparency.Editor.Settings;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace AppTrackingTransparency.Editor
{
    public class AppTrackingTransparencyPostprocessBuild : IPostprocessBuildWithReport
    {
        public int callbackOrder => AppTrackingTransparencySettingsManager.LoadSettings().AutomaticPostProcessingCallbackOrder;

        public void OnPostprocessBuild(BuildReport report)
        {
            var summary = report.summary;
            if (summary.platform == BuildTarget.iOS)
            {
                var appTrackingTransparencySettings = AppTrackingTransparencySettingsManager.LoadSettings();
                if (!appTrackingTransparencySettings.AutomaticPostProcessing)
                {
                    return;
                }

                var projectPath = UnityEditor.iOS.Xcode.PBXProject.GetPBXProjectPath(summary.outputPath);
                var pbxProject = new UnityEditor.iOS.Xcode.PBXProject();
                pbxProject.ReadFromFile(projectPath);

#if UNITY_2019_3_OR_NEWER
                var frameworkTargetGuid = pbxProject.GetUnityFrameworkTargetGuid();
                var mainTargetGuid = pbxProject.GetUnityMainTargetGuid();
#else
                var frameworkTargetGuid = pbxProject.GetUnityTargetName();
                var mainTargetGuid = pbxProject.GetUnityTargetName();
#endif

                if (appTrackingTransparencySettings.AddAppTransparencyTrackingFramework)
                {
                    pbxProject.AddFrameworkToProject(frameworkTargetGuid, "AppTrackingTransparency.framework", true);
                }

                if (appTrackingTransparencySettings.AddUserTrackingUsageDescription)
                {
                    string infoPlistPath;
                    if (appTrackingTransparencySettings.AutoDetectInfoPlistFilePath)
                    {
                        infoPlistPath = Path.Combine(
                            summary.outputPath,
                            pbxProject.GetBuildPropertyForAnyConfig(mainTargetGuid, "INFOPLIST_FILE"));
                    }
                    else
                    {
                        infoPlistPath = Path.Combine(
                            summary.outputPath,
                            appTrackingTransparencySettings.MainInfoPlistFilePath);
                    }

                    var infoPlist = new UnityEditor.iOS.Xcode.PlistDocument();
                    infoPlist.ReadFromFile(infoPlistPath);
                    infoPlist.root.SetString("NSUserTrackingUsageDescription", appTrackingTransparencySettings.UserTrackingUsageDescription);
                    infoPlist.WriteToFile(infoPlistPath);
                }

                pbxProject.WriteToFile(projectPath);
            }
        }
    }
}
