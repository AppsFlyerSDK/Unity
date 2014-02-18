//
//  mFlurryWrapper.m
//
//
//  Created by AppsFlyer 2013
//
//

#import "AppsFlyerWrapper.h"
#import "AppsFlyerTracker.h"
#import "AppsFlyerConversionDelegate.h"

@implementation AppsFlyerWarpper

extern "C" {
    const void initSession(const char *appsFlyerID,const char *appID){
        NSString *afID = [NSString stringWithFormat:@"%s",appsFlyerID];
        NSString *appleAppId = [NSString stringWithFormat:@"%s",appID];
        [AppsFlyerTracker sharedTracker].appleAppID = appleAppId;
        [AppsFlyerTracker sharedTracker].appsFlyerDevKey = afID;
        [[AppsFlyerTracker sharedTracker] trackAppLaunch];
        
    }
    
    const void mTrackEvent(const char *eventName,const char *eventValue){
        NSString *name = [NSString stringWithFormat:@"%s",eventName];
        NSString *value = [NSString stringWithFormat:@"%s",eventValue];
        [[AppsFlyerTracker sharedTracker] trackEvent:name withValue:value];
        
    }
    
    const void mSetCurrencyCode(const char *currencyCode){
        NSString *code = [NSString stringWithFormat:@"%s",currencyCode];
        [[AppsFlyerTracker sharedTracker] setCurrencyCode:code];
        
    }
    
    const void mSetCustomerUserID(const char *customerUserID){
        NSString *customerUserIDString = [NSString stringWithFormat:@"%s",customerUserID];
        [[AppsFlyerTracker sharedTracker] setCustomerUserID:customerUserIDString];
        
    }
    
    const void mLoadConversionData(const char *callbackObjectName, const char *callbackMethodName) {
        NSString *object = [NSString stringWithFormat:@"%s",callbackObjectName];
        NSString *method = [NSString stringWithFormat:@"%s",callbackMethodName];
        id<AppsFlyerTrackerDelegate> delegate = [[AppsFlyerConversionDelegate alloc] initWithObjectName:object method:method];
        
        [[AppsFlyerTracker sharedTracker] loadConversionDataWithDelegate:delegate];
    }
    
}

@end
