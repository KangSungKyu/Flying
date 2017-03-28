using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanAimEvent : LauncherParent {

    GameObject head;
    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update ()
    {
        if(head == null)
            head = GameObject.Find("Pan_Head");

        head.transform.Rotate(new Vector3(0.0f, -60.0f, 0.0f));
    }
    override public void AimEvent()
    {
        base.AimEvent();
    }

    public override void EndAimEvent()
    {
        base.EndAimEvent();
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
