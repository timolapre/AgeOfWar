using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameoverScript : MonoBehaviour {
    // Use this for initialization
    public Text WinnerText;
	void Start ()
    {
        WinnerText.text = PlayerPrefs.GetString("WinnerString") + " wins";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
