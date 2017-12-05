using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHelper {

    static List<Joycon> joycons;
	//mapping joycon buttons to keyboard keys
    static Dictionary<Joycon.Button, KeyCode> JoyToKey = new Dictionary<Joycon.Button, KeyCode>() { { Joycon.Button.DPAD_RIGHT, KeyCode.Alpha3 },
																									{ Joycon.Button.DPAD_LEFT, KeyCode.Alpha1 },
																									{ Joycon.Button.DPAD_UP, KeyCode.BackQuote },
																									{ Joycon.Button.DPAD_DOWN, KeyCode.Alpha2 },
																									{ Joycon.Button.HOME, KeyCode.Space },
																									{ Joycon.Button.SL, KeyCode.C },
																									{ Joycon.Button.SR, KeyCode.U },
																									{ Joycon.Button.PLUS, KeyCode.P },
																									{ Joycon.Button.STICK, KeyCode.Return },
                                                                                                    { Joycon.Button.SHOULDER_1, KeyCode.O } };

    // Use this for initialization
    public static void Start ()
    {
        joycons = JoyconManager.Instance.j;
    }

	// Get the stick data
	public static Vector2 GetStick(int PlayerID)
	{
		//if there is a joycon just return it's input
		if (joycons.Count > PlayerID)
		{
			return new Vector2(joycons[PlayerID].GetStick()[0], joycons[PlayerID].GetStick()[1]);
		}

		//else use WASD as stick
		return new Vector2((Input.GetKey(KeyCode.W) ? -1 : 0) + (Input.GetKey(KeyCode.S) ? 1 : 0), (Input.GetKey(KeyCode.A) ? -1 : 0) + (Input.GetKey(KeyCode.D) ? 1 : 0));
	}

	// Checks if button is held
	public static bool GetAction(int PlayerID, Joycon.Button button)
	{
		//if there is no joycon use corresponding keyboard button
		if (joycons.Count <= PlayerID)
		{
			return (Input.GetKey(JoyToKey[button]) /* || Maybe add Controller support?*/ ) ? true : false;
		}
		//else use the joycon

		//Make sure both controllers have same keymap
		if (button == Joycon.Button.HOME && joycons[PlayerID].isLeft)
		{
			button = Joycon.Button.CAPTURE;
		}
		else if (button == Joycon.Button.PLUS && joycons[PlayerID].isLeft)
		{
			button = Joycon.Button.MINUS;
		}
        else if (button == Joycon.Button.DPAD_RIGHT && joycons[PlayerID].isLeft)
        {
            button = Joycon.Button.DPAD_LEFT;
        }
        else if (button == Joycon.Button.DPAD_LEFT && joycons[PlayerID].isLeft)
        {
            button = Joycon.Button.DPAD_RIGHT;
        }
        else if (button == Joycon.Button.DPAD_UP && joycons[PlayerID].isLeft)
        {
            button = Joycon.Button.DPAD_DOWN;
        }
        else if (button == Joycon.Button.DPAD_DOWN && joycons[PlayerID].isLeft)
        {
            button = Joycon.Button.DPAD_UP;
        }

        return joycons[PlayerID].GetButton(button);
	}

	// Checks if button is released
	public static bool GetActionUp(int PlayerID, Joycon.Button button)
	{
		//if there is no joycon use corresponding keyboard button
		if (joycons.Count <= PlayerID)
		{
			return (Input.GetKeyUp(JoyToKey[button]) /* || Maybe add Controller support?*/ ) ? true : false;
		}
		//else use the joycon

		//Make sure both controllers have same keymap
		if (button == Joycon.Button.HOME && joycons[PlayerID].isLeft)
		{
			button = Joycon.Button.CAPTURE;
		}
		else if (button == Joycon.Button.PLUS && joycons[PlayerID].isLeft)
		{
			button = Joycon.Button.MINUS;
		}
        else if (button == Joycon.Button.DPAD_RIGHT && joycons[PlayerID].isLeft)
        {
            button = Joycon.Button.DPAD_LEFT;
        }
        else if (button == Joycon.Button.DPAD_LEFT && joycons[PlayerID].isLeft)
        {
            button = Joycon.Button.DPAD_RIGHT;
        }
        else if (button == Joycon.Button.DPAD_UP && joycons[PlayerID].isLeft)
        {
            button = Joycon.Button.DPAD_DOWN;
        }
        else if (button == Joycon.Button.DPAD_DOWN && joycons[PlayerID].isLeft)
        {
            button = Joycon.Button.DPAD_UP;
        }

        return joycons[PlayerID].GetButtonUp(button);
	}

	// Checks if button is pressed
	public static bool GetActionDown(int PlayerID, Joycon.Button button)
	{
		//if there is no joycon use corresponding keyboard button
		if (joycons.Count <= PlayerID)
		{
			return (Input.GetKeyDown(JoyToKey[button]) /* || Maybe add Controller support?*/ ) ? true : false;
		}
		//else use the joycon

		//Make sure both controllers have same keymap
		if (button == Joycon.Button.HOME && joycons[PlayerID].isLeft)
		{
			button = Joycon.Button.CAPTURE;
		}
		else if (button == Joycon.Button.PLUS && joycons[PlayerID].isLeft)
		{
			button = Joycon.Button.MINUS;
		}
		else if (button == Joycon.Button.DPAD_RIGHT && joycons[PlayerID].isLeft)
		{
			button = Joycon.Button.DPAD_LEFT;
		}
		else if (button == Joycon.Button.DPAD_LEFT && joycons[PlayerID].isLeft)
		{
			button = Joycon.Button.DPAD_RIGHT;
        }
        else if (button == Joycon.Button.DPAD_UP && joycons[PlayerID].isLeft)
        {
            button = Joycon.Button.DPAD_DOWN;
        }
        else if (button == Joycon.Button.DPAD_DOWN && joycons[PlayerID].isLeft)
        {
            button = Joycon.Button.DPAD_UP;
        }

        return joycons[PlayerID].GetButtonDown(button);
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
