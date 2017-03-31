using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidePlaneSelView : MonoBehaviour {

    GameObject canv;

	// Use this for initialization
	void Start () {
        canv = GameObject.Find("PlaneCanv");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        canv.GetComponent<PlaneSelectScene>().BeginDragView(Input.mousePosition);
    }

    private void OnMouseUp()
    {
        canv.GetComponent<PlaneSelectScene>().EndDragView(Input.mousePosition);
    }
    
}
