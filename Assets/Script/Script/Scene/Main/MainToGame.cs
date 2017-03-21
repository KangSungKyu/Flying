using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainToGame : MonoBehaviour {

    private void Awake()
    {
        C_GAMEMANAGER.GetInstance().InitMgr();
        C_GAMEMANAGER.GetInstance().GetDataMgr().InitMgr();
    }
    public void ButtonEvent()
    {
        C_GAMEMANAGER.GetInstance().ChangeScene("Test");
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
