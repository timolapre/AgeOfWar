using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {

    public Base BaseScript;
    public GameObject Enemy;
    public Transform spawnEnemy;

    private int timer;
    private int random;

	// Use this for initialization
	void Start () {
        BaseScript = GetComponentInParent<Base>();
    }
	
	// Update is called once per frame
	void Update () {

        timer++;
        if(timer > random)
        {
            SpawnEnemy(Random.Range(1,4));
            random = Random.Range(50, 210);
            timer = 0;
        }

        if (Input.GetKeyDown(KeyCode.L))
            SpawnEnemy(Random.Range(1,4));
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
