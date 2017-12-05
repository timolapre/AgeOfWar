using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Base : MonoBehaviour {
    public GameObject Player;
    public GameObject Enemy;
    public GameObject HealthBarPlayer;
    public GameObject HealthBarEnemy;
    public Transform SpawnPlayerLocation;
    public Transform SpawnEnemyLocation;
    public Text MoneyText;
    public Text XpText;
    public Text GameOverText;
    public float FirstPlayer;
    public float FirstEnemy;
    public int Money;
    private int StartMoney = 20;
    public float PlayerBaseHealth;
    public float EnemyBaseHealth;
    public int XP;
    public int WhatTier = 1;
    public bool VsAI;
    public bool Teams;
    public bool GameOver;

    public List<GameObject> PlayerList;
    public List<GameObject> EnemyList;

    private int Timer;
    private int Random;

    int PlayerID = 0;

	void Start () {
        //Instantiate(Object, spawn.position, spawn.rotation);
        Teams = false;
        VsAI = true;
        Money = StartMoney;
        PlayerBaseHealth = 1000;
        EnemyBaseHealth = 1000;
        Random = UnityEngine.Random.Range(100, 1000);
    }

	void Update () {
        if (!GameOver)
        {
            MoneyText.text = "Money: " + Money;
            XpText.text = "XP: " + XP;
        }
        else
        {
            MoneyText.text = "";
            XpText.text = "";
        }
        HealthBarPlayer.transform.localScale = new Vector3(((float)15/1000*PlayerBaseHealth),1,1);
        HealthBarEnemy.transform.localScale = new Vector3(((float)15 / 1000 * EnemyBaseHealth), 1, 1);
        if (PlayerBaseHealth <= 0 && !GameOver)
        {
            GameOver = true;
            SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);
        }
        else if(EnemyBaseHealth <= 0 && !GameOver)
        {
            GameOver = true;
            SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);
        }

        if (InputHelper.GetActionDown(PlayerID, Joycon.Button.DPAD_LEFT) && Money >= 1)
        {
            SpawnPlayer(WhatTier*3-2, 1);
        }
        if (InputHelper.GetActionDown(PlayerID, Joycon.Button.DPAD_DOWN) && Money >= 3)
        {
            SpawnPlayer(WhatTier*3-1, 3);
        }
        if (InputHelper.GetActionDown(PlayerID, Joycon.Button.DPAD_RIGHT) && Money >= 5)
        {
            SpawnPlayer(WhatTier*3, 5);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
             GameObject tempEnemy = Instantiate(Enemy, SpawnEnemyLocation.position, SpawnEnemyLocation.rotation, transform) as GameObject;
             EnemyList.Add(tempEnemy);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Money++;
        }
        if (InputHelper.GetActionDown(PlayerID, Joycon.Button.SR) && XP >= 10*WhatTier)
        {
            WhatTier++;
        }
        if (Input.GetKeyDown(KeyCode.R) && GameOver)
        {
            Reset();
        }
    }

    public void SpawnPlayer(int id, int cost)
    {
        if (Money >= cost && !GameOver)
        {
            GameObject tempPlayer = Instantiate(Player, SpawnPlayerLocation.position, SpawnPlayerLocation.rotation, transform) as GameObject;
            Player tempPlayerScript = tempPlayer.GetComponent<Player>();
            tempPlayerScript.WhichUnit = id;
            PlayerList.Add(tempPlayer);
            Money -= cost;
        }
    }
    public void Reset()
    {
        PlayerBaseHealth = 1000;
        EnemyBaseHealth = 1000;
        GameOver = false;
        Money = StartMoney;
        XP = 0;
        Timer = 0;
        foreach (GameObject g in PlayerList)
        {
            Destroy(g);
        }
        foreach(GameObject g in EnemyList)
        {
            Destroy(g);
        }
        PlayerList.Clear();
        EnemyList.Clear();
        FirstPlayer = -8;
        FirstEnemy = 10;
    }
}
