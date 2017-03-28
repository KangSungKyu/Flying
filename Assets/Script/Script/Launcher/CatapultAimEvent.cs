using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultAimEvent : LauncherParent
{
    public GameObject hand;

	// Use this for initialization
	void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update () {
        if(hand == null)
            hand = GameObject.Find("CatapultHand");
    }

    override public void AimEvent()
    {
        base.AimEvent();
        
        hand.transform.rotation = Quaternion.Euler(0.0f, 0.0f, faimAngle);
    }

    public override void EndAimEvent()
    {
        base.EndAimEvent();
        
        player.transform.position = GameObject.Find("CataSpot").transform.position;
    }
    public override void LaunchEvent()
    {
        base.LaunchEvent();
    }

    public override void BeforeLaunchEvent()
    {
        base.BeforeLaunchEvent();

        iTween.RotateAdd(hand, new Vector3(0.0f, 0.0f, -120.0f), 1.0f);
    }
}
