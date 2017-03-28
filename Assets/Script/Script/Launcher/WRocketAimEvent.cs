using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WRocketAimEvent : LauncherParent {
    public GameObject pan;
    public GameObject ship;
    public GameObject water;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (pan == null)
            pan = GameObject.Find("WRocket_Pan");
        if (ship == null)
            ship = GameObject.Find("WRocket_Ship");
        if(water == null)
            water = GameObject.Find("WR_Water");
        
	}

    public override void AimEvent()
    {
        base.AimEvent();

        pan.transform.rotation = Quaternion.Euler(0.0f, 0.0f, faimAngle-90.0f);
        ship.transform.rotation = Quaternion.Euler(0.0f, 0.0f, faimAngle-90.0f);

        water.GetComponent<ParticleSystem>().Stop();
    }

    public override void EndAimEvent()
    {
        base.EndAimEvent();

        player.transform.position = GameObject.Find("WRSpot").transform.position;
    }

    public override void LaunchEvent()
    {
        base.LaunchEvent();

        Destroy(ship);
    }

    public override void BeforeLaunchEvent()
    {
        base.BeforeLaunchEvent();

        water.GetComponent<ParticleSystem>().Play();
        ship.transform.SetParent(player.transform);
    }
}
