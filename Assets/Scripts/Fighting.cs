﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighting : MonoBehaviour {

    Base basescript;

    // Use this for initialization
    void Start () {
        basescript = GetComponent<Base>();
	}
	
	// Update is called once per frame
	void Update () {
        try
        {
            Player player = GetFirstPlayer().GetComponent<Player>();
            Enemy enemy = GetFirstEnemy().GetComponent<Enemy>();

            if (basescript.FirstEnemy - basescript.FirstPlayer <= 1)
            {
                player.health -= enemy.Damage;
                enemy.Health -= player.damage;
            }
        }
        catch { }
	}

    GameObject GetFirstPlayer()
    {
        foreach (GameObject player in basescript.PlayerList)
            if (player.transform.position.x == basescript.FirstPlayer)
                return player;
        return null;
    }

    GameObject GetFirstEnemy()
    {
        foreach (GameObject enemy in basescript.EnemyList)
            if (enemy.transform.position.x == basescript.FirstEnemy)
                return enemy;
        return null;
    }
}
