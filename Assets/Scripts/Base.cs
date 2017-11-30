using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Base : MonoBehaviour {

    public GameObject player;
    public GameObject Enemy;
    public GameObject healthbarplayer;
    public GameObject healthbarenemy;
    public Transform spawnPlayer;
    public Transform spawnEnemy;
    public Text moneytext;
    public Text XPtext;
    public float FirstPlayer;
    public float FirstEnemy;
    public int money;
    public float PlayerBaseHealth;
    public float EnemyBaseHealth;
    public int XP;
    public int WhatCentury = 1;

    public List<GameObject> playerlist; 
    public List<GameObject> enemylist;

    private int timer;
    public int random;

	void Start () {
        //Instantiate(Object, spawn.position, spawn.rotation);
        PlayerBaseHealth = 1000;
        EnemyBaseHealth = 1000;
        random = Random.Range(100, 1000);
    }
	
	void Update () {
        moneytext.text = "Money: " + money;
        XPtext.text = "XP: " + XP;
        healthbarplayer.transform.localScale = new Vector3(((float)15/1000*PlayerBaseHealth),1,1);
        healthbarenemy.transform.localScale = new Vector3(((float)15 / 1000 * EnemyBaseHealth), 1, 1);

        if (WhatCentury == 1)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                SpawnPlayer(1, 1);
            }
            if (Input.GetKeyDown(KeyCode.X) && money >= 3)
            {
                SpawnPlayer(2, 3);
            }
            if (Input.GetKeyDown(KeyCode.C) && money >= 5)
            {
                SpawnPlayer(3, 5);
            }
        }
        else if(WhatCentury == 2)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                SpawnPlayer(4, 3);
            }
            if (Input.GetKeyDown(KeyCode.X) && money >= 3)
            {
                SpawnPlayer(5, 5);
            }
            if (Input.GetKeyDown(KeyCode.C) && money >= 5)
            {
                SpawnPlayer(6, 7);
            }
        }

        timer++;
        if(timer >= random)
        {
            GameObject tempEnemy = Instantiate(Enemy, spawnEnemy.position, spawnEnemy.rotation, transform) as GameObject;
            enemylist.Add(tempEnemy);
            timer = 0;
            random = Random.Range(100, 300);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
             GameObject tempEnemy = Instantiate(Enemy, spawnEnemy.position, spawnEnemy.rotation, transform) as GameObject;
             enemylist.Add(tempEnemy);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            money++;
        }
        if (Input.GetKeyDown(KeyCode.S) && XP >= 10*WhatCentury)
        {
            WhatCentury++;
        }
    }

    void SpawnPlayer(int id, int cost)
    {
        if (money >= cost)
        {
            GameObject tempPlayer = Instantiate(player, spawnPlayer.position, spawnPlayer.rotation, transform) as GameObject;
            Player tempPlayerScript = tempPlayer.GetComponent<Player>();
            tempPlayerScript.WhichUnit = id;
            playerlist.Add(tempPlayer);
            money -= cost;
        }
    }
}
