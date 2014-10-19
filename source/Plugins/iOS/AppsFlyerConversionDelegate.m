#import "AppsFlyerConversionDelegate.h"

@implementation AppsFlyerConversionDelegate

@synthesize objectName;
@synthesize methodName;

- (id) initWithObjectName:(NSString*)object method:(NSString*)method {
    if ( self = [super init] ) {
        self.objectName = object;
        self.methodName = method;
    }
    return self;
    
}

-(void)onConversionDataReceived:(NSDictionary*) installData {
    NSString *json;
    NSError *error;
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:installData
                                                       options:(NSJSONWritingOptions)    (NO ? NSJSONWritingPrettyPrinted : 0)
                                                         error:&error];
    
    if (! jsonData) {
        NSLog(@"bv_jsonStringWithPrettyPrint: error: %@", error.localizedDescription);
        json = @"{}";
    } else {
        json = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
    }

    UnitySendMessage( [objectName cStringUsingEncoding:NSUTF8StringEncoding] , [methodName cStringUsingEncoding:NSUTF8StringEncoding], [json cStringUsingEncoding:NSUTF8StringEncoding]);
    NSLog(@"=============%@ object:%@  function:%@",json,objectName , methodName);

}

-(void)onConversionDataRequestFailure:(NSError *) error {
    
}

@end