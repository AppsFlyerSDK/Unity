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

		//AppsFlyer.setAppID ("YOUR_ANDROID_PACKAGE_NAME_HERE"); // un-comment this in case you are not working with the manifest file
		AppsFlyer.loadConversionData("AppsFlyerTrackerCallbacks", "didReceiveConversionData");

		#endif

	}

	// Update is called once per frame
	void Update () {

	}

}
