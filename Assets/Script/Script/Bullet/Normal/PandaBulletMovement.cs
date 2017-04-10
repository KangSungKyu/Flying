using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaBulletMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(360.0f * Time.deltaTime, 0.0f, 0.0f));
	}
}
