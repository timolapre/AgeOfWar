using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextScroller : MonoBehaviour {

    GameObject text1;
    GameObject text2;
    float speed = 1.25f;

	// Use this for initialization
	void Start () {
        text1 = GameObject.Find("Titles");
        text2 = GameObject.Find("Names");
	}
	
	// Update is called once per frame
	void Update () {
        float spd = speed * (-2 * InputHelper.GetStick(0).y + 1);
        if (spd < 0)
            spd = 0;
        text1.transform.Translate(new Vector2(0, spd));
        text2.transform.Translate(new Vector2(0, spd));

        if (InputHelper.GetActionDown(0, Joycon.Button.PLUS) || text2.transform.localPosition.y - text2.GetComponent<RectTransform>().rect.height / 2 > 900)
            SceneManager.LoadScene("Menu");
    }
}
