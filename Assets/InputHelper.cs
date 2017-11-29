using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHelper : MonoBehaviour {

    static List<Joycon> joycons;
    static Dictionary<Joycon.Button, KeyCode> JoyToKey = new Dictionary<Joycon.Button, KeyCode>() { { Joycon.Button.HOME, KeyCode.C },
                                                                                                    { Joycon.Button.DPAD_DOWN, KeyCode.DownArrow } };

    // Use this for initialization
    void Start ()
    {
        joycons = JoyconManager.Instance.j;
    }
	
	// Update is called once per frame
	void Update () {

	}

    // Checks if button is held
    public static bool GetAction(int PlayerID, Joycon.Button button)
    {
        if (button == Joycon.Button.CAPTURE)
        {
            button = Joycon.Button.HOME;
        }

        //if there is a joycon just return it's input
        if(joycons.Count > PlayerID)
        {
            return joycons[PlayerID].GetButton(button);
        }

        //else use the corresponding keyboard button
        return (Input.GetKey(JoyToKey[button]) /* || Maybe add Controller support?*/ ) ? true : false;
            
    }

    //Vibrate the Controller
    public static void SetRumble(int playerID, float low_freq, float high_freq, float amp, int time = 0)
    {
        if(joycons.Count > playerID)
        {
            joycons[playerID].SetRumble(low_freq, high_freq, amp, time);
        }
        else
        {
            Debug.Log("Can't Rumble a keyboard");
        }
    }

    //Get the Controller it's gyro data
    public static Vector3 GetGyro(int playerID)
    {
        //Get the joycon Gyro if it exists
        if (joycons.Count > playerID)
        {
            return joycons[playerID].GetGyro();
        }

        //Create a keyboard solution
        return Vector3.zero;
    }

    //Check if a joycon is the Left one
    public static bool isLeft(int playerID)
    {
        if (joycons.Count > playerID)
        {
            return joycons[playerID].isLeft;
        }

        //This doesn't matter for keyboard
        return false;
    }
}
