using AppTrackingTransparency.Common;

namespace AppTrackingTransparency.Editor
{
    public static class AppTrackingTransparencyEditorPrefs
    {
        /// <summary>
        /// The IDFA value when the user has Not determined or Denied the tracking.
        /// </summary>
        internal const string AnonymousIdfa = "00000000-0000-0000-0000-000000000000";

        private const string IdfaEditorPrefKey = "com.lupidan.unity-apptrackingtransparency.idfa";
        private const string AuthorizationStatusEditorPrefKey = "com.lupidan.unity-apptrackingtransparency.authorizationstatus";
        private const int DefaultAuthorizationStatus = (int)AppTrackingTransparencyAuthorizationStatus.NotDetermined;

        /// <summary>
        /// Mocked IDFA value to be used by the unity editor implementation.
        /// </summary>
        public static string Idfa
        {
            get => UnityEditor.EditorPrefs.GetString(IdfaEditorPrefKey, AnonymousIdfa);
            set => UnityEditor.EditorPrefs.SetString(IdfaEditorPrefKey, value);
        }

        /// <summary>
        /// Mocked Authorization status value for tracking permissions to be used by the unity editor implementation.
        /// </summary>
        public static AppTrackingTransparencyAuthorizationStatus AuthorizationStatus
        {
            get => (AppTrackingTransparencyAuthorizationStatus) UnityEditor.EditorPrefs.GetInt(AuthorizationStatusEditorPrefKey, DefaultAuthorizationStatus);
            set => UnityEditor.EditorPrefs.SetInt(AuthorizationStatusEditorPrefKey, (int) value);
        }

        /// <summary>
        /// Clears all mocked values used by the editor implementation to its default values.
        /// </summary>
        public static void Clear()
        {
            UnityEditor.EditorPrefs.DeleteKey(IdfaEditorPrefKey);
            UnityEditor.EditorPrefs.DeleteKey(AuthorizationStatusEditorPrefKey);
        }
    }
}
