                                             
Unity
====================

Integrating AppsFlyer iOS Plugin (v2.5.3.7)
============================================

Installation instruction for the AppsFlyer's plugin:

1. Copy the Assets folder from AppsFlyer's Unity plugin to your Unity project.

2. Open /Assets/Editor/PostprocessBuildPlayer and set your AppsFlyer's code and Apple app bundle ID.
 my $APPS_FLYER_KEY = "PLACE YOUR KEY HERE";
 my $APPLE_ID   = "YOUR BUNDLE ID";

3. Build the project for iOS.

4. Open Xcode and add the AdSupport framework to your project:
	4.1 In the project navigator select the root.
	4.2 Chose the unity target
	4.3 Go to the "Build Phase"
	4.4 Expand "Link Binary With Libraries"
	4.5 Click the "+" at the bottom and add the "AdSupport" framework.

Pelase refer to section 6 at the iOS SDK integration guide for in-app event API documentation.

http://support.appsflyer.com/entries/22973717-iOS-SDK-Integration-Guide-

Testing SDK integration:
http://support.appsflyer.com/entries/22904293-Testing-AppsFlyer-iOS-SDK-Integration-



Integrating AppsFlyer Android Plugin
====================================
1. Copy the Assets folder from AppsFlyer's Unity plugin to your Unity project.

2. Modify application manifest file:
   
   2.1 Open Unity and build your project.
   
   2.2 Once built, go to the Temp/StagingArea directory within your project and copy the 
       AndroidManifest.xml file to the directory Assets/Plugins/Android/.
       
   2.3 Open the copied AndroidManifest.xml and change the activity's android:name to com.appsflyer.AppsFlyerOverrideActivity. 
       It should look similar to the following:

       <activity android:name="com.appsflyer.AppsFlyerOverrideActivity" android:launchMode=...
      
       This will tell the android to start your application with AppsFlyer extension.
      
   2.4   set permissions (if missing)
        <uses-permission android:name="android.permission.INTERNET" />
        <uses-permission android:name="android.permission.ACCESS_NETWORK_STATE" />
        <uses-permission android:name="android.permission.READ_PHONE_STATE” />
        
        * READ_PHONE_STATE permission is optional. 
        Adding this permission will enable Carrier tracking Android_id and IMEI (required for tracking out of Google Play)

   2.4 Set a receiver AndroidManifest.xml
       Android app cannot have multiple receivers which have the same intent-filtered action.
       AppsFlyer provide a solution that broadcasts INSTALL_REFERRER to all other receivers automatically. 
       In the AndroidManifest.xml, please add the following receiver as the FIRST for INSTALL_REFERRER: 
       
       <receiver android:name="com.appsflyer.MultipleInstallBroadcastReceiver" android:exported="true">
          <intent-filter>
              <action android:name="com.android.vending.INSTALL_REFERRER" />
          </intent-filter>
       </receiver>
       
       In case you would like to use multiple receivers see AppsFlyer android integration guide.
       http://support.appsflyer.com/entries/22801952-Android-SDK-Integration-Guide

    2.5 Set your AppsFlyer's dev key by adding the following line at the end of the application section:
    
        <meta-data android:name="AppsFlyerDevKey" android:value="SET YOUR DEV KEY HERE"/>
		            
3. Build and run the app. 

   Please refer to chapter 10 at the Android SDK integration guide for testing the integration.
   http://support.appsflyer.com/attachments/token/dfbenhahuv62oez/?name=AF-Android-Integration-Guide-v1.3.16.0.pdf    



Plugin API:
===========

Tracking event:

    AppsFlyer.trackEvent("MyEventName","TheEventValue");
    
Setting user local currency code for in app purchases:
	The currency code should be a 3 character IOS 4217 code. (default is USD)    

    AppsFlyer.setCurrencyCode("GBP");

Settings the user ID as used by the app:

    AppsFlyer.setCustomerUserID("someId");
    
Using AppsFlyer's conversion data:
==================================

To load AppsFlyer's conversion data from it's servers you need add the following code to a Javascript code attached to a GameObject: 

In the script's Start() function add the following line:
    
    AppsFlyer.loadConversionData(this.name,"someFunction");

The first parameter is the script name. The second is the name of the function which handles the server's JSON result:

    function someFunction(json){
        Debug.Log("AppsFlyer conversion data: "+json);
    }
        
Initializing AppsFlyer using the CS API:
========================================

If you wish to use API directly instead of the iOS's PostprocessBuildPlayer PERL script 
or Android AppsFlyerOverrideActivity mentioned above, you can use the API directly as follow:

	AppsFlyer.setAppsFlyerKey("YOU DEV KEY");
	AppsFlyer.setAppID("APPLE APP ID"); // only required for iOS.
	AppsFlyer.trackAppLaunch();
        
        

