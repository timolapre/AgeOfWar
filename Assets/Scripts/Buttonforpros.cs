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
    public GameObject Can4;
    public GameObject Can5;
    public Dropdown Playerfaction;
    public Dropdown Enemyfaction;
    public Dropdown Diff;
    public Dropdown Player1;
    public Dropdown Player2;

    private void Start()
    {
        if (Can1.activeSelf == true)
        {
            Can2.SetActive(false);
            Can3.SetActive(false);
            Can4.SetActive(false);
            Can5.SetActive(false);
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
            PlayerPrefs.SetString("PlayerMode", "Multiplayer");
            Can1.SetActive(false);
            Can4.SetActive(true);
        }
        else if (ChangeTo == "Singleplayer")
        {
            PlayerPrefs.SetString("PlayerMode", "Singleplayer");
            Can1.SetActive(false);
            Can2.SetActive(true);
        }
        else if (ChangeTo == "Skirmish")
        {
            Can2.SetActive(false);
            Can3.SetActive(true);
        }
        else if (ChangeTo == "skrimischmulti")
        {
            Can4.SetActive(false);
            Can5.SetActive(true);
        }
        else if (ChangeTo == "SKRSingle")
        {
            int playerval = Playerfaction.value;
            string player = Playerfaction.options[playerval].text;
            PlayerPrefs.SetString("Player", player);
            int Enemyval = Enemyfaction.value;
            string Enemy = Enemyfaction.options[Enemyval].text;
            PlayerPrefs.SetString("Enemy", Enemy);
            int Diffval = Diff.value;
            string Difficulty = Diff.options[Diffval].text;
            PlayerPrefs.SetString("Difficulty", Difficulty);
            PlayerPrefs.SetString("Mode", "SkrimischSingle");
            SceneManager.LoadScene("AgeOfWar", LoadSceneMode.Single);
        }
        else if (ChangeTo == "SKRMulti")
        {
            int player1 = Player1.value;
            string player1val = Player1.options[player1].text;
            PlayerPrefs.SetString("Player1", player1val);
            int player2 = Player2.value;
            string player2val = Player2.options[player2].text;
            PlayerPrefs.SetString("player2", player2val);
            PlayerPrefs.SetString("Mode", "SkrimischMulti");
            SceneManager.LoadScene("AgeOfWar", LoadSceneMode.Single);
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