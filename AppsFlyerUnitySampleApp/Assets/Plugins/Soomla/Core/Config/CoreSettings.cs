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
using System.IO;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Soomla
{

#if UNITY_EDITOR
	[InitializeOnLoad]
#endif
	/// <summary>
	/// This class holds the store's configurations.
	/// </summary>
	public class CoreSettings : ISoomlaSettings
	{

#if UNITY_EDITOR
		public static string DB_KEY_PREFIX = "soomla.";

		static CoreSettings instance = new CoreSettings();
		static CoreSettings()
		{
			SoomlaEditorScript.addSettings(instance);
		}

//		GUIContent emptyContent = new GUIContent("");

		GUIContent frameworkVersion = new GUIContent("Core Version [?]", "The SOOMLA Framework version. ");
		GUIContent buildVersion = new GUIContent("Core Build [?]", "The SOOMLA Framework build.");

		public void OnEnable() {
			// Generating AndroidManifest.xml
			SoomlaManifestTools.GenerateManifest();
		}

		public void OnModuleGUI() {

		}

		public void OnInfoGUI() {
			EditorGUILayout.HelpBox("SOOMLA Framework Info", MessageType.None);
			SoomlaEditorScript.SelectableLabelField(frameworkVersion, "1.0.6");
			SoomlaEditorScript.SelectableLabelField(buildVersion, "1");
			EditorGUILayout.Space();
		}

		GUIContent soomlaSecLabel = new GUIContent("Soomla Secret [?]:", "All the user information will be encrypted using this secret.");
		GUIContent debugMsgsLabel = new GUIContent("Debug Native [?]:", "Check if you want to show debug messages from native code in the log (iOS and Android).");
		GUIContent debugUnityMsgsLabel = new GUIContent("Debug Unity [?]:", "Check if you want to show debug messages from Unity code in the log (Editor, iOS and Android).");

		public void OnSoomlaGUI() {
			FileStream fs = new FileStream(Application.dataPath + @"/Soomla/Resources/soom_logo.png", FileMode.Open, FileAccess.Read);
			byte[] imageData = new byte[fs.Length];
			fs.Read(imageData, 0, (int)fs.Length);
			Texture2D logoTexture = new Texture2D(300, 92);
			logoTexture.LoadImage(imageData);

			EditorGUILayout.BeginHorizontal();
			GUIContent logoImgLabel = new GUIContent (logoTexture);
			EditorGUILayout.LabelField(logoImgLabel, GUILayout.MaxHeight(70), GUILayout.ExpandWidth(true));
			EditorGUILayout.EndHorizontal();

			GameObject.DestroyImmediate(logoTexture);

			EditorGUILayout.HelpBox("Make sure you fill out all the information below", MessageType.None);

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(soomlaSecLabel, SoomlaEditorScript.FieldWidth, SoomlaEditorScript.FieldHeight);
			SoomlaSecret = EditorGUILayout.TextField(SoomlaSecret, SoomlaEditorScript.FieldHeight);
			EditorGUILayout.EndHorizontal();

			DebugMessages = EditorGUILayout.Toggle(debugMsgsLabel, DebugMessages);
			DebugUnityMessages = EditorGUILayout.Toggle(debugUnityMsgsLabel, DebugUnityMessages);

			EditorGUILayout.Space();


			if (!SoomlaAndroidUtil.IsSetupProperly())
			{
				var msg = "You have errors in your Android setup. More info in the SOOMLA docs.";
				switch (SoomlaAndroidUtil.SetupError)
				{
				case SoomlaAndroidUtil.ERROR_NO_SDK:
					msg = "You need to install the Android SDK!  Set the location of Android SDK in: " + (Application.platform == RuntimePlatform.OSXEditor ? "Unity" : "Edit") + "->Preferences->External Tools";
					break;
				case SoomlaAndroidUtil.ERROR_NO_KEYSTORE:
					msg = "Your defined keystore doesn't exist! You'll need to create a debug keystore or point to your keystore in 'Publishing Settings' from 'File -> Build Settings -> Player Settings...'";
					break;
				}

				EditorGUILayout.HelpBox(msg, MessageType.Error);
			}
		}
#endif

		public static string ONLY_ONCE_DEFAULT = "SET ONLY ONCE";

		public static string SoomlaSecret
		{
			get {
				string value;
				return SoomlaEditorScript.Instance.SoomlaSettings.TryGetValue("SoomlaSecret", out value) ? value : ONLY_ONCE_DEFAULT;
			}
			set
			{
				string v;
				SoomlaEditorScript.Instance.SoomlaSettings.TryGetValue("SoomlaSecret", out v);
				if (v != value)
				{
					SoomlaEditorScript.Instance.setSettingsValue("SoomlaSecret",value);
					SoomlaEditorScript.DirtyEditor ();
				}
			}
		}

		public static bool DebugMessages
		{
			get {
				string value;
				return SoomlaEditorScript.Instance.SoomlaSettings.TryGetValue("DebugMessages", out value) ? Convert.ToBoolean(value) : false;
			}
			set
			{
				string v;
				SoomlaEditorScript.Instance.SoomlaSettings.TryGetValue("DebugMessages", out v);
				if (Convert.ToBoolean(v) != value)
				{
					SoomlaEditorScript.Instance.setSettingsValue("DebugMessages",value.ToString());
					SoomlaEditorScript.DirtyEditor();
				}
			}
		}

		public static bool DebugUnityMessages
		{
			get {
				string value;
				return SoomlaEditorScript.Instance.SoomlaSettings.TryGetValue("DebugUnityMessages", out value) ? Convert.ToBoolean(value) : true;
			}
			set
			{
				string v;
				SoomlaEditorScript.Instance.SoomlaSettings.TryGetValue("DebugUnityMessages", out v);
				if (Convert.ToBoolean(v) != value)
				{
					SoomlaEditorScript.Instance.setSettingsValue("DebugUnityMessages",value.ToString());
					SoomlaEditorScript.DirtyEditor();
				}
			}
		}

	}
}
