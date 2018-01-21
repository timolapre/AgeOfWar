using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CampaingnSelect : MonoBehaviour {

	List<string> Order = new List<string>() { "Germany", "Russia", "Imperium of Man" };
	int Selected = 0;

	GameObject Left;
	Text Faction;
	Image Image;
	GameObject Right;

	List<string> Beaten = new List<string>() { "Germany", "Russia", "Imperium of Man" };

	// Use this for initialization
	void Start () {
		Left = GameObject.Find("Left");
		Faction = GameObject.Find("FactionText").GetComponent<Text>();
		Image = GameObject.Find("Image").GetComponent<Image>();
		Right = GameObject.Find("Right");

		if (File.Exists(Application.persistentDataPath + "/SaveData.json"))
			Beaten = new List<string>(File.ReadAllLines(Application.persistentDataPath + "/SaveData.json"));//File.Delete(Application.persistentDataPath + "/SaveData.json");

			if (Beaten.Contains(Faction.text))
			Right.GetComponent<UnityEngine.UI.Button>().interactable = true;
	}

	public void SelectSide(string side)
	{
		if(side == "middle")
		{
			PlayerPrefs.SetString("Faction", "Germany");				///Change this so you can choose yourself
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

		if (Selected == Order.Count - 1 || !Beaten.Contains(Faction.text))
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
