using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour {
    private Base BaseScript;
    private SpriteRenderer SpriteRenderer;
	// Use this for initialization
	void Start () {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        BaseScript = GameObject.Find("PBase /main object").GetComponent<Base>();
        if (BaseScript.VsAI)
        {
            SpriteRenderer.sprite = Resources.Load("Backgrounds/" + BaseScript.WhatFaction, typeof(Sprite)) as Sprite;
        }
        else
        {
            SpriteRenderer.sprite = Resources.Load("Backgrounds/Germany", typeof(Sprite)) as Sprite;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
