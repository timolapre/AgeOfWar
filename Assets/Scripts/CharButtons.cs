using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharButtons : MonoBehaviour {

    public int id;

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
        if (basescript.WhatTier == 2)
        {
            spriterenderer.sprite = sprite2;
            transform.localScale = new Vector3(0.4f,0.4f,1);
            animator.enabled = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit))
            {
                if (raycastHit.transform.tag == "Button" && basescript.Money > 0)
                {
                    basescript.SpawnPlayer(basescript.WhatTier * 3 - (3-id));
                    Debug.Log(id);
                }
            }
        }
    }

    void OnMouseEnter()
    {
        Debug.Log(id);
        if (Input.GetMouseButtonDown(0))
        {
            basescript.SpawnPlayer(basescript.WhatTier * 3 - (3 - id));
            Debug.Log(id);
        }
    }
}
