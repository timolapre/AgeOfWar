using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Base : MonoBehaviour {
    SpriteRenderer SpriteRenderer;
    public Turret TurretScript;
    public GameObject Player;
    public GameObject Enemy;
    public GameObject HealthBarPlayer;
    public GameObject HealthBarEnemy;
    public Transform SpawnPlayerLocation;
    public Transform SpawnEnemyLocation;
    public Text MoneyText;
    public Text XpText;
    public Text GameOverText;
    public Text WhatTierText;
    public float FirstPlayer;
    public float FirstEnemy;
    public int Money;
    public int StartMoney = 20;
    public float PlayerBaseHealth;
    public float EnemyBaseHealth;
    public int XP;
    public int WhatTier = 1;
    public bool GameOver;
    public bool VsAI;
    public bool Teams;
    public int Faction1;

    public Sprite Sprite1;
    public Sprite Sprite2;
    public Sprite Sprite3;
    public Sprite Sprite4;
    public Sprite Sprite5;

    public bool Playing;
    public bool Paused;

    public List<GameObject> PlayerList;
    public List<GameObject> EnemyList;

    private int Timer;
    private int Random;

    public EBase eBase;
    int PlayerID = 0;

    public int[,] UnitCosts = { {1, 2, 3},
                                 {4, 5, 6} };

	void Start () {
        //Instantiate(Object, spawn.position, spawn.rotation);
        SpriteRenderer = GetComponent<SpriteRenderer>();
        TurretScript = GetComponentInParent<Turret>();
        eBase = GetComponentInChildren<EBase>();
        Teams = false;
        VsAI = false;
        Money = StartMoney;
        PlayerBaseHealth = 1000;
        EnemyBaseHealth = 1000;
        Random = UnityEngine.Random.Range(100, 1000);
        Playing = true;
        Faction1 = 0;
    }

	void Update () {
        if (WhatTier == 1)
            SpriteRenderer.sprite = Sprite1;
        else if (WhatTier == 2)
            SpriteRenderer.sprite = Sprite2;
        else if (WhatTier == 3)
            SpriteRenderer.sprite = Sprite3;
        else if (WhatTier == 4)
            SpriteRenderer.sprite = Sprite4;
        else if (WhatTier == 5)
            SpriteRenderer.sprite = Sprite5;
        if (Playing)
        {
            MoneyText.text = "Money: " + Money;
            XpText.text = "XP: " + XP;
            WhatTierText.text = "Tier " + WhatTier;
        }
        else
        {
            MoneyText.text = "";
            XpText.text = "";
            WhatTierText.text = "";
        }
        HealthBarPlayer.transform.localScale = new Vector3(((float)3/1000*PlayerBaseHealth),0.2f,0.2f);
        HealthBarPlayer.transform.position = new Vector3(HealthBarPlayer.transform.localScale.x/2 - 9.5f, HealthBarPlayer.transform.position.y, HealthBarPlayer.transform.position.z);
        HealthBarEnemy.transform.localScale = new Vector3(((float)3 / 1000 * EnemyBaseHealth), 0.2f, 0.2f);
        HealthBarEnemy.transform.position = new Vector3(HealthBarEnemy.transform.localScale.x / 2 + 14f, HealthBarEnemy.transform.position.y, HealthBarEnemy.transform.position.z);
        if (PlayerBaseHealth <= 0 && !GameOver)
        {
            GameOver = true;
            Playing = false;
            SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);
        }
        else if(EnemyBaseHealth <= 0 && !GameOver)
        {
            GameOver = true;
            Playing = false;
            SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);
        }

        if (InputHelper.GetActionDown(PlayerID, Joycon.Button.DPAD_LEFT) && Money >= 1)
        {
            SpawnPlayer(WhatTier*3-2);
        }
        if (InputHelper.GetActionDown(PlayerID, Joycon.Button.DPAD_DOWN) && Money >= 3)
        {
            SpawnPlayer(WhatTier*3-1);
        }
        if (InputHelper.GetActionDown(PlayerID, Joycon.Button.DPAD_RIGHT) && Money >= 5)
        {
            SpawnPlayer(WhatTier*3);
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
        if (InputHelper.GetActionDown(PlayerID, Joycon.Button.PLUS) && !Paused)
        {
            Paused = true;
            Playing = false;
            SceneManager.LoadScene("Paused", LoadSceneMode.Additive);
        }
        else if (InputHelper.GetActionDown(PlayerID, Joycon.Button.PLUS) && Paused)
        {
            Paused = false;
            Playing = true;
            SceneManager.UnloadSceneAsync("Paused");
        }
        if (Input.GetKeyDown(KeyCode.R) && GameOver)
        {
            Reset();
        }
    }

    public void SpawnPlayer(int id)
    {
        if (Money >= UnitCosts[(id-1)/3,(id-1)%3] && Playing)
        {
            GameObject tempPlayer = Instantiate(Player, SpawnPlayerLocation.position, SpawnPlayerLocation.rotation, transform) as GameObject;
            Player tempPlayerScript = tempPlayer.GetComponent<Player>();
            tempPlayerScript.WhichUnit = id;
            PlayerList.Add(tempPlayer);
            Money -= UnitCosts[(id-1) / 3, (id - 1) % 3];
        }
    }
    public void Reset()
    {
        PlayerBaseHealth = 1000;
        EnemyBaseHealth = 1000;
        GameOver = false;
        Playing = true;
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
        WhatTier = 1;
    }
}
