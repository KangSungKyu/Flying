using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideLauncherSelView : MonoBehaviour {

    GameObject canv;

	// Use this for initialization
	void Start () {
        canv = GameObject.Find("LaunchCanv");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        canv.GetComponent<LaunchSelectScene>().BeginDragView(Input.mousePosition);
    }

    private void OnMouseUp()
    {
        canv.GetComponent<LaunchSelectScene>().EndDragView(Input.mousePosition);
    }
    
}
