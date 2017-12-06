using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    SpriteRenderer SpriteRenderer;
    public Sprite Sprite1;
    public Sprite Sprite2;
    public Sprite Sprite3;
    public Sprite Sprite4;
    public Sprite Sprite5;
    public Sprite Sprite6;

    private Base BaseScript;
    public int Health;
    public int Damage;
    public int Range;
    private float Closest;
    private int Xp = 5;
    private int Money = 2;

    public int WhichUnit;

	// Use this for initialization
	void Start () {
        BaseScript = GetComponentInParent<Base>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        transform.localScale = new Vector3(-6, 6, 1);
        GetStartValues(WhichUnit);

        PolygonCollider2D comp = gameObject.AddComponent<PolygonCollider2D>();
        comp.isTrigger = true;
    }

    bool AtOtherBase = false;
    bool Colliding = false;
    GameObject Attackee;
    // Update is called once per frame
    void Update () {
        if (transform.position.x <= BaseScript.SpawnPlayerLocation.position.x)
            AtOtherBase = true;

        if (!Colliding && !AtOtherBase && BaseScript.Playing)
            transform.Translate(-.05f, 0, 0);
        else if (AtOtherBase && !Colliding && BaseScript.Playing)
            BaseScript.PlayerBaseHealth -= Damage;
        else if (BaseScript.Playing)
            Attackee.GetComponent<Player>().TakeDamage(Damage);

        if (Health <= 0)
        {
            BaseScript.XP += Xp;
            BaseScript.Money += Money;
            Destroy(gameObject);
        }

        /*if (transform.position.x > BaseScript.FirstPlayer + 1 && !BaseScript.GameOver)
	// Update is called once per frame
	void Update () {
        if (transform.position.x > BaseScript.FirstPlayer + 1 && BaseScript.Playing)
        {
            if (transform.position.x > Closest + 1)
                transform.Translate(-0.05f, 0, 0);
        }

        if (transform.position.x <= -6 && BaseScript.PlayerBaseHealth>0)
            BaseScript.PlayerBaseHealth -= Damage;

        if (Health <= 0)
        {
            BaseScript.XP += Xp;
            BaseScript.Money += Money;
            BaseScript.FirstEnemy = 10;
            BaseScript.EnemyList.Remove(gameObject);
            Destroy(gameObject);
        }

        if (transform.position.x < BaseScript.FirstEnemy && Health > 0)
        {
            BaseScript.FirstEnemy = transform.position.x;
        }

        Closest = -1000;
        foreach (GameObject OtherGameObject in BaseScript.EnemyList)
            if (OtherGameObject.transform.position.x > Closest && OtherGameObject.transform.position.x < transform.position.x)
                Closest = OtherGameObject.transform.position.x;*/
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (transform.position.x > collision.gameObject.transform.position.x && (collision.tag == "Enemy" || collision.tag == "Player"))
            Colliding = true;
        if (collision.tag == "Player")
            Attackee = collision.gameObject;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        Colliding = false;
        Attackee = null;
    }

    public void TakeDamage(int Damage)
    {
        Health -= Damage;
    }

    void GetStartValues(int id)
    {
        if (id == 1)
        {
            transform.localScale = new Vector3(-6, 6, 1);
            Health = 100;
            Damage = 1;
            Range = 1;
            SpriteRenderer.sprite = Sprite1;
            Xp = 1;
            Money = 1;
        }

        if (id == 2)
        {
            transform.localScale = new Vector3(-6, 6, 1);
            Health = 200;
            Damage = 2;
            Range = 1;
            SpriteRenderer.sprite = Sprite2;
            Xp = 2;
            Money = 2;
        }

        if (id == 3)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            Health = 300;
            Damage = 2;
            Range = 1;
            SpriteRenderer.sprite = Sprite3;
            Xp = 3;
            Money = 3;
        }

        if (id == 4)
        {
            transform.localScale = new Vector3(-2, 2, 1);
            Health = 250;
            Damage = 2;
            Range = 1;
            SpriteRenderer.sprite = Sprite4;
            Xp = 4;
            Money = 4;
        }

        if (id == 5)
        {
            transform.localScale = new Vector3(-2, 2, 1);
            Health = 400;
            Damage = 4;
            Range = 1;
            SpriteRenderer.sprite = Sprite5;
            Xp = 5;
            Money = 5;
        }

        if (id == 6)
        {
            transform.localScale = new Vector3(-2, 2, 1);
            Health = 500;
            Damage = 5;
            Range = 1;
            SpriteRenderer.sprite = Sprite6;
            Xp = 5;
            Money = 5;
        }
    }
}
