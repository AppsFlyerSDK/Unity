//
//  AppsFlyerAppController.m
//
//  Created by Dan Linenberg on 11/09/2016.
//
//

#import "UnityAppController.h"
#import "AppsFlyerTracker.h"

@interface AppsFlyerAppController : UnityAppController
{

}
@end

@implementation AppsFlyerAppController


- (BOOL)application:(UIApplication *)application continueUserActivity:(NSUserActivity *)userActivity restorationHandler:(void (^)(NSArray *))restorationHandler {
    [[AppsFlyerTracker sharedTracker] continueUserActivity:userActivity restorationHandler:restorationHandler];
    return YES;
}

-(BOOL) application:(UIApplication *)application openUrl:(NSURL *)url options:(NSDictionary *)options {
    [[AppsFlyerTracker sharedTracker] handleOpenUrl:url options:options];
    return YES;
}

@end

IMPL_APP_CONTROLLER_SUBCLASS(AppsFlyerAppController)
