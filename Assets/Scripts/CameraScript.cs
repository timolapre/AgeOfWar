using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.RightArrow) && gameObject.transform.position.x < 8)
            transform.Translate(0.1f,0,0);
        if (Input.GetKey(KeyCode.LeftArrow) && gameObject.transform.position.x > 0)
            transform.Translate(-0.1f, 0, 0);
    }
}
