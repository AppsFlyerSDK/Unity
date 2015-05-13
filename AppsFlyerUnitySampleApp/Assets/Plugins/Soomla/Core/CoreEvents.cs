/// Copyright (C) 2012-2014 Soomla Inc.
///
/// Licensed under the Apache License, Version 2.0 (the "License");
/// you may not use this file except in compliance with the License.
/// You may obtain a copy of the License at
///
///      http://www.apache.org/licenses/LICENSE-2.0
///
/// Unless required by applicable law or agreed to in writing, software
/// distributed under the License is distributed on an "AS IS" BASIS,
/// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
/// See the License for the specific language governing permissions and
/// limitations under the License.

using UnityEngine;
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Soomla {

	/// <summary>
	/// This class provides functions for event handling.
	/// </summary>
	public class CoreEvents : MonoBehaviour {

#if UNITY_IOS && !UNITY_EDITOR
		[DllImport ("__Internal")]
		private static extern void soomlaCore_Init(string secret, [MarshalAs(UnmanagedType.Bool)] bool debug);
#endif

		private const string TAG = "SOOMLA CoreEvents";

		private static CoreEvents instance = null;

		/// <summary>
		/// Initializes game state before the game starts.
		/// </summary>
		void Awake(){
			if(instance == null){ 	// making sure we only initialize one instance.
				instance = this;
				GameObject.DontDestroyOnLoad(this.gameObject);
				Initialize();
			} else {				// Destroying unused instances.
				GameObject.Destroy(this.gameObject);
			}
		}

		public static void Initialize() {
			SoomlaUtils.LogDebug(TAG, "Initializing CoreEvents and Soomla Core ...");
#if UNITY_ANDROID && !UNITY_EDITOR
			AndroidJNI.PushLocalFrame(100);

			using(AndroidJavaClass jniStoreConfigClass = new AndroidJavaClass("com.soomla.SoomlaConfig")) {
				jniStoreConfigClass.SetStatic("logDebug", CoreSettings.DebugMessages);
			}

			// Initializing SoomlaEventHandler
			using(AndroidJavaClass jniEventHandler = new AndroidJavaClass("com.soomla.core.unity.SoomlaEventHandler")) {
				jniEventHandler.CallStatic("initialize");
			}

			// Initializing Soomla Secret
			using(AndroidJavaClass jniSoomlaClass = new AndroidJavaClass("com.soomla.Soomla")) {
				jniSoomlaClass.CallStatic("initialize", CoreSettings.SoomlaSecret);
			}
			AndroidJNI.PopLocalFrame(IntPtr.Zero);
#elif UNITY_IOS && !UNITY_EDITOR
			soomlaCore_Init(CoreSettings.SoomlaSecret, CoreSettings.DebugMessages);
#endif
		}

		/// <summary>
		/// Will be called when a reward was given to the user.
		/// </summary>
		/// <param name="message">Will contain a JSON representation of a <c>Reward</c> and a flag saying if it's a Badge or not.</param>
		public void onRewardGiven(string message) {
			SoomlaUtils.LogDebug(TAG, "SOOMLA/UNITY onRewardGiven:" + message);
			
			JSONObject eventJSON = new JSONObject(message);
			string rewardId = eventJSON["rewardId"].str;

			CoreEvents.OnRewardGiven(Reward.GetReward(rewardId));
		}

		/// <summary>
		/// Will be called when a reward was given to the user.
		/// </summary>
		/// <param name="message">Will contain a JSON representation of a <c>Reward</c> and a flag saying if it's a Badge or not.</param>
		public void onRewardTaken(string message) {
			SoomlaUtils.LogDebug(TAG, "SOOMLA/UNITY onRewardTaken:" + message);
			
			JSONObject eventJSON = new JSONObject(message);
			string rewardId = eventJSON["rewardId"].str;
			
			CoreEvents.OnRewardTaken(Reward.GetReward(rewardId));
		}

		/// <summary>
		/// Will be called on custom events. Used for internal operations.
		/// </summary>
		public void onCustomEvent(string message) {
			SoomlaUtils.LogDebug(TAG, "SOOMLA/UNITY onCustomEvent:" + message);

			JSONObject eventJSON = new JSONObject(message);
			string name = eventJSON["name"].str;
			Dictionary<string, string> extra = eventJSON["extra"].ToDictionary();

			CoreEvents.OnCustomEvent(name, extra);
		}

		public delegate void Action();

		public static Action<Reward> OnRewardGiven = delegate {};
		public static Action<Reward> OnRewardTaken = delegate {};
		public static Action<string, Dictionary<string, string>> OnCustomEvent = delegate {};

	}
}
