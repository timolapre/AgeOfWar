using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int WhichUnit;
    public GameObject Explosion, Particle;

    SpriteRenderer SpriteRenderer;
    public Sprite Sprite1, Sprite2, Sprite3, Sprite4, Sprite5, Sprite6;

    private Base BaseScript;
    private float Closest;

    public float Health, Damage, Speed, Range, Xp, Money;
    private float AttackAfterXSeconds, AttackEveryXSeconds, GetDamage;

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
            transform.Translate(-Speed * Time.deltaTime, 0, 0);
        //else if (AtOtherBase && !Colliding && BaseScript.Playing)
            //BaseScript.PlayerBaseHealth -= Damage * 200 * Time.deltaTime;
        //else if (BaseScript.Playing) { };
        //Attackee.GetComponent<Player>().StartTakingDamage(Damage,);

        if (Health <= 0)
        {
            BaseScript.XP += Xp;
            BaseScript.Money += Money;
            Destroy(gameObject);
            GameObject expl = Instantiate(Explosion);
            expl.transform.parent = transform.parent;
            expl.transform.position = new Vector3(transform.position.x, 0, 0);
            expl.GetComponent<Explosion>().Expl = (WhichUnit - 1) / 3 + 1;
            expl.GetComponent<Explosion>().ExplTimes = 0;
            expl.transform.Translate(0, -2f, 0);
            Destroy(gameObject);
        }       
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().StartTakingDamage(Damage, AttackAfterXSeconds, AttackEveryXSeconds);
            Attackee = collision.gameObject;
        }
        if (collision.tag == "Base")
        {
            collision.gameObject.GetComponent<Base>().StartTakingDamage(Damage, AttackAfterXSeconds, AttackEveryXSeconds);
            Attackee = collision.gameObject;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (transform.position.x > collision.gameObject.transform.position.x && (collision.tag == "Enemy" || collision.tag == "Player"))
            Colliding = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        Colliding = false;
        Attackee = null;
        if(collision.tag == "Player")
            CancelInvoke("TakeDamage");
    }

    public void StartTakingDamage(float Damage, float GetHitAfterSeconds, float GetHitEverySeconds)
    {
        GetDamage = Damage;
        InvokeRepeating("TakeDamage", GetHitAfterSeconds, GetHitEverySeconds);
    }

    void TakeDamage()
    {
        Health -= GetDamage;
        Instantiate(Particle, new Vector3(transform.position.x + Random.Range(-0.5f,0.5f),transform.position.y + Random.Range(0.1f, 0.9f), transform.position.z-2), transform.rotation);
        SpriteRenderer.color = new Color(1,0.8f,0.8f);
        Invoke("ChangeColorToWhite",0.1f);
    }

    void TakeBulletDamage(int Damage)
    {
        Health -= Damage;
        Instantiate(Particle, new Vector3(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(0.1f, 0.9f), transform.position.z - 2), transform.rotation);
        SpriteRenderer.color = new Color(1, 0.75f, 0.75f);
        Invoke("ChangeColorToWhite", 0.1f);
    }

    void ChangeColorToWhite()
    {
        SpriteRenderer.color = Color.white;
    }

    void GetStartValues(int id)
    {
        SpriteRenderer.sprite = Resources.Load(BaseScript.WhatFactionEnemy + "/Units/" + BaseScript.WhatTierEnemy.ToString() + "_" + (((id - 1) % 3) + 1).ToString(), typeof(Sprite)) as Sprite;

        transform.localScale = new Vector3(2, 2, 1);
        if (id == 1)
        {
            Health = 3 * BaseScript.WhatTierEnemy;
            Damage = BaseScript.WhatTierEnemy;
            Range = 1;
            Xp = 1;
            Money = 5;
            AttackAfterXSeconds = 0.45f;
            AttackEveryXSeconds = 1f;
            Speed = 3;
        }

        if (id == 2)
        {
            Health = 6 * BaseScript.WhatTierEnemy;
            Damage = 2 + BaseScript.WhatTierEnemy;
            Range = 1;
            Xp = 2;
            Money = 10;
            AttackAfterXSeconds = 0.45f;
            AttackEveryXSeconds = 1f;
            Speed = 2;
        }

        if (id == 3)
        {
            Health = 9 * BaseScript.WhatTierEnemy;
            Damage = 1 + BaseScript.WhatTierEnemy;
            Range = 1;
            Xp = 3;
            Money = 15;
            AttackAfterXSeconds = 0.45f;
            AttackEveryXSeconds = 1f;
            Speed = 1;
        }

        /*if (id == 4)
        {
            Health = 25;
            Damage = 2;
            Range = 1;
            Xp = 4;
            Money = 4;
            AttackAfterXSeconds = 0.3f;
            AttackEveryXSeconds = 0.3f;
        }

        if (id == 5)
        {
            Health = 40;
            Damage = 4;
            Range = 1;
            Xp = 5;
            Money = 5;
            AttackAfterXSeconds = 0.3f;
            AttackEveryXSeconds = 0.3f;
        }

        if (id == 6)
        {
            Health = 50;
            Damage = 5;
            Range = 1;
            Xp = 5;
            Money = 5;
            AttackAfterXSeconds = 0.3f;
            AttackEveryXSeconds = 0.3f;
        }*/
    }
}
