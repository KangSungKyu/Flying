using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoAimEvent : LauncherParent {

    public GameObject leg;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(leg == null)
            leg = GameObject.Find("Dino_Leg");
    }

    public override void AimEvent()
    {
        base.AimEvent();
    }
    public override void EndAimEvent()
    {
        base.EndAimEvent();

        if(leg != null)
            iTween.ScaleAdd(leg, new Vector3(0.25f, 0.25f, 1.0f), 2.0f);
    }

    public override void LaunchEvent()
    {
        base.LaunchEvent();
    }

    public override void BeforeLaunchEvent()
    {
        base.BeforeLaunchEvent();

        iTween.ScaleAdd(leg, new Vector3(3.25f, 6.25f, 1.0f), 1.5f);
        iTween.PunchRotation(leg, new Vector3(0.0f, 0.0f, 250.0f), 2.0f);
    }
}
