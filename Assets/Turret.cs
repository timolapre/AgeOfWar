using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    private List<Joycon> joycons;
    private float rotation;
    private float offset;
    private int offsetcount;

    public int joyconID;
    public Vector3 gyro;

    // Use this for initialization
    void Start () {
        joycons = JoyconManager.Instance.j;
        rotation = 90 * (joyconID == 1 ? -1 : 1);
    }
	
	// Update is called once per frame
	void Update () {
        if (joycons.Count > joyconID)
        {
            Joycon j = joycons[joyconID];
            if (j.GetButtonDown(Joycon.Button.CAPTURE) || j.GetButtonDown(Joycon.Button.HOME))
            {
                rotation = 90 * (joyconID == 1 ? -1 : 1);
            }
            // GetButtonDown checks if a button has been pressed (not held)
            if (j.GetButtonDown(Joycon.Button.SHOULDER_2))
            {
                Debug.Log("Shoulder button 2 pressed");
                // GetStick returns a 2-element vector with x/y joystick components
                Debug.Log(string.Format("Stick x: {0:N} Stick y: {1:N}", j.GetStick()[0], j.GetStick()[1]));

                // Joycon has no magnetometer, so it cannot accurately determine its yaw value. Joycon.Recenter allows the user to reset the yaw value.
                //j.Recenter();
            }
            // GetButtonDown checks if a button has been released
            if (j.GetButtonUp(Joycon.Button.SHOULDER_2))
            {
                Debug.Log("Shoulder button 2 released");
            }
            // GetButtonDown checks if a button is currently down (pressed or held)
            if (j.GetButton(Joycon.Button.SHOULDER_2))
            {
                Debug.Log("Shoulder button 2 held");
            }

            if (j.GetButtonDown(Joycon.Button.DPAD_DOWN))
            {
                Debug.Log("Rumble");

                // Rumble for 200 milliseconds, with low frequency rumble at 160 Hz and high frequency rumble at 320 Hz. For more information check:
                // https://github.com/dekuNukem/Nintendo_Switch_Reverse_Engineering/blob/master/rumble_data_table.md

                j.SetRumble(160, 320, 0.6f, 200);

                // The last argument (time) in SetRumble is optional. Call it with three arguments to turn it on without telling it when to turn off.
                // (Useful for dynamically changing rumble values.)
                // Then call SetRumble(0,0,0) when you want to turn it off.
            }

            if (true || offsetcount <= 20)
            {
                
            }
            
            gyro = j.GetGyro();
            if (Mathf.Floor(Mathf.Abs(gyro.y) * 90) != 0)
            {
                rotation += (gyro.y - offset) * (j.isLeft ? 1 : -1);
            }
            else
            {
                offset = (offset * offsetcount + joycons[joyconID].GetGyro().y) / (offsetcount + 1);
                offsetcount++;
            }
            gameObject.transform.rotation = Quaternion.AngleAxis(rotation, Vector3.back);
        }
    }
}
