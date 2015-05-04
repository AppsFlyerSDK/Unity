//
//  AppsFlyerConversionConnectionDelegate.h
//  AppsFlyerLib
//
//  Created by Gil Meroz on 6/20/13.
//
//

#import <Foundation/Foundation.h>
#import "AppsFlyerTracker.h"

#define kAppsFlyerConversiondataKey  @"AppsFlyerConversiondataKey"
#define kAppsFlyerErrorDomain       @"com.appsflyer.ErrorDomain"


@interface AppsFlyerConversionConnectionDelegate : NSObject{
    id<AppsFlyerTrackerDelegate> conversionDelegate;
    NSMutableData *_responseData;
    BOOL isDebug;
    int httpStatusCode;

}

@property BOOL isDebug;
@property (nonatomic,assign) id<AppsFlyerTrackerDelegate> conversionDelegate;

@end
