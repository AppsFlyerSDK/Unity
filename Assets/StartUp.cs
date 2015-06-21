Inusing UnityEngine;
using System.Collections;

public class StartUp : MonoBehaviour {

	// Use this for initialization
	void Start () {

		AppsFlyer.setAppsFlyerKey ("rbz2mfgZQY5mSEYNTyjwni");

#if UNITY_IOS 

		AppsFlyer.setAppID ("112233445");
		AppsFlyer.setIsDebug (true);
		AppsFlyer.getConversionData ();

	
#elif UNITY_ANDROID

		AppsFlyer.setAppID ("com.Appsflyer.AndroidUntiyTest");
		AppsFlyer.loadConversionData("AppsFlyerTrackerCallbacks", "didReceiveConversionData");

#endif
	
		AppsFlyer.trackAppLaunch ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
}
