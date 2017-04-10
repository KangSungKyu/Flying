﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class FlyScript : MonoBehaviour {

    Rigidbody2D myBody;
    C_ObjectPool bulls;
    C_ObjectPool chars; //충전탄들

    bool bBACheck;
    public bool bShoting = false;

    float fChargeTimer;
    float fVerticalPower;
    float fHorizontalPower;
    string strPrevEat;

    Vector2 vBCtrl;

    public float fGravityScore;
    public float fToqueCtrlDeg;

    Vector3 vecPrevPos = Vector3.zero;

    public GameObject gobjHand;

    // Use this for initialization
    void Start()
    {
    }

    public void Init()
    {
        bulls = new C_ObjectPool(C_GAMEMANAGER.GetInstance().GetPlayer().GetBulletCount(), C_GAMEMANAGER.GetInstance().GetObjectMgr().GetObject(C_GAMEMANAGER.GetInstance().strSelectedCharName + "_Bullet"));
        chars = new C_ObjectPool(C_GAMEMANAGER.GetInstance().GetPlayer().GetChargeBulletCount(), C_GAMEMANAGER.GetInstance().GetObjectMgr().GetObject(C_GAMEMANAGER.GetInstance().strSelectedCharName + "_Bullet_Charge"));

        GetComponent<BoxCollider2D>().size = new Vector2(C_GAMEMANAGER.GetInstance().GetPlayer().GetPlayerStats().m_fColliderScale,
            C_GAMEMANAGER.GetInstance().GetPlayer().GetPlayerStats().m_fColliderScale);
    }
	public void BeginChargeBullet()
    {
        fChargeTimer = 0.0f;

        //gobjHand.GetComponent<Animator>().SetBool("Hand_ShotIdle",true);
        //gobjHand.GetComponent<Animator>().SetBool("Hand_IdleShot", false);
    }

    public void UpdateChargeBullet()
    {
        if(fChargeTimer < C_GAMEMANAGER.GetInstance().GetPlayer().GetPlayerStats().m_fChargeBulletTime)
        {
            fChargeTimer += Time.deltaTime;
            if(!bShoting)
                gobjHand.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);
        }
    }
    
    public void EndChargeBullet()
    {
        //gobjHand.GetComponent<Animator>().SetBool("Hand_IdleShot", true);
        //gobjHand.GetComponent<Animator>().SetBool("Hand_ShotIdle", false);
        iTween.PunchRotation(gobjHand, new Vector3(0.0f,0.0f,-120.0f),1.5f);
        bShoting = true;
    }

    public void Shot()
    {
        if (fChargeTimer < C_GAMEMANAGER.GetInstance().GetPlayer().GetPlayerStats().m_fChargeBulletTime)
        {
            ShotEvent();
        }
        else
        {
            ChargeShotEvent();
        }

        fChargeTimer = 0.0f;
        bShoting = false;
        gobjHand.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
       if(col.tag == "Launcher")
        {
            
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Plat")
        {
            float fSpeed = C_GAMEMANAGER.GetInstance().GetPlayer().GetCurrentSpeed();
            float fFriction = fSpeed * 0.4f;
            fSpeed = fSpeed - fFriction;

            C_GAMEMANAGER.GetInstance().GetPlayer().SetCurrentSpeed(fSpeed);
        }
    }

    public void ShotEvent()
    {
        if (C_GAMEMANAGER.GetInstance().GetPlayer().GetCurrentBulletCount() > 0)
        {
            GameObject BulletInstance = bulls.Alloc();

            if (BulletInstance == null)
            {
                bulls.AllFree();

                BulletInstance = bulls.Alloc();

                if (BulletInstance == null)
                    return;
            }

            BulletInstance.GetComponent<Transform>().position = transform.FindChild("ArrowSpot").position;
            float fDegree = C_GAMEMANAGER.GetInstance().GetPlayer().GetPlayerStats().m_fBulletDegree;
            Vector3 dir = C_GAMEMANAGER.GetInstance().GetPlayer().GetRealForwardDir();
            float fBulletShotAng = 0.0f;

            Vector2 jdir = C_GAMEMANAGER.GetInstance().GetJoyStickDir();

            if (jdir.y >= 0.0f)
                fBulletShotAng = Mathf.Acos(Vector2.Dot(jdir,Vector2.right)) * Mathf.Rad2Deg;
            else
                fBulletShotAng = 360.0f - Mathf.Acos(Vector2.Dot(jdir,Vector2.right)) * Mathf.Rad2Deg;
            
            dir = Quaternion.Euler(0.0f, 0.0f, fBulletShotAng + fDegree) * dir;
            Vector3 vAdd = new Vector3(C_GAMEMANAGER.GetInstance().GetPlayer().GetCurrentSpeed(), C_GAMEMANAGER.GetInstance().GetPlayer().GetVerticalSpeed(),0.0f);
            
            BulletInstance.transform.rotation = Quaternion.Euler(0.0f, 0.0f, transform.eulerAngles.z + fBulletShotAng + fDegree);
            BulletInstance.GetComponent<Rigidbody2D>().velocity = (dir.normalized * C_GAMEMANAGER.GetInstance().GetPlayer().GetPlayerStats().m_fBulletSpeed)+vAdd;

            int b = C_GAMEMANAGER.GetInstance().GetPlayer().GetCurrentBulletCount();

            C_GAMEMANAGER.GetInstance().GetPlayer().SetCurrentBulletCount(b - 1);
        }
	}

    public void ChargeShotEvent()
    {
        if (C_GAMEMANAGER.GetInstance().GetPlayer().GetCurrentChargeBulletCount() > 0)
        {
            GameObject ChargeInst = chars.Alloc();

            if (ChargeInst == null)
            {
                chars.AllFree();

                ChargeInst = chars.Alloc();

                if (ChargeInst == null)
                    return;
            }

            ChargeInst.GetComponent<Transform>().position = transform.FindChild("ArrowSpot").position;
            float fDegree = C_GAMEMANAGER.GetInstance().GetPlayer().GetPlayerStats().m_fChargeBulletDegree;
            Vector3 dir = C_GAMEMANAGER.GetInstance().GetPlayer().GetRealForwardDir();
            float fBulletShotAng = 0.0f;

            Vector2 jdir = C_GAMEMANAGER.GetInstance().GetJoyStickDir();

            if (jdir.y >= 0.0f)
                fBulletShotAng = Mathf.Acos(Vector2.Dot(jdir,Vector2.right)) * Mathf.Rad2Deg;
            else
                fBulletShotAng = 360.0f - Mathf.Acos(Vector2.Dot(jdir,Vector2.right)) * Mathf.Rad2Deg;

            dir = Quaternion.Euler(0.0f, 0.0f, fBulletShotAng + fDegree) * dir;
            Vector3 vAdd = new Vector3(C_GAMEMANAGER.GetInstance().GetPlayer().GetCurrentSpeed(), C_GAMEMANAGER.GetInstance().GetPlayer().GetVerticalSpeed(), 0.0f);

            ChargeInst.transform.rotation = Quaternion.Euler(0.0f, 0.0f, transform.eulerAngles.z + fBulletShotAng + fDegree);
            ChargeInst.GetComponent<Rigidbody2D>().velocity = dir.normalized * C_GAMEMANAGER.GetInstance().GetPlayer().GetPlayerStats().m_fChargeBulletSpeed+vAdd;

            if (ChargeInst.name.Equals("Chicken_Bullet_Charge(Clone)"))
                ChargeInst.GetComponent<ChickenChargeMovement>().Movement();

            int ccb = C_GAMEMANAGER.GetInstance().GetPlayer().GetCurrentChargeBulletCount();

            C_GAMEMANAGER.GetInstance().GetPlayer().SetCurrentChargeBulletCount(ccb - 1);
        }
    }

    void Update()
    {
        if (C_GAMEMANAGER.GetInstance().GetPlayer().GetState() == E_PLAYERSTATE.E_PLAYERRELEASE ||
            C_GAMEMANAGER.GetInstance().GetPlayer().GetState() == E_PLAYERSTATE.E_PLAYERFEVERTIME)
        {
            Vector3 vMove = new Vector3(C_GAMEMANAGER.GetInstance().GetPlayer().GetCurrentSpeed(), C_GAMEMANAGER.GetInstance().GetPlayer().GetVerticalSpeed(), 0.0f);

            transform.position += vMove*Time.deltaTime;
            vecPrevPos = C_GAMEMANAGER.GetInstance().GetPlayer().GetRealPos();

            C_GAMEMANAGER.GetInstance().GetPlayer().SetRealPos(transform.position);
        }

        if(C_GAMEMANAGER.GetInstance().GetPlayer().GetRealPos().y < -C_GAMEMANAGER.GetInstance().GetCMHeight())
        {
            C_GAMEMANAGER.GetInstance().GetPlayer().SetState(E_PLAYERSTATE.E_PLAYERDIE);
        }

        if (strPrevEat != C_GAMEMANAGER.GetInstance().GetPlayer().GetCurrentEat())
        {
            strPrevEat = C_GAMEMANAGER.GetInstance().GetPlayer().GetCurrentEat();
        }
    }
    
    
    void FixedUpdate()
    {
        //player forward vector setting
        if (C_GAMEMANAGER.GetInstance().GetPlayer().GetState() != E_PLAYERSTATE.E_PLAYERDIE)
        {
            Vector2 pos = C_GAMEMANAGER.GetInstance().GetPlayer().GetRealPos() - vecPrevPos;
            Vector2 ax = Vector2.right;

            pos.Normalize();

            float rad = Mathf.Acos(Vector2.Dot(ax, pos));
            float tog = Mathf.Rad2Deg * rad;

            if (pos.y < 0.0f)
                tog = -tog;

            if (C_GAMEMANAGER.GetInstance().GetPlayer().GetState() == E_PLAYERSTATE.E_PLAYERRELEASE ||
            C_GAMEMANAGER.GetInstance().GetPlayer().GetState() == E_PLAYERSTATE.E_PLAYERFEVERTIME)
            {
                transform.rotation = Quaternion.Euler(0.0f, 0.0f, tog + fToqueCtrlDeg);
                C_GAMEMANAGER.GetInstance().SetCMHeight(1.0f);
            }

            C_GAMEMANAGER.GetInstance().GetPlayer().SetRealForwardDir(pos);

            float deg = Mathf.Rad2Deg * Mathf.Acos(Vector2.Dot(Vector2.right, pos));

            if (tog < -10.0f &&
                C_GAMEMANAGER.GetInstance().GetPlayer().GetRealPos().y < 100.0f)
            {
                float fSpeed = C_GAMEMANAGER.GetInstance().GetPlayer().GetCurrentSpeed();
                float fVSpeed = C_GAMEMANAGER.GetInstance().GetPlayer().GetVerticalSpeed();

                C_GAMEMANAGER.GetInstance().GetPlayer().SetCurrentSpeed(fSpeed - fSpeed * 0.45f * Time.deltaTime);
                C_GAMEMANAGER.GetInstance().GetPlayer().SetVerticalSpeed(fVSpeed - fVSpeed * 0.45f * Time.deltaTime);
            }
        }
    }
    
}
