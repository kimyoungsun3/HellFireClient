using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTTT : MonoBehaviour {
	private void OnBecameInvisible()
	{
		Debug.Log(this + "안보임");
		GetComponent<MeshRenderer>().enabled = false;
	}

	private void OnBecameVisible()
	{
		Debug.Log(this + "보임");
		GetComponent<MeshRenderer>().enabled = true;
	}
}
