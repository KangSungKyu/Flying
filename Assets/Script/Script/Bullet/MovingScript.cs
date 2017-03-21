using UnityEngine;
using System.Collections;

public class MovingScript : MonoBehaviour {

    
    private Rigidbody2D myBody;
    public Vector2 moveVelocity;
    public float fRotate;
    Transform trans;
	void Start () {
        myBody = GetComponent<Rigidbody2D>();
        myBody.velocity = moveVelocity;
        trans = GetComponent<Transform>();
        trans.rotation = Quaternion.Euler(0, 0, fRotate);
	}
	
	
	void Update () {

    }
}
