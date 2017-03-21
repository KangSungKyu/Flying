using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovementScript : MonoBehaviour {

    public float fZRotate;


    Transform trans;
    private void Start()
    {
        trans = GetComponent<Transform>();
        /*
        Vector2 pos = trans.position;
        Vector2 ax = Vector2.right;
        pos.Normalize();

        float rad = Mathf.Acos(Vector2.Dot(ax, pos));
        trans.rotation = Quaternion.Euler(0.0f, 0.0f,Mathf.Rad2Deg * rad);
        //*/
    }

    // Update is called once per frame
    void Update () {
        trans.Rotate(0.0f, 0.0f, fZRotate);
    }
}
