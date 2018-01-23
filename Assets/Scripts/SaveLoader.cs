using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoader : MonoBehaviour {

    public static List<string> Unlocked = new List<string>();

	// Use this for initialization
	void Awake () {
        if (File.Exists(Application.persistentDataPath + "/SaveData.json"))
            Unlocked = new List<string>(File.ReadAllLines(Application.persistentDataPath + "/SaveData.json"));//File.Delete(Application.persistentDataPath + "/SaveData.json");
    }
}
