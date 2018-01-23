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
    AudioSource Audio;

    private void Start()
    {
        if (Can1.activeSelf == true)
        {
            Can2.SetActive(false);
            Can3.SetActive(false);
            Can4.SetActive(false);
            Can5.SetActive(false);
        }

        Audio = GetComponent<AudioSource>();
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
            //PlayerPrefs.SetInt("VsAI", 0);
            //PlayerPrefs.SetString("Mode", "Multiplayer");
            //SceneManager.LoadScene("AgeofWar", LoadSceneMode.Single);
            PlayerPrefs.SetString("PlayerMode", "Multiplayer");
            Can1.SetActive(false);
            Can4.SetActive(true);
        }
        else if (ChangeTo == "Singleplayer")
        {
            PlayerPrefs.SetString("PlayerMode", "Singleplayer");
            Can1.SetActive(false);
            Can2.SetActive(true);
            
            PlayerPrefs.SetInt("VsAI", 1);
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
            string Faction = Playerfaction.options[playerval].text;
            PlayerPrefs.SetString("Faction", Faction);
            int Enemyval = Enemyfaction.value;
            string FactionEnemy = Enemyfaction.options[Enemyval].text;
            PlayerPrefs.SetString("FactionEnemy", FactionEnemy);
            int Diffval = Diff.value;
            string Difficulty = Diff.options[Diffval].text;
            PlayerPrefs.SetString("Difficulty", Difficulty);
            PlayerPrefs.SetString("Mode", "SkrimischSingle");
            SceneManager.LoadScene("AgeOfWar", LoadSceneMode.Single);
        }
        else if (ChangeTo == "SKRMulti")
        {
            int player1 = Player1.value;
            string Faction = Player1.options[player1].text;
            PlayerPrefs.SetString("Faction", Faction);
            int player2 = Player2.value;
            string FactionEnemy = Player2.options[player2].text;
            PlayerPrefs.SetString("FactionEnemy", FactionEnemy);
            PlayerPrefs.SetString("Mode", "SkrimischMulti");
            SceneManager.LoadScene("AgeOfWar", LoadSceneMode.Single);
            //int PlayerFaction = PlayerPrefs.SetInt("PlayerFaction", Can1.GetComponentInChildren<Dropdown>().value);
            Dropdown[] DropdownValues = Can1.GetComponentsInChildren<Dropdown>();

        }
        else if (ChangeTo == "Menu" && PlayerPrefs.GetInt("InGame") == 1)
        {
            SceneManager.UnloadSceneAsync("Options");
            SceneManager.LoadScene("Paused", LoadSceneMode.Additive);
            PlayerPrefs.SetInt("InGame", 0);
        }
        else if (ChangeTo == "Back")
        {
            if (Can2.activeSelf == true)
            {
                Can2.SetActive(false);
                Can1.SetActive(true);
            }
            if (Can3.activeSelf == true)
            {
                Can3.SetActive(false);
                Can2.SetActive(true);
            }
            if (Can4.activeSelf == true)
            {
                Can4.SetActive(false);
                Can2.SetActive(true);
            }
            if (Can5.activeSelf == true)
            {
                Can5.SetActive(false);
                Can4.SetActive(true);
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
        PlayerPrefs.SetFloat("MusicVol", Vol);
    }
}