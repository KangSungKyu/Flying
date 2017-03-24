using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainToGame : MonoBehaviour {

    public Text youdontSel;

    private void Awake()
    {
        C_GAMEMANAGER.GetInstance().InitMgr();
        C_GAMEMANAGER.GetInstance().GetDataMgr().InitMgr();
    }
    public void ButtonEvent()
    {
        string strC = C_GAMEMANAGER.GetInstance().strSelectedCharName;
        string strP = C_GAMEMANAGER.GetInstance().strSelectedPlaneName;
        string strL = C_GAMEMANAGER.GetInstance().strSelectedLaunchName;

        if(strC != "" && strP != "" && strL != "")
            C_GAMEMANAGER.GetInstance().ChangeScene("Test");
        else
        {
            youdontSel.enabled = true;

            StartCoroutine(CloseYDSel());
        }
    }
    IEnumerator CloseYDSel()
    {
        yield return new WaitForSeconds(3.0f);

        youdontSel.enabled = false;
    }
    public void GoCharc()
    {
        C_GAMEMANAGER.GetInstance().ChangeScene("Charc");
    }
    public void GoRoulette()
    {
        C_GAMEMANAGER.GetInstance().ChangeScene("Roulette");
    }
}
