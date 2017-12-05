using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {

    public Base basescript;
    public GameObject Enemy;
    public Transform spawnEnemy;

    private float random;

	// Use this for initialization
	void Start () {
        basescript = GetComponentInParent<Base>();
	}
	
	// Update is called once per frame
	void Update () {

        if(Time.fixedTime > random)
        {
            SpawnEnemy(Random.Range((int)Time.fixedTime/50+1,Mathf.Min(4,(int)Time.fixedTime/25+2)));
            random = Random.Range(1f, 3f) + Time.fixedTime;
        }

        if (Input.GetKeyDown(KeyCode.L))
            SpawnEnemy(Random.Range(1,4));
    }

    void SpawnEnemy(int id)
    {
        if (!basescript.GameOver)
        {
            GameObject tempEnemy = Instantiate(Enemy, spawnEnemy.position, spawnEnemy.rotation, transform) as GameObject;
            Enemy tempEnemyScript = tempEnemy.GetComponent<Enemy>();
            tempEnemyScript.WhichUnit = id;
            basescript.enemylist.Add(tempEnemy);
        }
        
    }
}
