using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdGunAimEvent : LauncherParent {

    public GameObject band;
    // Use this for initialization
    
    void Start ()
    {
        GetComponentsInChildren<ropeScript>()[0].EndTrans = GameObject.Find("TheAttachSpot (1)").transform;
        GetComponentsInChildren<ropeScript>()[1].EndTrans = GameObject.Find("TheAttachSpot").transform;
    }
	
	// Update is called once per frame
	void Update () {

    }

    override public void AimEvent()
    {
        base.AimEvent();
    }

    public override void EndAimEvent()
    {
        base.EndAimEvent();

        player.transform.position = OriginPos + (vecBeforeDot - OriginPos) * fLauchPower;
        band.transform.position = OriginPos + (vecBeforeDot - OriginPos) * fLauchPower;
    }

    public override void LaunchEvent()
    {
        base.LaunchEvent();
    }

    public override void BeforeLaunchEvent()
    {
        base.BeforeLaunchEvent();
    }
}
