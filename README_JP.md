
Unity
AppsFlyer iOSプラグイン（v2.5.3.14.2）の導入
AppsFlyerのプラグインをインストールする方法:
1. AppsFlyerUnityPlugin.unitypackageをUnityプロジェクトにインポートします.

2. Assets/Editor/appcontroller.pyを開き、AppsFlyerコードとApple IDを設定します。

[AppsFlyerTracker sharedTracker].appleAppID = @"APPLE_APP_ID_HERE"; [AppsFlyerTracker sharedTracker].appsFlyerDevKey = @"APPSFLYER_DEV_KEY_HERE";

3. iOS向けにプロジェクトを構築します。 
アプリ内イベントAPIについては、APIiOS SDKインテグレーションガイドのセクション6を参照してください。

https://support.appsflyer.com/entries/80418519-iOS-SDK-%E3%81%AE%E3%83%80%E3%82%A6%E3%83%B3%E3%83%AD%E3%83%BC%E3%83%89-%E3%82%A4%E3%83%B3%E3%83%86%E3%82%B0%E3%83%AC%E3%83%BC%E3%82%B7%E3%83%A7%E3%83%B3%E3%82%AC%E3%82%A4%E3%83%89-v2-5-3-x-New-API-
SDKの導入テスト方法：https://support.appsflyer.com/entries/80614809-Apple-Store-%E3%81%AB%E6%8F%90%E5%87%BA%E5%89%8D%E3%81%8A%E3%82%88%E3%81%B3%E6%8F%90%E5%87%BA%E5%BE%8C%E3%81%AEAppsFlyer-iOS-SDK%E3%82%A4%E3%83%B3%E3%83%86%E3%82%B0%E3%83%AC%E3%83%BC%E3%82%B7%E3%83%A7%E3%83%B3%E3%83%86%E3%82%B9%E3%83%88

4.アプリケーション内での購入レシート検証（In-App Purchase Receipt Validation）

4.1. UnityコンポーネントにAppsFlyer Tracker Callbackスクリプトを添付します。

4.2. validateReceipt(string eventName, string failedEventName, string eventValue, string productIdentifier, double price, string currency); を呼び出します。

4.3. 詳細は、添付の「Sample」アプリを参照してください。


AppsFlyer Android プラグインの導入（2.3.1.17）

1. AppsFlyerのUnityプラグインからAssetsフォルダを、Unityプロジェクトにコピーします。
2. アプリケーションのマニフェストファイルを変更します：
2.1. Unityを開きプロジェクトを構築します。
2.2. 構築後、Temp/StagingAreaディレクトリをプロジェクト内に開き、AndroidManifest.xmlファイルを直接Assets/Plugins/Androidにコピーします。
2.3. コピーしたAndroidManifest.xmlを開き、アクティビティのandroid:nameをcom.appsflyer.AppsFlyerOverrideActivityに変更します。以下の例に近い構成になります：
<activity android:name="com.appsflyer.AppsFlyerOverrideActivity" android:launchMode=...
これでアンドロイド側にAppsFlyerエクステンションでアプリケーションをはじめるよう指定できます。
2.4. パーミッションを設定（未設定の場合）
複数のレシーバーを利用したい場合は、AppsFlyerのAndroidインテグレーションガイドを参照してください。
https://support.appsflyer.com/entries/80418419-Android-SDK-%E3%81%AE%E3%83%80%E3%82%A6%E3%83%B3%E3%83%AD%E3%83%BC%E3%83%89-%E3%82%A4%E3%83%B3%E3%83%86%E3%82%B0%E3%83%AC%E3%83%BC%E3%82%B7%E3%83%A7%E3%83%B3%E3%82%AC%E3%82%A4%E3%83%89
2.5. Google Advertising IDを収集する方法：Google Play Servicesを導入してください。Android SDKマネージャを開き、Extrasフォルダまでスクロールダウンし、Google Play Services パッケージがダウンロードされていることを確認してください。
参照：http://developer.android.com/intl/ja/google/play-services/setup.html
AndroidManifest.xmlファイルの以下の行をアンコメントします：
<meta-data android:name="com.google.android.gms.version"
           android:value="@integer/google_play_services_version" />

2.6. 以下の行をアプリケーションの最後のセクションに追加し、AppsFlyerのDeveloper Keyを設定します。
<meta-data android:name="AppsFlyerDevKey" android:value="YOUR_DEV_KEY_HERE"/>

2.7. Androidパッケージ名を設定します。
<manifest xmlns:android="http://schemas.android.com/apk/res/android" android:installLocation="preferExternal" android:theme="@android:style/Theme.NoTitleBar" package="YOUR_PACKAGE_NAME_HERE"
3. アプリを構築し実行します。
SDKインテグレーションのテスト方法：
https://support.appsflyer.com/entries/80901515-Google-Play-%E3%81%AB%E6%8F%90%E5%87%BA%E5%89%8D%E3%81%8A%E3%82%88%E3%81%B3%E6%8F%90%E5%87%BA%E5%BE%8C%E3%81%AEAppsFlyer-Android-SDK-%E3%82%A4%E3%83%B3%E3%83%86%E3%82%B0%E3%83%AC%E3%83%BC%E3%82%B7%E3%83%A7%E3%83%B3%E3%83%86%E3%82%B9%E3%83%88

プラグインAPI：
イベントトラッキング：

AppsFlyer.trackEvent("MyEventName","TheEventValue");

アプリ内の購入イベントに関して、通貨コードを設定する方法：通貨コードは3文字のISO 4217コードを利用してください（デフォルトはUSDです）。

AppsFlyer.setCurrencyCode("GBP");

アプリで使われているユーザーIDを設定する方法：

AppsFlyer.setCustomerUserID("someId");


アプリ内のリッチイベントを計測する方法：添付のUnity「Sample」アプリのTrackEventTests.csを参照してください。
AppsFlyerのConversion Dataを利用する方法
AppsFlyerのConversion Dataをサーバーからロードするには、AppsFlyerTrackerCallbacks prefabを追加し、AppsFlyerTrackerCallbacksスクリプトを添付する必要があります。

CS APIを使ってAppsFlyerを初期化する方法：
上記 iOSのPostprocessBuildPlayer PERLスクリプトやAndroidのAppsFlyerOverrideActivityの代わりに直接APIを利用する場合は、以下のように直接APIを利用します。

AppsFlyer.setAppsFlyerKey("YOUR_DEV_KEY_HERE");
AppsFlyer.setAppID("APPLE_APP_ID"); // iOSの場合のみ必須。
AppsFlyer.trackAppLaunch();


