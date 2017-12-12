using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Buttonforpros : MonoBehaviour
{
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