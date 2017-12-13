﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    public Sprite SpriteDestroy;
    public Base BaseScript;

	// Use this for initialization
	void Start () {
        BaseScript = GetComponentInParent<Base>();
	}
	
	// Update is called once per frame
	void Update () {
        if (BaseScript.Paused)
            GetComponent<Animator>().enabled = false;
        else
            GetComponent<Animator>().enabled = true;

        if (GetComponent<SpriteRenderer>().sprite == SpriteDestroy)
            Destroy();
	}

    void Destroy()
    {
        Destroy(gameObject, 0.08f);
    }
}
