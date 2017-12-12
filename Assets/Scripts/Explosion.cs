using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("Destroy", 1.47f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Destroy()
    {
        Destroy(gameObject);
    }
}
