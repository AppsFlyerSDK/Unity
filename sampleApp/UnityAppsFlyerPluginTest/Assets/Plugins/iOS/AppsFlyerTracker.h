//
//  AppsFlyerTracker.h
//  AppsFlyerLib
//
//  AppsFlyer iOS SDK v2.5.3.6
//  6-Feb-2013
//  Copyright (c) 2013 AppsFlyer Ltd. All rights reserved.
//

#import <Foundation/Foundation.h>

/*
 This delegate should be use if you want to use AppsFlyer conversion data
 */
@protocol AppsFlyerTrackerDelegate <NSObject>

@optional
- (void) onConversionDataReceived:(NSDictionary*) installData;
- (void) onConversionDataRequestFailure:(NSError *)error;

@end

@interface AppsFlyerTracker : NSObject<AppsFlyerTrackerDelegate> {
    NSString* customerUserID;
    NSString* appsFlyerDevKey;
    NSString* appleAppID;
    NSString* currencyCode;
    BOOL deviceTrackingDisabled;
    id<AppsFlyerTrackerDelegate> appsFlyerDelegate;
    
    NSTimeInterval entryTime;
    
    BOOL isDebug;
    
    BOOL isHTTPS;
    
    BOOL disableAppleAdSupportTracking;
}

@property (nonatomic,retain) NSString *customerUserID;
@property (nonatomic,retain) NSString *appsFlyerDevKey;
@property (nonatomic,retain) NSString *appleAppID;
@property (nonatomic,retain) NSString *currencyCode;
@property BOOL isHTTPS;

/* 
AppsFLyer SDK collect Apple's advertisingIdentifier if the AdSupport framework included in the SDK. You can disable this behavior by setting the following property to YES.
*/
@property BOOL disableAppleAdSupportTracking;

@property BOOL isDebug;
/*
Opt-out tracking for specific user
*/
@property BOOL deviceTrackingDisabled;

+(AppsFlyerTracker*) sharedTracker;

- (void) trackAppLaunch;
- (void) trackEvent:(NSString*)eventName withValue:(NSString*)value;
- (NSString *) getAppsFlyerUID;
- (void) loadConversionDataWithDelegate:(id<AppsFlyerTrackerDelegate>) delegate;

@end
