using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrackEventTests : MonoBehaviour {

	// Use this for initialization
	void Start () {
		print ("trackEventClass Start called");
		TrackRichEventTest ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Test in app event
	public void TrackRichEventTest() {
		print ("trackRichEventTest called");
		Dictionary<string, string> dict = new Dictionary<string, string> ();
		dict.Add ("currency", "USD");
		dict.Add ("productId", "123123");
		dict.Add ("price", "100");
		AppsFlyer.trackRichEvent ("add_to_wish_list", dict);
	}

	public void ValidateReceiptTest () {
	
		
	}
}
