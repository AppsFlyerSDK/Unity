//
//  AppsFlyerTracker.h
//  AppsFlyerLib
//
//  Created by Gil Meroz on 11/7/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface AppsFlyerTracker : NSObject {
    NSString* appUID;
    NSString* deviceUDID;
    NSString* appsFlyerKey;
    NSString* appleAppID;
    NSString* currencyCode;
    BOOL isDebug;
    BOOL trackMACAddress;
}

@property (nonatomic,retain) NSString *appUID;
@property (nonatomic,retain) NSString *deviceUDID;
@property (nonatomic,retain) NSString *appsFlyerKey;
@property (nonatomic,retain) NSString *appleAppID;
@property (nonatomic,retain) NSString *currencyCode;
@property BOOL isDebug;
@property BOOL trackMACAddress;

+(AppsFlyerTracker*) sharedTracker;

- (void) init:(NSString*)appsFlyerID appleAppId:(NSString*)appID;
- (void) trackAppLaunch;
- (void) trackInAppPurchase:(NSString*)product withPrice:(float)price;
- (void) trackEvent:(NSString*)eventName withValue:(NSString*)value;
- (NSString *) getAppsFlyerUID;

@end
