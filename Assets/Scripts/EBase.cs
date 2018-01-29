using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBase : MonoBehaviour {

    public Base BaseScript;
    public GameObject Enemy, SpawnTimerObject;
    public Transform spawnEnemy;

    private float RandomSpawnTime, RandomSpawnTimeAtPause;
    private float UpgradeTime, UpgradeTimeAtPause;
    public float Money, XP, GetDamage;

    SpriteRenderer SpriteRenderer;

    int PlayerID = 1;

    public List<float[]> SpawnList = new List<float[]>();
    public float SpawnTimer, SpawnUnitID, SpawnUnitTier;

    // Use this for initialization
    void Start () {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        BaseScript = GetComponentInParent<Base>();

        Money = BaseScript.StartMoney;

        UpgradeTime = Time.fixedTime + 40;
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
            AddSpawnPlayer(1);
        }
        if (InputHelper.GetActionDown(PlayerID, Joycon.Button.DPAD_DOWN) && Money >= 3)
        {
            AddSpawnPlayer(2);
        }
        if (InputHelper.GetActionDown(PlayerID, Joycon.Button.DPAD_RIGHT) && Money >= 5)
        {
            AddSpawnPlayer(3);
        }
        if (InputHelper.GetActionDown(PlayerID, Joycon.Button.SR) && XP >= 10 * BaseScript.WhatTierEnemy && BaseScript.WhatTierEnemy != 5)
        {
            XP -= BaseScript.WhatTierEnemy * 10;
            BaseScript.WhatTierEnemy++;
            BaseScript.EnemyBaseHealthStart += 25;
            BaseScript.EnemyBaseHealth += 25;
        }

        if (SpawnList.Count > 0 && SpawnTimer == 0)
        {
            SpawnTimer = ((SpawnList[0][0] * SpawnList[0][1]) / 4);
            SpawnUnitID = SpawnList[0][0];
            SpawnUnitTier = SpawnList[0][1];
            Invoke("SpawnPlayer", SpawnTimer);
            SpawnTimerObject.transform.localScale = new Vector3(2.7f, SpawnTimerObject.transform.localScale.y, SpawnTimerObject.transform.localScale.z);
        }
    }

    public void AddSpawnPlayer(int id)
    {
        if (Money >= id * 5 + 5 * (BaseScript.WhatTierEnemy - 1) && BaseScript.Playing)
        {
            if (SpawnList.Count < 5)
                Money -= id * 5 + 5 * (BaseScript.WhatTierEnemy - 1);
            SpawnList.Add(new float[2] { id, BaseScript.WhatTierEnemy });
        }
    }

    private void SpawnPlayer()
    {
            GameObject tempPlayer = Instantiate(BaseScript.Enemy, BaseScript.SpawnEnemyLocation.position, BaseScript.SpawnEnemyLocation.rotation, transform) as GameObject;
            Enemy tempPlayerScript = tempPlayer.GetComponent<Enemy>();
            tempPlayerScript.WhichUnit = (int)SpawnUnitID;
            tempPlayerScript.WhichTier = (int)SpawnUnitTier;
            SpawnList.RemoveAt(0);
            BaseScript.EnemyList.Add(tempPlayer);
            SpawnTimer = 0;
    }

    void AI()
    {
        if (Time.fixedTime > RandomSpawnTime && !BaseScript.GameOver && !BaseScript.Paused)
        {
            SpawnEnemy(Random.Range(1, Mathf.Min(4, (int)Time.fixedTime / 25 + 2))); 
            RandomSpawnTime = Random.Range(3f, 7f) + Time.fixedTime;
        }
        if (Time.fixedTime > UpgradeTime && BaseScript.WhatTierEnemy != 5)
        {
            BaseScript.WhatTierEnemy++;
            BaseScript.EnemyBaseHealth += 25;
            BaseScript.EnemyBaseHealthStart += 25;
            UpgradeTime = Time.fixedTime + 80;
        }
    }

    void SpawnEnemy(int id)
    {
        if (!BaseScript.GameOver)
        {
            GameObject tempEnemy = Instantiate(Enemy, spawnEnemy.position, spawnEnemy.rotation, transform) as GameObject;
            Enemy tempEnemyScript = tempEnemy.GetComponent<Enemy>();
            tempEnemyScript.WhichUnit = id;
            tempEnemyScript.WhichTier = BaseScript.WhatTierEnemy;
            BaseScript.EnemyList.Add(tempEnemy);
        }
    }

    public void Reset()
    {
        XP = 0;
        Money = BaseScript.StartMoney;
        RandomSpawnTime = Random.Range(3f, 7f) + Time.fixedTime;
        UpgradeTime = Time.fixedTime + 40;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            CancelInvoke("TakeDamage");
    }

    public void StartTakingDamage(float Damage, float GetHitAfterSeconds, float GetHitEverySeconds)
    {
        GetDamage = Damage;
        InvokeRepeating("TakeDamage", GetHitAfterSeconds, GetHitEverySeconds);
    }

    void TakeDamage()
    {
        BaseScript.EnemyBaseHealth -= GetDamage;
    }

    public void Pause()
    {
        RandomSpawnTimeAtPause = RandomSpawnTime - Time.fixedTime;
        UpgradeTimeAtPause = UpgradeTime - Time.fixedTime;
    }

    public void Unpause()
    {
        RandomSpawnTime = RandomSpawnTimeAtPause + Time.fixedTime;
        UpgradeTime = UpgradeTimeAtPause + Time.fixedTime;
    }
}
