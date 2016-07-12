//
//  AppsFlyerDelegate.m
//  
//
//  Created by Golan on 6/21/15.
//
//

#import "AppsFlyerDelegate.h"


@interface AppsFlyerDelegate() {
    BOOL didEnteredBackGround;
}

@end

@implementation AppsFlyerDelegate

- (instancetype)init
{
    self = [super init];
    if (self) {
    
        UnityRegisterAppDelegateListener(self);
        UnityRegisterLifeCycleListener(self);
    }
    return self;
}


- (void) onConversionDataReceived:(NSDictionary*) installData {
    
    NSString *conversionDataResult = [self getJsonStringFromDictionary:installData];
    NSLog (@"AppsFlyerDelegate onConversionDataReceived = %@", conversionDataResult);
    UnitySendMessage(UNITY_SENDMESSAGE_CALLBACK_MANAGER, UNITY_SENDMESSAGE_CALLBACK_CONVERSION, [conversionDataResult UTF8String]);
}

- (void) onConversionDataRequestFailure:(NSError *)error {
    
    NSString *errDesc = [error localizedDescription];
    NSLog (@"AppsFlyerDelegate onConversionDataRequestFailure = %@", errDesc);
    UnitySendMessage(UNITY_SENDMESSAGE_CALLBACK_MANAGER, UNITY_SENDMESSAGE_CALLBACK_CONVERSION_ERROR, [errDesc UTF8String]);
}

- (void) onAppOpenAttribution:(NSDictionary*) attributionData {
    NSString *attrData = [self getJsonStringFromDictionary:attributionData];
    NSLog (@"AppsFlyerDelegate onAppOpenAttribution = %@", attrData);
    UnitySendMessage(UNITY_SENDMESSAGE_CALLBACK_MANAGER, UNITY_SENDMESSAGE_CALLBACK_RETARGETTING, [attrData UTF8String]);
}

- (void) onAppOpenAttributionFailure:(NSError *)error{
    NSString *errDesc = [error localizedDescription];
    NSLog (@"AppsFlyerDelegate onAppOpenAttributionFailure = %@", errDesc);
    UnitySendMessage(UNITY_SENDMESSAGE_CALLBACK_MANAGER, UNITY_SENDMESSAGE_CALLBACK_RETARGETTING_ERROR, [errDesc UTF8String]);
    
}


- (void)onOpenURL:(NSNotification*)notification {
    NSLog(@"got onOpenURL = %@", notification.userInfo);
    NSURL *url = notification.userInfo[@"url"];
    NSString *sourceApplication = notification.userInfo[@"sourceApplication"];
    
    if (url != nil) {
        [[AppsFlyerTracker sharedTracker] handleOpenURL:url sourceApplication:sourceApplication withAnnotation:nil];
    }
    
}

- (void)didReceiveRemoteNotification:(NSNotification*)notification {
    NSLog(@"got didReceiveRemoteNotification = %@", notification.userInfo);
    [[AppsFlyerTracker sharedTracker] handlePushNotification:notification.userInfo];
}

-(void)didBecomeActive:(NSNotification*)notification {
    
    if (didEnteredBackGround == YES) {
        NSLog(@"got didBecomeActive = %@", notification.userInfo);
        [[AppsFlyerTracker sharedTracker] trackAppLaunch];
        didEnteredBackGround = NO;

    }

}

- (void)didEnterBackground:(NSNotification*)notification {
    NSLog(@"got didEnterBackground = %@", notification.userInfo);
    didEnteredBackGround = YES;
}


- (NSString *) getJsonStringFromDictionary:(NSDictionary *)dict {
    NSError *error;
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:dict
                                                       options:0
                                                         error:&error];
    
    if (!jsonData) {
        NSLog(@"JSON error: %@", error);
        return nil;
    } else {
        
        NSString *JSONString = [[NSString alloc] initWithBytes:[jsonData bytes] length:[jsonData length] encoding:NSUTF8StringEncoding];
        NSLog(@"JSON OUTPUT: %@", JSONString);
        return JSONString;
    }
}


@end
