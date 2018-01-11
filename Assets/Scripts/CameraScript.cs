using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraScript : MonoBehaviour {
    public Base BaseScript;

    float Deadzone = .5f;

    // Use this for initialization
    void Start ()
    {
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (BaseScript.Playing)
        {
            if (InputHelper.GetStick(0).x > Deadzone && gameObject.transform.position.x < 10)
            {
                    transform.Translate(0.1f, 0, 0);
            }
            if (InputHelper.GetStick(0).x < -Deadzone && gameObject.transform.position.x > -1.23f)
            {
                transform.Translate(-0.1f, 0, 0);
            }   
        }
        else if(BaseScript.Paused == false)
        {
            transform.position = new Vector3(0, 0.9f, -10);
        }
 
        PlayerPrefs.SetFloat("CameraX", transform.position.x);
    }
}
