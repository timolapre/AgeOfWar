using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FactionUnlocker : MonoBehaviour {

	// Use this for initialization
	void Awake()
    {
        List<string> Options = SaveLoader.Unlocked;
        Options.Remove("Germany");
        GameObject.Find("Player").GetComponent<Dropdown>().AddOptions(Options);
        GameObject.Find("Player 1").GetComponent<Dropdown>().AddOptions(Options);
        GameObject.Find("Player 2").GetComponent<Dropdown>().AddOptions(Options);
    }
}
