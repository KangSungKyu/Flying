using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdGunAimEvent : LauncherParent {

    // Use this for initialization
    void Start ()
    {
        GetComponentsInChildren<ropeScript>()[0].EndTrans = GameObject.Find("TheAttachSpot").transform;
        GetComponentsInChildren<ropeScript>()[1].EndTrans = GameObject.Find("TheAttachSpot (1)").transform;
    }
	
	// Update is called once per frame
	void Update () {

    }

    override public void AimEvent()
    {
        base.AimEvent();

        Vector3 vecMouseWorld = myCam.ScreenToWorldPoint(Input.mousePosition);
        vecMouseWorld.z = 0.0f;

        float fLength = Vector3.Distance(OriginPos, vecMouseWorld);

        if (fLength > 3.0f)
        {
            return;
        }

        Vector3 vecDir = OriginPos - vecMouseWorld;
        vecDir = Vector3.Normalize(vecDir) * 3.0f;

        vecBlueDot = OriginPos + vecDir;

        vecBeforeDot = OriginPos - vecDir;

        Vector3 vecBlueDir = vecBlueDot - OriginPos;
        faimAngle = Mathf.Acos(Vector3.Dot(new Vector3(1, 0, 0), vecBlueDir.normalized)) * Mathf.Rad2Deg;
        float center = 90.0f;

        if (vecBlueDot.y < vecBeforeDot.y)
            faimAngle = -faimAngle;

        if (faimAngle < C_GAMEMANAGER.GetInstance().GetPlayer().GetPlayerStats().m_fMinDegree)
        {
            faimAngle = C_GAMEMANAGER.GetInstance().GetPlayer().GetPlayerStats().m_fMinDegree;
            vecBlueDot = rightPos;
            vecBeforeDot = OriginPos - new Vector3(center - C_GAMEMANAGER.GetInstance().GetPlayer().GetPlayerStats().m_fMinDegree,
                C_GAMEMANAGER.GetInstance().GetPlayer().GetPlayerStats().m_fMinDegree).normalized * 3.0f;
        }
        else if (faimAngle > C_GAMEMANAGER.GetInstance().GetPlayer().GetPlayerStats().m_fMaxDegree)
        {
            faimAngle = C_GAMEMANAGER.GetInstance().GetPlayer().GetPlayerStats().m_fMaxDegree;
            vecBlueDot = UpPos;
            vecBeforeDot = OriginPos - new Vector3(center - C_GAMEMANAGER.GetInstance().GetPlayer().GetPlayerStats().m_fMaxDegree,
                C_GAMEMANAGER.GetInstance().GetPlayer().GetPlayerStats().m_fMaxDegree).normalized * 3.0f;
        }
    }

    public override void EndAimEvent()
    {
        base.EndAimEvent();

        player.transform.position = OriginPos + (vecBeforeDot - OriginPos) * fLauchPower;
    }

    public override void LaunchEvent()
    {
        base.LaunchEvent();
    }
}
