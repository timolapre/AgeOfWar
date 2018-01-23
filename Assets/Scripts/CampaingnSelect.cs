using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CampaingnSelect : MonoBehaviour {

	List<string> Order = new List<string>() { "Germany", "Russia", "Imperium of Man" };
	int Selected = 0;

    Text Self;
	GameObject Left;
	Text Faction;
	Image Image;
	GameObject Right;

    List<string> Unlocked = new List<string>();

    // Use this for initialization
    void Start () {
        if(File.Exists(Application.persistentDataPath + "/SaveData.json"))
            Unlocked = new List<string>(File.ReadAllLines(Application.persistentDataPath + "/SaveData.json"));
        List<string> Options = new List<string>(Unlocked);
        Options.Remove("Germany");
        GameObject.Find("Player").GetComponent<Dropdown>().AddOptions(Options);
        Self = GameObject.Find("Self").GetComponent<Text>();
		Left = GameObject.Find("Left");
		Faction = GameObject.Find("FactionText").GetComponent<Text>();
		Image = GameObject.Find("Image").GetComponent<Image>();
		Right = GameObject.Find("Right");
        
		if (Unlocked.Contains(Faction.text))
		    Right.GetComponent<UnityEngine.UI.Button>().interactable = true;
	}

	public void SelectSide(string side)
	{
        if(side == "back")
        {
            SceneManager.LoadScene("Pre-GameSettings");
            return;
        }

		if(side == "middle")
		{
			PlayerPrefs.SetString("Faction", Self.text);
			PlayerPrefs.SetString("FactionEnemy", Faction.text);
			PlayerPrefs.SetString("Difficulty", "Normal");
			PlayerPrefs.SetString("Mode", "Campaign");
			SceneManager.LoadScene("AgeOfWar", LoadSceneMode.Single);
			return;
		}

		Selected += (side == "left" ? -1 : 1);
		if (Selected > 0)
		{
			Left.GetComponent<UnityEngine.UI.Button>().interactable = true;
			Left.GetComponent<Image>().color = Color.white;
		}
		else
		{
			Left.GetComponent<UnityEngine.UI.Button>().interactable = false;
			Left.GetComponent<Image>().color = Color.clear;
		}

		Faction.text = Order[Selected];
		Image.sprite = Resources.Load<Sprite>("Backgrounds/" + Order[Selected]);

		if (Selected == Order.Count - 1 || !Unlocked.Contains(Faction.text))
		{
			Right.GetComponent<UnityEngine.UI.Button>().interactable = false;
			Right.GetComponent<Image>().color = Color.clear;
		}
		else
		{
			Right.GetComponent<UnityEngine.UI.Button>().interactable = true;
			Right.GetComponent<Image>().color = Color.white;
		}
	}
}
