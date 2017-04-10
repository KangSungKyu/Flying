﻿using UnityEngine;
using System.Collections;

public class CamScript : MonoBehaviour {
    GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update () {
        if(C_GAMEMANAGER.GetInstance().GetPlayer().GetState() != E_PLAYERSTATE.E_PLAYERATTACH)
        {
            this.transform.position = new Vector3(
                player.GetComponent<Transform>().position.x,
                player.GetComponent<Transform>().position.y,
                -10.0f);
            if (transform.position.y < -6.28f)
            {
                transform.position = new Vector3(0.0f, -6.28f, -10.0f);
            }
        }
    }
}
