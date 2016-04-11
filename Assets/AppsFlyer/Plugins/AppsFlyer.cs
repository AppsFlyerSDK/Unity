using System;
using UnityEngine;
using System.Collections;
// We need this one for importing our IOS functions
using System.Runtime.InteropServices;
using System.Collections.Generic;

public class AppsFlyer : MonoBehaviour {
	
	
	#if UNITY_IOS
	[DllImport("__Internal")]
	private static extern void mTrackEvent(string eventName,string eventValue);
	
	[DllImport("__Internal")]
	private static extern void mSetCurrencyCode(string currencyCode);
	
	[DllImport("__Internal")]
	private static extern void mSetCustomerUserID(string customerUserID);
	
	[DllImport("__Internal")]
	private static extern void mSetAppsFlyerDevKey(string devKey);
	
	[DllImport("__Internal")]
	private static extern void mTrackAppLaunch();
	
	[DllImport("__Internal")]
	private static extern void mSetAppID(string appleAppId);
	
	[DllImport("__Internal")]
	private static extern void mTrackRichEvent(string eventName, string eventValues);
	
	[DllImport("__Internal")]
	private static extern void mValidateReceipt(string productIdentifier, string price, string currency, string transactionId ,string additionalParams);
	
	[DllImport("__Internal")]
	private static extern void mSetIsDebug(bool isDebug);
	
	[DllImport("__Internal")]
	private static extern void mSetIsSandbox(bool isSandbox);
	
	[DllImport("__Internal")]
	private static extern void mGetConversionData();
	
	[DllImport("__Internal")]
	private static extern void mHandleOpenUrl(string url, string sourceApplication, string annotation);
	
	[DllImport("__Internal")]
	private static extern string mGetAppsFlyerId();

	[DllImport("__Internal")]
	private static extern void mHandlePushNotification(string payload);

	
	public static void trackEvent(string eventName,string eventValue){
		mTrackEvent(eventName,eventValue);
	}
	
	public static void setCurrencyCode(string currencyCode){
		mSetCurrencyCode(currencyCode);
	}
	
	public static void setCustomerUserID(string customerUserID){
		mSetCustomerUserID(customerUserID);
	}
	
	public static void setAppsFlyerKey(string key){
		mSetAppsFlyerDevKey(key);
	}
	
	public static void trackAppLaunch(){
		mTrackAppLaunch();
	}
	
	public static void setAppID(string appleAppId){
		mSetAppID(appleAppId);
	}
	
	public static void trackRichEvent(string eventName, Dictionary<string, string> eventValues) {
		
		string attributesString = "";
		foreach(KeyValuePair<string, string> kvp in eventValues)
		{
			attributesString += kvp.Key + "=" + kvp.Value + "\n";
		}
		
		mTrackRichEvent (eventName, attributesString);
	}
	
	public static void validateReceipt(string productIdentifier, string price, string currency, string transactionId, Dictionary<string,string> additionalParametes) {
		string attributesString = "";
		foreach(KeyValuePair<string, string> kvp in additionalParametes)
		{
			attributesString += kvp.Key + "=" + kvp.Value + "\n";
		}
		mValidateReceipt (productIdentifier, price, currency, transactionId, attributesString);
	}
	
	public static void setIsDebug(bool isDebug){
		mSetIsDebug(isDebug);
	}
	
	public static void setIsSandbox(bool isSandbox){
		mSetIsSandbox(isSandbox);
	}
	
	public static void getConversionData () {
		mGetConversionData ();
	}

	public static string getAppsFlyerId () {
		return mGetAppsFlyerId ();
	}

	public static void handleOpenUrl(string url, string sourceApplication, string annotation) {
		
		mHandleOpenUrl (url, sourceApplication, annotation);
	}

	public static void handlePushNotification(Dictionary<string, string> payload) {
		string attributesString = "";
		foreach(KeyValuePair<string, string> kvp in payload) {
			attributesString += kvp.Key + "=" + kvp.Value + "\n";
		}
		mHandlePushNotification(attributesString);
	}

	#elif UNITY_ANDROID

	private static AndroidJavaClass obj = new AndroidJavaClass ("com.appsflyer.AppsFlyerLib");
	private static AndroidJavaObject cls_AppsFlyer = obj.CallStatic<AndroidJavaObject>("getInstance");
	private static AndroidJavaClass cls_AppsFlyerHelper = new AndroidJavaClass("com.appsflyer.AppsFlyerUnityHelper");
	private static string devKey;

	
	public static void trackEvent(string eventName,string eventValue){
		using(AndroidJavaClass cls_UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) 
		{
			using(AndroidJavaObject cls_Activity = cls_UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity")) 
			{
				cls_AppsFlyer.Call("trackEvent",cls_Activity, eventName, eventValue);
			}
		}
		
	}
	
	public static void setCurrencyCode(string currencyCode){
		cls_AppsFlyer.Call("setCurrencyCode", currencyCode);
	}
	
	public static void  setCustomerUserID(string customerUserID){
		cls_AppsFlyer.Call("setAppUserId", customerUserID);
	}
	
	public static void loadConversionData(string callbackObject,string callbackMethod, string callbackFailedMethod){
		using(AndroidJavaClass cls_UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) 
		{
			using(AndroidJavaObject cls_Activity = cls_UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity")) {
				cls_AppsFlyerHelper.CallStatic("createConversionDataListener", cls_Activity, callbackObject, callbackMethod, callbackFailedMethod);	
			}
		}
	}
	
	public static void setCollectIMEI (bool shouldCollect) {
		cls_AppsFlyer.Call("setCollectIMEI", shouldCollect);
	}
	
	public static void setCollectAndroidID (bool shouldCollect) {
		print("AF.cs setCollectAndroidID");
		cls_AppsFlyer.Call("setCollectAndroidID", shouldCollect);
	}

	public static void init(string key){
		print("AF.cs init");
		devKey = key;
		using (AndroidJavaClass cls_UnityPlayer = new AndroidJavaClass ("com.unity3d.player.UnityPlayer")) {
			using (AndroidJavaObject cls_Activity = cls_UnityPlayer.GetStatic<AndroidJavaObject> ("currentActivity")) {
				cls_Activity.Call("runOnUiThread", new AndroidJavaRunnable(init_cb));
			}
		}
	}

	static void init_cb() {
		print("AF.cs init_cb");

		using (AndroidJavaClass cls_UnityPlayer = new AndroidJavaClass ("com.unity3d.player.UnityPlayer")) {
			using (AndroidJavaObject cls_Activity = cls_UnityPlayer.GetStatic<AndroidJavaObject> ("currentActivity")) {
				cls_AppsFlyer.Call("init", cls_Activity, devKey);
			}
		}
	}

	
	public static void setAppsFlyerKey(string key){
		print("AF.cs setAppsFlyerKey");
		init(key);
	}
	
	public static void trackAppLaunch(){
		print("AF.cs trackAppLaunch");
		trackEvent(null, null);
	}

	public static void setAppID(string packageName){
		// In Android we take the package name
		cls_AppsFlyer.Call("setAppId", packageName);
	}
	
	public static void createValidateInAppListener(string aObject, string callbackMethod, string callbackFailedMethod) {
		print ("AF.cs createValidateInAppListener called");
		
		using(AndroidJavaClass cls_UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
			using(AndroidJavaObject cls_Activity = cls_UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity")) {
				cls_AppsFlyerHelper.CallStatic("createValidateInAppListener", cls_Activity, aObject, callbackMethod, callbackFailedMethod);
			}
		}		
	}
	

	public static void validateReceipt(string publicKey, string purchaseData, string signature, string price, string currency) {
		print ("AF.cs validateReceipt pk = " + publicKey + " data = " + purchaseData + "sig = " + signature);
		
		using(AndroidJavaClass cls_UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
			using(AndroidJavaObject cls_Activity = cls_UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity")) {
				print ("inside cls_activity");
				cls_AppsFlyer.Call("validateAndTrackInAppPurchase",cls_Activity, publicKey, signature, purchaseData, price, currency, null);
			}
		}		
	}
	
	public static void trackRichEvent(string eventName, Dictionary<string, string> eventValues){
		
		using(AndroidJavaObject obj_HashMap = new AndroidJavaObject("java.util.HashMap")) {
			IntPtr method_Put = AndroidJNIHelper.GetMethodID(obj_HashMap.GetRawClass(), "put", 
			                                                 "(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;");
			object[] args = new object[2];
			foreach(KeyValuePair<string, string> kvp in eventValues){
				using(AndroidJavaObject k = new AndroidJavaObject("java.lang.String", kvp.Key)){
					using(AndroidJavaObject v = new AndroidJavaObject("java.lang.String", kvp.Value)){
						args[0] = k;
						args[1] = v;
						AndroidJNI.CallObjectMethod(obj_HashMap.GetRawObject(), 
						                            method_Put, AndroidJNIHelper.CreateJNIArgArray(args));
					}
				}
			}
			using(AndroidJavaClass cls_UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
				using(AndroidJavaObject cls_Activity = cls_UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity")) {
					cls_AppsFlyer.Call("trackEvent",cls_Activity, eventName, obj_HashMap);
				}
			}		
		}
	}

	public static void setImeiData(string imeiData) {
		print("AF.cs setImeiData");
		cls_AppsFlyer.Call("setImeiData", imeiData);
	}

	public static void setAndroidIdData(string androidIdData) {
		print("AF.cs setImeiData");
		cls_AppsFlyer.Call("setAndroidIdData", androidIdData);
	}


	public static void setIsDebug(bool isDebug) {
		print("AF.cs setDebugLog");
		cls_AppsFlyer.Call("setDebugLog", isDebug);
	}
	
	public static void setIsSandbox(bool isSandbox){
	}
	
	public static void getConversionData () {
	}
	
	public static void handleOpenUrl(string url, string sourceApplication, string annotation) {
	}

	public static string getAppsFlyerId () {

		string appsFlyerId;
		using (AndroidJavaClass cls_UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
			using (AndroidJavaObject cls_Activity = cls_UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity")) {
				appsFlyerId = cls_AppsFlyer.Call <string> ("getAppsFlyerUID", cls_Activity);
			}
		}
		return appsFlyerId;
	}

	#else
	
	public static void trackEvent(string eventName,string eventValue){}
	public static void setCurrencyCode(string currencyCode){}
	public static void setCustomerUserID(string customerUserID){}
	public static void loadConversionData(string callbackObject,string callbackMethod){}
	public static void setAppsFlyerKey(string key){}
	public static void trackAppLaunch(){}
	public static void setAppID(string appleAppId){}
	public static void trackRichEvent(string eventName, Dictionary<string, string> eventValues){}
	public static void validateReceipt(string productIdentifier, string price, string currency){}
	public static void setIsDebug(bool isDebug){}
	public static void setIsSandbox(bool isSandbox){}
	public static void getConversionData (){}
	public static string getAppsFlyerId () {return null;}
	public static void handleOpenUrl(string url, string sourceApplication, string annotation) {}
	#endif
}
