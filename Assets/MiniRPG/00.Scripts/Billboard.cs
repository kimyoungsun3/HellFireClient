using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {

    Transform Camtrans;
	void Start () {
        Camtrans = Camera.main.transform;
	}
	
	void Update () {
        transform.rotation = Camtrans.rotation;
    }
}
