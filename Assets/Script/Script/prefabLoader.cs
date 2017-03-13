using UnityEngine;
using System.Collections;

public class prefabLoader : MonoBehaviour {
    public GameObject test;
	// Use this for initialization
	IEnumerator Start () {
        C_GAMEMANAGER.GetInstance().InitMgr();
        yield return new WaitForSeconds(1f);

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
