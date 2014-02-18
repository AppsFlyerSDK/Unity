package com.appsflyer;

import org.json.JSONException;
import org.json.JSONObject;
import java.util.Map;
import android.content.Context;

public class AppsFlyerUnityHelper  {
    
    public static void createConversionDataListener(Context context,String callbackObject,String callbackMethod){
        final String callbackMethodName = callbackMethod;
        final String callbackObjectName = callbackObject;
           ConversionDataListener listener = new ConversionDataListener(){
               public void onConversionDataLoaded(Map<String,String> conversionData){
                   JSONObject jsonObject = new JSONObject(conversionData);
                   com.unity3d.player.UnityPlayer.UnitySendMessage(callbackObjectName,callbackMethodName,jsonObject.toString());
               }
            
               public void onConversionFailure(String errorMessage){}
            
           };
        AppsFlyerLib.getConversionData(context,listener);
    }
    
} 