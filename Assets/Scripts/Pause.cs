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
        }
        if (Button == "Restart")
        {
            if (SceneManager.GetSceneByName("GameOver") == SceneManager.GetActiveScene())
            {
                SceneManager.LoadScene("AgeOfWar", LoadSceneMode.Single);
            }
            else
            {
                SceneManager.UnloadSceneAsync("Paused");
                BaseScript.Reset();
            }
        }
        if (Button == "Options")
        {
            SceneManager.UnloadSceneAsync("Paused");
            PlayerPrefs.SetInt("InGame", 1);
            SceneManager.LoadScene("Options", LoadSceneMode.Additive);
        }
        if (Button == "ExitToMenu")
        {
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }
        if (Button == "ExitToDesktop")
        {
            Application.Quit();
        }
    }
}
