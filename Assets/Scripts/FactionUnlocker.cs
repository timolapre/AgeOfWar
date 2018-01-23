using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FactionUnlocker : MonoBehaviour {

	// Use this for initialization
	void Awake()
    {
        List<string> Options = new List<string>();
        if (File.Exists(Application.persistentDataPath + "/SaveData.json"))
            Options = new List<string>(File.ReadAllLines(Application.persistentDataPath + "/SaveData.json"));
        Options.Remove("Germany");
        GameObject.Find("Player").GetComponent<Dropdown>().AddOptions(Options);
        GameObject.Find("Player 1").GetComponent<Dropdown>().AddOptions(Options);
        GameObject.Find("Player 2").GetComponent<Dropdown>().AddOptions(Options);
    }
}
