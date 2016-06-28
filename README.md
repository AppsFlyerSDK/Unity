
<h2>Integrating AppsFlyer Plugin into Unity Project</h2>

Whats new:

V-4.9.3
- Updated iOS native SDK to v4.5.0
- Updated Android native SDK to v4.4.0
- Fixed issue with Unity 4.x.x
- Added additional parameters to the validate in app purchase.


Installation instructions for the AppsFlyer's plugin:

1. Import the AppsFlyerUnityPlugin.unitypackage into your Unity project. go to Assets/import Package/Custom Pacakge and chose AppsFlyerUnityPlugin.unitypackage file.


2. In your Start / Init methods add the following code:

<pre><code>void Start () {
		
	#if UNITY_IOS

    AppsFlyer.setAppsFlyerKey ("YOUR_APPSFLYER_DEV_KEY_HERE");
	AppsFlyer.setAppID ("YOUR_APP_ID_HERE");
		
	// For detailed logging
	//AppsFlyer.setIsDebug (true); 
		
	// For getting the conversion data will be triggered on AppsFlyerTrackerCallbacks.cs file
	AppsFlyer.getConversionData (); 
		
	// For testing validate in app purchase (test against Apple's sandbox environment
	//AppsFlyer.setIsSandbox(true); 		
	
	AppsFlyer.trackAppLaunch ();

	#elif UNITY_ANDROID
	AppsFlyer.init ("YOUR_APPSFLYER_DEV_KEY_HERE"); 
    AppsFlyer.setAppID ("YOUR_ANDROID_PACKAGE_NAME_HERE"); 

	//AppsFlyer.setIsDebug (true);
	//AppsFlyer.createValidateInAppListener ("AppsFlyerTrackerCallbacks", "onInAppBillingSuccess", "onInAppBillingFailure");
	AppsFlyer.loadConversionData("AppsFlyerTrackerCallbacks","didReceiveConversionData", "didReceiveConversionDataWithError");

	#endif
}	</code></pre>

<h3>Important: The conversion data response will be triggered in the AppsFlyerTrackerCallbacks.cs class.</h3>

	
There is a sample project located here:
https://github.com/AppsFlyerSDK/AppsFlyerUnitySampleApp.git


<h2>Getting Conversion Data:</h2>

To load AppsFlyer's conversion data from its servers:
Add Empty Object and attach to it the AppsFlyerTrackerCallbacks.cs which is included in the project.
For more information, please refer to the <a href="https://github.com/AppsFlyerSDK/AppsFlyerUnitySampleApp">Sample Project</a>.

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
<pre><code>AppsFlyer.validateReceipt(string publicKey, string purchaseData, string signature, string price, string currency, Dictionary<string,string> additionalParametes);
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

Set the AF receiver and permissions (Mandatory):

    <receiver android:name="com.appsflyer.MultipleInstallBroadcastReceiver" android:exported="true">
    <intent-filter>
    <action android:name="com.android.vending.INSTALL_REFERRER" />
    </intent-filter>
    </receiver>
    // receiver should be inside the <application> tag

    //For permissions:
    <uses-permission android:name="android.permission.INTERNET" />
    <uses-permission android:name="android.permission.ACCESS_NETWORK_STATE" />
    <uses-permission android:name="android.permission.READ_PHONE_STATE" />


<h3>ADVANCED FEATURES:</h3>

You can set your Dev Key using your Manifest:

	<meta-data android:name="AppsFlyerDevKey" android:value="YOUR_DEV_KEY_HERE"/>


If you wish to avoid adding any initialization code, you can use AppsFlyer's Override Activity explicitly (Optional):

    <activity android:name="com.appsflyer.AppsFlyerOverrideActivity" android:launchMode="singleTask" android:label="@string/app_name" android:configChanges="fontScale|keyboard|keyboardHidden|locale|mnc|mcc|navigation|orientation|screenLayout|screenSize|smallestScreenSize|uiMode|touchscreen" android:screenOrientation="portrait">
    <intent-filter>
    <action android:name="android.intent.action.MAIN" />
    <category android:name="android.intent.category.LAUNCHER" />
    </intent-filter>
    </activity>


 #Additional Links:

SDK integration guides for complete API documentation:

[iOS Integration Guide](http://support.appsflyer.com/entries/25458906-iOS-SDK-Integration-Guide-v2-5-3-x-New-API-).

[Android Integration Guide](http://support.appsflyer.com/entries/22801952-Android-SDK-Integration-Guide).

Unity Sample Project:

[Sample Project](https://github.com/AppsFlyerSDK/AppsFlyerUnitySampleApp).

