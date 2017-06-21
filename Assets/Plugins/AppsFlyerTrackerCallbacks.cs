using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AppsFlyerTrackerCallbacks : MonoBehaviour {

	public Text callbacks;

	// Use this for initialization
	void Start () {
		print ("AppsFlyerTrackerCallbacks on Start");
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void didReceiveConversionData(string conversionData) {
		printCallback ("AppsFlyerTrackerCallbacks:: got conversion data = " + conversionData);
	}
	
	public void didReceiveConversionDataWithError(string error) {
		printCallback ("AppsFlyerTrackerCallbacks:: got conversion data error = " + error);
	}
	
	public void didFinishValidateReceipt(string validateResult) {
		printCallback ("AppsFlyerTrackerCallbacks:: got didFinishValidateReceipt  = " + validateResult);
		
	}
	
	public void didFinishValidateReceiptWithError (string error) {
		printCallback ("AppsFlyerTrackerCallbacks:: got idFinishValidateReceiptWithError error = " + error);
		
	}
	
	public void onAppOpenAttribution(string validateResult) {
		printCallback ("AppsFlyerTrackerCallbacks:: got onAppOpenAttribution  = " + validateResult);
		
	}
	
	public void onAppOpenAttributionFailure (string error) {
		printCallback ("AppsFlyerTrackerCallbacks:: got onAppOpenAttributionFailure error = " + error);
		
	}
	
	public void onInAppBillingSuccess () {
		printCallback ("AppsFlyerTrackerCallbacks:: got onInAppBillingSuccess succcess");
		
	}
	public void onInAppBillingFailure (string error) {
		printCallback ("AppsFlyerTrackerCallbacks:: got onInAppBillingFailure error = " + error);
		
	}

	void printCallback(string str) {
		callbacks.text += str + "\n";
	}
}
