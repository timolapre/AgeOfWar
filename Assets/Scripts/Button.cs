using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{

    private Base BaseObject;
    public Turret TurretScript;
    public int id;

    // Use this for initialization
    void Start()
    {
        BaseObject = GetComponentInParent<Base>();
        TurretScript = GetComponentInParent<Turret>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit))
            {
                if (raycastHit.transform.CompareTag("Button"))
                {
                    try
                    {
                        Touched(raycastHit.transform.GetComponent<Button>().id);
                    }
                    catch { }
                }
            }
        }
    }

    void Touched(int id)
    {
        if (id <= 3)
        {
            BaseObject.SpawnPlayer(id);
        }
        if (id == 4)
        {
            TurretScript.UpgradeTurret();
        }
    }
}