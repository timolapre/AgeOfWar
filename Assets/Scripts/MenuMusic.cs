﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour {

    AudioSource Audio;
    bool Play;
    bool Toggle;
    int playing;
    // Use this for initialization
    void Start()
    {
        PlayerPrefs.SetInt("AudioPlaying", 0);
        Audio = GetComponent<AudioSource>();
        Play = true;
        Toggle = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Play == true && Toggle == true)
        {
            Audio.clip = Resources.Load("music/AoWMainTheme", typeof(AudioClip)) as AudioClip;
            Audio.Play();
            Audio.loop = true;
            Toggle = false;
        }
        if (Play == false && Toggle == false)
        {
            Audio.Stop();
        }
        PlayerPrefs.SetFloat("MusicTime", Audio.time);
    }
}
