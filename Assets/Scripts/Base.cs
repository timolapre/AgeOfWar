using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Base : MonoBehaviour {
    SpriteRenderer SpriteRenderer;
    private EBase EBaseScript;
    public Turret TurretScript, EnemyTurretScript;
    public GameObject Player, Enemy, HealthBarPlayer, HealthBarEnemy, SpawnTimerObject, CanvasSingle, CanvasMulti;
    public Transform SpawnPlayerLocation, SpawnEnemyLocation;
    public Text MoneyText, XpText, WhatTierText, MoneyTextP1, XpTextP1, WhatTierTextP1, MoneyTextP2, XpTextP2, WhatTierTextP2, GameOverText;
    public float FirstPlayer, FirstEnemy;
    public float Money, XP, StartMoney = 20;
    public float PlayerBaseHealth, EnemyBaseHealth, PlayerBaseHealthStart, EnemyBaseHealthStart, GetDamage;
    public float Difficulty;
    public int WhatTier = 1, WhatTierEnemy = 1;
    public string WhatFaction, WhatFactionEnemy;
    public bool GameOver, VsAI, Teams;

    public bool Playing, Paused;

    public List<GameObject> PlayerList, EnemyList;

    public EBase eBase;
    public int PlayerID = 0;

    public List<float[]> SpawnList = new List<float[]>();
    public float SpawnTimer, SpawnUnitID, SpawnUnitTier;

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
        else if (Dif == "Normal")
            Difficulty = 1f;
        else if (Dif == "Hard")
            Difficulty = 0.9f;
        else
            Difficulty = 1;
        if (!VsAI)
            Difficulty = 1;
        SpriteRenderer = GetComponent<SpriteRenderer>();
        TurretScript = GameObject.Find("Turret").GetComponent<Turret>();
        EnemyTurretScript = GameObject.Find("Turret2").GetComponent<Turret>();
        eBase = GetComponentInChildren<EBase>();
        Teams = false;
        //  VsAI = true;
        Money = StartMoney;
        PlayerBaseHealth = 100;
        PlayerBaseHealthStart = PlayerBaseHealth;
        EnemyBaseHealth = 100;
        EnemyBaseHealthStart = EnemyBaseHealth;
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
            XpText.text = "";
            WhatTierText.text = "";
            MoneyTextP1.text = "";
            XpTextP1.text = "";
            WhatTierTextP1.text = "";
            MoneyTextP2.text = "";
            XpTextP2.text = "";
            WhatTierTextP2.text = "";
        }
        HealthBarPlayer.transform.localScale = new Vector3(((float)3 * (PlayerBaseHealth / PlayerBaseHealthStart)), 0.2f, 0.2f);
        HealthBarPlayer.transform.position = new Vector3(HealthBarPlayer.transform.localScale.x / 2 - 11.5f, HealthBarPlayer.transform.position.y, HealthBarPlayer.transform.position.z);
        HealthBarEnemy.transform.localScale = new Vector3(((float)3 * (EnemyBaseHealth / EnemyBaseHealthStart)), 0.2f, 0.2f);
        HealthBarEnemy.transform.position = new Vector3(HealthBarEnemy.transform.localScale.x / 2 + 16f, HealthBarEnemy.transform.position.y, HealthBarEnemy.transform.position.z);
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
			if (PlayerPrefs.GetString("Mode") == "Campaign")
			{
                List<string> Unlocked = new List<string>();
                if (File.Exists(Application.persistentDataPath + "/SaveData.json"))
                     Unlocked = new List<string>(File.ReadAllLines(Application.persistentDataPath + "/SaveData.json"));
                if (!Unlocked.Contains(WhatFactionEnemy))
				{
					Unlocked.Add(WhatFactionEnemy);
					File.WriteAllLines(Application.persistentDataPath + "/SaveData.json", Unlocked.ToArray());
				}
			}
            SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);
        }

        if (InputHelper.GetActionDown(PlayerID, Joycon.Button.DPAD_LEFT))
        {
            AddSpawnPlayer(1);
        }
        if (InputHelper.GetActionDown(PlayerID, Joycon.Button.DPAD_DOWN))
        {
            AddSpawnPlayer(2);
        }
        if (InputHelper.GetActionDown(PlayerID, Joycon.Button.DPAD_RIGHT))
        {
            AddSpawnPlayer(3);
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

        if(SpawnList.Count > 0 && SpawnTimer == 0)
        {
            SpawnTimer = ((SpawnList[0][0] * WhatTier) / 4);
            SpawnUnitID = SpawnList[0][0];
            SpawnUnitTier = SpawnList[0][1];
            Invoke("SpawnPlayer", SpawnTimer);
        }
    }

    public void AddSpawnPlayer(int id)
    {
        if (Money >= id * 2*WhatTier + 2 && Playing)
        {            
            if (SpawnList.Count < 5)
            {
                Money -= id * 2 * WhatTier + 2;
                SpawnList.Add(new float[2] { id, WhatTier });
            }                
            //SpawnTimerObject.transform.localScale = new Vector3(2.7f, SpawnTimerObject.transform.localScale.y, SpawnTimerObject.transform.localScale.z);
        }
    }

    private void SpawnPlayer()
    {
        GameObject tempPlayer = Instantiate(Player, SpawnPlayerLocation.position, SpawnPlayerLocation.rotation, transform) as GameObject;
        Player tempPlayerScript = tempPlayer.GetComponent<Player>();
        tempPlayerScript.WhichUnit = (int)SpawnUnitID;
        tempPlayerScript.WhichTier = (int)SpawnUnitTier;
        SpawnList.RemoveAt(0);
        PlayerList.Add(tempPlayer);
        SpawnTimer = 0;
    }

    public void Reset()
    {
        PlayerBaseHealth = 100;
        EnemyBaseHealth = 100;
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
        TurretScript.Reset();
    }
    public void UpgradeTier()
    {
        if(XP >= 10 * WhatTier)
        {
            XP -= WhatTier * 10;
            WhatTier++;
            PlayerBaseHealth += 25;
            PlayerBaseHealthStart += 25;
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

    void OnTriggerExit2D(Collider2D collision)
    {
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
        PlayerBaseHealth -= GetDamage;
    }
}
