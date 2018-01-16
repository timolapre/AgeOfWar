using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour {

    public int WhichUnit;
    public GameObject Explosion;

    SpriteRenderer SpriteRenderer;

    private Base BaseScript;
    private float Closest = 1000;

    public float Health, Damage, Speed, Range, Xp, Money;
    private float AttackEveryXSeconds, AttackAfterXSeconds;
    private float GetDamage;

    // Use this for initialization
    void Start () {
        BaseScript = GetComponentInParent<Base>();
        SpriteRenderer = GetComponent<SpriteRenderer>();

        GetStartValues(WhichUnit);
        if(BaseScript.WhatFaction == "Germany" && BaseScript.VsAI)
        {
            Speed ++;
        }
        if(BaseScript.WhatFaction == "Russia" && BaseScript.VsAI)
        {
            Health += 10 * BaseScript.WhatTier;
        }
        if(BaseScript.WhatFaction == "Imperium of Man" && BaseScript.VsAI)
        {
            Damage *= 1.25f;
        }

        PolygonCollider2D comp = gameObject.AddComponent<PolygonCollider2D>();
        comp.isTrigger = true;
    }

    bool AtOtherBase = false;
    bool Colliding = false;
    GameObject Attackee = null;
    // Update is called once per frame
    void Update() {
        if (Health <= 0)
        {
            BaseScript.eBase.XP += Xp;
            BaseScript.eBase.Money += Money;
            GameObject expl = Instantiate(Explosion);
            expl.transform.parent = transform.parent;
            expl.transform.position = new Vector3(transform.position.x,0,0);
            expl.GetComponent<Explosion>().Expl = (WhichUnit-1) / 3 +1;
            expl.GetComponent<Explosion>().ExplTimes = 0;
            expl.transform.Translate(0,-2f,0);
            Destroy(gameObject);
        }

        if (transform.position.x >= BaseScript.SpawnEnemyLocation.position.x)
            AtOtherBase = true;

        if (!Colliding && !AtOtherBase && BaseScript.Playing)
            transform.Translate(Speed * Time.deltaTime, 0, 0);
        else if (AtOtherBase && !Colliding && BaseScript.Playing)
            BaseScript.EnemyBaseHealth -= Damage * 200 * Time.deltaTime;
        else if (BaseScript.Playing) { };          
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().StartTakingDamage(Damage, AttackAfterXSeconds, AttackEveryXSeconds);
            Attackee = collision.gameObject;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (transform.position.x < collision.gameObject.transform.position.x && (collision.tag == "Enemy" || collision.tag == "Player"))
            Colliding = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        Colliding = false;
        Attackee = null;
        if (collision.tag == "Enemy")
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
    }

    void TakeBulletDamage(int Damage)
    {
        Health -= Damage;
    }

    void GetStartValues(int id)
    {
        //SpriteRenderer.sprite = SpriteList[id-1];
        transform.localScale = new Vector3(-2, 2, 1);
        
        //Get Sprites. in Units folder + sprites names should be 1_1, 1_2, 2_1 etc (Tier_ID)
        SpriteRenderer.sprite = Resources.Load(BaseScript.WhatFaction + "/Units/" + BaseScript.WhatTier.ToString() + "_" + (((id-1)%3)+1).ToString(), typeof(Sprite)) as Sprite;

        if (id == 1)
        {
            Health = 10 * BaseScript.WhatTier * BaseScript.Difficulty;
            Damage = BaseScript.WhatTier * BaseScript.Difficulty;
            Range = 1;
            Xp = 1;
            Money = 7;
            AttackAfterXSeconds = 0.3f;
            AttackEveryXSeconds = 0.3f;
            Speed = 3;
        }

        if (id == 2)
        {
            Health = 20 * BaseScript.WhatTier * BaseScript.Difficulty;
            Damage = (2 + BaseScript.WhatTier) * BaseScript.Difficulty;
            Range = 1;
            Xp = 2;
            Money = 13;
            AttackAfterXSeconds = 0.3f;
            AttackEveryXSeconds = 0.3f;
            Speed = 2;
        }

        if (id == 3)
        {
            Health = 30 * BaseScript.WhatTier * BaseScript.Difficulty;
            Damage = (1 + BaseScript.WhatTier) * BaseScript.Difficulty;
            Range = 1;
            Xp = 3 ;
            Money = 19;
            AttackAfterXSeconds = 0.3f;
            AttackEveryXSeconds = 0.3f;
            Speed = 1;
        }
    }
}
