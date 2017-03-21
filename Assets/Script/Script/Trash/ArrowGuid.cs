using UnityEngine;
using System.Collections;

public class ArrowGuid : MonoBehaviour {

	
	
	// Update is called once per frame
	void Update () {
        transform.position = GameObject.Find("FlyingObject").GetComponent<Transform>().position;
	}
}
