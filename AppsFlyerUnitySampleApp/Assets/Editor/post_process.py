import os
from sys import argv
from mod_pbxproj import XcodeProject
import appcontroller
path = argv[1]
fileToAddPath = argv[2]


print('post_process.py xcode build path --> ' + path)
print('post_process.py third party files path --> ' + fileToAddPath)

print('Step 1: add system libraries ')
#if framework is optional, add `weak=True`
project = XcodeProject.Load(path +'/Unity-iPhone.xcodeproj/project.pbxproj')
project.add_file('System/Library/Frameworks/AdSupport.framework', tree='SDKROOT')

print('Step 2: modify the AppController')
appcontroller.touch_implementation(path + '/Classes/UnityAppController.mm')
appcontroller.touch_header(path + '/Classes/UnityAppController.h')

print('Step 3: change build setting')
project.add_other_buildsetting('GCC_ENABLE_OBJC_EXCEPTIONS', 'YES')

print('Step 4: save our change to xcode project file')
if project.modified:
    project.backup()
    project.saveFormat3_2()
