#import "AppsflyerTracker.h"

@interface AppsFlyerConversionDelegate : NSObject<AppsFlyerTrackerDelegate> {

	NSString *objectName;
	NSString *methodName;
}

@property (nonatomic,retain) NSString *objectName;
@property (nonatomic,retain) NSString *methodName;


- (id) initWithObjectName:(NSString*)objectName method:(NSString*)methodName;

@end

