using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepChargeMovement : MonoBehaviour {

    // Use this for initialization
    int prevRed;
    float timr;
	void Start ()
    {
        this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        this.gameObject.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
        prevRed = C_GAMEMANAGER.GetInstance().GetPlayer().GetReduceHP();

        C_GAMEMANAGER.GetInstance().GetPlayer().SetReduceHP(0);
        timr = 0.0f;
    }
	
	// Update is called once per frame
	void Update () {
		if(timr > 1.0f)
        {
            C_GAMEMANAGER.GetInstance().GetPlayer().SetReduceHP(prevRed);
            this.gameObject.SetActive(false);
        }

        timr += Time.deltaTime;
	}
    
}
