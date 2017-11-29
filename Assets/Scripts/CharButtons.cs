using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharButtons : MonoBehaviour {

    public Sprite sprite2;

    private Base basescript;
    private SpriteRenderer spriterenderer;
    private Animator animator;

    // Use this for initialization
    void Start () {
        basescript = GetComponentInParent<Base>();
        spriterenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
        if (basescript.WhatCentury == 2)
        {
            spriterenderer.sprite = sprite2;
            transform.localScale = new Vector3(0.4f,0.4f,1);
            animator.enabled = false;
        }
    }
}
