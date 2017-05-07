using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GorillaAimEvent : LauncherParent {

    public GameObject arm;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (arm == null)
            arm = GameObject.Find("Gorilla_Arm");
	}

    public override void AimEvent()
    {
        base.AimEvent();

        arm.transform.rotation = Quaternion.Euler(0.0f, 0.0f, faimAngle- 45.0f);
    }

    public override void EndAimEvent()
    {
        base.EndAimEvent();
        GameObject.FindGameObjectWithTag("Player").transform.position = GameObject.Find("GorillaSpot").transform.position;
    }

    public override void LaunchEvent()
    {
        base.LaunchEvent();
    }

    public override void BeforeLaunchEvent()
    {
        base.BeforeLaunchEvent();

        iTween.PunchRotation(arm, new Vector3(0.0f, 0.0f, -180.0f), 1.0f);
    }
}
