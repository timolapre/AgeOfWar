using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Music : MonoBehaviour {

    AudioSource Audio;
    bool Play;
    bool Toggle;
    string Faction;
	// Use this for initialization
	void Start () {
        Audio = GetComponent<AudioSource>();
        Play = true;
        Faction = PlayerPrefs.GetString("Faction");
        Toggle = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (Play == true && Toggle == true)
        {
            Audio.clip = Resources.Load("music/AoW" + Faction, typeof(AudioClip)) as AudioClip;
            Audio.Play();
            Audio.loop = true;
            Toggle = false;
        }
        if (Play == false && Toggle == false)
        {
            Audio.Stop();
        }
    }
}
