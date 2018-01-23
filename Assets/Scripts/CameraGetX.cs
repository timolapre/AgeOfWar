using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGetX : MonoBehaviour {
    // Use this for initialization
    float Camerax;
    string Mode;
    private Base BaseScript;
	void Start () {
        Camerax = PlayerPrefs.GetFloat("CameraX");
        transform.position = new Vector3(Camerax,0,-9);
        BaseScript = GameObject.Find("PBase /main object").GetComponent<Base>();        
    }
	
	// Update is called once per frame
	void Update ()
    {
        string Mode = PlayerPrefs.GetString("PlayerMode");
        if (Mode == "Singleplayer" && BaseScript.Paused)
        {
            transform.localScale = new Vector3(0.6f, 0.6f, 1);
            transform.localPosition = new Vector3(Camerax, 1, -9);
            Debug.Log("kaas");
        }
        else if (Mode == "Multiplayer" && BaseScript.Paused)
        {            
            transform.localScale = new Vector3(0.9f, 0.9f, 1);
            transform.localPosition = new Vector3(Camerax, 4, -9);
        }
        else
        {
            if(Mode == "Singleplayer" && !BaseScript.Paused)
            {
                transform.localScale = new Vector3(2.6f, 2.6f, 1);
            }
            if (Mode == "Multiplayer" && !BaseScript.Paused)
            {
                transform.localScale = new Vector3(3.9f, 3.9f, 1);
            }
            transform.localPosition = new Vector3(Camerax, 2, -9);
        }
    }
}
