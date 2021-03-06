﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaneSelCtrl : MonoBehaviour {

    public string strPlaneName;
    public Text peace;
    bool bClick = false;
    // Use this for initialization
    void Start () {
        SettingPlaneSprite();
    }
	
	// Update is called once per frame
	void Update () {

        if (C_GAMEMANAGER.GetInstance().GetPlayer().GetIHaveLaunch(strPlaneName))
        {
            Color c = new Color(1.0f, 1.0f, 1.0f, 1.0f);

            this.gameObject.GetComponentInChildren<SpriteRenderer>().color = c;
        }

        int id = C_GAMEMANAGER.GetInstance().GetPlanePeaceMeter().GetIDFromCharName(strPlaneName);

        peace.text = C_GAMEMANAGER.GetInstance().GetPlanePeaceMeter().GetPeace(id) + " / " + C_GAMEMANAGER.GetInstance().GetPlanePeaceMeter().GetMaxPeace(id);

        for (int i = 0; i < C_GAMEMANAGER.GetInstance().GetPlayer().GetPlaneLevel(strPlaneName); ++i)
            GetComponentsInChildren<SpriteRenderer>()[1 + i].enabled = true;
    }
    public void SettingPlaneSprite()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = C_GAMEMANAGER.GetInstance().GetSpriteMgr().GetSprite(strPlaneName + "_PlaneSel");

        for (int i = 0; i < C_GAMEMANAGER.GetInstance().GetPlayer().GetPlaneLevel(strPlaneName); ++i)
            GetComponentsInChildren<SpriteRenderer>()[1+i].enabled = true;
    }

    private void OnMouseDown()
    {
        if (!bClick)
            bClick = true;
    }
    private void OnMouseUp()
    {
        if (bClick)
        {
            ItsMe();

            bClick = false;
        }
    }

    public void ItsMe()
    {
        if (!C_GAMEMANAGER.GetInstance().GetPlayer().GetIHavePlane(strPlaneName))
        {
            Debug.Log("I don't have it!");
            return;
        }

        C_GAMEMANAGER.GetInstance().strSelectedPlaneName = strPlaneName;

        Debug.Log(strPlaneName);
    }
}
