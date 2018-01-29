using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    SpriteRenderer SpriteRenderer;
    private Base BaseScript;
    private float rotation, offset;
    private int offsetcount;
    public int TurretLevel;
    float Cooldown = 2f, Cooling = 0;
    SpriteRenderer TurretBase;
    AudioSource Audio;

    public int PlayerID;
	public GameObject projectile;
    public Vector3 gyro;

    public List<GameObject> ProjectileList;

    private int UpgradeTurretOnce;

    // Use this for initialization
    void Start()
    {
        Audio = GetComponent<AudioSource>();
        TurretLevel = 1;
        BaseScript = GetComponentInParent<Base>();        
        rotation = (PlayerID == 1 ? 0 : 180);
        SpriteRenderer = GetComponent<SpriteRenderer>();
        TurretBase = PlayerID == 0 ? GameObject.Find("Turret Body").GetComponent<SpriteRenderer>(): GameObject.Find("Turret Body2").GetComponent<SpriteRenderer>();        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerID == 0)
        {
            Audio.clip = Resources.Load("Sound/Gun/Tier " + TurretLevel + "/1", typeof(AudioClip)) as AudioClip;
        }
        else if(PlayerID == 1)
        {
            Audio.clip = Resources.Load("Sound/Gun/Tier " + TurretLevel + "/1", typeof(AudioClip)) as AudioClip;
        }
        Cooling -= Time.deltaTime;

        if (PlayerID == 0)
        {
            SpriteRenderer.sprite = Resources.Load<Sprite>(BaseScript.WhatFaction + "/Turrets/" + TurretLevel);
            TurretBase.sprite = Resources.Load<Sprite>(BaseScript.WhatFaction + "/Turrets/B" + TurretLevel);
        }
        if (PlayerID == 1)
        {
            SpriteRenderer.sprite = Resources.Load<Sprite>(BaseScript.WhatFactionEnemy + "/Turrets/" + TurretLevel);
            TurretBase.sprite = Resources.Load<Sprite>(BaseScript.WhatFactionEnemy + "/Turrets/B" + TurretLevel);
        }

        if (BaseScript.VsAI && PlayerID > 0)
        {
            DoAI();
            if (BaseScript.WhatTierEnemy > TurretLevel && UpgradeTurretOnce == 0)
            {
                Invoke("UpgradeTurretAI", Random.Range(13, 25));
                UpgradeTurretOnce = 1;
            }
        }
		//Recenter the turret
		if (InputHelper.GetActionDown(PlayerID, Joycon.Button.STICK) && BaseScript.Playing)
        {
            rotation = 180 - 180*PlayerID;
        }

		if (Cooling <= 0 && InputHelper.GetAction(PlayerID, Joycon.Button.HOME) && BaseScript.Playing)
		{
            Shoot();
            Audio.Play();
        }

        if (Input.GetKeyDown(KeyCode.R) && BaseScript.GameOver)
        {
            Reset();
        }

        if (InputHelper.GetActionDown(PlayerID, Joycon.Button.SL))
        {
            UpgradeTurret();
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

    void UpgradeTurretAI()
    {
        TurretLevel++;
        UpgradeTurretOnce = 0;
    }

    public void UpgradeTurret()
    {
        if(BaseScript.Playing && TurretLevel != 5)
        {
            if(PlayerID == 0 &BaseScript.WhatTier > TurretLevel && BaseScript.Money >= 20 * TurretLevel)
            {
                BaseScript.Money -= 20 * TurretLevel;
                TurretLevel++;


                GetComponentsInChildren<Transform>()[1].localPosition = new Vector3(-1, 0);
                transform.localPosition = new Vector3(0, 0, .1f);
            }
            else if(PlayerID == 1 & BaseScript.WhatTierEnemy > TurretLevel && BaseScript.eBase.Money >= 20 * TurretLevel)
            {
                BaseScript.eBase.Money -= 20 * TurretLevel;
                TurretLevel++;

                GetComponentsInChildren<Transform>()[1].localPosition = new Vector3(-1, 0);
                transform.localPosition = new Vector3(0, 0, .1f);
            }
        }
    }

    public bool CanUpgradeTurret()
    {
        if (BaseScript.Money >= 20 * TurretLevel && BaseScript.WhatTier > TurretLevel && TurretLevel != 5)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    void Shoot()
    {
        GameObject proj = Instantiate(projectile, GetComponentsInChildren<Transform>()[1].position, new Quaternion(0, 0, 0, 0));
        proj.GetComponent<Projectile>().direction = (transform.rotation.eulerAngles.z + 90) % 360;
        proj.GetComponent<Projectile>().kills = PlayerID == 0 ? "Enemy" : "Player";
        proj.GetComponent<Projectile>().damage = 2.5f * TurretLevel;
        proj.GetComponent<Projectile>().transform.localScale = new Vector3(TurretLevel * 0.75f, TurretLevel * 0.75f, 1);
        ProjectileList.Add(proj);
        proj.transform.parent = transform.parent;
        Cooling = Cooldown;
    }

    public void Reset()
    {
        TurretLevel = 1;
        SpriteRenderer.sprite = Resources.Load<Sprite>("Germany/Turrets/german_turret_barrel_" + TurretLevel);
        TurretBase.sprite = Resources.Load<Sprite>("Germany/Turrets/german_turret_body_" + TurretLevel);
        foreach (GameObject g in ProjectileList)
        {
            Destroy(g);
        }
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
        rotation = -Mathf.Atan((transform.position.y - Closest.transform.position.y) / (transform.position.x - Closest.transform.position.x)) / Mathf.PI * 180 + Shortest/2 + 10;
        gameObject.transform.rotation = Quaternion.AngleAxis(rotation, Vector3.back);
        if (Cooling <= 0 && Shortest < 13 && !BaseScript.GameOver && !BaseScript.Paused)
        {
            Shoot();
            Audio.Play();
        }
    }
}
