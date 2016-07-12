package com.appsflyer;


import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.Window;
import android.util.Log;

/**
 * Created by tarasleskiv on 29/01/16.
 *
 * This activity is created because we cannot get Activity.onNewIntent callback in Unity when the app is running in background.
 *
 * When the app is running in background and opened from e.g. browsers intent url, this activity intercepts the intent and forwards in to GetSocial.
 */
public class GetDeepLinkingActivity extends Activity
{
	private static String TAG = "AppsflyerDeepLinkActivity";

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		requestWindowFeature(Window.FEATURE_NO_TITLE);
		
		//start main activity
		Intent newIntent = new Intent(this, getMainActivityClass());
		AppsFlyerLib.getInstance().setDeepLinkData(getIntent());

		this.startActivity(newIntent);
		finish();
	}

	private Class<?> getMainActivityClass() {
		String packageName = this.getPackageName();
		Intent launchIntent = this.getPackageManager().getLaunchIntentForPackage(packageName);
		try {
			return Class.forName(launchIntent.getComponent().getClassName());
		} catch (Exception e) {
			Log.e(TAG, "Unable to find Main Activity Class");
			return null;
		}
	}
}