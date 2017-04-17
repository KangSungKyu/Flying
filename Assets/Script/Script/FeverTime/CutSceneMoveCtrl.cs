using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneMoveCtrl : MonoBehaviour {
    
    GameObject player;
    Vector3 loc;
	// Use this for initialization
	void Start ()
    {
        loc = this.transform.localPosition;
    }
	
	// Update is called once per frame
	void Update ()
    {
        this.transform.position = player.transform.position + loc;
    }
    public void Set()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        /*
        Vector3 par = player.transform.position;
        Vector3 [] usPath = new Vector3[2];

        par.z = 0.0f;

        this.transform.position = par + this.transform.localPosition;
        this.transform.localPosition = Vector3.zero;
        usPath[0] = par;
        usPath[1] = transform.position;
        
        iTween.MoveTo(this.gameObject, iTween.Hash("path", usPath, "speed", 5.0f));
        //*/
    }
    public void Reset()
    {  
    }
}
