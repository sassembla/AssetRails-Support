using UnityEngine;

using System;
using System.IO;
using System.Collections;

public class CubeRotator : MonoBehaviour {
	void Update () {
		transform.Rotate(new Vector3(0.1f, 0.1f, 0.1f));
	}
}