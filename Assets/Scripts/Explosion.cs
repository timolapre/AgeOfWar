using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    public Sprite SpriteDestroy;
    private Base BaseScript;

    private Animator animator;

    public int Expl = 1;
    public int ExplTimes = 1;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        BaseScript = GetComponentInParent<Base>();
        if (Expl == 1)
            animator.Play("SmallExplosion");
        else if (Expl == 2)
        {
            animator.Play("MediumExplosion");
            if (ExplTimes == 1)
                transform.localScale = new Vector3(1.2f * ExplTimes, 1.2f * ExplTimes, 1);
        }
        else if (Expl == 3)
        {
            animator.Play("MediumExplosion");
            if (ExplTimes == 1)
                transform.localScale = new Vector3(1.2f * ExplTimes, 1.2f * ExplTimes, 1);
        }
        else if (Expl == 4)
            animator.Play("LargeExplosion");
        else if (Expl == 5)
        {
            animator.Play("HugeExplosion");
            if (ExplTimes == 1)
            {
                transform.localScale = new Vector3(1.7f * ExplTimes, 1.7f * ExplTimes, 1);
                transform.Translate(0, 1.5f, 0);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (BaseScript.Paused)
            GetComponent<Animator>().enabled = false;
        else
            GetComponent<Animator>().enabled = true;

        if (GetComponent<SpriteRenderer>().sprite == SpriteDestroy)
            Destroy();
	}

    void Destroy()
    {
        Destroy(gameObject);
    }
}
