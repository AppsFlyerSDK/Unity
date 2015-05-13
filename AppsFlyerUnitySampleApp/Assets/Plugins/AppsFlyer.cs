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
	private static extern void mValidateReceipt(string eventName, string failedEventName, string eventValue, string productIdentifier, double price, string currency);


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

	public static void validateReceipt(string eventName, string failedEventName, string eventValue, string productIdentifier, double price, string currency) {
		mValidateReceipt (eventName, failedEventName, eventValue, productIdentifier, price, currency);
	}


#elif UNITY_ANDROID
	private static AndroidJavaClass cls_AppsFlyer = new AndroidJavaClass("com.appsflyer.AppsFlyerLib");
	private static AndroidJavaClass cls_AppsFlyerHelper = new AndroidJavaClass("com.appsflyer.AppsFlyerUnityHelper");

	public static void trackEvent(string eventName,string eventValue){
		using(AndroidJavaClass cls_UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) 
		{
			using(AndroidJavaObject cls_Activity = cls_UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity")) 
			{
				cls_AppsFlyer.CallStatic("sendTrackingWithEvent",cls_Activity, eventName, eventValue);
			}
		}

	}
	
	public static void setCurrencyCode(string currencyCode){
		cls_AppsFlyer.CallStatic("setCurrencyCode", currencyCode);
	}

	public static void  setCustomerUserID(string customerUserID){
		cls_AppsFlyer.CallStatic("setAppUserId", customerUserID);
	}

	public static void loadConversionData(string callbackObject,string callbackMethod){
		using(AndroidJavaClass cls_UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) 
		{
			using(AndroidJavaObject cls_Activity = cls_UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity")) 
			{

				cls_AppsFlyerHelper.CallStatic("createConversionDataListener", cls_Activity, callbackObject, callbackMethod);
			
			}
		}
    }

	public static void setAppsFlyerKey(string key){
		cls_AppsFlyer.CallStatic("setAppsFlyerKey", key);
	}

	public static void trackAppLaunch(){
		using(AndroidJavaClass cls_UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) 
		{
			using(AndroidJavaObject cls_Activity = cls_UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity")) 
			{
				cls_AppsFlyer.CallStatic("sendTracking",cls_Activity);
			}
		}
	}

	public static void setAppID(string appleAppId){
		// In Android we take the package name
	}


	public static void validateReceipt(string eventName, string failedEventName, string eventValue, string productIdentifier, string price, string currency) {
	}


	public static void trackRichEvent(string eventName, Dictionary<string, string> eventValues){


		using(AndroidJavaObject obj_HashMap = new AndroidJavaObject("java.util.HashMap")) 
		{
		
			IntPtr method_Put = AndroidJNIHelper.GetMethodID(obj_HashMap.GetRawClass(), "put", 
			                                                 "(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;");
			
			object[] args = new object[2];
			foreach(KeyValuePair<string, string> kvp in eventValues)
			{
				using(AndroidJavaObject k = new AndroidJavaObject("java.lang.String", kvp.Key))
				{
					using(AndroidJavaObject v = new AndroidJavaObject("java.lang.String", kvp.Value))
					{
						args[0] = k;
						args[1] = v;
						AndroidJNI.CallObjectMethod(obj_HashMap.GetRawObject(), 
						                            method_Put, AndroidJNIHelper.CreateJNIArgArray(args));
					}
				}
			}
			using(AndroidJavaClass cls_UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) 
			{
				using(AndroidJavaObject cls_Activity = cls_UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity")) 
				{
					cls_AppsFlyer.CallStatic("trackEvent",cls_Activity, eventName, obj_HashMap);
				}
			}		
		}
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
	public static void validateReceipt(string eventName, string failedEventName, string eventValue, string productIdentifier, double price, string currency){}


#endif
}
