using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGetX : MonoBehaviour {

	// Use this for initialization
	void Start () {
        float Camerax = PlayerPrefs.GetFloat("CameraX");
        transform.position = new Vector3(Camerax,0,-9);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
