
#Integrating AppsFlyer Plugin into Unity

Installation instructions for the AppsFlyer's plugin:

1. Import the AppsFlyerUnityPlugin.unitypackage into your Unity project. go to Assets/import Package/Custom Pacakge and chose AppsFlyerUnityPlugin.unitypackage file.


2. In your Start / Init methods add the following code:

		void Start () {
		
			AppsFlyer.setAppsFlyerKey ("YOUR_APPSFLYER_DEV_KEY_HERE");

		#if UNITY_IOS 

			AppsFlyer.setAppID ("YOUR_APP_ID_HERE");
			AppsFlyer.setIsDebug (true); // for detailed logging
			AppsFlyer.getConversionData (); // for getting the conversion data will be triggered on AppsFlyerTrackerCallbacks.cs file
			AppsFlyer.trackAppLaunch ();

		#elif UNITY_ANDROID

			AppsFlyer.setAppID ("YOUR_ANDROID_PACKAGE_NAME_HERE");
			AppsFlyer.loadConversionData("AppsFlyerTrackerCallbacks","didReceiveConversionData");

		#endif
	
	}	
	
	<p><h3>Important: The conversion data response will be triggered in the AppsFlyerTrackerCallbacks.cs class.</h3></p>

	
There is a sample StartUp.cs sample file included in the project, please refer to it for more information.


#Getting Conversion Data:

To load AppsFlyer's conversion data from it's servers:
Add Empty Object call it AppsFlyerTrackerCallbacks, and attach to it the AppsFlyerTrackerCallbacks.cs which is included in the project, for more information, please refer to the sample scene provided with the project.

	public void didReceiveConversionData(string conversionData) {
		print ("AppsFlyerTrackerCallbacks:: got conversion data = " + conversionData);
	}

	//The conversion data is in Json Format.


#In App Purchase Receipt Validation (iOS only)
For testing make sure you test against Apple sandbox server call:

		AppsFlyer.setIsSandbox(true);
		AppsFlyer.validateReceipt(string eventName, string failedEventName, string eventValue, string productIdentifier, double price, string currency);
		
		
<h3>The validate purchase response will be triggered in the AppsFlyerTrackerCallbacks.cs class.</h3>


#For Android:

The Android Override Activity in the manifest sends the TrackAppLaunch() automatically.

If your manifest file is occupied by other services, you can initialize the Appsflyer tracker manually in the startup script, and remove all related code from the manifest.

Set permissions mandatory (if missing):

	<uses-permission android:name="android.permission.INTERNET" />
	<uses-permission android:name="android.permission.ACCESS_NETWORK_STATE" />
	<uses-permission android:name="android.permission.READ_PHONE_STATE” />

*READ_PHONE_STATE permission is optional.
Adding this permission will enable Carrier tracking Android_id and IMEI (required for tracking out of Google Play)

<h3> To collect Google advertising ID</h3>

Integrate Google Play Services. 

Open the Android SDK manager, scroll down to the Extras folder and verify that you have downloaded the Google Play Services package. See http://developer.android.com/google/play-services/setup.html. Uncomment the following line in the AndroidManifest.xml file:

	<meta-data android:name="com.google.android.gms.version" android:value="@integer/google_play_services_version" />


Plugin API:
===========

Tracking event:

AppsFlyer.trackEvent("MyEventName","TheEventValue");

Setting user local currency code for in app purchases:
The currency code should be a 3 character ISO 4217 code. (default is USD)    

	AppsFlyer.setCurrencyCode("GBP");

Settings the user ID as used by the app:

	AppsFlyer.setCustomerUserID("someId");


Pelase refer to SDK integration guides for complete API documentation.

[iOS Integration Guide](http://support.appsflyer.com/entries/25458906-iOS-SDK-Integration-Guide-v2-5-3-x-New-API-).

[Android Integration Guide](http://support.appsflyer.com/entries/22801952-Android-SDK-Integration-Guide).

