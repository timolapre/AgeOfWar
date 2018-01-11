using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraScript : MonoBehaviour {
    public Base BaseScript;
    // Use this for initialization
    void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (BaseScript.Playing && BaseScript.VsAI)
        {
            if (Input.GetKey(KeyCode.RightArrow) && gameObject.transform.position.x < 8.75f)
            {
                    transform.Translate(0.1f, 0, 0);
                }
            if (Input.GetKey(KeyCode.LeftArrow) && gameObject.transform.position.x > -1.23f)
            {
                transform.Translate(-0.1f, 0, 0);
            }   
        }

        if (!BaseScript.VsAI && transform.position != new Vector3(3.6f, 4, -10))
        {
            Camera.current.orthographicSize = 8.8f;
            transform.position = new Vector3(3.6f, 4, -10);
        }
        else if(BaseScript.VsAI && transform.position == new Vector3(3.6f, 4, -10))
        {
            Camera.current.orthographicSize = 6;
            transform.position = new Vector3(-1.23f, 0.9f, -10);
        }

        PlayerPrefs.SetFloat("CameraX", transform.position.x);
    }
}
