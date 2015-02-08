#Sample project for AssetRails output


##sample usage
* open Assets/SampleAssetBundleLoader.unity
* Play

will load AssetBundles data list from **"AssetBundleReaderProject/VersionedPool(AssetRails)/1/iPhone/versionedList.JSON"**,

 then load and open these AssetBundles from folder.

##caution
These AssetBundles were built by Unity 4.3.x, which has no-compatibility to Unity 4.5.x, 4.6.x, 5.0.x(beta).

And

The default platform setting is "iPhone".
Defined by below.

[Assets/Scripts/AssetBundleDownloader.cs](https://github.com/sassembla/AssetRails-Support/blob/master/Samples/AssetBundleReaderProject/Assets/Scripts/AssetBundleDownloader.cs#L14)


	public const string PLATFORM_STR = "iPhone";
	
Please set it to "WebPlayer" for WebPlayer play.