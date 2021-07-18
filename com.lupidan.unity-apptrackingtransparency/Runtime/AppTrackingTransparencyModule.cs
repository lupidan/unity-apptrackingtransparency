#if UNITY_IOS || UNITY_TVOS
#define PLATFORM_SUPPORTS_APPTRACKINGTRANSPARENCY
#endif

using AppTrackingTransparency.Common;

namespace AppTrackingTransparency
{
    public static class AppTrackingTransparencyModule
    {
        public static bool IsSupported
        {
            get
            {
                #if PLATFORM_SUPPORTS_APPTRACKINGTRANSPARENCY
                    return true;
                #else
                    return false;
                #endif
            }
        }

        public static IAppTrackingTransparencyManager CreateManager()
        {
            #if PLATFORM_SUPPORTS_APPTRACKINGTRANSPARENCY
                #if UNITY_EDITOR
                    return new Editor.EditorAppTrackingTransparencyManager();
                #else
                    return new Native.NativeAppTrackingTransparencyManager();
                #endif
            #else
                throw new System.Exception("Unsupported platform");
            #endif
        }
    }
}
