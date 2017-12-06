using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public float direction; // 0 is up
    public string kills = "Enemy";
    public int damage;

    float gravity = 9.81f;
	float speed = 10;
    float lifeLeft = 1;

    void Start()
    {
        transform.rotation = Quaternion.AngleAxis(-direction + 90, Vector3.back);
    }

    // Update is called once per frame
    void Update () {
        lifeLeft -= Time.deltaTime;
        if (lifeLeft <= 0)
            Destroy(gameObject);
		transform.position = new Vector3(transform.position.x - Mathf.Sin(direction * Mathf.PI / 180) * speed * Time.deltaTime, transform.position.y + Mathf.Cos(direction * Mathf.PI / 180) * speed * Time.deltaTime, transform.position.z);
        if (Mathf.Abs(transform.position.x) > 50 || Mathf.Abs(transform.position.y) > 50)
            Destroy(gameObject);
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
