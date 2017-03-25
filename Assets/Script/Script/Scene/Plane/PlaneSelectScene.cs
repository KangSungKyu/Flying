using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSelectScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BackMain()
    {
        C_GAMEMANAGER.GetInstance().ChangeScene("Main");
    }
}
