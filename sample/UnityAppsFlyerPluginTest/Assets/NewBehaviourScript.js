﻿#pragma strict

function Start () {
    AppsFlyer.setCurrencyCode("USD");
    AppsFlyer.setCustomerUserID("someId");
    AppsFlyer.trackEvent("aaa","bb");
    Debug.Log("About to call loadConversionData ");
    AppsFlyer.loadConversionData(this.name,"takeConversionData");
}

function Update () {

}

function takeConversionData(json){
    Debug.Log("AppsFlyer conversion data: "+json);
}

