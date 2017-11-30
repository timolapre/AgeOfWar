using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    private List<Joycon> joycons;
    private float rotation;
    private float offset;
    private int offsetcount;

    public int PlayerID;
    public Vector3 gyro;

    // Use this for initialization
    void Start()
    {
        joycons = JoyconManager.Instance.j;
        rotation = 90 * (PlayerID == 1 ? -1 : 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (InputHelper.GetActionDown(PlayerID, Joycon.Button.HOME))
        {
            rotation = 90 * (PlayerID == 1 ? -1 : 1);
        }
        // GetButtonDown checks if a button has been pressed (not held)
        if (InputHelper.GetActionDown(PlayerID, Joycon.Button.DPAD_DOWN))
        {
            Debug.Log("Rumble");

            // Rumble for 200 milliseconds, with low frequency rumble at 160 Hz and high frequency rumble at 320 Hz. For more information check:
            // https://github.com/dekuNukem/Nintendo_Switch_Reverse_Engineering/blob/master/rumble_data_table.md

            InputHelper.SetRumble(PlayerID, 160, 320, 0.6f, 10000);

            // The last argument (time) in SetRumble is optional. Call it with three arguments to turn it on without telling it when to turn off.
            // (Useful for dynamically changing rumble values.)
            // Then call SetRumble(0,0,0) when you want to turn it off.
        }

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
}
