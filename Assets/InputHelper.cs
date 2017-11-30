using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHelper : MonoBehaviour {

    static List<Joycon> joycons;
    static Dictionary<Joycon.Button, KeyCode> JoyToKey = new Dictionary<Joycon.Button, KeyCode>() { { Joycon.Button.HOME, KeyCode.C },
                                                                                                    { Joycon.Button.DPAD_UP, KeyCode.UpArrow },
																									{ Joycon.Button.DPAD_DOWN, KeyCode.DownArrow },
																									{ Joycon.Button.DPAD_LEFT, KeyCode.LeftArrow },
																									{ Joycon.Button.DPAD_RIGHT, KeyCode.RightArrow } };

    // Use this for initialization
    void Start ()
    {
        joycons = JoyconManager.Instance.j;
    }

    // Checks if button is held
    public static bool GetAction(int PlayerID, Joycon.Button button)
    {
        if (button == Joycon.Button.CAPTURE)
        {
            button = Joycon.Button.HOME;
        }
		else if(button == Joycon.Button.MINUS)
		{
			button = Joycon.Button.PLUS;
		}

        //if there is a joycon just return it's input
        if(joycons.Count > PlayerID)
        {
            return joycons[PlayerID].GetButton(button);
        }

        //else use the corresponding keyboard button
        return (Input.GetKey(JoyToKey[button]) /* || Maybe add Controller support?*/ ) ? true : false;
    }

	// Checks if button is released
	public static bool GetActionUp(int PlayerID, Joycon.Button button)
	{
		if (button == Joycon.Button.CAPTURE)
		{
			button = Joycon.Button.HOME;
		}
		else if (button == Joycon.Button.MINUS)
		{
			button = Joycon.Button.PLUS;
		}

		//if there is a joycon just return it's input
		if (joycons.Count > PlayerID)
		{
			return joycons[PlayerID].GetButtonUp(button);
		}

		//else use the corresponding keyboard button
		return (Input.GetKeyUp(JoyToKey[button]) /* || Maybe add Controller support?*/ ) ? true : false;
	}

	// Checks if button is pressed
	public static bool GetActionDown(int PlayerID, Joycon.Button button)
	{
		if (button == Joycon.Button.CAPTURE)
		{
			button = Joycon.Button.HOME;
		}
		else if (button == Joycon.Button.MINUS)
		{
			button = Joycon.Button.PLUS;
		}

		//if there is a joycon just return it's input
		if (joycons.Count > PlayerID)
		{
			return joycons[PlayerID].GetButtonDown(button);
		}

		//else use the corresponding keyboard button
		return (Input.GetKeyDown(JoyToKey[button]) /* || Maybe add Controller support?*/ ) ? true : false;
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

		//Create some gyro with keyboard buttons
		return new Vector3(0, (playerID % 2 == 0 ? 1 : -1) * ((Input.GetKey(KeyCode.W) ? 1 : 0) + (Input.GetKey(KeyCode.S) ? -1 : 0)), 0);
    }

	//check if the controller is a joycon
	public static bool isJoycon(int playerID)
	{
		return (joycons.Count > playerID);
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
