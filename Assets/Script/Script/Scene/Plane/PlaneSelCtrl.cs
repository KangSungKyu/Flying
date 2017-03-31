using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSelCtrl : MonoBehaviour {

    public string strPlaneName;
    bool bClick = false;
    // Use this for initialization
    void Start () {
        SettingPlaneSprite();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void SettingPlaneSprite()
    {
        Debug.Log(strPlaneName);
        GetComponentInChildren<SpriteRenderer>().sprite = C_GAMEMANAGER.GetInstance().GetSpriteMgr().GetSprite(strPlaneName + "_PlaneSel");
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
            return;

        C_GAMEMANAGER.GetInstance().strSelectedPlaneName = strPlaneName;
    }
}
