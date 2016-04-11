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


static AppsFlyerDelegate *mAppsFlyerdelegate;

@interface AppsFlyerWarpper () {
}

@end

@implementation AppsFlyerWarpper

+(AppsFlyerDelegate *) getAppsFlyerDelegate {
    
    if (mAppsFlyerdelegate == nil) {
        mAppsFlyerdelegate = [[AppsFlyerDelegate alloc] init];
        
    }
    return mAppsFlyerdelegate;
}



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
    
    const void mValidateReceipt(const char *productIdentifier,  const char *price, const char *currency, const char *transactionId ,const char *additionalParams) {
        
        NSString *productIdentifierString = [NSString stringWithUTF8String:productIdentifier];
        NSString *currencyString = [NSString stringWithUTF8String:currency];
        NSString *priceValue = [NSString stringWithUTF8String:price];
        NSString *transactionIdString = [NSString stringWithUTF8String:transactionId];
        
        NSString *attris = [NSString stringWithUTF8String:additionalParams];
        NSArray *attributesArray = [attris componentsSeparatedByString:@"\n"];
        
        NSMutableDictionary *customParams = [[NSMutableDictionary alloc] init];
        for (int i=0; i < [attributesArray count]; i++) {
            NSString *keyValuePair = [attributesArray objectAtIndex:i];
            NSRange range = [keyValuePair rangeOfString:@"="];
            if (range.location != NSNotFound) {
                NSString *key = [keyValuePair substringToIndex:range.location];
                NSString *value = [keyValuePair substringFromIndex:range.location+1];
                [customParams setObject:value forKey:key];
            }
        }

        
        [[AppsFlyerTracker sharedTracker] validateAndTrackInAppPurchase:productIdentifierString price:priceValue currency:currencyString transactionId:transactionIdString additionalParameters:customParams success:^(NSDictionary *result){
            
            NSLog(@"Purcahse succeeded And verified!!!");
            
            NSData *jsonData;
            if (result[@"receipt"] != nil) {
                NSError *jsonError;
                jsonData = [NSJSONSerialization dataWithJSONObject:result[@"receipt"]
                                                               options:0
                                                                 error:&jsonError];
            }
            
            if (!jsonData) {
                NSLog(@"JSON parse error");
                UnitySendMessage(UNITY_SENDMESSAGE_CALLBACK_MANAGER, UNITY_SENDMESSAGE_CALLBACK_VALIDATE_ERROR, [@"Invalid Response" UTF8String]);
            }
            else {
                
                NSString *JSONString = [[NSString alloc] initWithBytes:[jsonData bytes] length:[jsonData length] encoding:NSUTF8StringEncoding];
                UnitySendMessage(UNITY_SENDMESSAGE_CALLBACK_MANAGER, UNITY_SENDMESSAGE_CALLBACK_VALIDATE, [JSONString UTF8String]);
                
            }
        } failure:^(NSError *error, id response) {
            NSLog(@"response = %@", response);
            NSString *errorString;
            if ([response objectForKey:@"error"] != nil){
                errorString = response[@"error"];
            }
            else if ([response objectForKey:@"status"] != nil) {
                errorString = [NSString stringWithFormat:@"Error code = %@", response[@"status"]];
            }
            else {
                errorString = @"Unknown Error";
            }
            
            UnitySendMessage(UNITY_SENDMESSAGE_CALLBACK_MANAGER, UNITY_SENDMESSAGE_CALLBACK_VALIDATE_ERROR, [errorString UTF8String]);
            
        }];
    }
    
    const void mSetIsDebug(bool isDebug) {
        [AppsFlyerTracker sharedTracker].isDebug = isDebug;
    }
    
    const void mSetIsSandbox(bool isSandbox) {
        [AppsFlyerTracker sharedTracker].useReceiptValidationSandbox = isSandbox;
    }

    
    const void mGetConversionData() {
        [[AppsFlyerTracker sharedTracker] setDelegate:[AppsFlyerWarpper getAppsFlyerDelegate]];
    }
    

    const void mHandleOpenUrl(const char *url, const char *sourceApplication, const char *annotation) {
        [[AppsFlyerTracker sharedTracker] handleOpenURL:[NSURL URLWithString:[NSString stringWithUTF8String:url]] sourceApplication:[NSString stringWithUTF8String:sourceApplication] withAnnotation:[NSString stringWithUTF8String:annotation]];
    }
    
    const void mHandlePushNotification(const char *payloadData) {
      
        NSString *attris = [NSString stringWithUTF8String:payloadData];
        NSArray *attributesArray = [attris componentsSeparatedByString:@"\n"];
        
        NSMutableDictionary *pushPayloadDict = [[NSMutableDictionary alloc] init];
        for (int i=0; i < [attributesArray count]; i++) {
            NSString *keyValuePair = [attributesArray objectAtIndex:i];
            NSRange range = [keyValuePair rangeOfString:@"="];
            if (range.location != NSNotFound) {
                NSString *key = [keyValuePair substringToIndex:range.location];
                NSString *value = [keyValuePair substringFromIndex:range.location+1];
                [pushPayloadDict setObject:value forKey:key];
            }
        }
        [[AppsFlyerTracker sharedTracker] handlePushNotification:pushPayloadDict];
        
    }

    
    char* cStringCopy(const char* string)
    {
        if (string == NULL)
            return NULL;
        
        char* res = (char*)malloc(strlen(string) + 1);
        strcpy(res, string);
        
        return res;
    }
    
    const char *mGetAppsFlyerId () {
        NSString *afid = [[AppsFlyerTracker sharedTracker] getAppsFlyerUID];
        return cStringCopy([afid UTF8String]);
    }
    

}

@end
