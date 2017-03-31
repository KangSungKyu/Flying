using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideCharSelView : MonoBehaviour {

    GameObject canv;

	// Use this for initialization
	void Start () {
        canv = GameObject.Find("CharCanv");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        canv.GetComponent<SettCharSheet>().BeginDragView(Input.mousePosition);
    }

    private void OnMouseUp()
    {
        canv.GetComponent<SettCharSheet>().EndDragView(Input.mousePosition);
    }
    
}
