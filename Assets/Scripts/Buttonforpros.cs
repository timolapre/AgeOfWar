﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Buttonforpros : MonoBehaviour
{
    public Camera Cam;
    public GameObject Can1;
    public GameObject Can2;
    public GameObject Can3;
    public GameObject Playerfaction;
    public GameObject Enemyfaction;
    public GameObject Difficulty;

    private void Start()
    {
        Can2.SetActive(false);
        Can3.SetActive(false);
    }

    public void ChangeScene (string ChangeTo)
    {
        if (ChangeTo == "Exit")
        {
            Application.Quit();
        }
        else if (ChangeTo == "Fullscreen")
        {
            if (Screen.fullScreen == false)
            {
                Screen.fullScreen = true; 
            }
            else
            {
                Screen.fullScreen = false;
            }
        }
        else if (ChangeTo == "Multiplayer")
        {
            PlayerPrefs.SetString("Mode", "Multiplayer");
            SceneManager.LoadScene("AgeofWar", LoadSceneMode.Single);
        }
        else if (ChangeTo == "Singleplayer")
        {
            Can1.SetActive(false);
            Can2.SetActive(true);
        }
        else if (ChangeTo == "Skirmish")
        {
            Can2.SetActive(false);
            Can3.SetActive(true);
        }
        else if (ChangeTo == "SKRPlay")
        {
            PlayerPrefs.SetString("Mode", "Skrimisch");
            //PlayerPrefs.SetString("Player", Playerfaction.GetComponent<GameObject>().value;);
            //PlayerPrefs.SetString("Enemy", Enemyfaction);
        }
        else
        {
            SceneManager.LoadScene(ChangeTo, LoadSceneMode.Single);
        }
    }

    public void MasterVolume (float Vol)
    {
        AudioListener.volume = Vol;
    }
}