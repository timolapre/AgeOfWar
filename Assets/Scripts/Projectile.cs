using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public Base BaseScript;
    
	public float direction; // 0 to the right
    public string kills = "Enemy";
    public int damage;

    public GameObject Explosion;
    
	float speed = 10;
	float gravity = -9.81f;

	float verspeed;
	float horspeed;

	void Start()
    {
        BaseScript = GetComponentInParent<Base>();
        transform.rotation = Quaternion.AngleAxis(-direction, Vector3.back);
		verspeed = Mathf.Cos(direction * Mathf.PI / 180) * speed;
		horspeed = -Mathf.Sin(direction * Mathf.PI / 180) * speed;
	}

    // Update is called once per frame
    void Update ()
    {
        if (!BaseScript.Paused)
        {
            if (transform.position.y < -5)
            {
                GameObject Expl = Instantiate(Explosion, new Vector3(transform.position.x, -0.6f, transform.position.z), new Quaternion(0, 0, 0, 0));
                Destroy(gameObject);
            }
                
            verspeed += gravity * Time.deltaTime;
            direction = Mathf.Atan(verspeed / horspeed) * 180 / Mathf.PI;
            transform.rotation = Quaternion.AngleAxis(-direction + 90 * (horspeed <= 0 ? -1 : 1), Vector3.back);

            transform.position = new Vector3(transform.position.x + horspeed * Time.deltaTime, transform.position.y + verspeed * Time.deltaTime, transform.position.z);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == kills)
        {
            collision.gameObject.SendMessage("TakeDamage", damage);
            Destroy(gameObject);
        }

        if (collision.tag == "Enemy" && kills == "Enemy")
            collision.gameObject.SendMessage("TakeBulletDamage", damage);

        if (collision.tag == "Player" && kills == "Player")
            collision.gameObject.SendMessage("TakeBulletDamage", damage);
    }
}
