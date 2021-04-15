namespace AppTrackingTransparency.Common
{
    /// <summary>
    /// The status values for app tracking authorization.
    /// </summary>
    /// <remarks>
    /// https://developer.apple.com/documentation/apptrackingtransparency/attrackingmanagerauthorizationstatus
    /// </remarks>
    public enum AppTrackingTransparencyAuthorizationStatus
    {
        /// <summary>
        /// The value returned if a user has not yet received a request to authorize access to app-related data that can be used for tracking the user or the device.
        /// </summary>
        NotDetermined = 0,

        /// <summary>
        /// The value returned if authorization to access app-related data that can be used for tracking the user or the device is restricted.
        /// </summary>
        Restricted = 1,

        /// <summary>
        /// The value returned if the user denies authorization to access app-related data that can be used for tracking the user or the device.
        /// </summary>
        Denied = 2,

        /// <summary>
        /// The value returned if the user authorizes access to app-related data that can be used for tracking the user or the device.
        /// </summary>
        Authorized = 3,
    }
}
