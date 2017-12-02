﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
	
    private float rotation;
    private float offset;
    private int offsetcount;

    public int PlayerID;
    public Vector3 gyro;

    // Use this for initialization
    void Start()
    {
        rotation = 90 * (PlayerID == 1 ? -1 : 1);
    }

    // Update is called once per frame
    void Update()
    {
		//Recenter the turret
        if (InputHelper.GetActionDown(PlayerID, Joycon.Button.STICK))
        {
            rotation = 90 * (PlayerID == 1 ? -1 : 1);
        }

		//Play the Mario Theme
        if (InputHelper.GetActionDown(PlayerID, Joycon.Button.DPAD_LEFT))
        {
            Debug.Log("Mario!");

            // Rumble for 200 milliseconds, with low frequency rumble at 160 Hz and high frequency rumble at 320 Hz. For more information check:
            // https://github.com/dekuNukem/Nintendo_Switch_Reverse_Engineering/blob/master/rumble_data_table.md

            StartCoroutine(PlayMario());
            //InputHelper.SetRumble(PlayerID, 160, 320, 0.6f, 10000);

            // The last argument (time) in SetRumble is optional. Call it with three arguments to turn it on without telling it when to turn off.
            // (Useful for dynamically changing rumble values.)
            // Then call SetRumble(0,0,0) when you want to turn it off.
        }

		//Rotating the turret
		gyro = InputHelper.GetGyro(PlayerID);
		if (Mathf.Floor(Mathf.Abs(gyro.y) * 90) != 0)
		{
			rotation += (gyro.y - offset) * (InputHelper.isLeft(PlayerID) ? 1 : -1);
		}
		else if(offsetcount < 200)
		{
			offset = (offset * offsetcount + gyro.y) / (offsetcount + 1);
			offsetcount++;
		}
		gameObject.transform.rotation = Quaternion.AngleAxis(rotation, Vector3.back);
	}



    float E3 = 329.628f;//164.814f;
    float G3 = 391.995f;//195.998f;
    float A3 = 440;//220f;
    float Am3 = 466.164f;//233.082f;
    float B3 = 493.883f;//246.942f;
    float C4 = 523.251f;//261.626f;
    float D4 = 587.330f;//293.665f;
    float E4 = 659.255f;//329.628f;
    float F4 = 698.456f;//349.228f;
    float G4 = 783.991f;//391.995f;
    float A4 = 880;//440;

    bool mario = false;
    IEnumerator PlayMario()
    {
        int speed = 60000/(128*4);
        InputHelper.SetRumble(PlayerID, E4, 320, 1f, speed); //E4
        yield return new WaitForSeconds(speed * 2 / 1000f);
        InputHelper.SetRumble(PlayerID, E4, 320, 1f, speed); //E4
        yield return new WaitForSeconds(speed * 2 / 1000f);
        InputHelper.SetRumble(PlayerID, E4, 320, 1f, speed); //E4
        yield return new WaitForSeconds(speed * 3 / 1000f);
        InputHelper.SetRumble(PlayerID, C4, 320, 1f, speed); //C4
        yield return new WaitForSeconds(speed * 2 / 1000f);
        InputHelper.SetRumble(PlayerID, E4, 320, 1f, speed); //E4
        yield return new WaitForSeconds(speed * 3 / 1000f);
        InputHelper.SetRumble(PlayerID, G4, 320, 1f, speed); //G4
        yield return new WaitForSeconds(speed * 6 / 1000f);
        InputHelper.SetRumble(PlayerID, G3, 320, 1f, speed); //G3
        yield return new WaitForSeconds(speed * 5 / 1000f);
        InputHelper.SetRumble(PlayerID, C4, 320, 1f, speed); //C4
        yield return new WaitForSeconds(speed * 4 / 1000f);
        InputHelper.SetRumble(PlayerID, G3, 320, 1f, speed); //G3
        yield return new WaitForSeconds(speed * 4 / 1000f);
        InputHelper.SetRumble(PlayerID, E3, 320, 1f, speed); //E3
        yield return new WaitForSeconds(speed * 4 / 1000f);
        InputHelper.SetRumble(PlayerID, A3, 320, 1f, speed); //A3
        yield return new WaitForSeconds(speed * 2 / 1000f);
        InputHelper.SetRumble(PlayerID, B3, 320, 1f, speed); //B3
        yield return new WaitForSeconds(speed * 3 / 1000f);
        InputHelper.SetRumble(PlayerID, Am3, 320, 1f, speed); //A#3
        yield return new WaitForSeconds(speed * 2 / 1000f);
        InputHelper.SetRumble(PlayerID, A3, 320, 1f, speed); //A3
        yield return new WaitForSeconds(speed * 3 / 1000f);
        InputHelper.SetRumble(PlayerID, G3, 320, 1f, speed); //G3
        yield return new WaitForSeconds(speed * 2 / 1000f);
        InputHelper.SetRumble(PlayerID, E4, 320, 1f, speed); //E4
        yield return new WaitForSeconds(speed * 3 / 1000f);
        InputHelper.SetRumble(PlayerID, G4, 320, 1f, speed); //G4
        yield return new WaitForSeconds(speed * 2 / 1000f);
        InputHelper.SetRumble(PlayerID, A4, 320, 1f, speed); //A4
        yield return new WaitForSeconds(speed * 3 / 1000f);
        InputHelper.SetRumble(PlayerID, F4, 320, 1f, speed); //F4
        yield return new WaitForSeconds(speed * 2 / 1000f);
        InputHelper.SetRumble(PlayerID, G4, 320, 1f, speed); //G4
        yield return new WaitForSeconds(speed * 3 / 1000f);
        InputHelper.SetRumble(PlayerID, E4, 320, 1f, speed); //E4
        yield return new WaitForSeconds(speed * 3 / 1000f);
        InputHelper.SetRumble(PlayerID, C4, 320, 1f, speed); //C4
        yield return new WaitForSeconds(speed * 2 / 1000f);
        InputHelper.SetRumble(PlayerID, D4, 320, 1f, speed); //D4
        yield return new WaitForSeconds(speed * 2 / 1000f);
        InputHelper.SetRumble(PlayerID, B3, 320, 1f, speed); //B3
    }
}