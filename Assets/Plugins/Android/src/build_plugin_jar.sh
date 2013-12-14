#!/bin/sh

# RUN THIS FROM COMMAND LINE IF YOU CHANGE AppsFlyerOverrideActivity.java

UNITY_LIBS="/Applications/Unity/Unity.app/Contents/PlaybackEngines/AndroidPlayer/bin/classes.jar"
export UNITY_LIBS

ANDROID_SDK_ROOT="/Users/gilmeroz/Development/android-sdk-mac_x86"

export ANDROID_SDK_ROOT
CLASSPATH=../AF-Android-SDK.jar:$UNITY_LIBS:$ANDROID_SDK_ROOT/platforms/android-8/android.jar
export CLASSPATH

echo "Compiling ..."
echo $CLASSPATH
javac AppsFlyerOverrideActivity.java -classpath $CLASSPATH -d .

javap -s com.appsflyer.AppsFlyerOverrideActivity

echo "Creating jar file..."
jar cvfM ../AppsFlyerAndroidPlugin.jar com/

echo ""
echo "Done!"
