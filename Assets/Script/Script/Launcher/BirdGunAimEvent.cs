using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdGunAimEvent : LauncherParent {

    public GameObject band;
    GameObject spot;
    GameObject l1, l2, e1, e2;
    // Use this for initialization
    
    void Start ()
    {
        GetComponentsInChildren<ropeScript>()[0].EndTrans = GameObject.Find("TheAttachSpot (1)").transform;
        GetComponentsInChildren<ropeScript>()[1].EndTrans = GameObject.Find("TheAttachSpot").transform;
    }
	
	// Update is called once per frame
	void Update () {
        if(spot == null)
        {
            spot = GameObject.Find("BandSpot");
            l1 = GameObject.Find("BandLine1");
            l2 = GameObject.Find("BandLine2");
            e1 = GameObject.Find("End1");
            e2 = GameObject.Find("End2");
        }
    }

    override public void AimEvent()
    {
        base.AimEvent();
    }

    public override void EndAimEvent()
    {
        base.EndAimEvent();

        band.transform.position = OriginPos + (vecBeforeDot - OriginPos) * fLauchPower;

        if (spot != null)
        {
            Vector2 v1 = l1.transform.position - e1.transform.position;
            Vector2 v2 = l2.transform.position - e2.transform.position;
            float d1 = Vector2.Distance(l1.transform.position,e1.transform.position);
            float d2 = Vector2.Distance(l2.transform.position, e2.transform.position);
            float a1 = 0.0f;
            float a2 = 0.0f;

            v1.Normalize();
            v2.Normalize();

            if (l1.transform.position.y < e1.transform.position.y)
                a1 = 180.0f - Mathf.Acos(Vector2.Dot(Vector2.right, v1)) * Mathf.Rad2Deg;
            else
                a1 = Mathf.Acos(Vector2.Dot(Vector2.right, v1)) * Mathf.Rad2Deg;

            if (l2.transform.position.y < e2.transform.position.y)
                a2 = 180.0f - Mathf.Acos(Vector2.Dot(Vector2.right, v2)) * Mathf.Rad2Deg;
            else
                a2 = Mathf.Acos(Vector2.Dot(Vector2.right, v2)) * Mathf.Rad2Deg;

            l1.transform.rotation = Quaternion.Euler(0.0f, 0.0f, a1);
            l2.transform.rotation = Quaternion.Euler(0.0f, 0.0f, a2);
            l1.transform.localScale = new Vector3(d1/8.0f, 1.0f, 1.0f);
            l2.transform.localScale = new Vector3(d2/8.0f, 1.0f, 1.0f);

            GameObject.FindGameObjectWithTag("Player").transform.position = spot.transform.position;
        }
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
