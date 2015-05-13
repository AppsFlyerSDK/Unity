package com.appsflyer;

import com.unity3d.player.UnityPlayerActivity;

import android.os.Bundle;
import android.util.Log;
import org.json.JSONException;
import org.json.JSONObject;
import java.util.Map;

import android.content.pm.ApplicationInfo;
import android.content.pm.PackageManager;


public class AppsFlyerOverrideActivity extends UnityPlayerActivity {
    
    protected void onCreate(Bundle savedInstanceState) {
        
        // call UnityPlayerActivity.onCreate()
        super.onCreate(savedInstanceState);
        
        Log.d("AppsFlyerUnity", "onCreate called!");

        // Take the dev key from the manifest file
        try {
            ApplicationInfo applicationInfo = this.getPackageManager().getApplicationInfo(this.getPackageName(), PackageManager.GET_META_DATA);
            Bundle bundle = applicationInfo.metaData;
            if (bundle != null){
                Object devKeyObj = bundle.get("AppsFlyerDevKey");
                if (devKeyObj == null){
                    Log.d("AppsFlyerUnity", "AppsFlyer dev key missing, please set in in the menifest file.");
                } else {
                    String devKeyString = devKeyObj instanceof String ? (String)devKeyObj : devKeyObj.toString();
                    AppsFlyerLib.setAppsFlyerKey(devKeyString);
                    AppsFlyerLib.setCollectAndroidID(false);
                    AppsFlyerLib.setCollectIMEI(false);

                    
                    AppsFlyerLib.registerConversionListener(this,new AppsFlyerConversionListener() {
                        public void onInstallConversionDataLoaded(Map<String, String> conversionData) {
                            Log.d("AppsFlyerUnity", "getting conversion data: "+ conversionData.toString());
                            
                            JSONObject jsonObject = new JSONObject(conversionData);
                            com.unity3d.player.UnityPlayer.UnitySendMessage("AppsFlyerTrackerCallbacks" ,"didReceiveConversionData", jsonObject.toString());
                        }
                        
                        public void onInstallConversionFailure(String errorMessage) {
                            Log.d("AppsFlyerTest", "error getting conversion data: "+errorMessage);
                            com.unity3d.player.UnityPlayer.UnitySendMessage("AppsFlyerTrackerCallbacks" ,"didReceiveConversionDataWithError", errorMessage);

                        }
                        
                        public void onAppOpenAttribution(Map<String, String> attributionData) {
                        }
                        
                        public void onAttributionFailure(String errorMessage) {
                            
                        }
                        
                    });
                    
                }
            }
        } catch(Exception e){
            Log.d("AppsFlyerUnity", "Could not fetch devkey "+e.getMessage());
        }
        
        Log.d("AppsFlyerUnity", "set dev key");

        AppsFlyerLib.sendTracking(this);

    }

       
} 