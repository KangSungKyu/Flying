using System;
using System.Collections.Generic;
using UnityEngine;

public class LauncherCost : MonoBehaviour
{
    private void Start()
    {
    }
    private void Update()
    {

    }
    public int GetCost(string _who)
    {
        string[] arr = C_GAMEMANAGER.GetInstance().GetDataMgr().GetStorage("LauncherCost").GetFilter();

        string strData = C_GAMEMANAGER.GetInstance().GetDataMgr().GetData("LauncherCost", _who, arr[1]);
        int cost = Int32.Parse(strData);

        return cost;
    }
}

