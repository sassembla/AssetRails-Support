#AssetRails Overview

what is this:  
-> Generating AssetBundles from **command line** and CI(e.g. Jenkins).

![jenkins](https://raw.githubusercontent.com/sassembla/AssetRails-Support/master/image/webInterface.png "jenkins")

##AssetBundle Generator as a command line tool

Like this.

	/Applications/Unity/Unity.app/Contents/MacOS/Unity -batchmode\
	 -quit -projectPath $(pwd)\
	 -executeMethod AssetRailsController.Bundlize


[command line args](https://github.com/sassembla/AssetRails-Support/blob/master/CommandLineArgs.md#assetrails-command-line-args)

##build pipeline
You can construct pipeline which called "route".

* import, prefabricate, bundlize, versioning.
* Run multiple routes in order.
Supported format is json & toml(experimental).

* AssetRails has Runner-API for each route.
You can programming it's runner.


##Web console & run Jenkins job
AssetRails has browser interface.  
![jenkins](https://raw.githubusercontent.com/sassembla/AssetRails-Support/master/image/webInterface.png "jenkins")

Also you can run Jenkins job from AssetRails Console if AssetRails contained project is under the Jenkins.



##folder format supported
Below is default folder format for AssetRails. but you can import your own data format which is supported by Unity,

also **.meta** files too.

![overview](https://raw.githubusercontent.com/sassembla/AssetRails-Support/master/image/overview.png "overview")


##manage AssetBundles
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
* AssetRails can only run under "External Version Control Support = visible Meta Files".
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
