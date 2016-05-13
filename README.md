
<h2>Integrating AppsFlyer Plugin into Unity Project</h2>

Whats new:

V-4.3
- Fixed Android build issue.
- Updated iOS native SDK to v4.3.7


For iOS: add the Security.framework


Installation instructions for the AppsFlyer's plugin:

1. Import the AppsFlyerUnityPlugin.unitypackage into your Unity project. go to Assets/import Package/Custom Pacakge and chose AppsFlyerUnityPlugin.unitypackage file.


2. In your Start / Init methods add the following code:

<pre><code>void Start () {
		
	AppsFlyer.setAppsFlyerKey ("YOUR_APPSFLYER_DEV_KEY_HERE");

	#if UNITY_IOS 

	AppsFlyer.setAppID ("YOUR_APP_ID_HERE");
		
	// For detailed logging
	//AppsFlyer.setIsDebug (true); 
		
	// For getting the conversion data will be triggered on AppsFlyerTrackerCallbacks.cs file
	AppsFlyer.getConversionData (); 
		
	// For testing validate in app purchase (test against Apple's sandbox environment
	//AppsFlyer.setIsSandbox(true); 		
	
	AppsFlyer.trackAppLaunch ();

	#elif UNITY_ANDROID
		
	// All Initialization occur in the override activity defined in the mainfest.xml, 
	// including the track app launch
	// For your convinence (if your manifest is occupied) you can define AppsFlyer library
	// here, use this commented out code.
		
	//AppsFlyer.setAppID ("YOUR_ANDROID_PACKAGE_NAME_HERE"); 
	//AppsFlyer.setIsDebug (true);
	//AppsFlyer.createValidateInAppListener ("AppsFlyerTrackerCallbacks", "onInAppBillingSuccess", "onInAppBillingFailure");
	//AppsFlyer.loadConversionData("AppsFlyerTrackerCallbacks","didReceiveConversionData", "didReceiveConversionDataWithError");

	#endif
}	</code></pre>

<h3>Important: The conversion data response will be triggered in the AppsFlyerTrackerCallbacks.cs class.</h3>

	
There is a sample StartUp.cs sample file included in the project, please refer to it for more information.


<h2>Getting Conversion Data:</h2>

To load AppsFlyer's conversion data from it's servers:
Add Empty Object call it AppsFlyerTrackerCallbacks, and attach to it the AppsFlyerTrackerCallbacks.cs which is included in the project, for more information, please refer to the sample scene provided with the project.

<pre><code>public void didReceiveConversionData(string conversionData) {
	print ("AppsFlyerTrackerCallbacks:: got conversion data = " + conversionData);
}
//The conversion data is in Json Format.
</code></pre>


<h2>In App Purchase Receipt Validation</h2>
<h3>iOS:</h3>

For testing make sure you test against Apple sandbox server call:
<pre><code>AppsFlyer.setIsSandbox(true);
AppsFlyer.validateReceipt(string productIdentifier, string price, string currency, string transactionId, Dictionary<string,string> additionalParametes);
</code></pre>

<h3>Android:</h3>
For Android call:
<pre><code>AppsFlyer.validateReceipt(string publicKey, string purchaseData, string signature, string price, string currency);
</code></pre>

Note: If you are <b><u>NOT</u></b> using Appsflyer Override Activity defined in the manifest.xml
you should call this code in order to initialize the callback response:

<pre><code>AppsFlyer.createValidateInAppListener ("AppsFlyerTrackerCallbacks", "onInAppBillingSuccess", "onInAppBillingFailure");</code></pre>
					

				
<h3>The validate purchase response will be triggered in the AppsFlyerTrackerCallbacks.cs class.</h3>


Track Event API:
---------------

Tracking event example:
		
	System.Collections.Generic.Dictionary<string, string> purchaseEvent = new System.Collections.Generic.Dictionary<string, string> ();
	purchaseEvent.Add ("af_currency", "USD");
	purchaseEvent.Add ("af_revenue", "0.99");
	purchaseEvent.Add ("af_quantity", "1");
	AppsFlyer.trackRichEvent ("af_purchase", purchaseEvent);


Setting user local currency code for in app purchases:
The currency code should be a 3 character ISO 4217 code. (default is USD)    
<pre><code>AppsFlyer.setCurrencyCode("GBP");
</code></pre>

Settings the user ID as used by the app:
<pre><code>AppsFlyer.setCustomerUserID("someId");
</code></pre>



#For Android:

The Android Override Activity in the manifest sends the <b>TrackAppLaunch()</b> automatically. <br>
Set the Dev_Key in the manifest. 
		
	<meta-data android:name="AppsFlyerDevKey" android:value="YOUR_DEV_KEY_HERE"/>


If your manifest file is occupied by other services, you can initialize the Appsflyer tracker manually in the startup script and call <b> AppsFlyer.trackAppLaunch ();</b> explicitly. <br>Remove all related code from the manifest besides the receiver tag which holds the reffer information.

Set permissions mandatory (if missing):

	<pre><code><receiver android:name="com.appsflyer.MultipleInstallBroadcastReceiver" android:exported="true">
	<intent-filter>
		<action android:name="com.android.vending.INSTALL_REFERRER" />
	</intent-filter>
	</receiver>
	// inside the <application> tag
	
	//For permissions:
	<uses-permission android:name="android.permission.INTERNET" />
	<uses-permission android:name="android.permission.ACCESS_NETWORK_STATE" />
	<uses-permission android:name="android.permission.READ_PHONE_STATE" />
</code></pre>

*READ_PHONE_STATE permission is optional.
Adding this permission will enable Carrier tracking Android_id and IMEI (required for tracking out of Google Play)

<h3> To collect Google advertising ID</h3>

Integrate Google Play Services. 

Open the Android SDK manager, scroll down to the Extras folder and verify that you have downloaded the Google Play Services package. See http://developer.android.com/google/play-services/setup.html. Uncomment the following line in the AndroidManifest.xml file:

	<meta-data android:name="com.google.android.gms.version" android:value="@integer/google_play_services_version"/>


Pelase refer to SDK integration guides for complete API documentation.

[iOS Integration Guide](http://support.appsflyer.com/entries/25458906-iOS-SDK-Integration-Guide-v2-5-3-x-New-API-).

[Android Integration Guide](http://support.appsflyer.com/entries/22801952-Android-SDK-Integration-Guide).

