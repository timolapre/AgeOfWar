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
	void Update () {
<<<<<<< HEAD
        if (BaseScript.Playing)
        {
            if (Input.GetKey(KeyCode.RightArrow) && gameObject.transform.position.x < 8)
            {
                transform.Translate(0.1f, 0, 0);
            }
            if (Input.GetKey(KeyCode.LeftArrow) && gameObject.transform.position.x > 0)
            {
                transform.Translate(-0.1f, 0, 0);
            }
        }
        else
        {
            transform.position = new Vector3(0, 0, -10);
        }
        
=======
        if (Input.GetKey(KeyCode.RightArrow) && gameObject.transform.position.x < 15)
            transform.Translate(0.1f,0,0);
        if (Input.GetKey(KeyCode.LeftArrow) && gameObject.transform.position.x > 0)
            transform.Translate(-0.1f, 0, 0);
>>>>>>> timo
    }
}
