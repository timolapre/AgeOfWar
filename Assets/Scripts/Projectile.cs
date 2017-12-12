using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public float direction; // 0 is up
    public string kills = "Enemy";
    public int damage;
    
	float speed = 10;
	float gravity = -9.81f;

	float verspeed;
	float horspeed;

    void Start()
    {
        transform.rotation = Quaternion.AngleAxis(-direction + 90, Vector3.back);
		verspeed = Mathf.Cos(direction * Mathf.PI / 180) * speed;
		horspeed = -Mathf.Sin(direction * Mathf.PI / 180) * speed;
	}

    // Update is called once per frame
    void Update () {
        if (transform.position.y < -5)
            Destroy(gameObject);

		verspeed += gravity * Time.deltaTime;
		direction = Mathf.Tan(verspeed / horspeed) * 180 / Mathf.PI;
		transform.rotation = Quaternion.AngleAxis(-direction + 90, Vector3.back);

		transform.position = new Vector3(transform.position.x + horspeed * Time.deltaTime, transform.position.y + verspeed * Time.deltaTime, transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == kills)
        {
            collision.gameObject.SendMessage("TakeDamage", damage);
            Destroy(gameObject);
        }
    }
}
