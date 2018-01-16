﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBase : MonoBehaviour {

    public Base BaseScript;
    public GameObject Enemy;
    public Transform spawnEnemy;

    private float RandomSpawnTime;
    private float UpgradeTime;
    public float Money;
    string Faction;
    public float XP;

    SpriteRenderer SpriteRenderer;

    int PlayerID = 1;

	// Use this for initialization
	void Start () {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        BaseScript = GetComponentInParent<Base>();

        Money = BaseScript.StartMoney;
        Faction = BaseScript.WhatFactionEnemy;

        UpgradeTime = Time.fixedTime + 30;
    }

	// Update is called once per frame
	void Update () {
        SpriteRenderer.sprite = Resources.Load(BaseScript.WhatFactionEnemy + "/Bases/" + BaseScript.WhatTierEnemy, typeof(Sprite)) as Sprite;

        if (BaseScript.VsAI)
        {
            AI();
            return;
        }

        if (InputHelper.GetActionDown(PlayerID, Joycon.Button.DPAD_LEFT) && Money >= 1)
        {
            SpawnPlayer(1);
        }
        if (InputHelper.GetActionDown(PlayerID, Joycon.Button.DPAD_DOWN) && Money >= 3)
        {
            SpawnPlayer(2);
        }
        if (InputHelper.GetActionDown(PlayerID, Joycon.Button.DPAD_RIGHT) && Money >= 5)
        {
            SpawnPlayer(3);
        }
        if (InputHelper.GetActionDown(PlayerID, Joycon.Button.SR) && XP >= 10 * BaseScript.WhatTierEnemy && BaseScript.WhatTierEnemy != 5)
        {
            XP -= BaseScript.WhatTierEnemy * 10;
            BaseScript.WhatTierEnemy++;          
        }
    }

    void SpawnPlayer(int id)
    {
        if (Money >= 5 * id && BaseScript.Playing)
        {
            GameObject tempPlayer = Instantiate(BaseScript.Enemy, BaseScript.SpawnEnemyLocation.position, BaseScript.SpawnEnemyLocation.rotation, transform) as GameObject;
            Enemy tempPlayerScript = tempPlayer.GetComponent<Enemy>();
            tempPlayerScript.WhichUnit = id;
            BaseScript.EnemyList.Add(tempPlayer);
            Money -= 5 * id;
        }
    }


    void AI()
    {
        if (Time.fixedTime > RandomSpawnTime)
        {
            SpawnEnemy(Random.Range((int)Time.fixedTime / 50 + 1, Mathf.Min(4, (int)Time.fixedTime / 25 + 2)));
            RandomSpawnTime = Random.Range(3f, 7f) + Time.fixedTime;
        }
        if (Time.fixedTime > UpgradeTime)
        {
            BaseScript.WhatTierEnemy++;
            UpgradeTime = Time.fixedTime + 30;
        }

        /*if (Input.GetKeyDown(KeyCode.L))
            SpawnEnemy(Random.Range(1, 4));*/
    }

    void SpawnEnemy(int id)
    {
        if (!BaseScript.GameOver)
        {
            GameObject tempEnemy = Instantiate(Enemy, spawnEnemy.position, spawnEnemy.rotation, transform) as GameObject;
            Enemy tempEnemyScript = tempEnemy.GetComponent<Enemy>();
            tempEnemyScript.WhichUnit = id;
            BaseScript.EnemyList.Add(tempEnemy);
        }
    }
    public void Reset()
    {
        XP = 0;
        Money = BaseScript.StartMoney;
        random = Random.Range(3f, 7f) + Time.fixedTime;
    }
}
