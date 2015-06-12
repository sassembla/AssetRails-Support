#Versioning in deep

Bundlize some datas to one data are useful for game developing & driving.

But there are many troubles through update resources.
AssetRails helps solving these troubles, a little bit.


##"versioning" route

The "versioning" route outputs the "versioned" AssetBundles datas to below place by default.

	PROJECT_FOLDER/VersionedPool(AssetRails)/

![versionedBase](https://raw.githubusercontent.com/sassembla/AssetRails-Support/master/image/versionedBase.png "versionedBase")

This pool structure helps you:

* Bundle AssetBundles to the "version" includes "versionedList.json"
* Combine a part of old AssetBundles and new AssetBundles to "new version"


##"versionedList.json" data

There are "versioned" AssetBundles in version/platform folder and also "versionedList.json" is exists.

versionedList.json contains the data like below.

versionedList.json
	
	{
		"versioned": 1,
		"AssetBundles": [
			{
				"bundleName": "A.assetBundle",
				"revision": 1,
				"resourceNames": [
					"texture",
					"enemy"
				],
				"size": 45802,
				"crc": 2821581243
			},
			{
				"bundleName": "B.assetBundle",
				"revision": 1,
				"resourceNames": [
					"texture",
					"hero"
				],
				"size": 53908,
				"crc": 293022807
			}
		]
	}
	
parameter of versionedList itself:

name | detail
---|---
versioned | version of AssetBundles.
AssetBundles | the list of AssetBundle's info.

parameter of AssetBundle:

name | detail
---|---
bundleName | the name of AssetBundle file.
revision | the number when this AssetBundle was contained first time, and also when the AssetBundle was last updated.
size | the size of AssetBundle file. actual size or compressed. see [bundlize](https://github.com/sassembla/AssetRails-Support/blob/master/CommandLineArgs.md#bundlize)
crc | the crc of AssetBundle which written by Unity.
resourceNames | the list of resource-names of bundled in this AssetBundle.

When you download this "versionedList.json", your client side app also has these information of AssetBundles.


##Combine a part of old AssetBundles and new AssetBundles to "new version"

If your client side app is downloaded by players,
and assume that they maybe downloaded "version 1" resources already.

When you encountered the timing to update AssetBundles for your client side app,
although almost the resource which players already downloaded is still relevant.

If you have old-versioned AssetBundles (assume that version is "1"),
You can create The version "2" AssetBundles from combination of the version "1"'s AssetBundles + new additional AssetBundles.


![append](https://raw.githubusercontent.com/sassembla/AssetRails-Support/master/image/versionedAppend.png "append")

sample command line is like below.

	-executeMethod AssetRailsController.Versioning\
		-v 2 -p iPhone --base-version 1


##Overwrite the old AssetBundles by new AssetBundles
If version 1 contains AssetBundle named "A.assetBundle", and also new AssetBundles contains named "A.assetBundle" too,

the "A.assetBundle" will be overwritten by new version, in this case it is **"version 2"**.

![update](https://raw.githubusercontent.com/sassembla/AssetRails-Support/master/image/versionedUpdate.png "update")

##Combine, but also exclude specific AssetBundles to new version
Another case, if you don't want to include "version 1"'s AssetBundle "A.assetBundle" anymore,
you can exclude it by -e --exclude-assets option and specific list file of bundle names.

PROJECT_FOLDER/exclude.json

	["enemy.asset"]

And sample command line is below,

	-executeMethod AssetRailsController.Versioning\
		-v 2 -p iPhone --base-version 1 --exclude-assets excludes.json


will generate these files & versionedList.json.

![exclude](https://raw.githubusercontent.com/sassembla/AssetRails-Support/master/image/versionedExclude.png "exclude")


