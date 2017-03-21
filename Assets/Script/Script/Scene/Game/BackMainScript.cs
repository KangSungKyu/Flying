using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackMainScript : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
        this.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void BackMain()
    {
        C_GAMEMANAGER.GetInstance().ChangeScene("Main");
    }
}
