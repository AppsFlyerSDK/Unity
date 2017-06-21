//
//  AppsFlyerAppController.mm
//
//  Created by Shachar Aharon on 15/05/2017.
//
//

#import "UnityAppController.h"
#import "AppsFlyerTracker.h"
#import "AppsFlyerDelegate.h"
#import "AppDelegateListener.h"

@interface AppsFlyerAppController : UnityAppController <AppDelegateListener>
{
    BOOL didEnteredBackGround;
}
@end

@implementation AppsFlyerAppController

- (instancetype)init
{
    NSLog(@"initializing AppsFlyerAppController");
    self = [super init];
	if (self) {
        UnityRegisterAppDelegateListener(self);
	}
    return self;
}

- (BOOL)application:(UIApplication *)application continueUserActivity:(NSUserActivity *)userActivity restorationHandler:(void (^)(NSArray *))restorationHandler {
    [[AppsFlyerTracker sharedTracker] continueUserActivity:userActivity restorationHandler:restorationHandler];
    return YES;
}

-(BOOL) application:(UIApplication *)application openUrl:(NSURL *)url options:(NSDictionary *)options {
    NSLog(@"got openUrl: %@",url);
    [[AppsFlyerTracker sharedTracker] handleOpenUrl:url options:options];
    return YES;
}


// AppDelegateListener protocol

- (void)onOpenURL:(NSNotification*)notification {
    NSLog(@"got onOpenURL = %@", notification.userInfo);
    NSURL *url = notification.userInfo[@"url"];
    NSString *sourceApplication = notification.userInfo[@"sourceApplication"];
    
    if (sourceApplication == nil) {
        sourceApplication = @"";
    }
    
    if (url != nil) {
        [[AppsFlyerTracker sharedTracker] handleOpenURL:url sourceApplication:sourceApplication withAnnotation:nil];
    }
    
}

- (void)didReceiveRemoteNotification:(NSNotification*)notification {
    NSLog(@"got didReceiveRemoteNotification = %@", notification.userInfo);
    [[AppsFlyerTracker sharedTracker] handlePushNotification:notification.userInfo];
}

// LifeCycleListener protocol

- (void)didFinishLaunching:(NSNotification*)notification {
    NSLog(@"got didFinishLaunching = %@",notification.userInfo);
    if (notification.userInfo[@"url"]) {
       [self onOpenURL:notification];
    }
}

-(void)didBecomeActive:(NSNotification*)notification {
    
    NSLog(@"got didBecomeActive(out) = %@", notification.userInfo);
    if (didEnteredBackGround == YES) {
        NSLog(@"got didBecomeActive = %@", notification.userInfo);
        [[AppsFlyerTracker sharedTracker] trackAppLaunch];
        didEnteredBackGround = NO;
        
    }

}

- (void)willResignActive:(NSNotification*)notification {
    NSLog(@"got willResignActive = %@", notification.userInfo);
    
}

- (void)didEnterBackground:(NSNotification*)notification {
    NSLog(@"got didEnterBackground = %@", notification.userInfo);
    didEnteredBackGround = YES;
}

- (void)willEnterForeground:(NSNotification*)notification {
    NSLog(@"got willEnterForeground = %@", notification.userInfo);
    
}

- (void)willTerminate:(NSNotification*)notification {
    NSLog(@"got willTerminate = %@", notification.userInfo);
   
}

@end

IMPL_APP_CONTROLLER_SUBCLASS(AppsFlyerAppController)
