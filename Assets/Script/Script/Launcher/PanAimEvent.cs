using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanAimEvent : LauncherParent {

    GameObject head;
    GameObject head2;
    GameObject spot;
    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (head == null)
        {
            head = GameObject.Find("Pan_Head");
            head2 = GameObject.Find("Pan_Head2");
            spot = GameObject.Find("PanSpot");
        }

        head.transform.Rotate(new Vector3(0.0f, -1080.0f*Time.deltaTime*(fLauchPower+1.0f), 0.0f));
        head2.transform.Rotate(new Vector3(0.0f, -1080.0f * Time.deltaTime * (fLauchPower+1.0f), 0.0f));
        player.transform.position = spot.transform.position;
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
