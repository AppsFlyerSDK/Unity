using UnityEngine;
using System.Collections;

public class StartUp : MonoBehaviour {

	// Use this for initialization
	void Start () {

		AppsFlyer.setAppsFlyerKey ("YOUR_APPSFLYER_DEV_KEY_HERE");

		#if UNITY_IOS 

		AppsFlyer.setAppID ("YOUR_APPLE_APP_ID_HERE");
		AppsFlyer.setIsDebug (true);
		AppsFlyer.getConversionData ();
		AppsFlyer.trackAppLaunch ();

		#elif UNITY_ANDROID

		// All Initialization occur in the override activity defined in the mainfest.xml, including track app launch
		// You can define AppsFlyer library here use this commented out code.

		// un-comment this in case you are not working with the android manifest file
		//AppsFlyer.setAppID ("YOUR_ANDROID_PACKAGE_NAME_HERE"); 

		// for getting the conversion data
		//AppsFlyer.loadConversionData("AppsFlyerTrackerCallbacks","didReceiveConversionData", "didReceiveConversionDataWithError");

		// for in app billing validation
		//AppsFlyer.createValidateInAppListener ("AppsFlyerTrackerCallbacks", "onInAppBillingSuccess", "onInAppBillingFailure"); 

		//AppsFlyer.trackAppLaunch ();
		#endif


		print ("AppsFlyerId = " + AppsFlyer.getAppsFlyerId());

	}

	// Update is called once per frame
	void Update () {

	}

}
