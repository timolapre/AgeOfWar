using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGetX : MonoBehaviour {
    // Use this for initialization
    string Mode;
	void Start () {
        float Camerax = PlayerPrefs.GetFloat("CameraX");
        transform.position = new Vector3(Camerax,0,-9);
        
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        string Mode = PlayerPrefs.GetString("PlayerMode");
        if (Mode == "Singleplayer")
        {
            transform.localScale = new Vector3(6, 7, 1);
            transform.localPosition = new Vector3(-1.23f, 2.5f, -9);
            Debug.Log("kaas");
        }
        else
        {            
            transform.localScale = new Vector3(7, 10, 1);
            transform.localPosition = new Vector3(3.6f, 6.5f, -9);
        }
    }
}
