//
//  AppsFlyerDelegate.h
//  
//
//  Created by Golan on 6/21/15.
//
//

#import <Foundation/Foundation.h>
#import "AppsFlyerTracker.h"


static const char * UNITY_SENDMESSAGE_CALLBACK_MANAGER = "AppsFlyerTrackerCallbacks";

// corresponds to the AppsFlyers Validate purchase Delegate
static const char * UNITY_SENDMESSAGE_CALLBACK_VALIDATE = "didFinishValidateReceipt";
static const char * UNITY_SENDMESSAGE_CALLBACK_VALIDATE_ERROR = "didFinishValidateReceiptWithError";

// corresponds to the AppsFlyers Conversions Delegate
static const char * UNITY_SENDMESSAGE_CALLBACK_CONVERSION = "didReceiveConversionData";
static const char * UNITY_SENDMESSAGE_CALLBACK_CONVERSION_ERROR = "didReceiveConversionDataWithError";
static const char * UNITY_SENDMESSAGE_CALLBACK_RETARGETTING = "onAppOpenAttribution";
static const char * UNITY_SENDMESSAGE_CALLBACK_RETARGETTING_ERROR = "onAppOpenAttributionFailure";

@interface AppsFlyerDelegate : NSObject <AppsFlyerTrackerDelegate>

@end
