using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour {
    private Base BaseScript;
    public int Id;
    SpriteRenderer SpriteRenderer;
    // Use this for initialization
    void Start ()
    {
        BaseScript = GetComponentInParent<Base>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Id == 1)
        {
            SpriteRenderer.sprite = Resources.Load(BaseScript.WhatFaction + "/Bases/Platform", typeof(Sprite)) as Sprite;
        }
        else if (Id == 2)
        {
            SpriteRenderer.sprite = Resources.Load(BaseScript.WhatFactionEnemy + "/Bases/Platform", typeof(Sprite)) as Sprite;
        }        
    }
}
