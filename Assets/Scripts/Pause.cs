using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {
    public Base BaseScript;
    public void Start()
    {
        BaseScript =  GameObject.Find("PBase /main object").GetComponent<Base>();
    }
    public void Buttons (string Button)
    {
        if (Button == "Resume")
        {
            SceneManager.UnloadSceneAsync("Paused");
            BaseScript.Paused = false;
            BaseScript.Playing = true;
        }
        if (Button == "Restart")
        {
            if (BaseScript.GameOver)
            {
                BaseScript.Reset();
                SceneManager.LoadScene("AgeOfWar", LoadSceneMode.Single);
            }
            else
            {
                BaseScript.Reset();
                SceneManager.UnloadSceneAsync("Paused");
                BaseScript.Paused = false;
                BaseScript.Playing = true;
            }
        }
        if (Button == "Options")
        {
            SceneManager.UnloadSceneAsync("Paused");
            PlayerPrefs.SetInt("InGame", 1);
            PlayerPrefs.SetInt("AudioPlaying", 1);
            SceneManager.LoadScene("Options", LoadSceneMode.Additive);
        }
        if (Button == "ExitToMenu")
        {
            PlayerPrefs.SetInt("AudioPlaying", 0);
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }
        if (Button == "ExitToDesktop")
        {
            PlayerPrefs.SetInt("AudioPlaying", 0);
            Application.Quit();
        }
    }
}
