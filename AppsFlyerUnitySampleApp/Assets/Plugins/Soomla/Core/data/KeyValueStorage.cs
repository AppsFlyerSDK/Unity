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

namespace Soomla
{
	public class KeyValueStorage
	{

		protected const string TAG = "SOOMLA KeyValueStorage"; // used for Log error messages

		static KeyValueStorage _instance = null;
		static KeyValueStorage instance {
			get {
				if(_instance == null) {
					#if UNITY_ANDROID && !UNITY_EDITOR
					_instance = new KeyValueStorageAndroid();
					#elif UNITY_IOS && !UNITY_EDITOR
					_instance = new KeyValueStorageIOS();
					#else
					_instance = new KeyValueStorage();
					#endif
				}
				return _instance;
			}
		}
			
		public static string GetValue(string key) {
			return instance._getValue(key);
		}

		public static void SetValue(string key, string val) {
			instance._setValue(key, val);
		}

		public static void DeleteKeyValue(string key) {
			instance._deleteKeyValue(key);
		}

		virtual protected string _getValue(string key) {
#if UNITY_EDITOR
			return PlayerPrefs.GetString (key);
#else
			return null;
#endif
		}

		virtual protected void _setValue(string key, string val) {
#if UNITY_EDITOR
			PlayerPrefs.SetString (key, val);
#endif
		}

		virtual protected void _deleteKeyValue(string key) {
#if UNITY_EDITOR
			PlayerPrefs.DeleteKey(key);
#endif
		}

	}
}

