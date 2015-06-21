#!/bin/sh

# RUN THIS FROM COMMAND LINE IF YOU CHANGE AppsFlyerOverrideActivity.java

echo "Handle Android jar"

UNITY_LIBS="/Applications/Unity/Unity.app/Contents/PlaybackEngines/AndroidPlayer/Development/bin/classes.jar"
export UNITY_LIBS

ANDROID_SDK_ROOT="/Users/golan/Documents/Dev/android-sdk-macosx/"

export ANDROID_SDK_ROOT
CLASSPATH=../AF-Android-SDK.jar:$UNITY_LIBS:$ANDROID_SDK_ROOT/platforms/android-21/android.jar
export CLASSPATH

echo "Compiling ..."
echo $CLASSPATH
javac *.java -classpath $CLASSPATH -d .

javap -s com.appsflyer.AppsFlyerOverrideActivity
javap -s com.appsflyer.AppsFlyerUnityHelper

echo "Manifest-Version: 1.0" > MANIFEST.MF

echo "Creating jar file..."
jar cvfM ../AppsFlyerAndroidPlugin.jar com/

echo ""
echo "Done!"
