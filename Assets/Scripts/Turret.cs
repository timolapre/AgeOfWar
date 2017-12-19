using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    SpriteRenderer SpriteRenderer;
    private Base BaseScript;
    private float rotation;
    private float offset;
    private int offsetcount;
    public int TurretLevel;
    float Cooldown = 2f;
    float Cooling = 0;
    
    public SpriteRenderer TurretBase;

    public int PlayerID;
	public GameObject projectile;
    public Vector3 gyro;

    // Use this for initialization
    void Start()
    {
        TurretLevel = 1;
        BaseScript = GetComponentInParent<Base>();
        rotation = 90 * (PlayerID == 1 ? -1 : 1);
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {   
        Cooling -= Time.deltaTime;

        if (BaseScript.VsAI && PlayerID > 0)
        {
            DoAI();
            return;
        }
		//Recenter the turret
		if (InputHelper.GetActionDown(PlayerID, Joycon.Button.STICK) && BaseScript.Playing)
        {
            rotation = 90 * (PlayerID == 1 ? -1 : 1);
        }

		if (Cooling <= 0 && InputHelper.GetAction(PlayerID, Joycon.Button.HOME) && BaseScript.Playing)
		{
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R) && BaseScript.GameOver)
        {
            Reset();
        }

        if (InputHelper.GetActionDown(PlayerID, Joycon.Button.SL))
        {
            UpgradeTurret();
        }

        //Play the Mario Theme
        if (InputHelper.GetActionDown(PlayerID, Joycon.Button.SHOULDER_1))
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
		if (Mathf.Floor(Mathf.Abs(gyro.y) * 90) != 0 && BaseScript.Playing)
		{
			rotation += (gyro.y - offset) * (InputHelper.IsLeft(PlayerID) ? 1 : -1);
		}
		else if(offsetcount < 200 && BaseScript.Playing)
		{
			offset = (offset * offsetcount + gyro.y) / (offsetcount + 1);
			offsetcount++;
		}
		gameObject.transform.rotation = Quaternion.AngleAxis(rotation, Vector3.back);
	}

    public void UpgradeTurret()
    {
        if(BaseScript.Money >= 0/*add cost here later*/ && BaseScript.WhatTier > TurretLevel && BaseScript.Playing)
        {
            TurretLevel++;
            SpriteRenderer.sprite = Resources.Load<Sprite>("Germany/Turrets/german_turret_barrel_" + TurretLevel);
            TurretBase.sprite = Resources.Load<Sprite>("Germany/Turrets/german_turret_body_" + TurretLevel);

            GetComponentsInChildren<Transform>()[1].localPosition = new Vector3(-1, 0);
            transform.localPosition = new Vector3(0, 0, .1f);
            //if(TurretLevel == 2)

        }
    }

    void Shoot()
    {
        GameObject proj = Instantiate(projectile, GetComponentsInChildren<Transform>()[1].position, new Quaternion(0, 0, 0, 0));
        proj.GetComponent<Projectile>().direction = (transform.rotation.eulerAngles.z + 90) % 360;
        proj.GetComponent<Projectile>().kills = PlayerID == 0 ? "Enemy" : "Player";
        proj.GetComponent<Projectile>().damage = 7 * TurretLevel;
        proj.transform.parent = transform.parent;
        Cooling = Cooldown;
    }

    public void Reset()
    {
        TurretLevel = 0;
    }

    void DoAI()
    {
        GameObject[] Players = GameObject.FindGameObjectsWithTag("Player");
        if (Players.Length <= 0)
            return;
        GameObject Closest = null;
        float Shortest = 0;
        foreach(GameObject Player in Players)
        {
            if(Closest == null)
            {
                Closest = Player;
                Shortest = Vector2.Distance(transform.position, Closest.transform.position);
                continue;
            }
            float dist = Vector2.Distance(transform.position, Player.transform.position);
            if (dist < Shortest)
            {
                Closest = Player;
                Shortest = dist;
            }
        }
        //rotation = Vector2.Angle(transform.position, Closest.transform.position) - 90;
        rotation = -Mathf.Atan((transform.position.y - Closest.transform.position.y) / (transform.position.x - Closest.transform.position.x)) / Mathf.PI * 180 + 15;
        gameObject.transform.rotation = Quaternion.AngleAxis(rotation, Vector3.back);

        if (Cooling <= 0)
            Shoot();
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
