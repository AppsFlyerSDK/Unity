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
    
    const void mSetAppsFlyerDevKey(const char *devKey){
        NSString *devKeyString = [NSString stringWithFormat:@"%s",devKey];
        [AppsFlyerTracker sharedTracker].appsFlyerDevKey = devKeyString;
    }
    
    const void mTrackAppLaunch() {
        [[AppsFlyerTracker sharedTracker] trackAppLaunch];
    }
    
    const void mSetAppID(const char *appleAppID){
        NSString *appleAppIDString = [NSString stringWithFormat:@"%s",appleAppID];
        [AppsFlyerTracker sharedTracker].appleAppID = appleAppIDString;
    }
    
}

@end
