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
                if (raycastHit.transform.name == "Button" && BaseObject.Money > 0)
                {
                    GameObject tempPlayer = Instantiate(BaseObject.Player, BaseObject.SpawnPlayerLocation.position, BaseObject.SpawnPlayerLocation.rotation, BaseObject.transform) as GameObject;
                    BaseObject.PlayerList.Add(tempPlayer);
                    BaseObject.Money--;
                }
            }
        }
    }
}
