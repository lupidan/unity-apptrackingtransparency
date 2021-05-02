using System;
using System.Collections.Generic;
using System.Globalization;
using AppTrackingTransparency.Common;
using AppTrackingTransparency.Editor.Settings;
using UnityEditor;
using UnityEngine;

namespace AppTrackingTransparency.Editor
{
    public class EditorAppTrackingTransparencyManager : IAppTrackingTransparencyManager
    {
        private readonly List<Action> _pendingCallbacks = new List<Action>();

        public string Idfa => GetIdfa();

        public AppTrackingTransparencyAuthorizationStatus TrackingAuthorizationStatus => AppTrackingTransparencyEditorPrefs.AuthorizationStatus;

        public void RequestTrackingAuthorization(Action<AppTrackingTransparencyAuthorizationStatus> completion)
        {
            switch (AppTrackingTransparencyEditorPrefs.AuthorizationStatus)
            {
                case AppTrackingTransparencyAuthorizationStatus.NotDetermined:
                {
                    var appTrackingTransparencySettings = AppTrackingTransparencySettingsManager.LoadSettings();
                    var allowTracking = EditorUtility.DisplayDialog(
                        Application.productName + " would like permission to track you across apps and websites owned by other companies",
                        appTrackingTransparencySettings.UserTrackingUsageDescription,
                        "Allow Tracking",
                        "Ask App Not to Track");

                    if (allowTracking)
                    {
                        AppTrackingTransparencyEditorPrefs.AuthorizationStatus = AppTrackingTransparencyAuthorizationStatus.Authorized;
                        AppTrackingTransparencyEditorPrefs.Idfa = Guid.NewGuid().ToString("D").ToUpper(CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        AppTrackingTransparencyEditorPrefs.AuthorizationStatus = AppTrackingTransparencyAuthorizationStatus.Denied;
                    }

                    break;
                }
            }

            this._pendingCallbacks.Add(() => completion(AppTrackingTransparencyEditorPrefs.AuthorizationStatus));
        }

        public void Update()
        {
            while (this._pendingCallbacks.Count > 0)
            {
                var action = this._pendingCallbacks[0];
                this._pendingCallbacks.RemoveAt(0);
                action.Invoke();
            }
        }

        private static string GetIdfa()
        {
            return AppTrackingTransparencyEditorPrefs.AuthorizationStatus == AppTrackingTransparencyAuthorizationStatus.Authorized
                ? AppTrackingTransparencyEditorPrefs.Idfa
                : AppTrackingTransparencyEditorPrefs.AnonymousIdfa;
        }
    }
}
