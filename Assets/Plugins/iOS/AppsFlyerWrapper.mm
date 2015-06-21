//
//  AppsFlyerWarpper.m
//
//
//  Created by AppsFlyer 2013
//
//

#import "AppsFlyerWrapper.h"
#import "AppsFlyerTracker.h"
#import "AppsFlyerDelegate.h"

@interface AppsFlyerWarpper ()

@end


@implementation AppsFlyerWarpper

extern "C" {
    
    const void mTrackEvent(const char *eventName,const char *eventValue){
        NSString *name = [NSString stringWithUTF8String:eventName];
        NSString *value = [NSString stringWithUTF8String:eventValue];
        [[AppsFlyerTracker sharedTracker] trackEvent:name withValue:value];
        
    }
    
    const void mTrackRichEvent(const char *eventName, const char *eventValues){
        NSString *name = [NSString stringWithUTF8String:eventName];
    
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
        NSString *code = [NSString stringWithUTF8String:currencyCode];
        [[AppsFlyerTracker sharedTracker] setCurrencyCode:code];
        
    }
    
    const void mSetCustomerUserID(const char *customerUserID){
        NSString *customerUserIDString = [NSString stringWithUTF8String:customerUserID];
        [[AppsFlyerTracker sharedTracker] setCustomerUserID:customerUserIDString];
        
    }
    
    const void mSetAppsFlyerDevKey(const char *devKey){
        NSString *devKeyString = [NSString stringWithUTF8String:devKey];
        [AppsFlyerTracker sharedTracker].appsFlyerDevKey = devKeyString;
    }
    
    const void mTrackAppLaunch() {
        [[AppsFlyerTracker sharedTracker] trackAppLaunch];
    }
    
    const void mSetAppID(const char *appleAppID){
        NSString *appleAppIDString = [NSString stringWithUTF8String:appleAppID];
        [AppsFlyerTracker sharedTracker].appleAppID = appleAppIDString;
    }
    
    const void mValidateReceipt(const char * eventName, const char *failedEventName, const char *eventValue, const char *productIdentifier,  double price, const char *currency) {
        NSString *eventNameString = [NSString stringWithUTF8String:eventName];
        NSString *failedEventNameString = [NSString stringWithUTF8String:failedEventName];
        NSString *eventValueString = [NSString stringWithUTF8String:eventValue];
        NSString *productIdentifierString = [NSString stringWithUTF8String:productIdentifier];
        NSString *currencyString = [NSString stringWithUTF8String:currency];
        NSDecimalNumber *priceValue = [[NSDecimalNumber alloc] initWithDouble:price];

        [[AppsFlyerTracker sharedTracker] validateAndTrackInAppPurchase:eventNameString eventNameIfFailed:failedEventNameString withValue:eventValueString withProduct:productIdentifierString price:priceValue currency:currencyString success:^(NSDictionary *result){
            NSLog(@"Purcahse succeeded And verified!!! response: %@", result[@"receipt"]);
            UnitySendMessage(UNITY_SENDMESSAGE_CALLBACK_MANAGER, UNITY_SENDMESSAGE_CALLBACK_VALIDATE, [result[@"receipt"] UTF8String]);
        } failure:^(NSError *error, id response) {
            NSLog(@"response = %@", response);
            UnitySendMessage(UNITY_SENDMESSAGE_CALLBACK_MANAGER, UNITY_SENDMESSAGE_CALLBACK_VALIDATE_ERROR, [response[@"error"] UTF8String]);
            
        }];
    }
    
    const void mSetIsDebug(bool isDebug) {
        [AppsFlyerTracker sharedTracker].isDebug = isDebug;
    }
    
    const void mSetIsSandbox(bool isSandbox) {
        [AppsFlyerTracker sharedTracker].useReceiptValidationSandbox = isSandbox;
    }

    
    const void mGetConversionData() {
        [[AppsFlyerTracker sharedTracker] setDelegate:[[AppsFlyerDelegate alloc] init]];
    }
}









@end
