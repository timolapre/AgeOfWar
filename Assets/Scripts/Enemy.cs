using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private Base BaseObject;
    public int lives;
    public int damage;
    private float Closest;
    private int xp = 5;
    private int money = 2;

	// Use this for initialization
	void Start () {
        BaseObject = GetComponentInParent<Base>();
        transform.localScale = new Vector3(-6, 6, 1);
    }
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x > BaseObject.FirstPlayer + 1)
        {
            if (transform.position.x > Closest + 1)
                transform.Translate(-0.05f, 0, 0);
        }

        if (transform.position.x <= -6 && BaseObject.PlayerBaseHealth>0)
            BaseObject.PlayerBaseHealth -= damage;

        if (lives <= 0)
        {
            BaseObject.XP += xp;
            BaseObject.money += money;
            BaseObject.FirstEnemy = 10;
            BaseObject.enemylist.Remove(gameObject);
            Destroy(gameObject);
        }

        if (transform.position.x < BaseObject.FirstEnemy && lives > 0)
        {
            BaseObject.FirstEnemy = transform.position.x;
        }

        Closest = -1000;
        foreach (GameObject OtherGameObject in BaseObject.enemylist)
            if (OtherGameObject.transform.position.x > Closest && OtherGameObject.transform.position.x < transform.position.x)
                Closest = OtherGameObject.transform.position.x;
    }
}
