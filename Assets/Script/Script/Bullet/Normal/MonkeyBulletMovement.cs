using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyBulletMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Obstacle")
        {

            Vector2 pos = C_GAMEMANAGER.GetInstance().GetPlayer().GetRealPos();
            Vector2 ax = Vector2.right;
            pos.Normalize();
            
            pos = Quaternion.Euler(0.0f, 0.0f, Random.Range(-60.0f,60.0f))*pos;

            this.gameObject.GetComponent<Rigidbody2D>().velocity += -pos * C_GAMEMANAGER.GetInstance().GetPlayer().GetPlayerStats().m_fBulletSpeed;
        }
    }
}
