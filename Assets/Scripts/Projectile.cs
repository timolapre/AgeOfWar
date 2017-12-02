using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public float direction; // 0 is to the right

	float gravity = 9.81f;
	float speed = 10;
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(transform.position.x - Mathf.Sin(direction * Mathf.PI / 180) * speed * Time.deltaTime, transform.position.y + Mathf.Cos(direction * Mathf.PI / 180) * speed * Time.deltaTime, transform.position.z);
	}
}
