using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherSelCtrl : MonoBehaviour {

    public string strLaunchName;
    bool bClick = false;
    // Use this for initialization
    void Start ()
    {
        SettingLauncherSprite();
    }
	
	// Update is called once per frame
	void Update () {

    }
    public void SettingLauncherSprite()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = C_GAMEMANAGER.GetInstance().GetSpriteMgr().GetSprite(strLaunchName + "_LauncherSel");

        for (int i = 0; i < C_GAMEMANAGER.GetInstance().GetPlayer().GetLauncherLevel(strLaunchName); ++i)
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
        if (!C_GAMEMANAGER.GetInstance().GetPlayer().GetIHaveLaunch(strLaunchName))
            return;

        C_GAMEMANAGER.GetInstance().strSelectedLaunchName = strLaunchName;
    }
}
