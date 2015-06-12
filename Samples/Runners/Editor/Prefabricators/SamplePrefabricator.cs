using UnityEngine;
using UnityEditor;

using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// "AssetRails.PrefabricatorBase" class is base class for making prefabs of resources for each "bundleName".
// 
// see online documents. 
//	https://github.com/sassembla/AssetRails-Support/blob/master/RunnersAPIDocument.md#prefabricator
public class SamplePrefabricator : AssetRails.PrefabricatorBase {

	public override void Prefabricate (string categoryName, string bundleName, Dictionary<string, string> resNameAndResourceLoadablePathsDict, string recommendedOutputPath) {
		Debug.Log("Prefabricate:categoryName:" + categoryName);
		Debug.Log("Prefabricate:bundleName:" + bundleName);


		// generate prefab if category is characters.
		if (categoryName == "characters") {

			/*
				we will generate the Cube Object which has character's texture.
				then output it to "recommendedOutputPath"/"bundleName".prefab.
			*/


			// "resNameAndResourceLoadablePathsDict" contains resName : resourceLoadablePath combinations. you can use it.
			var mainImageResourcePath = resNameAndResourceLoadablePathsDict["texture"];
			
			var characterTexture = Resources.Load(mainImageResourcePath) as Texture2D;
			
			if (characterTexture) Debug.Log("Prefabricate:loaded:" + mainImageResourcePath);
			else Debug.LogError("Prefabricate:failed to load:" + mainImageResourcePath);

			
			// generate texture material
			var characterMaterial = new Material(Shader.Find ("Transparent/Diffuse"));
			AssetDatabase.CreateAsset(characterMaterial, recommendedOutputPath + "/" + bundleName + "_material.mat");

			// then set loaded texture to material
			characterMaterial.mainTexture = characterTexture;


			// generate cube then set texture to it.
			var cubeObj = GameObject.CreatePrimitive(PrimitiveType.Cube);

			var meshRenderer = cubeObj.GetComponent<MeshRenderer>();
			meshRenderer.material = characterMaterial;


			// generate prefab in bundleName folder.
			var prefabOutputPath = recommendedOutputPath + "/" + bundleName + "_prefab.prefab";
			UnityEngine.Object prefabFile = PrefabUtility.CreateEmptyPrefab(prefabOutputPath);
			

			// export prefab data.
			PrefabUtility.ReplacePrefab(cubeObj, prefabFile);
		}

	}
}
