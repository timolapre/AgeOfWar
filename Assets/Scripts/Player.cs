using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour {

    public int WhichUnit;

    SpriteRenderer spriterenderer;
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;
    public Sprite sprite4;
    public Sprite sprite5;
    public Sprite sprite6;

    private Base BaseObject;
    private float Closest = 1000;

    public int health;
    public int damage;
    public int range;

    // Use this for initialization
    void Start () {
        BaseObject = GetComponentInParent<Base>();
        spriterenderer = GetComponent<SpriteRenderer>();

        GetStartValues(WhichUnit);

        PolygonCollider2D comp = gameObject.AddComponent<PolygonCollider2D>();
        comp.isTrigger = true;
    }

	// Update is called once per frame
	void Update () {
        if (transform.position.x < BaseObject.FirstEnemy - 1 && !BaseObject.GameOver)
        {
            if (transform.position.x < Closest - 1)
                transform.Translate(0.05f, 0, 0);
        }

        if (transform.position.x >= 5 && BaseObject.EnemyBaseHealth > 0)
            BaseObject.EnemyBaseHealth -= damage;

        if (health <= 0)
        {
            BaseObject.FirstPlayer = -8 ;
            BaseObject.PlayerList.Remove(gameObject);
            Destroy(gameObject);
        }

        if (transform.position.x > BaseObject.FirstPlayer && health > 0)
        {
            BaseObject.FirstPlayer = transform.position.x;
        }

        Closest = 1000;
        foreach (GameObject OtherGameObject in BaseObject.PlayerList)
            if (OtherGameObject.transform.position.x < Closest && OtherGameObject.transform.position.x > transform.position.x)
                Closest = OtherGameObject.transform.position.x;
    }

    void TakeDamage(int damage)
    {
        health -= damage;
    }

    void GetStartValues(int id)
    {
        if(id == 1)
        {
            transform.localScale = new Vector3(6, 6, 1);
            health = 100;
            damage = 1;
            range = 1;
            spriterenderer.sprite = sprite1;
        }

        if(id == 2)
        {
            transform.localScale = new Vector3(6, 6, 1);
            health = 200;
            damage = 2;
            range = 1;
            spriterenderer.sprite = sprite2;
        }

        if (id == 3)
        {
            transform.localScale = new Vector3(1, 1, 1);
            health = 300;
            damage = 2;
            range = 1;
            spriterenderer.sprite = sprite3;
        }

        if (id == 4)
        {
            transform.localScale = new Vector3(2, 2, 1);
            health = 250;
            damage = 2;
            range = 1;
            spriterenderer.sprite = sprite4;
        }

        if (id == 5)
        {
            transform.localScale = new Vector3(2, 2, 1);
            health = 400;
            damage = 4;
            range = 1;
            spriterenderer.sprite = sprite5;
        }

        if (id == 6)
        {
            transform.localScale = new Vector3(2, 2, 1);
            health = 500;
            damage = 5;
            range = 1;
            spriterenderer.sprite = sprite6;
        }
    }
}
