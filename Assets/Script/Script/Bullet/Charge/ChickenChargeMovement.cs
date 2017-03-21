using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenChargeMovement : MonoBehaviour {

    // Use this for initialization
	void Start ()
    {
        this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
	}
	
	// Update is called once per frame
	void Update () {
    }

    public void Movement()
    {

        Vector3[] path = iTweenPath.GetPath("ChickCharge");
        for (int i = 0; i < path.Length; ++i)
        {
            path[i] = Quaternion.Euler(0.0f, 0.0f, this.gameObject.transform.eulerAngles.z) * path[i];
            path[i] += this.gameObject.transform.position;
        }

        iTween.MoveTo(this.gameObject, iTween.Hash("path", path, "speed", 0.85f));
    }
}
