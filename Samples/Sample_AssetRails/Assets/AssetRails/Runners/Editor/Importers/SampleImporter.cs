using UnityEditor;
using UnityEngine;


// you can use AssetRails based importing mechanism instead of Unity's UnityEditor.AssetPostprocessor series.
// 	NEED "override".
// 
// see online documents. 
//	https://github.com/sassembla/AssetRails-Support/blob/master/RunnersAPIDocument.md#importer

public class SampleAssetRailsBasedImporter : AssetRails.ImporterBase {

	public override void AssetRailsOnPreprocessTexture () {
		// Debug.Log("Import:categoryName:" + categoryName);
		// Debug.Log("Import:bundleName:" + bundleName);


		// run import settings for specific category.
		if (categoryName == "characters") {
			UnityEditor.TextureImporter importer = assetImporter as UnityEditor.TextureImporter;
			importer.textureType 	= UnityEditor.TextureImporterType.Image;
			importer.textureFormat 	= TextureImporterFormat.ARGB16;
		}


		if (categoryName == "images") {
			UnityEditor.TextureImporter importer = assetImporter as UnityEditor.TextureImporter;
			importer.textureType			= UnityEditor.TextureImporterType.Advanced;
			importer.npotScale				= TextureImporterNPOTScale.None;
			importer.isReadable				= true;
			importer.alphaIsTransparency 	= true;
			importer.mipmapEnabled			= false;
			importer.wrapMode				= TextureWrapMode.Repeat;
			importer.filterMode				= FilterMode.Bilinear;
			importer.textureFormat 			= TextureImporterFormat.ARGB16;
			return;
		}

	}


	// other Import methods are below.


	// public override void AssetRailsOnPostprocessGameObjectWithUserProperties(GameObject g, string[] propNames, object[] values){
	// 	Debug.Log("Import:AssetRails based AssetRailsOnPostprocessGameObjectWithUserProperties  bundleName:" + bundleName + " gameobject:" + g);
	// }
	
	
	// public override void AssetRailsOnPostprocessTexture(Texture2D texture){
	// 	Debug.Log("Import:AssetRails based AssetRailsOnPostprocessTexture bundleName:" + bundleName + " texture:" + texture);
	// }

	// public override void AssetRailsOnPreprocessAudio(){
	// 	Debug.Log("Import:AssetRails based AssetRailsOnPreprocessAudio bundleName:" + bundleName);
	// }
	// public override void AssetRailsOnPostprocessAudio(AudioClip clip){
	// 	Debug.Log("Import:AssetRails based AssetRailsOnPostprocessAudio bundleName:" + bundleName + " clip:" + clip);
	// }


	// public override void AssetRailsOnPreprocessModel(){
	// 	Debug.Log("Import:AssetRails based AssetRailsOnPreprocessModel bundleName:" + bundleName);
	// }
	// public override void AssetRailsOnAssignMaterialModel(Material material, Renderer renderer){
	// 	Debug.Log("Import:AssetRails based AssetRailsOnAssignMaterialModel bundleName:" + bundleName + " material:" + material + " renderer:" + renderer);
	// }
	// public override void AssetRailsOnPostprocessModel(GameObject g){
	// 	Debug.Log("Import:AssetRails based AssetRailsOnPostprocessModel bundleName:" + bundleName + " gameobject:" + g);
	// }


}

