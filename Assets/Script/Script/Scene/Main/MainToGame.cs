using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class MainToGame : MonoBehaviour {

    public Text youdontSel;
    public Text playcountNotEnough;
    public Text Play;
    public Text Coin;
    public GameObject adbtn;

    ShowOptions m_cShowOpts = new ShowOptions();
    ShowResult m_eShowRet = ShowResult.Failed;
    int m_nAdCount = 0;


    private void Awake()
    {
        C_GAMEMANAGER.GetInstance().InitMgr();
        C_GAMEMANAGER.GetInstance().GetDataMgr().InitMgr();
        C_GAMEMANAGER.GetInstance().GetSaveLoadCtr().LoadXML();

        Advertisement.Initialize("1399728", true);
        m_cShowOpts.resultCallback = OnAdFunc;
    }
    private void Update()
    {
        Play.text = C_GAMEMANAGER.GetInstance().GetPlayCount().ToString() + " / " + C_GAMEMANAGER.GetInstance().GetMaxPlayCount().ToString();
        Coin.text = C_GAMEMANAGER.GetInstance().GetCoin().ToString();

        //if (playcountNotEnough.enabled)
        {
            adbtn.SetActive(playcountNotEnough.enabled && Advertisement.IsReady("rewardedVideo"));
            //adbtn.SetActive(playcountNotEnough.enabled && Advertisement.IsReady("video"));
        }
    }
    public void OnAdBtn()
    {
        m_nAdCount++;
        Advertisement.Show("rewardedVideo", m_cShowOpts);
        //Advertisement.Show("video", m_cShowOpts);
    }
    public void ButtonEvent()
    {
        //C_GAMEMANAGER.GetInstance().strSelectedCharName = "치킨";
        //C_GAMEMANAGER.GetInstance().strSelectedPlaneName = "종이비행기";
        //C_GAMEMANAGER.GetInstance().strSelectedLaunchName = "공룡";

        string strC = C_GAMEMANAGER.GetInstance().strSelectedCharName;
        string strP = C_GAMEMANAGER.GetInstance().strSelectedPlaneName;
        string strL = C_GAMEMANAGER.GetInstance().strSelectedLaunchName;

        Debug.Log(strC + " " + strP + " " + strL);
        if (strC != "" && strP != "" && strL != "")
        {
            int p = C_GAMEMANAGER.GetInstance().GetPlayCount();
            
            if (p > 0)
            {
                C_GAMEMANAGER.GetInstance().ChangeScene("Test");
                C_GAMEMANAGER.GetInstance().SetPlayCount(p - 1);
            }
            else
            {
                //플레이카운트 채워줘양!
                playcountNotEnough.enabled = true;

                StartCoroutine(ClosePlayCount());
            }
        }
        //*
        else
        {
            youdontSel.enabled = true;

            StartCoroutine(CloseYDSel()); 
        }
        //*/
    }
    IEnumerator CloseYDSel()
    {
        yield return new WaitForSeconds(3.0f);

        youdontSel.enabled = false;
    }
    IEnumerator ClosePlayCount()
    {
        yield return new WaitForSeconds(3.0f);

        playcountNotEnough.enabled = false;
    }
    public void GoCharc()
    {
        C_GAMEMANAGER.GetInstance().ChangeScene("Charc");
    }
    public void GoRoulette()
    {
        C_GAMEMANAGER.GetInstance().ChangeScene("Roulette");
    }

    public void GoPlane()
    {
        C_GAMEMANAGER.GetInstance().ChangeScene("Plane");
    }

    public void GoLaunch()
    {
        C_GAMEMANAGER.GetInstance().ChangeScene("Launch");
    }

    private void OnApplicationQuit()
    {
        C_GAMEMANAGER.GetInstance().GetSaveLoadCtr().SaveXML();
    }
    private void OnApplicationPause(bool pause)
    {
        C_GAMEMANAGER.GetInstance().GetSaveLoadCtr().SaveXML();
    }

    public void OnAdFunc(ShowResult _result)
    {
        m_eShowRet = _result;

        if(_result == ShowResult.Finished)
        {
            int p = C_GAMEMANAGER.GetInstance().GetPlayCount();
            C_GAMEMANAGER.GetInstance().SetPlayCount(p+1);

            if(adbtn != null)
                adbtn.SetActive(false);
        }
    }
}
