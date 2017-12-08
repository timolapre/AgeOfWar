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
        else
        {
            SceneManager.LoadScene(ChangeTo, LoadSceneMode.Single);
        }
    }
}