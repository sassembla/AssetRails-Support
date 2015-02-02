#Versioning in deep

Bundlize some datas to one data are useful for game developing & driving.

But there are many troubles through update resources.
AssetRails helps solving these troubles, a little bit.


##"versioning" route

The "versioning" route outputs the "versioned" AssetBundles datas to below place by default.

	PROJECT_FOLDER/VersionedPool(AssetRails)/

☆図示、version/プラットフォームごと/AssetBundles、あとリスト
書いた。

This pool structure helps you:

* Bundle AssetBundles to the "version" includes "versionedList.json"
* Combine a part of old AssetBundles and new AssetBundles to "new version"


##Bundle AssetBundles to the "version" includes "versionedList.json"

Here is versioning-d folder, there are "versioned" AssetBundles in folder and also "versionedList.json" is exists.

☆AssetBunle x n とlistの中身
書いた。

versionedList.json contains the data like below.

versionedList.json
	
	{
		"versioned": 1,
		"AssetBundles": [
			{
				"bundleName": "A.assetBundle",
				"version": 1,
				"resourceNames": [
					"texture",
					"enemy"
				],
				"size": 45802,
				"crc": 2821581243
			},
			{
				"bundleName": "B.assetBundle",
				"version": 1,
				"resourceNames": [
					"texture",
					"hero"
				],
				"size": 53908,
				"crc": 293022807
			}
		]
	}
	
この辺に図解

name | detail
---|---
version | which version is contained this AssetBundle first, and also when the AssetBundle was last updated at version.
bundleName | the name of AssetBundle file.
size | the size of AssetBundle file
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

☆フォルダ絵、古いバージョンのAssetBundles1を持っていて、AssetBundle AとBがある。新version 2では、それにCをくわえたものにする。
A, B, C P25の左半分

sample command line is like below.

	-executeMethod AssetRailsController.Versioning\
		-v 2 -p iPhone --base-version 1

☆リストも更新されるが、古いAssetBundlesにはそのversionがくっつけられる。
その結果で出来るversion 2のフォルダの内容とリスト
P25


##Overwrite the old AssetBundles by new AssetBundles
If version 1 contains AssetBundle named "A.assetBundle", and also new AssetBundles contains named "A.assetBundle" too,

the "A.assetBundle" will be overwritten by new version, in this case it is **"version 2"**.

☆フォルダの様子、リストの様子
書いた奴の変更でいける

##Combine, but also exclude specific AssetBundles to new version
Another case, if you don't want to include "version 1"'s AssetBundle "A.assetBundle" anymore,
you can exclude it by -e --exclude-assets option and specific list file of bundle names.

PROJECT_FOLDER/exclude.json

	["A.assetBundle"]
	

And sample command line is below,

	-executeMethod AssetRailsController.Versioning\
		-v 2 -p iPhone --base-version 1 --exclude-assets excludes.json


will generate these files & versionedList.json.


☆フォルダの様子、リストの様子
書いた奴の変更でいける

