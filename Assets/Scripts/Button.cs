using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

    private Base BaseObject;

	// Use this for initialization
	void Start () {
        BaseObject = GetComponentInParent<Base>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit))
            {
                if (raycastHit.transform.name == "Button" && BaseObject.money > 0)
                {
                    GameObject tempPlayer = Instantiate(BaseObject.player, BaseObject.spawnPlayer.position, BaseObject.spawnPlayer.rotation, BaseObject.transform) as GameObject;
                    BaseObject.playerlist.Add(tempPlayer);
                    BaseObject.money--;
                }
            }
        }
    }
}
