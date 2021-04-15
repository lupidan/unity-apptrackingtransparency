using System;

namespace AppTrackingTransparency.Common
{
    public interface IAppTrackingTransparencyManager
    {
        /// <summary>
        /// The UUID that is specific to a device.
        /// </summary>
        /// <remarks>
        /// https://developer.apple.com/documentation/adsupport/asidentifiermanager/1614151-advertisingidentifier
        /// </remarks>
        string Idfa { get; }

        /// <summary>
        /// The authorization status that is current for the calling application.
        /// </summary>
        /// <remarks>
        /// https://developer.apple.com/documentation/apptrackingtransparency/attrackingmanager/3547038-trackingauthorizationstatus
        /// </remarks>
        AppTrackingTransparencyAuthorizationStatus TrackingAuthorizationStatus { get; }

        /// <summary>
        /// RequestTrackingAuthorization is the one-time request to authorize or deny access to app-related data that can be used for tracking the user or the device. The system remembers the user’s choice and doesn't prompt again unless a user uninstalls and then re-installs the app on the device.
        /// </summary>
        /// <param name="completion">Completion block called when the user finishes interacting with the request prompt for tracking.</param>
        /// <remarks>
        /// https://developer.apple.com/documentation/apptrackingtransparency/attrackingmanager/3547037-requesttrackingauthorizationwith
        /// </remarks>
        void RequestTrackingAuthorization(Action<AppTrackingTransparencyAuthorizationStatus> completion);

        /// <summary>
        /// Updates the manager to execute pending callbacks from native code.
        /// </summary>
        void Update();
    }
}
