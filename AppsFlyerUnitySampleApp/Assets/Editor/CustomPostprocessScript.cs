#if UNITY_IOS
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System;
using System.Diagnostics;

public class CustomPostprocessScript : MonoBehaviour
{
	[PostProcessBuild(100)] // <- this is where the magic happens
	public static void OnPostprocessBuild(BuildTarget target, string pathToBuildProject)
	{        
		UnityEngine.Debug.Log("----Custome Script---Executing post process build phase."); 		
		string objCPath = Application.dataPath + "/Plugins/iOS";
		Process myCustomProcess = new Process();		
		myCustomProcess.StartInfo.FileName = "python";
		myCustomProcess.StartInfo.Arguments = string.Format("Assets/Editor/post_process.py \"{0}\" \"{1}\"", pathToBuildProject, objCPath);
		myCustomProcess.StartInfo.UseShellExecute = false;
		myCustomProcess.StartInfo.RedirectStandardOutput = false;
		myCustomProcess.Start(); 
		myCustomProcess.WaitForExit();
		UnityEngine.Debug.Log("----Custome Script--- Finished executing post process build phase.");  		
		
	}
}
#elif UNITY_ANDROID



#endif