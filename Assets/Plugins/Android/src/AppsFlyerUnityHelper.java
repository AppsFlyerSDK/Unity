package com.appsflyer;

import org.json.JSONException;
import org.json.JSONObject;
import java.util.Map;
import android.content.Context;
import android.util.Log;

public class AppsFlyerUnityHelper  {
    
    public static void createConversionDataListener(Context context,String callbackObject,String callbackMethod, String callbackFailedMethod){
        
        final String callbackMethodName = callbackMethod;
        final String CallbackMethodFailedName = callbackFailedMethod;
        final String callbackObjectName = callbackObject;
        
        ConversionDataListener listener = new ConversionDataListener(){
          public void onConversionDataLoaded(Map<String,String> conversionData){
             JSONObject jsonObject = new JSONObject(conversionData);
                   com.unity3d.player.UnityPlayer.UnitySendMessage(callbackObjectName,callbackMethodName,jsonObject.toString());
               }
            
            public void onConversionFailure(String errorMessage){
                 com.unity3d.player.UnityPlayer.UnitySendMessage(callbackObjectName,CallbackMethodFailedName, errorMessage);
             }    
           };
          AppsFlyerLib.getConversionData(context,listener);
    }
    
    
    public static void createValidateInAppListener(Context context,String callbackObject,String callbackMethod,String callbackFailedMethod){
        final String callbackMethodName = callbackMethod;
        final String callbackMethodFailedName = callbackFailedMethod;
        final String callbackObjectName = callbackObject;
        
        AppsFlyerInAppPurchaseValidatorListener listener = new AppsFlyerInAppPurchaseValidatorListener(){
            public void onValidateInApp(Boolean var){
                Log.i ("AppsFlyerLibUnityhelper", "onValidateInApp called.");
                com.unity3d.player.UnityPlayer.UnitySendMessage(callbackObjectName,callbackMethodName,var.toString());
            }
            
            public void onValidateInAppFailure(String errorMessage){
                Log.i ("AppsFlyerLibUnityhelper", "onValidateInAppFailure called.");
                com.unity3d.player.UnityPlayer.UnitySendMessage(callbackObjectName,callbackMethodFailedName,errorMessage);
            }
          };
        AppsFlyerLib.registerValidatorListener (context, listener);

    }

}