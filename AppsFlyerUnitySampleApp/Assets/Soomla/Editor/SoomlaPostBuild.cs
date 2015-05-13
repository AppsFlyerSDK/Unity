using UnityEngine;
using System.Collections;
using UnityEditor.Callbacks;
using UnityEditor;
using System.Diagnostics;
using System.IO;

public class PostProcessScriptStarter : MonoBehaviour {
	[PostProcessBuild(1000)]
	public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject) {
#if UNITY_IOS
		string buildToolsDir = Application.dataPath + @"/Soomla/Editor/build-tools";

		string searchPattern = "Soomla_*.py";  // This would be for you to construct your prefix

		DirectoryInfo di = new DirectoryInfo(buildToolsDir);
		FileInfo[] files = di.GetFiles(searchPattern);

		foreach (FileInfo fi in files) {
			Process proc = new Process();
			proc.StartInfo.FileName = "python2.6";

			int prefixLength = "Soomla_".Length;
			string targetModule = fi.Name.Substring(prefixLength, fi.Name.Length - ".py".Length - prefixLength);
			Soomla.ISoomlaPostBuildTool tool = Soomla.SoomlaPostBuildTools.GetTool(targetModule);
			string metaData = "";
			if (tool != null) {
				metaData = tool.GetToolMetaData(target);
				metaData = (metaData != null) ? metaData : "";
			}

//			UnityEngine.Debug.Log("Trying to run: " + fi.FullName + " " + metaData);
			proc.StartInfo.Arguments = string.Format("\"{0}\" \"{1}\" \"{2}\"", fi.FullName, pathToBuiltProject, metaData);
			proc.StartInfo.UseShellExecute = false;
			proc.StartInfo.RedirectStandardOutput = true;
			proc.StartInfo.RedirectStandardError = true;
			proc.Start();
//			string output = proc.StandardOutput.ReadToEnd();
			string err = proc.StandardError.ReadToEnd();
			proc.WaitForExit();
//			UnityEngine.Debug.Log("out: " + output);
			if (proc.ExitCode != 0) {
                UnityEngine.Debug.LogError("error: " + err + "   code: " + proc.ExitCode);
            }
		}
#endif
    }
}
