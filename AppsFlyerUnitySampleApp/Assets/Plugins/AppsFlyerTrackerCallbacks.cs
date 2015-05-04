using UnityEngine;
using System.Collections;


public class AppsFlyerTrackerCallbacks : MonoBehaviour {

	// Use this for initialization
	void Start () {
		print ("AppsFlyerTrackerCallbacks on Start");

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void didReceiveConversionData(string conversionData) {

		print ("got conversion data = " + conversionData);
	}

	public void didReceiveConversionDataWithError(string error) {
		
		print ("got conversion data error = " + error);
	}
}
