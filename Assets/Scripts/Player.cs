using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour {

    public int WhichUnit;

    SpriteRenderer spriterenderer;
    public Sprite Sprite1;
    public Sprite Sprite2;
    public Sprite Sprite3;
    public Sprite Sprite4;
    public Sprite Sprite5;
    public Sprite Sprite6;

    private Base BaseScript;
    private float Closest = 1000;

    public int health;
    public int damage;
    public int range;

    // Use this for initialization
    void Start () {
        BaseScript = GetComponentInParent<Base>();
        spriterenderer = GetComponent<SpriteRenderer>();

        GetStartValues(WhichUnit);

        PolygonCollider2D comp = gameObject.AddComponent<PolygonCollider2D>();
        comp.isTrigger = true;
    }

    bool AtOtherBase = false;
    bool Colliding = false;
    GameObject Attackee;
    // Update is called once per frame
    void Update () {
        if (transform.position.x >= BaseObject.SpawnEnemyLocation.position.x)
            AtOtherBase = true;

        if (!Colliding && !AtOtherBase)
            transform.Translate(.05f, 0, 0);
        else if (AtOtherBase && !Colliding)
            BaseObject.EnemyBaseHealth -= damage;
        else
            Attackee.GetComponent<Enemy>().TakeDamage(damage);

<<<<<<< HEAD
        if (health <= 0)
            Destroy(gameObject);

        /*if (transform.position.x < BaseObject.FirstEnemy - 1 && !BaseObject.GameOver)
=======
        if (transform.position.x < BaseScript.FirstEnemy - 1 && !BaseScript.GameOver)
>>>>>>> Youri
        {
            if (transform.position.x < Closest - 1)
                transform.Translate(0.05f, 0, 0);
        }

        if (transform.position.x >= 5 && BaseScript.EnemyBaseHealth > 0)
            BaseScript.EnemyBaseHealth -= damage;

        if (health <= 0)
        {
            BaseScript.FirstPlayer = -8 ;
            BaseScript.PlayerList.Remove(gameObject);
            Destroy(gameObject);
        }

        if (transform.position.x > BaseScript.FirstPlayer && health > 0)
        {
            BaseScript.FirstPlayer = transform.position.x;
        }

        Closest = 1000;
        foreach (GameObject OtherGameObject in BaseScript.PlayerList)
            if (OtherGameObject.transform.position.x < Closest && OtherGameObject.transform.position.x > transform.position.x)
                Closest = OtherGameObject.transform.position.x;*/
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(transform.position.x < collision.gameObject.transform.position.x)
            Colliding = true;
        if (collision.tag == "Enemy")
            Attackee = collision.gameObject;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        Colliding = false;
        Attackee = null;
    }

    public void TakeDamage(int damage)
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
            spriterenderer.sprite = Sprite1;
        }

        if(id == 2)
        {
            transform.localScale = new Vector3(6, 6, 1);
            health = 200;
            damage = 2;
            range = 1;
            spriterenderer.sprite = Sprite2;
        }

        if (id == 3)
        {
            transform.localScale = new Vector3(1, 1, 1);
            health = 300;
            damage = 2;
            range = 1;
            spriterenderer.sprite = Sprite3;
        }

        if (id == 4)
        {
            transform.localScale = new Vector3(2, 2, 1);
            health = 250;
            damage = 2;
            range = 1;
            spriterenderer.sprite = Sprite4;
        }

        if (id == 5)
        {
            transform.localScale = new Vector3(2, 2, 1);
            health = 400;
            damage = 4;
            range = 1;
            spriterenderer.sprite = Sprite5;
        }

        if (id == 6)
        {
            transform.localScale = new Vector3(2, 2, 1);
            health = 500;
            damage = 5;
            range = 1;
            spriterenderer.sprite = Sprite6;
        }
    }
}
