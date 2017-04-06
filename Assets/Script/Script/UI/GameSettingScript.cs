using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettingScript : MonoBehaviour {

    // Use this for initialization
    private void Awake()
    {
    }
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnApplicationQuit()
    {
        C_GAMEMANAGER.GetInstance().GetSaveLoadCtr().SaveXML();
    }

    private void OnApplicationPause(bool pause)
    {
        C_GAMEMANAGER.GetInstance().GetSaveLoadCtr().SaveXML();
    }
}
