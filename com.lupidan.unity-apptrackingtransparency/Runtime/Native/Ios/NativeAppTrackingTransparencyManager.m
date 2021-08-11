#if __IPHONE_OS_VERSION_MAX_ALLOWED >= 140000  || __TV_OS_VERSION_MAX_ALLOWED >= 140000 || __MAC_OS_X_VERSION_MAX_ALLOWED >= 110000
#define APP_TRACKING_TRANSPARENCY_AVAILABLE true
#import <AppTrackingTransparency/AppTrackingTransparency.h>
#endif

#pragma mark - NativeAppTrackingTransparencyManager

typedef void (*RequestTrackingAuthorizationCallbackDelegate)(uint requestId,  uint authorizationStatus);

@interface NativeAppTrackingTransparencyManager : NSObject
@property (nonatomic, assign) RequestTrackingAuthorizationCallbackDelegate requestTrackingAuthorizationCallback;
@property (nonatomic, weak) NSOperationQueue *callingOperationQueue;
@end

@implementation NativeAppTrackingTransparencyManager

static const uint AppTrackingTransparencyManagerAuthorizationStatusAuthorized = 3;

+ (instancetype) sharedManager {
    static NativeAppTrackingTransparencyManager *_defaultManager = nil;
    static dispatch_once_t defaultManagerInitialization;
    
    dispatch_once(&defaultManagerInitialization, ^{
        _defaultManager = [[NativeAppTrackingTransparencyManager alloc] init];
    });
    
    return _defaultManager;
}

- (uint) rawTrackingAuthorizationStatus {
#ifdef APP_TRACKING_TRANSPARENCY_AVAILABLE
    if (@available(iOS 14.0, tvOS 14.0, macOS 11.0, *)) {
        return (uint)[ATTrackingManager trackingAuthorizationStatus];
    } else {
        return AppTrackingTransparencyManagerAuthorizationStatusAuthorized;
    }
#else
    return AppTrackingTransparencyManagerAuthorizationStatusAuthorized;
#endif
}

- (void) initiateTrackingAuthorizationRequestWithRequestId:(uint)requestId {
#ifdef APP_TRACKING_TRANSPARENCY_AVAILABLE
    if (@available(iOS 14.0, tvOS 14.0, macOS 11.0, *)) {
        [ATTrackingManager requestTrackingAuthorizationWithCompletionHandler:^(ATTrackingManagerAuthorizationStatus status) {
            [self handleTrackingAuthorizationRequestResult:(uint)status forRequestId:requestId];
        }];
    } else {
        [self handleTrackingAuthorizationRequestResult:AppTrackingTransparencyManagerAuthorizationStatusAuthorized forRequestId:requestId];
    }
#else
    [self handleTrackingAuthorizationRequestResult:AppTrackingTransparencyManagerAuthorizationStatusAuthorized forRequestId:requestId];
#endif
    
}

- (void) handleTrackingAuthorizationRequestResult:(uint)rawStatus forRequestId:(uint)requestId {
    if ([self callingOperationQueue]) {
        [[self callingOperationQueue] addOperationWithBlock:^{
            if ([self requestTrackingAuthorizationCallback] != NULL) {
                [self requestTrackingAuthorizationCallback](requestId, rawStatus);
            }
        }];
    } else {
        if ([self requestTrackingAuthorizationCallback] != NULL) {
            [self requestTrackingAuthorizationCallback](requestId, rawStatus);
        }
    }
}

@end

#pragma mark - Native C Calls

uint AppTrackingTransparencyManager_GetTrackingAuthorizationStatus()
{
    return [[NativeAppTrackingTransparencyManager sharedManager] rawTrackingAuthorizationStatus];
}

void AppTrackingTransparencyManager_SetRequestTrackingAuthorizationCallbackHandler(RequestTrackingAuthorizationCallbackDelegate callbackHandler)
{
    [[NativeAppTrackingTransparencyManager sharedManager] setRequestTrackingAuthorizationCallback:callbackHandler];
    [[NativeAppTrackingTransparencyManager sharedManager] setCallingOperationQueue:[NSOperationQueue currentQueue]];
}

void AppTrackingTransparencyManager_RequestTrackingAuthorizationCallback(uint requestId)
{
    [[NativeAppTrackingTransparencyManager sharedManager] initiateTrackingAuthorizationRequestWithRequestId:requestId];
}
