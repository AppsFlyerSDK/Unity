//
//  mFlurryWrapper.m
//
//
//  Created by AppsFlyer 2013
//
//

#import "AppsFlyerWrapper.h"
#import "AppsFlyerTracker.h"

@implementation AppsFlyerWarpper

extern "C" {
    const void initSession(const char *appsFlyerID,const char *appID){
        NSString *afID = [NSString stringWithFormat:@"%s",appsFlyerID];
        NSString *appleAppId = [NSString stringWithFormat:@"%s",appID];
        [[AppsFlyerTracker sharedTracker] init:afID appleAppId:appleAppId];
        [[AppsFlyerTracker sharedTracker] trackAppLaunch];
        
    }
    
    const void mTrackEvent(const char *eventName,const char *eventValue){
        NSString *name = [NSString stringWithFormat:@"%s",eventName];
        NSString *value = [NSString stringWithFormat:@"%s",eventValue];
        [[AppsFlyerTracker sharedTracker] trackEvent:name withValue:value];
        
    }
    
    
    
}

@end
