using UnityEngine;

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

class AssetBundleCacheLoader {

	private static Dictionary<string, AssetBundle> onMemoryAssetBundleDict = new Dictionary<string, AssetBundle>();
	private static List<string> cachedAssetBundleLoadingList = new List<string>();
	

	/**
		load resource from Unity's AssetBundle cache.
	*/
	public static IEnumerator LoadCachedBundle <T> (
		string bundleName,
		string resourceName,
		uint crc,
		System.Action<string, T> succeeded,
		System.Action<string, string> failed) where T : UnityEngine.Object {

		Debug.Log("start LoadCachedBundle for Resource:" + resourceName + " from bundleName:" + bundleName);

		/*
			wait if marked as loading.
		*/
		while (cachedAssetBundleLoadingList.Contains(bundleName)) {
			Debug.Log("belonging assetBundle is loading. assetBundleName:" + bundleName + " resourceName:" + resourceName);
			yield return null;
		}
		
		/*
			load if already allocated to onMemoryCache = onMemoryAssetBundleDict.
		*/
		if (onMemoryAssetBundleDict.ContainsKey(bundleName)) {
			var assetBundle1 = onMemoryAssetBundleDict[bundleName];
			var loadedResource1 = (T)assetBundle1.Load(resourceName, typeof(T));

			if (loadedResource1 == null) {
				failed(resourceName, "resouce-is-null-in-assetBundle-1:" + bundleName);
				yield break;
			}

			Debug.Log("load from cache is done, resourceName:" + resourceName);
			succeeded(resourceName, loadedResource1);
			yield break;
		}

		// not on memory yet, start loading from AssetBundle cache.

		string url = AssetBundleDownloader.GetFileProtocolUrlForAssetBundle(bundleName);
		
		var isCached = Caching.IsVersionCached(url, AssetBundleDownloader.FIXED_CACHED_SLOT_VERSION);// use fixed version parameter. it's not version, this is SLOT.
		if (!isCached) {
			failed(resourceName, "assetBundle-not-cached:" + url + " maybe expired.");
			yield break;
		}

		// start loading from Unity's AssetBundle cache. set loading mark.
		cachedAssetBundleLoadingList.Add(bundleName);
		
		using (var www = WWW.LoadFromCacheOrDownload(url, AssetBundleDownloader.FIXED_CACHED_SLOT_VERSION, crc)) {// use fixed version parameter. it's not version, this is SLOT.
			yield return www;

			if (!String.IsNullOrEmpty(www.error)) {
				if (cachedAssetBundleLoadingList.Contains(bundleName)) cachedAssetBundleLoadingList.Remove(bundleName);
				failed(resourceName, "www-error:" + www.error);
				yield break;
			}
		
		
			// remove loading mark.
			if (cachedAssetBundleLoadingList.Contains(bundleName)) cachedAssetBundleLoadingList.Remove(bundleName);

			var assetBundle2 = www.assetBundle;

			// set to on-memory cache
			onMemoryAssetBundleDict[bundleName] = assetBundle2;
		}
		
		
		var loadedResource2 = (T)onMemoryAssetBundleDict[bundleName].Load(resourceName, typeof(T));
		
		if (loadedResource2 == null) {
			if (cachedAssetBundleLoadingList.Contains(bundleName)) cachedAssetBundleLoadingList.Remove(bundleName);
			failed(resourceName, "resouce-is-null-in-assetBundle-2:" + bundleName);
			yield break;
		}

		succeeded(resourceName, loadedResource2);
	}
}