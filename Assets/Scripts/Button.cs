﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    private Base BaseObject;
    public Turret TurretScript;
    public int id;
    public List<Sprite> Spritelist;

    private SpriteRenderer spriterenderer;
    private BoxCollider collider;

    // Use this for initialization
    void Start()
    {
        BaseObject = GameObject.Find("PBase /main object").GetComponent<Base>();
        TurretScript = GameObject.Find("Turret").GetComponent<Turret>();
        spriterenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(id == 4)
        {
            if (TurretScript.CanUpgradeTurret())
            {
                spriterenderer.sprite = Resources.Load("Buttons/turret_upgrade", typeof(Sprite)) as Sprite;                
            }
            else
            {
                spriterenderer.sprite = Resources.Load("Buttons/turret_upgrade_grey", typeof(Sprite)) as Sprite;
                Debug.Log("kaas");
            }
        }
        if(id == 5)
        {
            if (BaseObject.CanUpgradeTier())
            {
                spriterenderer.sprite = Resources.Load("Buttons/TierUpgrade", typeof(Sprite)) as Sprite;
            }
            else
            {
                spriterenderer.sprite = Resources.Load("Buttons/TierUpgrade_false", typeof(Sprite)) as Sprite;
                Debug.Log("kaas2");
            }
        }
        /*if (Input.GetMouseButtonDown(0))
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
                        Debug.Log(transform.name);
                    }
                    catch { }
                }
            }
        }*/

        if (id <= 3)
        {
            try
            {
                spriterenderer.sprite = Spritelist[BaseObject.WhatTier - 1];
                if (BaseObject.WhatTier > 1)
                    transform.localScale = new Vector3(0.007529334f, 0.06927653f, 1);
                    collider.size = spriterenderer.sprite.bounds.size;
            }
            catch { };
        }
    }

    void OnMouseDown()
    {
        Touched(id);
    }

    void Touched(int id)
    {
        if (id <= 3)
        {
            BaseObject.SpawnPlayer(id+3*(BaseObject.WhatTier-1));
        }
        else if (id == 4)
        {
            TurretScript.UpgradeTurret();
        }
        else if (id == 5)
        {
            BaseObject.UpgradeTier();
        }
        else if (id == 5)
        {
            BaseObject.UpgradeTier();
        }
    }
}
