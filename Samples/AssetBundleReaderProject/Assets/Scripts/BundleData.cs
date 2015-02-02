using UnityEngine;

using System;
using System.Collections.Generic;

class BundleData {
	public readonly string bundleName;
	public readonly int size;
	public readonly int version;
	public readonly uint crc;
	public readonly List<string> resourceNames;

	public BundleData (Dictionary<string, object> source) {
		
		bundleName = (string)source["bundleName"];
		var sizeResult = int.TryParse(source["size"].ToString(), out size);
		var versionResult = int.TryParse(source["version"].ToString(), out version);
		var crcResult = uint.TryParse(source["crc"].ToString(), out crc);
		
		var resNameBaseList = source["resourceNames"] as List<System.Object>;
		var resNameBaseStrList = new List<string>();
		
		foreach (var objBasedString in resNameBaseList) {
			var stringValue = objBasedString.ToString();
			resNameBaseStrList.Add(stringValue);
		}
		resourceNames = new List<string>(resNameBaseStrList);

		if (sizeResult && versionResult && crcResult) {}
		else Debug.Log("sizeResult:" + sizeResult + " versionResult:" + versionResult + " crcResult:" + crcResult);
	}
}