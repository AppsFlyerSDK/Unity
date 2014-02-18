package com.appsflyer;

import com.unity3d.player.UnityPlayerActivity;

import android.os.Bundle;
import android.util.Log;
import org.json.JSONException;
import org.json.JSONObject;
import java.util.Map;

public class AppsFlyerOverrideActivity extends UnityPlayerActivity {
    
    protected void onCreate(Bundle savedInstanceState) {
        
        // call UnityPlayerActivity.onCreate()
        super.onCreate(savedInstanceState);
        
        Log.d("AppsFlyer", "onCreate called!");
        AppsFlyerLib.setAppsFlyerKey("udqj9oVC22BQdWPoQQWMsN");
        Log.d("AppsFlyer", "set dev key");

        AppsFlyerLib.sendTracking(this);

    }

       
} 