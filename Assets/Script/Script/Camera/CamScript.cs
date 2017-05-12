using UnityEngine;
using System.Collections;

public class CamScript : MonoBehaviour {
    GameObject player;
    float xdiff = 0.0f;
    float ydiff = 0.0f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        xdiff = player.transform.position.x - this.transform.position.x;
        ydiff = player.transform.position.y - this.transform.position.y;
    }

    void Update () {
        if(C_GAMEMANAGER.GetInstance().GetPlayer().GetState() != E_PLAYERSTATE.E_PLAYERATTACH)
        {
            //*
            this.transform.position = new Vector3(
                player.GetComponent<Transform>().position.x-xdiff,
                player.GetComponent<Transform>().position.y-ydiff,
                -10.0f);
                //*/
        }
    }
}
