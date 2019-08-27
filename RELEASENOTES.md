# Release Notes

### 4.20.2
* Updated Android SDK to v4.10.2
* Updated iOS SDK to v4.10.3
* Removed iOS debug logs 
* Added `setOneLinkCustomDomain(params string[] domains)` API for Android and iOS
* Added `setValue(string value)` API for iOS

### 4.20.1
* Fix for ios debug logs

### 4.20.0
* Updated Android SDK to v4.10.0
* Added API: `setUserEmails(EmailCryptType cryptType, params string[] userEmails)` for passing user emails
* Added API: `setResolveDeepLinkURLs(params string[] userEmails)` for ESP

### 4.19.0
* Updated Android SDK to v4.9.0
* Updated iOS SDK to v4.9.0
* Fixed missing iOS `onOpenStoreLinkGenerated` callback method in `AppsFlyerCallbacks.cs`

### 4.18.0
* Updated Android SDK to v4.8.18
* Updated iOS SDK to v4.8.10
* Removed the `trackEvent(string eventName,string eventValue)` API 
- Use `trackRichEvent(string eventName, Dictionary<string, string> eventValues)` for event tracking.
* Fix for empty event values exception 

### 4.17.0
* Updated Android SDK to v4.8.13
* Updated iOS SDK to v4.8.6
* `setPreinstallAttribution(string MediaSource, string Campaign, string Site_Id);` API for Android.
* `setMinTimeBetweenSessions(int seconds)` API for Android and iOS
* Cross Promotion and User Invite APIs:

 `setAppInviteOneLinkID(string OneLinkID);`

 `generateUserInviteLink(Dictionary<string,string> parameters, string callbackObject, string 
callbackMethod, string callbackFailedMethod);`

 `trackCrossPromoteImpression(string promotedAppID, string campaign);`

 `trackAndOpenStore(string promotedAppID, string campaign, Dictionary<string,string> customParams);`

### 4.16.4
* Upated Android SDK to v4.8.11 
* Updated iOS SDK to v4.8.4
* stopTracking(bool) API for iOS and Android
* setCollectAndroidID(bool) API for Android

### 4.16.3 (Internal)
* Update native SDK for Unity
* init(key) called twice on Android (65)

### 4.16.2
* Update native SDK for Unity
    * Android v4.8.8
* Added `setAdditionalData(Dictionary<string,string>)` API for Android / iOS

### 4.16.0
* Update native SDK for Unity
    * Android v4.8.6
	* iOS v4.8.1 (b602)
* Support new google install referrer   

### 4.15.1
* Update native SDK for Unity
    * Android v4.8.3
	* iOS v4.8.1 (b602)
* Added new API for Android: `init(<DEV_KEY>,<CALLBACK_OBJECT>)`   

### 4.14.3
* Update native SDK for Unity (RD-4115)   
	* Android v4.7.3
	* iOS v4.7.11
* init(key) called twice on Android (65)

### 4.14.1
* Fixed iOS Receipt validation issue (RD-4796)



### 4.14.0
* Update Appsflyer Android SDK version to 4.6.2
* Fixed Android Deeplink callbacks.



### 4.13.0
* Update Appsflyer iOS SDK version to 4.5.12
* Update Appsflyer Android SDK version to 4.6.1
* Fixed Android un-install



### 4.12.0
* Update Appsflyer iOS SDK version to 4.5.9
* Supports Apple Search ads



### 4.11.0
* Added support for universal deeplinking.
* Android SDK v. 4.6.0
* iOS SDK v. 4.5.5

### 4.10.1
* Fixed push token for iOS uninstall


### 4.10.0
* Deep link support for Android & iOS
* Uninstall support for Android & iOS
* Support testing using the Unity Editor
