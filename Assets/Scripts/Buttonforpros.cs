using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Buttonforpros : MonoBehaviour {
	// Use this for initialization
	void Start () {

	}
    public string ID;
    public Camera cam;

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit))
            {
                try
                {
                    clicked(raycastHit.transform.GetComponent<Buttonforpros>().ID);
                }
                catch
                {
                }

            }
        }
    }

    public void clicked(string ID)
    {
        if (ID == "Play")
        {
            SceneManager.LoadScene("AgeOfWar", LoadSceneMode.Single);
        }
        if (ID == "Options")
        {
            cam.transform.position -= new Vector3 (20 ,0 ,0);
        }
        if (ID == "Credits")
        {
            //kaas
        }
        if (ID == "exit")
        {

        }
        if (ID == "Back")
        {
            cam.transform.position -= new Vector3(-20, 0, 0);
        }
    }
}
