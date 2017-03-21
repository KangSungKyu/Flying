using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour {

    private float fPlayerSpeed;
    float Speed;
	
	// Update is called once per frame
	void Update () {
        if (C_GAMEMANAGER.GetInstance().GetPlayer().GetState() == E_PLAYERSTATE.E_PLAYERRELEASE)
        {
            fPlayerSpeed = C_GAMEMANAGER.GetInstance().GetPlayer().GetCurrentSpeed();
            float fverticalSpeed = C_GAMEMANAGER.GetInstance().GetPlayer().GetPlayerStats().m_fVerticalSpeed / 1000.0f;
            Speed = fPlayerSpeed  / 1000.0f;
            transform.position = transform.position - new Vector3(Speed,fverticalSpeed);

            if (transform.position.x <= -15.00f)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
