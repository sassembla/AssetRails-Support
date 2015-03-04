#AssetRails Overview

[http://u3d.as/content/sassembla/asset-rails](http://u3d.as/content/sassembla/asset-rails)

what is this:  
-> Tool for generating AssetBundles from **command line** and CI(e.g. Jenkins).

![jenkins](https://raw.githubusercontent.com/sassembla/AssetRails-Support/master/image/webInterface.png "jenkins")

##AssetBundle Generator as a command line tool

command line for Mac:

	/Applications/Unity/Unity.app/Contents/MacOS/Unity -batchmode\
	 -quit -projectPath $(pwd)\
	 -executeMethod AssetRailsController.Bundlize


[command line args document](https://github.com/sassembla/AssetRails-Support/blob/master/CommandLineArgs.md#assetrails-command-line-args)

##Trial version
Please try AssetRails with free version.

[Trial version of AssetRails](https://github.com/sassembla/AssetRails-Support/tree/master/Samples/Sample_AssetRails)


##Sample usage
####From command line
from command line, you can generate AssetBundles from sample resources in 4 step.


1. decompress "PROJECT_FOLDER/AssetRails/Samples/Resources(AssetRails).zip"
1. move opened "Resources(AssetRails)" folder into PROJECT_FOLDER/
1. open Assets/AssetRails/AssetRailsConsole.html
1. running command line

in PROJECT_FOLDER, 

	Mac:
		sh Assets/AssetRails/Samples/ShellScript/run.sh
	
	Windows:
		"Assets\AssetRails\SampleScripts\Bat\run.bat"



you can see progress of generating!

####From Jenkins
here is generate AssetBundles via Jenkins in 5 steps.

1. setup Jenkins first.
1. create new Jenkins job.
1. move the project which contains AssetRails into Jenkins's job workspace folder.
	e.g. "/Users/Shared/Jenkins/Home/jobs/JENKINS_JOB/workspace/PROJECT_FOLDER/AssetRails"

1. in Jenkins setting, setup shell script or batch file like below.

run scripts from PROJECT_FOLDER.

	cd PROJECT_FOLDER

	Mac:
		sh Assets/AssetRails/Samples/ShellScript/run.sh
	
	Windows:
		"Assets\AssetRails\SampleScripts\Bat\run.bat"
		
+5. open Assets/AssetRails/AssetRailsConsole.html from Jenkins's workspace in browser.

	http://URL_OF_JENKINS_JOB/ws/AssetRails/AssetRailsConsole.html

That's all!


##Build pipeline
You can construct pipeline which called "route".

* import, prefabricate, bundlize, versioning.
* Run multiple routes in order.
Supported order format is json & toml(experimental).

[command line args document](https://github.com/sassembla/AssetRails-Support/blob/master/CommandLineArgs.md#assetrails-command-line-args)

* AssetRails has Runner-API for each route.
You can programming it's runner.

[runners API document](https://github.com/sassembla/AssetRails-Support/blob/master/RunnersAPIDocument.md#assetrails-runners-api-document)

##Web console & run Jenkins job
AssetRails has browser interface.  
![jenkins](https://raw.githubusercontent.com/sassembla/AssetRails-Support/master/image/webInterface.png "jenkins")

Also you can run Jenkins job from AssetRails Console if AssetRails contained project is under the Jenkins.



##Folder format
Below is default folder-format for AssetRails.  
You can import every data which is supported by Unity,  
including **.meta** files too.

![overview](https://raw.githubusercontent.com/sassembla/AssetRails-Support/master/image/overview.png "overview")


##Manage AssetBundles
###Generate AssetBundle-data-list by versioning.

versionedList.json

    {
        "versioned": 1,
        "AssetBundles": [
            {
                "bundleName": "characters_enemy.assetBundle",
                "revision": 1,
                "resourceNames": [
                    "texture",
                    "enemy_material",
                    "enemy_prefab"
                ],
                "size": 727140,
                "crc": 235040124
            },
            {
                "bundleName": "characters_hero.assetBundle",
                "revision": 1,
                "resourceNames": [
                    "texture",
                    "hero_material",
                    "hero_prefab"
                ],
                "size": 727140,
                "crc": 2232339018
            }
        ]
    }	

full example is in [Sample project](https://github.com/sassembla/AssetRails-Support/tree/master/Samples/AssetBundleReaderProject).

###fast multi platform bundlize & manage support 
Generate & hold versioned-AssetBundles for each platform.

###use inherited AssetBundles supported.
You can generate new versioned-AssetBundles group with old versioned-AssetBundles.

below example will generate new version 2 versiond-AssetBundles from version 1's AssetBundles and new AssetBundlse.

	/Applications/Unity/Unity.app/Contents/MacOS/Unity -batchmode\
	 -quit -projectPath $(pwd)\
	 -executeMethod AssetRailsController.Versioning -v 2 -p iPhone\
	 -b 1

see [versioning in deep](https://github.com/sassembla/AssetRails-Support/blob/master/Versioning.md#versioning-in-deep).


#Future
* Unity5 is beta support. but You can use this Asset with Unity 5's new feature, maybe.


#Caution
* AssetRails can only run under "External Version Control Support = Visible Meta Files".
* This Asset is **not** for generating AssetBundles without Unity Pro license.



##Online Support
Let's issue,, but, if you won't,

fill the check list below

**checklist:**

1. Unity version : Unity ~
2. Platform: Win7,8.x, or Mac OS X 10.x.x

then

please contact
[https://twitter.com/sassembla](https://twitter.com/sassembla)
or
[sassembla@mac.com](mailto:sassembla@mac.com)
