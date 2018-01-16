using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Base : MonoBehaviour {
    SpriteRenderer SpriteRenderer;
    private EBase EBaseScript;
    public Turret TurretScript;
    public Turret EnemyTurretScript;
    public GameObject Player;
    public GameObject Enemy;
    public GameObject HealthBarPlayer;
    public GameObject HealthBarEnemy;
    public GameObject CanvasSingle;
    public GameObject CanvasMulti;
    public Transform SpawnPlayerLocation;
    public Transform SpawnEnemyLocation;
    public Text MoneyText;
    public Text XpText;
    public Text WhatTierText;
    public Text MoneyTextP1;
    public Text XpTextP1;
    public Text WhatTierTextP1;
    public Text MoneyTextP2;
    public Text XpTextP2;
    public Text WhatTierTextP2;
    public Text GameOverText;    
    public float FirstPlayer;
    public float FirstEnemy;
    public float Money;
    public int StartMoney = 20;
    public float PlayerBaseHealth;
    public float EnemyBaseHealth;
    private float PlayerBaseHealthStart;
    private float EnemyBaseHealtStart;
    public float Difficulty;
    public float XP;
    public int WhatTier = 1;
    public int WhatTierEnemy = 1;
    public string WhatFaction;
    public string WhatFactionEnemy;
    public bool GameOver;
    public bool VsAI;
    public bool Teams;

    public bool Playing;
    public bool Paused;

    public List<GameObject> PlayerList;
    public List<GameObject> EnemyList;

    public EBase eBase;
    int PlayerID = 0;

    public int[,] UnitCosts = { {1, 2, 3},
                                {4, 5, 6},
                                {7, 8, 9},
                                {7, 8, 9},
                                {7, 8, 9} };

    void Start () {
        //Instantiate(Object, spawn.position, spawn.rotation);
        EBaseScript = GetComponentInChildren<EBase>();
        string vsai = PlayerPrefs.GetString("PlayerMode");
        CanvasSingle = GameObject.Find("Canvas Single");
        CanvasMulti = GameObject.Find("Canvas Multi");
        if (vsai == "Singleplayer")
        {
            VsAI = true;
            CanvasMulti.SetActive(false);
        }
        else
        {
            VsAI = false;
            CanvasSingle.SetActive(false);
        }
        
    //Debug.Log(vsai + " " + VsAI);

        WhatFaction = PlayerPrefs.GetString("Faction");
        WhatFactionEnemy = PlayerPrefs.GetString("FactionEnemy");
        string Dif = PlayerPrefs.GetString("Difficulty");
        if (Dif == "Easy")
            Difficulty = 1.1f;
        if (Dif == "Normal")
            Difficulty = 1f;
        if (Dif == "Hard")
            Difficulty = 0.9f;
        Debug.Log(WhatFaction + " " + WhatFactionEnemy);

        SpriteRenderer = GetComponent<SpriteRenderer>();
        TurretScript = GameObject.Find("Turret").GetComponent<Turret>();
        EnemyTurretScript = GameObject.Find("Turret2").GetComponent<Turret>();
        eBase = GetComponentInChildren<EBase>();
        Teams = false;
        //  VsAI = true;
        Money = StartMoney;
        PlayerBaseHealth = 1000;
        EnemyBaseHealth = 1000;
        PlayerBaseHealthStart = 1000;
        EnemyBaseHealtStart = 1000;
        Playing = true;
        WhatTier = 1;
        WhatTierEnemy = 1;        
    }

	void Update () {
        SpriteRenderer.sprite = Resources.Load(WhatFaction + "/Bases/" + WhatTier, typeof(Sprite)) as Sprite;

        if (Playing)
        {
            MoneyText.text = "Money: " + Money;
            XpText.text = "XP: " + XP;
            WhatTierText.text = "Tier " + WhatTier;
            MoneyTextP1.text = "Money: " + Money;
            XpTextP1.text = "XP: " + XP;
            WhatTierTextP1.text = "Tier " + WhatTier;
            MoneyTextP2.text = "Money: " + EBaseScript.Money;
            XpTextP2.text = "XP: " + EBaseScript.XP;
            WhatTierTextP2.text = "Tier " + WhatTierEnemy;
        }
        else
        {
            MoneyText.text = "";
            XpText.text = "XP: " + XP;
            WhatTierText.text = "";
            MoneyTextP1.text = "";
            XpTextP1.text = "";
            WhatTierTextP1.text = "";
            MoneyTextP2.text = "";
            XpTextP2.text = "";
            WhatTierTextP2.text = "Tier " + WhatTierEnemy;
        }
        HealthBarPlayer.transform.localScale = new Vector3(((float)3/PlayerBaseHealthStart*PlayerBaseHealth),0.2f,0.2f);
        HealthBarPlayer.transform.position = new Vector3(HealthBarPlayer.transform.localScale.x/2 - 9.5f, HealthBarPlayer.transform.position.y, HealthBarPlayer.transform.position.z);
        HealthBarEnemy.transform.localScale = new Vector3(((float)3/EnemyBaseHealtStart * EnemyBaseHealth), 0.2f, 0.2f);
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

        if (Input.GetKeyDown(KeyCode.M))
        {
             GameObject tempEnemy = Instantiate(Enemy, SpawnEnemyLocation.position, SpawnEnemyLocation.rotation, transform) as GameObject;
             EnemyList.Add(tempEnemy);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Money++;
        }
        if (InputHelper.GetActionDown(PlayerID, Joycon.Button.SR))
        {
            UpgradeTier();
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
            TurretScript.Reset();
            EnemyTurretScript.Reset();
        }
    }

    public void SpawnPlayer(int id)
    {
        if (Money >= id * 5 && Playing)
        {
            GameObject tempPlayer = Instantiate(Player, SpawnPlayerLocation.position, SpawnPlayerLocation.rotation, transform) as GameObject;
            Player tempPlayerScript = tempPlayer.GetComponent<Player>();
            tempPlayerScript.WhichUnit = id;
            PlayerList.Add(tempPlayer);
            Money -= id * 5;
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
        WhatTierEnemy = 1;
        eBase.Reset();
    }
    public void UpgradeTier()
    {
        if(XP >= 10 * WhatTier)
        {
            XP -= WhatTier * 10;
            WhatTier++;            
        }       
    }
    public bool CanUpgradeTier()
    {
        if (XP >= 10 * WhatTier && WhatTier != 5)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
