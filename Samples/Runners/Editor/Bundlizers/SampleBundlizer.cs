using UnityEditor;
using UnityEngine;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using AssetRails;

// "AssetRails.BundlizerBase" class is base class for handling generate AssetBundle for each "bundleName".
// 
// see online documents. 
//	https://github.com/sassembla/AssetRails-Support/blob/master/RunnersAPIDocument.md#bundlizer
public class SampleBundlizer : AssetRails.BundlizerBase {

	public override void Bundlize (string categoryName, string bundleName, Dictionary<string, string> resNameAndResourceLoadablePathsDict, Dictionary<BuildTarget, string> recommendedOutputPathDict, Dictionary<string, string> memoDict) {
		Debug.Log("Bundlize:categoryName:" + categoryName);
		Debug.Log("Bundlize:bundleName:" + bundleName);
		
		memoDict[bundleName] = "Bundlize";


		// bundle all resources to one AssetBundle for each "bundleName" in "character" category.
		if (categoryName == "characters") {

			// load texture image (texture.jpg should be exist.) as main asset.
			var mainResourceTexture = Resources.Load(resNameAndResourceLoadablePathsDict["texture"]);
			if (mainResourceTexture) Debug.Log("Bundlize:loaded:" + resNameAndResourceLoadablePathsDict["texture"]);
			else Debug.LogError("Bundlize:failed to load:" + resNameAndResourceLoadablePathsDict["texture"]);

			var otherResourcePaths = resNameAndResourceLoadablePathsDict.Keys
				.Where(key => key != "texture")
				.Select(key => resNameAndResourceLoadablePathsDict[key])
				.ToList();


			// load other resources.
			var subResources = new List<UnityEngine.Object>();

			foreach (var path in otherResourcePaths) {
				var subResource = Resources.Load(path);
				
				if (subResource) {
					subResources.Add(subResource);
					Debug.Log("Bundlize:loaded:" + path);
				} else {
					Debug.LogError("Bundlize:failed to load:" + path);
					return;
				}
			}
			
			
			uint crc = 0;

			/**
				generate AssetBundle for iOS
			*/
			var iOSAssetBundleOutputTargetPath = recommendedOutputPathDict[BuildTarget.iPhone] + "/" + categoryName + "_" + bundleName + ".assetBundle";
			
			try {
				AssetRailsBuildPipeline.BuildAssetBundle(
					mainResourceTexture,
					subResources.ToArray(),
					iOSAssetBundleOutputTargetPath,
					out crc,
					BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets,
					BuildTarget.iPhone
				);
			} catch (Exception e) {
				Debug.Log("Bundlize:e:" + e);
			}

			if (File.Exists(iOSAssetBundleOutputTargetPath)) {
				Debug.Log("Bundlize:generated iOSAssetBundleOutputTargetPath:" + iOSAssetBundleOutputTargetPath);
			} else {
				Debug.LogError("Bundlize:asset bundle was not generated! iOSAssetBundleOutputTargetPath:" + iOSAssetBundleOutputTargetPath);
			}


			/*
				generate AssetBundle for WebPlayer.
			*/
			var webPlayerAssetBundleTargetPath = recommendedOutputPathDict[BuildTarget.WebPlayer] + "/" + categoryName + "_" + bundleName + ".assetBundle";
			Debug.Log("Bundlize:webPlayerAssetBundleTargetPath:" + webPlayerAssetBundleTargetPath);
			
			try {
				AssetRailsBuildPipeline.BuildAssetBundle(
					mainResourceTexture,
					subResources.ToArray(),
					webPlayerAssetBundleTargetPath,
					out crc,
					BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets,
					BuildTarget.WebPlayer
				);
			} catch (Exception e) {
				Debug.Log("Bundlize:e:" + e);
			}
			if (File.Exists(webPlayerAssetBundleTargetPath)) {
				Debug.Log("Bundlize:generated webPlayerAssetBundleTargetPath:" + webPlayerAssetBundleTargetPath);
			} else {
				Debug.LogError("Bundlize:asset bundle was not generated! webPlayerAssetBundleTargetPath:" + webPlayerAssetBundleTargetPath);
			}
		}

		// bundle "main" resource to one AssetBundle for each "bundleName" in "chara" category.
		if (categoryName == "images") {
			// "resNameAndResourceLoadablePathsDict" contains resName : resourceLoadablePath combinations. you can use it.
			var mainResourceLoadablePath = resNameAndResourceLoadablePathsDict["main"];
			var mainRes = Resources.Load(mainResourceLoadablePath);
			if (mainRes) Debug.Log("Bundlize:loaded:" + mainResourceLoadablePath);
			else Debug.LogError("Bundlize:failed to loaded:" + mainResourceLoadablePath);



			// you can use "recommendedOutputPathDict" for each platform.
			// platform name : platform output recommended path.
			var iOSAssetBundleOutputTargetPath = recommendedOutputPathDict[BuildTarget.iPhone] + "/" + categoryName + "_" + bundleName + ".assetBundle";
			Debug.Log("Bundlize:iOSAssetBundleOutputTargetPath:" + iOSAssetBundleOutputTargetPath);

			// when you use BuildAssetBundle with crc, you don't need backup crc parameter. AssetRails will do it automatically.
			// Therefore you need to execute BuildAssetBundle with crc parameter.
			uint crc = 0;

			/**
				generate AssetBundle for iOS
			*/
			
			// generate assetBundles with "AssetRailsBuildPipeline.BuildAssetBundle" method.
			try {
				AssetRailsBuildPipeline.BuildAssetBundle(
					mainRes,
					new UnityEngine.Object[0],
					iOSAssetBundleOutputTargetPath,
					out crc,
					BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets,
					BuildTarget.iPhone
				);
			} catch (Exception e) {
				Debug.Log("Bundlize:e:" + e);
			}

			if (File.Exists(iOSAssetBundleOutputTargetPath)) {
				Debug.Log("Bundlize:generated iOSAssetBundleOutputTargetPath:" + iOSAssetBundleOutputTargetPath);
			} else {
				Debug.LogError("Bundlize:asset bundle was not generated! iOSAssetBundleOutputTargetPath:" + iOSAssetBundleOutputTargetPath);
			}


			/*
				generate AssetBundle for WebPlayer.
			*/
			var webPlayerAssetBundleTargetPath = recommendedOutputPathDict[BuildTarget.WebPlayer] + "/" + categoryName + "_" + bundleName + ".assetBundle";
			Debug.Log("Bundlize:webPlayerAssetBundleTargetPath:" + webPlayerAssetBundleTargetPath);
			
			try {
				AssetRailsBuildPipeline.BuildAssetBundle(
					mainRes,
					new UnityEngine.Object[0],
					webPlayerAssetBundleTargetPath,
					out crc,
					BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets,
					BuildTarget.WebPlayer
				);
			} catch (Exception e) {
				Debug.Log("Bundlize:e:" + e);
			}
			if (File.Exists(webPlayerAssetBundleTargetPath)) {
				Debug.Log("Bundlize:generated webPlayerAssetBundleTargetPath:" + webPlayerAssetBundleTargetPath);
			} else {
				Debug.LogError("Bundlize:asset bundle was not generated! webPlayerAssetBundleTargetPath:" + webPlayerAssetBundleTargetPath);
			}
		}



		// bundle all resources to one AssetBundle for each "bundleName" in "models" category.
		if (categoryName == "models") {
			var resources = new List<UnityEngine.Object>();

			foreach (var path in resNameAndResourceLoadablePathsDict.Values) {
				resources.Add(Resources.Load(path));
			}

			var main = resources.First();
			var sub = resources.GetRange(1, resources.Count - 1).ToArray();

			uint crc = 0;

			/**
				generate AssetBundle for iOS
			*/
			var iOSAssetBundleOutputTargetPath = recommendedOutputPathDict[BuildTarget.iPhone] + "/" + categoryName + "_" + bundleName + ".assetBundle";
			
			try {
				AssetRailsBuildPipeline.BuildAssetBundle(
					main,
					sub,
					iOSAssetBundleOutputTargetPath,
					out crc,
					BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets,
					BuildTarget.iPhone
				);
			} catch (Exception e) {
				Debug.Log("Bundlize:e:" + e);
			}

			if (File.Exists(iOSAssetBundleOutputTargetPath)) {
				Debug.Log("Bundlize:generated iOSAssetBundleOutputTargetPath:" + iOSAssetBundleOutputTargetPath);
			} else {
				Debug.LogError("Bundlize:asset bundle was not generated! iOSAssetBundleOutputTargetPath:" + iOSAssetBundleOutputTargetPath);
			}


			/*
				generate AssetBundle for WebPlayer.
			*/
			var webPlayerAssetBundleTargetPath = recommendedOutputPathDict[BuildTarget.WebPlayer] + "/" + categoryName + "_" + bundleName + ".assetBundle";
			
			try {
				AssetRailsBuildPipeline.BuildAssetBundle(
					main,
					sub,
					webPlayerAssetBundleTargetPath,
					out crc,
					BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets,
					BuildTarget.WebPlayer
				);
			} catch (Exception e) {
				Debug.Log("Bundlize:e:" + e);
			}

			if (File.Exists(webPlayerAssetBundleTargetPath)) {
				Debug.Log("Bundlize:generated webPlayerAssetBundleTargetPath:" + webPlayerAssetBundleTargetPath);
			} else {
				Debug.LogError("Bundlize:asset bundle was not generated! webPlayerAssetBundleTargetPath:" + webPlayerAssetBundleTargetPath);
			}
		}
	}

}


// "AssetRails.BundlizerBase" class is base class for handling generate AssetBundle for each "category".
// 
// see online documents. 
//	https://github.com/sassembla/AssetRails-Support/blob/master/RunnersAPIDocument.md#bundlizer
public class SampleCategoryBundlizer : AssetRails.Category_BundlizerBase {

	public override void Bundlize (string categoryName, Dictionary<string, Dictionary<string, string>> bundleName_resNameAndResourceLoadablePathsDict, Dictionary<BuildTarget, string> recommendedOutputPathDict, Dictionary<string, string> memoDict) {
		Debug.Log("Bundlize:Bundlize in category:" + categoryName);
		
		// bundleName_resNameAndResourceLoadablePathsDict contains all bundleName & resource paths which exists in this category.
		foreach (var bundleName in bundleName_resNameAndResourceLoadablePathsDict.Keys) {
			Debug.Log("Bundlize:bundleName:" + bundleName);

			var resNameAndResourceLoadablePathsDict = bundleName_resNameAndResourceLoadablePathsDict[bundleName];
			foreach (var resName in resNameAndResourceLoadablePathsDict.Keys) {
				Debug.Log("Bundlize:resName:" + resName + " resourceLoadablePath:" + resNameAndResourceLoadablePathsDict[resName]);
			}
		}

		/*
			this example uses "AssetBundle.CreateFromMemoryImmediate", is able to use Unity 4.5.2 ~ 
		if (categoryName == "models") {
			// layer 1
			AssetRailsBuildPipeline.PushAssetDependencies();

			var savePath = FileController.Combine(recommendedOutputPathDict[BuildTarget.iPhone], "1.asset");

			var resNameAndResourceLoadablePathsDict = bundleName_resNameAndResourceLoadablePathsDict["gunkan"];
			
			var resources = new List<UnityEngine.Object>();
			
			foreach (var path in resNameAndResourceLoadablePathsDict.Values) {
				resources.Add(Resources.Load(path));
			}

			AssetRailsBuildPipeline.BuildAssetBundle(
				resources[0],
				resources.Where((obj, index) => 0 < index).ToArray(),
				savePath,
				BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets,
				BuildTarget.iPhone
			);

			// layer 2
			{
				AssetRailsBuildPipeline.PushAssetDependencies();
				
				var bytes = File.ReadAllBytes(savePath);
				var layer1Obj = AssetBundle.CreateFromMemoryImmediate(bytes);// 4.5.2 or later
				
				var resources2 = new List<UnityEngine.Object>();
				resources2.Add(layer1Obj);

				var resNameAndResourceLoadablePathsDict2 = bundleName_resNameAndResourceLoadablePathsDict["sushi"];

				foreach (var path in resNameAndResourceLoadablePathsDict2.Values) {
					resources2.Add(Resources.Load(path));
				}

				var savePath2 = FileController.Combine(recommendedOutputPathDict[BuildTarget.iPhone], "2.asset");

				AssetRailsBuildPipeline.BuildAssetBundleExplicitAssetNames(
					resources2.ToArray(),
					resources2.Select(asset => "obj_" + asset.name).ToArray(),
					savePath2,
					BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets,
					BuildTarget.iPhone
				);

				AssetRailsBuildPipeline.PopAssetDependencies();
			}

			AssetRailsBuildPipeline.PopAssetDependencies();
		}
		*/
	}
}



