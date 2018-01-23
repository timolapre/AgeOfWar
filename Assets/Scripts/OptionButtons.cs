using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionButtons : MonoBehaviour {
    Slider MusicVolume;
    
	// Use this for initialization
	void Start () {
        MusicVolume = GetComponent<Slider>();
        MusicVolume.value = PlayerPrefs.GetFloat("MusicVol");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
