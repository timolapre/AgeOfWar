using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DropDownGetValue : MonoBehaviour {

    Dropdown DropDown;
    public int id;

    // Use this for initialization
    void Start () {
        DropDown = GetComponentInChildren<Dropdown>();
	}
	
	// Update is called once per frame
	void Update () {
        FollowID();
	}

    void FollowID()
    {
        if (id == 1)
            PlayerPrefs.SetString("Faction", DropDown.options[DropDown.value].text);
        if (id == 2)
            PlayerPrefs.SetString("FactionEnemy", DropDown.options[DropDown.value].text);
    }
}
