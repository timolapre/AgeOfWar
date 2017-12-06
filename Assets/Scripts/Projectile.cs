using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public float direction; // 0 is up
    public string kills = "Enemy";
    public int damage;
    
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

        if (direction < 180)
            direction++;
        else if (direction > 180)
            direction--;
        transform.rotation = Quaternion.AngleAxis(-direction + 90, Vector3.back);

        transform.position = new Vector3(transform.position.x - Mathf.Sin(direction * Mathf.PI / 180) * speed * Time.deltaTime, transform.position.y + Mathf.Cos(direction * Mathf.PI / 180) * speed * Time.deltaTime, transform.position.z);
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
