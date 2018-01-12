using System.Collections;
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
        if(id <= 3)
        {
            spriterenderer.sprite = Resources.Load(BaseObject.WhatFaction + "/Units/" + BaseObject.WhatTier.ToString() + "_" + (((id - 1) % 3) + 1).ToString(), typeof(Sprite)) as Sprite;
            if (BaseObject.VsAI)
            {
                transform.localPosition = new Vector3(-12 + 2.5f * id, -5.25f, 8);
            }
            else
            {
                transform.localPosition = new Vector3((-17 + 2.5f * id), -8, 8);
            }
        }
        if(id >= 6 && id <= 8)
        {
            if (BaseObject.VsAI)
            {
                transform.localPosition = new Vector3(-100, -100, 8);
            }
            else
            {
                transform.localPosition = new Vector3(16.5f - 2.5f * (id - 5), -8, 8);
                spriterenderer.sprite = Resources.Load(BaseObject.WhatFaction + "/Units/" + BaseObject.WhatTier.ToString() + "_" + (((id - 1) % 3) + 1).ToString(), typeof(Sprite)) as Sprite;
            }
        }
        if(id == 4)
        {
            if (TurretScript.CanUpgradeTurret())
            {
                spriterenderer.sprite = Resources.Load("Buttons/turret_upgrade", typeof(Sprite)) as Sprite;
            }
            else if(!TurretScript.CanUpgradeTurret())
            {
                spriterenderer.sprite = Resources.Load("Buttons/turret_upgrade_grey", typeof(Sprite)) as Sprite;
            }
            if (!BaseObject.VsAI)
            {
                transform.position = new Vector3(-100, -100, 0);
            }
            else
            {
                transform.localPosition = new Vector3(-2.3525f, -4.72f, 9);
            }
        }
        if(id == 5)
        {
            if (BaseObject.CanUpgradeTier())
            {
                spriterenderer.sprite = Resources.Load("Buttons/TierUpgrade", typeof(Sprite)) as Sprite;
            }
            else if(!BaseObject.CanUpgradeTier())
            {
                spriterenderer.sprite = Resources.Load("Buttons/TierUpgrade_false", typeof(Sprite)) as Sprite;
            }
            if (!BaseObject.VsAI)
            {
                transform.position = new Vector3(-100, -100, 0);
            }
            else
            {
                transform.localPosition = new Vector3(-0.6500001f, -4.72f, 9);
            }
        }
    }

    void OnMouseDown()
    {
        Touched(id);
    }

    void Touched(int id)
    {
        if (BaseObject.VsAI)
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
        }
    }
}
