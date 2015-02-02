using UnityEngine;

using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using MiniJSONForSamlples;

public class AssetBundleDownloader : MonoBehaviour {

	// use file protocol.
	public const string fileProtocolStr = "file://";

	// AssetBundle downloadable path.
	public const string assetBundlePath = "VersionedPool(AssetRails)/";

	// download version of AssetBundles.
	public const int version = 1;

	// fileName of versionedList.json
	public const string listName = "versionedList.JSON";



	void Start () {
		// DEMO code.
		Caching.CleanCache();

		StartCoroutine(DownloadList());
	}


	/**
		Download versionedList.json from local,
		then load sample AssetBundle.
	*/
	IEnumerator DownloadList() {
		// wait for cache mechanism is ready.
		if (!Caching.ready) yield return null;

		string url = GetFileProtocolUrlForList();
		
		var www = new WWW(url);
		yield return www;

		if (!String.IsNullOrEmpty(www.error)) {
			Debug.LogError("DownloadList www.error:" + www.error);
			yield break;
		}


		var remoteListDataStr = www.text;

		// transform text data to dictionary.
		var remoteListData = Json.Deserialize(remoteListDataStr) as Dictionary<string, object>;


		int remote_res_ver;
		int.TryParse(remoteListData["versioned"].ToString(), out remote_res_ver);
		var remoteAssetBundlesInfoList = remoteListData["AssetBundles"] as List<object>;

		// construct data dictionary of AssetBundle from versionedList.json.
		var remoteAssetBundlesDict = new Dictionary<string, BundleData>();

		foreach (var source in remoteAssetBundlesInfoList) {
			var assetBundleInfo = source as Dictionary<string, object>;
			var bundleData = new BundleData(assetBundleInfo);
			var bundleName = bundleData.bundleName;
			remoteAssetBundlesDict[bundleName] = bundleData;
		}
		
		// collect downloadable AssetBundle names. (in this time, all AssetBundle should be downloaded.)
		var shouldDownloadAssetBundleNames = remoteAssetBundlesDict.Values.Select(bundleData => bundleData.bundleName).ToList();

		Action<string> completed = (string cacheCompletedBundleName) => {
			if (shouldDownloadAssetBundleNames.Contains(cacheCompletedBundleName)) shouldDownloadAssetBundleNames.Remove(cacheCompletedBundleName);

			if (!shouldDownloadAssetBundleNames.Any()) {
				Debug.Log("cache Assetbundle completed.");
				AllAssetBundleCached(remoteAssetBundlesDict);
			}
		};


		foreach (var bundleName in shouldDownloadAssetBundleNames) {
			var bundleData = remoteAssetBundlesDict[bundleName];
			StartCoroutine(DownloadAssetBundleThenCache(bundleData, completed));
		}
	}


	IEnumerator DownloadAssetBundleThenCache (BundleData bundleData, Action<string> completed) {
		var bundleName = bundleData.bundleName;
		string url = GetFileProtocolUrlForAssetBundle(bundleName);
		int version = bundleData.version;
		uint crc = bundleData.crc;

		// get AssetBundle from file path.
		var www = WWW.LoadFromCacheOrDownload(url, version, crc);

		yield return www;

		if (!String.IsNullOrEmpty(www.error)) {
			Debug.LogError("DownloadAssetBundleThenCache www.error:" + www.error);
			yield break;
		}
		
		while (!Caching.IsVersionCached(url, version)) {
			// wait for cached.
			yield return null;
		}
		var assetBundle = www.assetBundle;

		// release AssetBundle resource now.
		assetBundle.Unload(false);

		www.Dispose();

		completed(bundleName);
	}

	/**
		loading sample.
	*/
	private void AllAssetBundleCached (Dictionary<string, BundleData> cachedAssetBundleDict) {
		
		// load sample AssetBundle resources from cache. These bundled resources are included in Resources(AssetRails) folder.

		/*
			using resources
		*/
		var characterResourceHero = "characters_hero.assetBundle";
		StartCoroutine(AssetBundleCacheLoader.LoadCachedBundle(
			characterResourceHero,
			cachedAssetBundleDict[characterResourceHero].resourceNames[1],// hero.prefab
			cachedAssetBundleDict[characterResourceHero].version,
			cachedAssetBundleDict[characterResourceHero].crc,
			(string loadSucceededResourceName, GameObject prefab) => {
				var heroCube = Instantiate(prefab);
				// var renderer = sushiCube.GetComponent<MeshRenderer>();
				// renderer.material.mainTexture = t;
			},
			(string loadFailedResourceName, string reason) => {
				Debug.LogError("loadFailedResourceName:"+loadFailedResourceName + " /reason:" + reason);
			}
		));

		var characterResourceEnemy = "characters_enemy.assetBundle";
		StartCoroutine(AssetBundleCacheLoader.LoadCachedBundle(
			characterResourceEnemy,
			cachedAssetBundleDict[characterResourceEnemy].resourceNames[1],// enemy.prefab
			cachedAssetBundleDict[characterResourceEnemy].version,
			cachedAssetBundleDict[characterResourceEnemy].crc,
			(string loadSucceededResourceName, GameObject prefab) => {
				var enemyCube = Instantiate(prefab);
			},
			(string loadFailedResourceName, string reason) => {
				Debug.LogError("loadFailedResourceName:"+loadFailedResourceName + " /reason:" + reason);
			}
		));

		/*
			using image resources.
			set to Cube as texture.
		*/
		var imageResource1 = "images_image_01.assetBundle";
		StartCoroutine(AssetBundleCacheLoader.LoadCachedBundle(
			imageResource1,
			cachedAssetBundleDict[imageResource1].resourceNames[0],
			cachedAssetBundleDict[imageResource1].version,
			cachedAssetBundleDict[imageResource1].crc,
			(string loadSucceededResourceName, Texture2D t) => {
				var sushiCube = GameObject.Find("Cube1");
				var renderer = sushiCube.GetComponent<MeshRenderer>();
				renderer.material.mainTexture = t;
			},
			(string loadFailedResourceName, string reason) => {
				Debug.LogError("loadFailedResourceName:"+loadFailedResourceName + " /reason:" + reason);
			}
		));

		var imageResource2 = "images_image_02.assetBundle";
		StartCoroutine(AssetBundleCacheLoader.LoadCachedBundle(
			imageResource2,
			cachedAssetBundleDict[imageResource2].resourceNames[0],
			cachedAssetBundleDict[imageResource2].version,
			cachedAssetBundleDict[imageResource2].crc,
			(string loadSucceededResourceName, Texture2D t) => {
				var udonCube = GameObject.Find("Cube2");
				var renderer = udonCube.GetComponent<MeshRenderer>();
				renderer.material.mainTexture = t;
			},
			(string loadFailedResourceName, string reason) => {
				Debug.LogError("loadFailedResourceName:"+loadFailedResourceName + " /reason:" + reason);
			}
		));

		/*
			models
		*/

	}


	public static string GetFileProtocolUrlForAssetBundle (string bundleName) {
		var dataPath = Application.dataPath;
		var projectPath = Directory.GetParent(dataPath).ToString();

		return AssetBundleDownloader.fileProtocolStr
			 + CombineAllPath(
			 	projectPath, 
			 	AssetBundleDownloader.assetBundlePath, 
			 	AssetBundleDownloader.version.ToString(),
			 	"iPhone",
			 	bundleName
			 );
	}


	public static string GetFileProtocolUrlForList () {
		var dataPath = Application.dataPath;
		var projectPath = Directory.GetParent(dataPath).ToString();

		return AssetBundleDownloader.fileProtocolStr
			 + CombineAllPath(projectPath, 
			 	AssetBundleDownloader.assetBundlePath, 
			 	AssetBundleDownloader.version.ToString(),
			 	"iPhone",
			 	AssetBundleDownloader.listName
			 );
	}


	private static string CombineAllPath (params string [] paths) {
		string combinedPath = string.Empty;
		foreach (var path in paths) {
			combinedPath = Path.Combine(combinedPath, path);
		}
		return combinedPath;
	}
}
