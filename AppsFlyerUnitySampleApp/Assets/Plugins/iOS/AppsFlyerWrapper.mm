//
//  AppsFlyerWarpper.m
//
//
//  Created by AppsFlyer 2013
//
//

#import "AppsFlyerWrapper.h"
#import "AppsFlyerTracker.h"


@implementation AppsFlyerWarpper

extern "C" {
    
    const void mTrackEvent(const char *eventName,const char *eventValue){
        NSString *name = [NSString stringWithFormat:@"%s",eventName];
        NSString *value = [NSString stringWithFormat:@"%s",eventValue];
        [[AppsFlyerTracker sharedTracker] trackEvent:name withValue:value];
        
    }
    
    const void mTrackRichEvent(const char *eventName, const char *eventValues){
        NSString *name = [NSString stringWithFormat:@"%s",eventName];
    
        NSString *attris = [NSString stringWithUTF8String:eventValues];
        
        NSArray *attributesArray = [attris componentsSeparatedByString:@"\n"];
        
        NSMutableDictionary *oAttributes = [[NSMutableDictionary alloc] init];
        for (int i=0; i < [attributesArray count]; i++) {
            NSString *keyValuePair = [attributesArray objectAtIndex:i];
            NSRange range = [keyValuePair rangeOfString:@"="];
            if (range.location != NSNotFound) {
                NSString *key = [keyValuePair substringToIndex:range.location];
                NSString *value = [keyValuePair substringFromIndex:range.location+1];
                [oAttributes setObject:value forKey:key];
            }
        }

        
        [[AppsFlyerTracker sharedTracker] trackEvent:name withValues:oAttributes];

    }
    
    const void mSetCurrencyCode(const char *currencyCode){
        NSString *code = [NSString stringWithFormat:@"%s",currencyCode];
        [[AppsFlyerTracker sharedTracker] setCurrencyCode:code];
        
    }
    
    const void mSetCustomerUserID(const char *customerUserID){
        NSString *customerUserIDString = [NSString stringWithFormat:@"%s",customerUserID];
        [[AppsFlyerTracker sharedTracker] setCustomerUserID:customerUserIDString];
        
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
