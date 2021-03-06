﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTimer : MonoBehaviour {

    public int ID;
    public Base BaseScript;
    public EBase EnemyBaseScript;
    public GameObject Sprite1, Sprite2, Sprite3, Sprite4, Sprite5;

	// Use this for initialization
	void Start () {
        //transform.localScale = new Vector3(0, transform.localScale.y, transform.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (ID == 1)
        {
            if (BaseScript.SpawnTimer > 0)
            {
                transform.localScale -= new Vector3(Time.deltaTime / (float)(BaseScript.SpawnTimer / 2.7), 0, 0);
            }
            else
            {
                transform.localScale = new Vector3(0, transform.localScale.y, transform.localScale.z);
            }

            if (BaseScript.SpawnList.Count >= 1)
                Sprite1.GetComponent<SpriteRenderer>().sprite = Resources.Load(BaseScript.WhatFaction + "/Units/" + BaseScript.SpawnList[0][1] + "_" + BaseScript.SpawnList[0][0].ToString(), typeof(Sprite)) as Sprite;
            else
                Sprite1.GetComponent<SpriteRenderer>().sprite = null;

            if (BaseScript.SpawnList.Count >= 2)
                Sprite2.GetComponent<SpriteRenderer>().sprite = Resources.Load(BaseScript.WhatFaction + "/Units/" + BaseScript.SpawnList[1][1] + "_" + BaseScript.SpawnList[1][0].ToString(), typeof(Sprite)) as Sprite;
            else
                Sprite2.GetComponent<SpriteRenderer>().sprite = null;

            if (BaseScript.SpawnList.Count >= 3)
                Sprite3.GetComponent<SpriteRenderer>().sprite = Resources.Load(BaseScript.WhatFaction + "/Units/" + BaseScript.SpawnList[2][1] + "_" + BaseScript.SpawnList[2][0].ToString(), typeof(Sprite)) as Sprite;
            else
                Sprite3.GetComponent<SpriteRenderer>().sprite = null;

            if (BaseScript.SpawnList.Count >= 4)
                Sprite4.GetComponent<SpriteRenderer>().sprite = Resources.Load(BaseScript.WhatFaction + "/Units/" + BaseScript.SpawnList[3][1] + "_" + BaseScript.SpawnList[3][0].ToString(), typeof(Sprite)) as Sprite;
            else
                Sprite4.GetComponent<SpriteRenderer>().sprite = null;

            if (BaseScript.SpawnList.Count >= 5)
                Sprite5.GetComponent<SpriteRenderer>().sprite = Resources.Load(BaseScript.WhatFaction + "/Units/" + BaseScript.SpawnList[4][1] + "_" + BaseScript.SpawnList[4][0].ToString(), typeof(Sprite)) as Sprite;
            else
                Sprite5.GetComponent<SpriteRenderer>().sprite = null;
        }
        else
        {
            if (EnemyBaseScript.SpawnTimer > 0)
            {
                transform.localScale -= new Vector3(Time.deltaTime / (float)(EnemyBaseScript.SpawnTimer / 2.7), 0, 0);
            }
            else
            {
                transform.localScale = new Vector3(0, transform.localScale.y, transform.localScale.z);
            }

            if (EnemyBaseScript.SpawnList.Count >= 1)
                Sprite1.GetComponent<SpriteRenderer>().sprite = Resources.Load(BaseScript.WhatFactionEnemy + "/Units/" + EnemyBaseScript.SpawnList[0][1].ToString() + "_" + EnemyBaseScript.SpawnList[0][0].ToString(), typeof(Sprite)) as Sprite;
            else
                Sprite1.GetComponent<SpriteRenderer>().sprite = null;

            if (EnemyBaseScript.SpawnList.Count >= 2)
                Sprite2.GetComponent<SpriteRenderer>().sprite = Resources.Load(BaseScript.WhatFactionEnemy + "/Units/" + EnemyBaseScript.SpawnList[1][1].ToString() + "_" + EnemyBaseScript.SpawnList[1][0].ToString(), typeof(Sprite)) as Sprite;
            else
                Sprite2.GetComponent<SpriteRenderer>().sprite = null;

            if (EnemyBaseScript.SpawnList.Count >= 3)
                Sprite3.GetComponent<SpriteRenderer>().sprite = Resources.Load(BaseScript.WhatFactionEnemy + "/Units/" + EnemyBaseScript.SpawnList[2][1].ToString() + "_" + EnemyBaseScript.SpawnList[2][0].ToString(), typeof(Sprite)) as Sprite;
            else
                Sprite3.GetComponent<SpriteRenderer>().sprite = null;

            if (EnemyBaseScript.SpawnList.Count >= 4)
                Sprite4.GetComponent<SpriteRenderer>().sprite = Resources.Load(BaseScript.WhatFactionEnemy + "/Units/" + EnemyBaseScript.SpawnList[3][1].ToString() + "_" + EnemyBaseScript.SpawnList[3][0].ToString(), typeof(Sprite)) as Sprite;
            else
                Sprite4.GetComponent<SpriteRenderer>().sprite = null;

            if (EnemyBaseScript.SpawnList.Count >= 5)
                Sprite5.GetComponent<SpriteRenderer>().sprite = Resources.Load(BaseScript.WhatFactionEnemy + "/Units/" + EnemyBaseScript.SpawnList[4][1].ToString() + "_" + EnemyBaseScript.SpawnList[4][0].ToString(), typeof(Sprite)) as Sprite;
            else
                Sprite5.GetComponent<SpriteRenderer>().sprite = null;
        }
    }
}
