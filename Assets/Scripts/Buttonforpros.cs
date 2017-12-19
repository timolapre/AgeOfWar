using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Buttonforpros : MonoBehaviour
{
    public Camera Cam;
    public GameObject Can1;
    public GameObject Can2;
    public GameObject Can3;
    public GameObject Playerfaction;
    public GameObject Enemyfaction;
    public GameObject Diff;

    private void Start()
    {
        if (Can1.activeSelf == true)
        {
            Can2.SetActive(false);
            Can3.SetActive(false);
        }
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
            PlayerPrefs.SetInt("VsAI", 0);
            PlayerPrefs.SetString("Mode", "Multiplayer");
            SceneManager.LoadScene("AgeofWar", LoadSceneMode.Single);
        }
        else if (ChangeTo == "Singleplayer")
        {
            Can1.SetActive(false);
            Can2.SetActive(true);
            PlayerPrefs.SetInt("VsAI", 1);
        }
        else if (ChangeTo == "Skirmish")
        {
            Can2.SetActive(false);
            Can3.SetActive(true);
        }
        else if (ChangeTo == "SKRPlay")
        {
            /*string player = Playerfaction.GetComponent<string>();
            string Enemy = Enemyfaction.GetComponent<string>();
            string Difficulty = Diff.GetComponent<string>();
            PlayerPrefs.SetString("Player", player);
            PlayerPrefs.SetString("Enemy", Enemy);
            PlayerPrefs.SetString("Difficulty", Difficulty);
            PlayerPrefs.SetString("Mode", "Skrimisch");*/
            SceneManager.LoadScene("AgeOfWar", LoadSceneMode.Single);
            //int PlayerFaction = PlayerPrefs.SetInt("PlayerFaction", Can1.GetComponentInChildren<Dropdown>().value);
            Dropdown[] DropdownValues = Can1.GetComponentsInChildren<Dropdown>();

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