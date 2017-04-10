using UnityEngine;
using System.Collections;

public class ropeScript : MonoBehaviour {


    public LineRenderer Drawline;
    public Transform StartTrans;
    public Transform EndTrans;
    void Start()
    {
        //Drawline = GetComponent<LineRenderer>();
    }
	// Update is called once per frame
	void Update () {
        if(C_GAMEMANAGER.GetInstance().GetPlayer().GetState() != E_PLAYERSTATE.E_PLAYERRELEASE)
        {
            Drawline.SetPosition(0, StartTrans.position);
            Drawline.SetPosition(1, EndTrans.position);
        }
        else
        {
            Drawline.enabled = false;
        }
	
	}
}
