#AssetRails Runners API Document

there are some runner APIs for import, prefabricate and bundlize routes.  
Some sample runners are located at PROJECT_FOLDER/Assets/AssetRails/Runners/Editor/*

You can replace it by your original runner script by extending "AssetRails.*Base" class.

##importer

Uses in "import" route.

###Should extends
AssetRails.ImporterBase

(Also you can use import API of Unity's default, but in AssetRails please use this class.)

###Variables
parameter | detail
---|---
assetImporter | Reference to the asset importer.
assetPath | The path name of the asset being imported.
categoryName | The category name which the importing item belongs to.
bundleName | The bundle name which the importing item belongs to.


###Functions
method definition | in Unity default
---|---
**void AssetRailsOnPostprocessGameObjectWithUserProperties (GameObject g, string[] propNames, object[] values) {}** | OnPostprocessGameObjectWithUserProperties
**void AssetRailsOnPreprocessTexture () {}** | OnPreprocessTexture
**void AssetRailsOnPostprocessTexture (Texture2D texture) {}** | OnPostprocessTexture
**void AssetRailsOnPreprocessAudio () {}** | OnPreprocessAudio
**void AssetRailsOnPostprocessAudio (AudioClip clip) {}** | OnPostprocessAudio
**void AssetRailsOnPreprocessModel () {}** | OnPreprocessModel
**void AssetRailsOnPostprocessModel (GameObject g) {}** | OnPostprocessModel
**void AssetRailsOnAssignMaterialModel (Material material, Renderer renderer) {}** | OnAssignMaterialModel

Almost same with Unity's default "AssetPostprocessor" class.

But there are 2 different point against Unity's original import mechanism.

1. should **override** for use.
2. can't use "OnPostprocessAllAssets" method, because of AssetRails mechanism does not supprt it. Please use Unity's default.

###usage
If the source files are like below,

	PROJECT_FOLDER/Resources(AssetRails_Importable)/characters/chara01/chara.png


The "AssetRailsOnPreprocessTexture" method will be called.

```
public class SampleAssetRailsBasedImporter : AssetRails.ImporterBase {

	public override void AssetRailsOnPreprocessTexture () {}

}
```

at that time,
**this.categoryName** will be "**characters**", and  
**this.bundleName** will be "**chara01**" in "AssetRailsOnPreprocessTexture" method.

also **assetPath** will be "**Assets/SOMEWHERE/characters/chara01/chara.png**".

You can use these parameters for running import process to the resource.



##prefabricator

Uses in "prefabricate" route.
You can generate prefabs and others.

###Should extends
AssetRails.PrefabricatorBase

###Variables
nothing.

###Functions
**void Prefabricate (string categoryName, string bundleName, Dictionary<string, string> resNameAndResourceLoadablePathsDict, string recommendedOutputPath)**

parameter | detail
---|---
string categoryName | The category name which the importing item belongs to.
string bundleName | The path name of the asset being imported.
Dictionary<string, string> resNameAndResourceLoadablePathsDict | resource file name without extenson : path which can be loaded like "UnityEngine.Resources.Load(path)".
string recommendedOutputPath | category & bundleName included path parameter for write out something like prefabs.



###usage
If the source files are like below, there are 2 same category, bundleName items,

	SOMEWHERE/characters/chara01/chara.png
	SOMEWHERE/characters/chara01/chara_basement.png

The "Prefabricate" method will be called only once by bundleName **"chara01"**.

```
public class SamplePrefabricator : AssetRails.PrefabricatorBase {

	public override void Prefabricate (string categoryName, string bundleName, Dictionary<string, string> resNameAndResourceLoadablePathsDict, string recommendedOutputPath) {}

}
```

at that time,
**categoryName** will be "**characters**", and  
**bundleName** will be "**chara01**".

**resNameAndResourceLoadablePathsDict** will contains these key-value parameters.

resource name| resource loadable path
---|---
chara | characters/chara01/chara
chara_basement | characters/chara01/chara_basement

Our reccomendation to use "resNameAndResourceLoadablePathsDict" is getting resource loadable path easily for load resource via UnityEngine.Resources.Load(resource loadable path).


The **recommendedOutputPath** parameter will become **SOMEWHERE/characters/chara01**.  
Once you made the resources something like prefab, please output it under **recommendedOutputPath**.

```
// generate prefab in bundleName folder.
var prefabOutputPath = Path.Combine(recommendedOutputPath, YOUR_PREFAB_NAME + ".prefab");
UnityEngine.Object prefabFile = PrefabUtility.CreateEmptyPrefab(prefabOutputPath);

// export prefab data.
PrefabUtility.ReplacePrefab(YOUR_NEW_PREFAB_OBJECT, prefabFile);
```
The YOUR_PREFAB_NAME.prefab will be generated and results will like below.

	SOMEWHERE_PREFABRICATED/characters/chara01/chara.png
	SOMEWHERE_PREFABRICATED/characters/chara01/chara_basement.png
	SOMEWHERE_PREFABRICATED/characters/chara01/YOUR_PREFAB_NAME.prefab




##bundlizer

Uses in "bundlize" route.
Take imported or imported + prefabricated resources for each bundleName units.

Please use AssetRailsBuildPipeline class methods for making AssetBundles. It's thin wrapper for Unity's default.

Supports making AssetBundles for multi platforms faster & easier than using Unity's default API.
* Record crc & parts data of AssetBundle in cache.
* Switching platforms in making AssetBundles very fast.


By default, "bundlizer" treats resources for each bundleName folder.
If you want to use Push/Pop between bundleName, use -c --category option.
Under this option, "bundlize" route uses Category_BundlizerBase extends runners instead of BundlizerBase based one.


###Should extends
AssetRails.BundlizerBase  

###Variables
nothing.

###Functions
**public override void Bundlize (string categoryName, string bundleName, Dictionary<string, string> resNameAndResourceLoadablePathsDict, Dictionary<BuildTarget, string> recommendedOutputPathDict, Dictionary<string, string> memoDict)**


parameter | detail
---|---
string categoryName | The category name which the importing item belongs to.
string bundleName | The path name of the asset being imported.
Dictionary<string, string> resNameAndResourceLoadablePathsDict | resource file name without extension : path which can be loaded like "UnityEngine.Resources.Load(path)".
string recommendedOutputPathDict | platform included path parameter for write out AssetBundles for each platforms.
Dictionary<string, string> memoDict | memo dictionary for you. can output with -o --output-memo option.

###AssetBundle Generator Functions

**AssetRailsBuildPipeline** class

There are series of static functions in AssetRailsBuildPipeline class for generating AssetBundles in bundlizer.




method definition | in Unity default
---|---
**public static bool BuildAssetBundle (UnityEngine.Object mainAsset, UnityEngine.Object[] assets, string pathName, BuildAssetBundleOptions assetBundleOptions = BuildAssetBundleOptions.CollectDependencies and BuildAssetBundleOptions.CompleteAssets, BuildTarget targetPlatform = BuildTarget.WebPlayer)** | BuildPipeline.BuildAssetBundle
**public static bool BuildAssetBundle (UnityEngine.Object mainAsset, UnityEngine.Object[] assets, string pathName, out uint crc, BuildAssetBundleOptions assetBundleOptions = BuildAssetBundleOptions.CollectDependencies and BuildAssetBundleOptions.CompleteAssets, BuildTarget targetPlatform = BuildTarget.WebPlayer)** | BuildPipeline.BuildAssetBundle with crc
**public static bool BuildAssetBundleExplicitAssetNames (UnityEngine.Object[] assets, string[] assetNames, string pathName, BuildAssetBundleOptions assetBundleOptions = BuildAssetBundleOptions.CollectDependencies and BuildAssetBundleOptions.CompleteAssets, BuildTarget targetPlatform = BuildTarget.WebPlayer)** | BuildPipeline.BuildAssetBundleExplicitAssetNames
**public static bool BuildAssetBundleExplicitAssetNames (UnityEngine.Object[] assets, string[] assetNames, string pathName, out uint crc, BuildAssetBundleOptions assetBundleOptions = BuildAssetBundleOptions.CollectDependencies and BuildAssetBundleOptions.CompleteAssets, BuildTarget targetPlatform = BuildTarget.WebPlayer)** | BuildPipeline.BuildAssetBundleExplicitAssetNames with crc
**public static string BuildPlayer (string[] levels, string locationPathName, BuildTarget target, BuildOptions options)** | BuildPipeline.BuildPlayer
**public static string BuildStreamedSceneAssetBundle (string[] levels, string locationPath, BuildTarget target, BuildOptions options = 0)** | BuildPipeline.BuildStreamedSceneAssetBundle
**public static string BuildStreamedSceneAssetBundle (string[] levels, string locationPath, BuildTarget target, out uint crc, BuildOptions options = 0)** | BuildPipeline.BuildStreamedSceneAssetBundle
**public static void PopAssetDependencies ()** | BuildPipeline.PopAssetDependencies
**public static void PushAssetDependencies ()** | BuildPipeline.PushAssetDependencies




###usage
If the source files are like below, there are 2 same "category", "bundleName" items,

	SOMEWHERE/characters/chara01/chara.png
	SOMEWHERE/characters/chara01/chara_cube.prefab

The "Bundlize" method will be called only once by bundleName **"chara01"**.

```
public class SampleBundlizer : AssetRails.BundlizerBase {

	public override void Bundlize (string categoryName, string bundleName, Dictionary<string, string> resNameAndResourceLoadablePathsDict, Dictionary<BuildTarget, string> recommendedOutputPathDict, Dictionary<string, string> memoDict) {}

}
```

at that time,
**categoryName** will be "**characters**", and  
**bundleName** will be "**chara01**".

**resNameAndResourceLoadablePathsDict** will contains these key-value parameters.

resource name| resource loadable path
---|---
chara | characters/chara01/chara
chara_cube | characters/chara01/chara_cube

The parameter "resNameAndResourceLoadablePathsDict" is getting resource loadable path easily for load resource via UnityEngine.Resources.Load(resourceLoadablePath).


The **recommendedOutputPathDict** parameter will contains these key-value parameters.

platform name | recommended AssetBundle output path by platform
---|---
WebPlayer | SOMEWHERE/WebPlayer
iPhone | SOMEWHERE/iPhone
Android | SOMEWHERE/Android
and more platforms... | SOMEWHERE/platforms...


Note that if you want to use crc, plaease use   "AssetRailsBuildPipeline.BuildAssetBundle with crc"  
or  
"AssetRailsBuildPipeline.BuildAssetBundleExplicitAssetNames with crc"  
method.

but you don't need to record crc parameter.
There methods records the crc parameter automatically.

You can use "memoDict" param as some memo dictionary for your way.
will be output "Bundlize" + -o or --output-memo-path option.

sample bundlizer code are below:


##category_bundlizer

category単位でbundlizeを行う。category内のbundleNameでPush/Popを使って組み合わせることが出来ると思う。


###Should extends
AssetRails.Category_BundlizerBase
