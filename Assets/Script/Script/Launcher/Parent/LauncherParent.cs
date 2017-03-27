using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class LauncherParent : MonoBehaviour
{
    public string strLaunch;

    protected GameObject player;
    protected float faimAngle;
    protected float fLauchPower;
    protected Vector3 vecAim;
    protected Vector3 vecBeforeDot;
    protected Vector3 vecBlueDot;
    protected Vector3 OriginPos;
    protected Camera myCam;
    protected Vector3 rightPos;
    protected Vector3 UpPos;

    private void Start()
    {
    }
    private void Update()
    {

    }

    virtual public void AimEvent()
    {
        float fMaxDegree = C_GAMEMANAGER.GetInstance().GetPlayer().GetPlayerStats().m_fMaxDegree;
        float fMinDegree = C_GAMEMANAGER.GetInstance().GetPlayer().GetPlayerStats().m_fMinDegree;

        float center = 90.0f;
        
        rightPos = Quaternion.Euler(0.0f, 0.0f, fMinDegree) * Vector2.right * 3.0f + OriginPos;
        UpPos = Quaternion.Euler(0.0f, 0.0f, fMaxDegree) * Vector2.right * 3.0f + OriginPos;
        myCam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    virtual public void EndAimEvent()
    {

    }

    virtual public void LaunchEvent()
    {

    }

    public void SetPlayer(GameObject _player)
    {
        player = _player;
    }

    public void SetOriginPos(Vector3 _pos)
    {
        OriginPos = _pos;
    }
    public Vector3 GetOriginPos()
    {
        return OriginPos;
    }

    public Vector3 GetAim()
    {
        return vecAim;
    }
    public Vector3 GetBeforeAim()
    {
        return vecBeforeDot;
    }
    public Vector3 GetBlueDot()
    {
        return vecBlueDot;
    }
    public float GetAimAngle()
    {
        return faimAngle;
    }
    public void SetLaunchPower(float _p)
    {
        fLauchPower = _p;
    }
    public float GetLaunchPower()
    {
        return fLauchPower;
    }
}

