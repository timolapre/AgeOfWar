using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour {

    public int WhichUnit;

    SpriteRenderer SpriteRenderer;
    public Sprite Sprite1, Sprite2, Sprite3, Sprite4, Sprite5, Sprite6;

    private Base BaseScript;
    private float Closest = 1000;

    public int Health, Damage, Speed = 2, Range, Xp, Money;
    private float AttackEveryXSeconds, AttackAfterXSeconds;
    private int GetDamage;

    // Use this for initialization
    void Start () {
        BaseScript = GetComponentInParent<Base>();
        SpriteRenderer = GetComponent<SpriteRenderer>();

        GetStartValues(WhichUnit);

        PolygonCollider2D comp = gameObject.AddComponent<PolygonCollider2D>();
        comp.isTrigger = true;
    }

    bool AtOtherBase = false;
    bool Colliding = false;
    GameObject Attackee = null;
    // Update is called once per frame
    void Update () {
        if (transform.position.x >= BaseScript.SpawnEnemyLocation.position.x)
            AtOtherBase = true;

        if (!Colliding && !AtOtherBase && BaseScript.Playing)
            transform.Translate(Speed * Time.deltaTime, 0, 0);
        else if (AtOtherBase && !Colliding && BaseScript.Playing)
            BaseScript.EnemyBaseHealth -= Damage * 200 * Time.deltaTime;
        else if (BaseScript.Playing)
            //Attackee.GetComponent<Enemy>().StartTakingDamage(Damage, AttackAfterXSeconds, AttackEveryXSeconds);

        if (Health <= 0)
        {
            BaseScript.eBase.XP += Xp;
            BaseScript.eBase.Money += Money;
            Destroy(gameObject);
        }

        /*if (transform.position.x < BaseObject.FirstEnemy - 1 && !BaseObject.GameOver)
        if (transform.position.x < BaseScript.FirstEnemy - 1 && !BaseScript.GameOver)
	// Update is called once per frame
	void Update () {
        if (transform.position.x < BaseObject.FirstEnemy - 1 && !BaseObject.GameOver)

        if (transform.position.x < BaseScript.FirstEnemy - 1 && BaseScript.Playing)
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(transform.position.x < collision.gameObject.transform.position.x && (collision.tag == "Enemy" || collision.tag == "Player"))
            Colliding = true;
        if (collision.tag == "Enemy")
            collision.gameObject.GetComponent<Enemy>().StartTakingDamage(Damage, AttackAfterXSeconds, AttackEveryXSeconds);
        //Attackee = collision.gameObject;
        Debug.Log(collision.gameObject.name);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        Colliding = false;
        Attackee = null;
    }

    public void StartTakingDamage(int Damage, float GetHitAfterSeconds, float GetHitEverySeconds)
    {
        GetDamage = Damage;
        InvokeRepeating("TakeDamage", GetHitAfterSeconds, GetHitEverySeconds);
    }

    void TakeDamage()
    {
        Health -= GetDamage;
    }

    void GetStartValues(int id)
    {
        if (id == 1)
        {
            transform.localScale = new Vector3(6, 6, 1);
            Health = 10;
            Damage = 1;
            Range = 1;
            SpriteRenderer.sprite = Sprite1;
            Xp = 1;
            Money = 1;
            AttackAfterXSeconds = 0.3f;
            AttackEveryXSeconds = 1f;
        }

        if (id == 2)
        {
            transform.localScale = new Vector3(6, 6, 1);
            Health = 20;
            Damage = 2;
            Range = 1;
            SpriteRenderer.sprite = Sprite2;
            Xp = 2;
            Money = 2;
            AttackAfterXSeconds = 0.3f;
            AttackEveryXSeconds = 1f;
        }

        if (id == 3)
        {
            transform.localScale = new Vector3(1, 1, 1);
            Health = 30;
            Damage = 2;
            Range = 1;
            SpriteRenderer.sprite = Sprite3;
            Xp = 3;
            Money = 3;
            AttackAfterXSeconds = 0.3f;
            AttackEveryXSeconds = 1f;
        }

        if (id == 4)
        {
            transform.localScale = new Vector3(2, 2, 1);
            Health = 25;
            Damage = 2;
            Range = 1;
            SpriteRenderer.sprite = Sprite4;
            Xp = 4;
            Money = 4;
            AttackAfterXSeconds = 0.3f;
            AttackEveryXSeconds = 1f;
        }

        if (id == 5)
        {
            transform.localScale = new Vector3(2, 2, 1);
            Health = 40;
            Damage = 4;
            Range = 1;
            SpriteRenderer.sprite = Sprite5;
            Xp = 5;
            Money = 5;
            AttackAfterXSeconds = 0.3f;
            AttackEveryXSeconds = 1f;
        }

        if (id == 6)
        {
            transform.localScale = new Vector3(2, 2, 1);
            Health = 50;
            Damage = 5;
            Range = 1;
            SpriteRenderer.sprite = Sprite6;
            Xp = 5;
            Money = 5;
            AttackAfterXSeconds = 0.3f;
            AttackEveryXSeconds = 1f;
        }
    }
}
