using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBase : MonoBehaviour {

    public Base BaseScript;
    public GameObject Enemy;
    public Transform spawnEnemy;

    private float random;
    public int Money;
    int Faction;
    public int XP;
    public int WhatTier = 1;

    int PlayerID = 1;

	// Use this for initialization
	void Start () {
        BaseScript = GetComponentInParent<Base>();

        Money = BaseScript.StartMoney;
        Faction = 0;
    }

	// Update is called once per frame
	void Update () {
        if (BaseScript.VsAI)
        {
            AI();
            return;
        }

        if (InputHelper.GetActionDown(PlayerID, Joycon.Button.DPAD_LEFT) && Money >= 1)
        {
            SpawnPlayer(WhatTier * 3 - 2);
        }
        if (InputHelper.GetActionDown(PlayerID, Joycon.Button.DPAD_DOWN) && Money >= 3)
        {
            SpawnPlayer(WhatTier * 3 - 1);
        }
        if (InputHelper.GetActionDown(PlayerID, Joycon.Button.DPAD_RIGHT) && Money >= 5)
        {
            SpawnPlayer(WhatTier * 3);
        }
        if (InputHelper.GetActionDown(PlayerID, Joycon.Button.SR) && XP >= 10 * WhatTier)
        {
            WhatTier++;
        }
    }

    void SpawnPlayer(int id)
    {
        if (Money >= BaseScript.UnitCosts[id / 3, (id - 1) % 3] && BaseScript.Playing)
        {
            GameObject tempPlayer = Instantiate(BaseScript.Enemy, BaseScript.SpawnEnemyLocation.position, BaseScript.SpawnEnemyLocation.rotation, transform) as GameObject;
            Enemy tempPlayerScript = tempPlayer.GetComponent<Enemy>();
            tempPlayerScript.WhichUnit = id;
            BaseScript.EnemyList.Add(tempPlayer);
            Money -= BaseScript.UnitCosts[(id - 1) / 3, (id - 1) % 3];
        }
    }


    void AI()
    {
        if (Time.fixedTime > random)
        {
            SpawnEnemy(Random.Range((int)Time.fixedTime / 50 + 1, Mathf.Min(4, (int)Time.fixedTime / 25 + 2)));
            random = Random.Range(3f, 7f) + Time.fixedTime;
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
}
