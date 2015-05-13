#!/usr/bin/python
import sys
import os
import fileinput

def process_app_controller_wrapper(appcontroller_filename, newContent, methodSignatures, valuesToAppend, positionsInMethod, contentToAppend=None):
    appcontroller = open(appcontroller_filename, 'r')
    lines = appcontroller.readlines()
    appcontroller.close()
    foundWillResignActive = False
    foundIndex = -1
    for line in lines:
        #print line
        newContent += line
        for idx, val in enumerate(methodSignatures):
            #print idx, val
            if (line.strip() == val):
                #print "founded match method: " + val
                foundIndex = idx
                foundWillResignActive = True
        if foundWillResignActive :
            if positionsInMethod[foundIndex] == 'begin' and line.strip() == '{':
                #print "add code to resign body"
                newContent += ("\n\t" + valuesToAppend[foundIndex] + "\n\n")
                foundWillResignActive = False
            if 	positionsInMethod[foundIndex] == 'end' and line.strip() == '}':
                newContent = newContent[:-3]
                newContent += ("\n\n\t" + valuesToAppend[foundIndex] + "\n")
                newContent += "}\n"
                foundWillResignActive = False
        if line.strip() == '@end' and (not contentToAppend is None):
            newContent = newContent[:-6]
            newContent += ("\n\n\t" + contentToAppend + "\n")
            newContent += "@end"

    #print "-------done loop close stream and content: " + newContent
    appcontroller = open(appcontroller_filename, 'w')
    appcontroller.write(newContent)
    appcontroller.close()

def injectAppsFlyerCode():
    return '''
    [AppsFlyerTracker sharedTracker].isDebug = NO;
    [AppsFlyerTracker sharedTracker].useReceiptValidationSandbox = YES;
    [AppsFlyerTracker sharedTracker].appleAppID = @"APPLE_APP_ID_HERE";
    [AppsFlyerTracker sharedTracker].appsFlyerDevKey = @"APPSFLYER_DEV_KEY_HERE";
    [AppsFlyerTracker sharedTracker].delegate = self;
    [[AppsFlyerTracker sharedTracker] trackAppLaunch];
        '''

def importHeaders():
    return '''
#import "AppsFlyerTracker.h"
        '''

def add_conversion_data_delegate_methods():
    return '''
//AppsFlyerTracker conversion data delegate
-(void)onConversionDataReceived:(NSDictionary*) installData {
    
    NSString *conversionDataResult = [installData description];
    NSLog(@"%@", conversionDataResult);
    UnitySendMessage(UNITY_SENDMESSAGE_CALLBACK_AFTRACKER, UNITY_SENDMESSAGE_CALLBACK_CONVERSION, [conversionDataResult UTF8String]);
}
        
-(void)onConversionDataRequestFailure:(NSError *) error {
        
    NSLog(@"Appsflyer ios: AppsFlyerConversionDelegate: Error = %@", error);
    NSString *errDesc = [error localizedDescription];
    UnitySendMessage(UNITY_SENDMESSAGE_CALLBACK_AFTRACKER, UNITY_SENDMESSAGE_CALLBACK_CONVERSION_ERROR, [errDesc UTF8String]);
}
        '''

def add_send_message_defines():
    return '''
const char * UNITY_SENDMESSAGE_CALLBACK_AFTRACKER = "AppsFlyerTrackerCallbacks";
        
// corresponds to the AppsFlyers Conversions Delegate
const char * UNITY_SENDMESSAGE_CALLBACK_CONVERSION = "didReceiveConversionData";
const char * UNITY_SENDMESSAGE_CALLBACK_CONVERSION_ERROR = "didReceiveConversionDataWithError";
        '''


def replace_delegate_signature (appcontroller_filename, line_to_replace, replace_with_this):
    for line in fileinput.input(appcontroller_filename, inplace=1):
        if line_to_replace in line:
            line = line.replace(line_to_replace,replace_with_this)
        sys.stdout.write(line)


def add_code_line_under (appcontroller_filename, line_to_find, code_to_add):
    for line in fileinput.input(appcontroller_filename, inplace=1):
        if line_to_find in line:
            line += ("\n\t" + code_to_add + "\n")
        sys.stdout.write(line)


def touch_implementation(appcontroller_filename):
    # appcontroller = open(appcontroller_filename, 'w')
    # print(" process AppController.mm add imports header")
    newContent = importHeaders()
    
    #starting line of method {
    methodSignatures = []
    #value to append near }
    valueToAppend = []
    #position to add insert at the beginning o
    positionsInMethod = []
    
    methodSignatures.append('- (BOOL)application:(UIApplication*)application didFinishLaunchingWithOptions:(NSDictionary*)launchOptions')
    valueToAppend.append(injectAppsFlyerCode())
    positionsInMethod.append("begin")
    
    process_app_controller_wrapper(appcontroller_filename, newContent, methodSignatures, valueToAppend, positionsInMethod, add_conversion_data_delegate_methods())
    add_code_line_under(appcontroller_filename, '@synthesize interfaceOrientation	= _curOrientation;', add_send_message_defines())


def touch_header(appcontroller_filename):
    
    # appcontroller = open(appcontroller_filename, 'w')
    # print(" process AppController.mm add imports header")
    newContent = importHeaders()
    #starting line of method {
    methodSignatures = []
    #value to append near }
    valueToAppend = []
    positionsInMethod = []
    
    process_app_controller_wrapper(appcontroller_filename, newContent, methodSignatures, valueToAppend, positionsInMethod)
    replace_delegate_signature(appcontroller_filename, '@interface UnityAppController : NSObject<UIApplicationDelegate>', '@interface UnityAppController : NSObject<UIApplicationDelegate, AppsFlyerTrackerDelegate>')



