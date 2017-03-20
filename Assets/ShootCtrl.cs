using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShootCtrl : MonoBehaviour {
    public GameObject Flying;
	// Use this for initialization
	void Start () {
        Flying.GetComponent<FlyScript>().BeginChargeBullet();
	}
    private void Update()
    {
        if(GetComponent<JoyStickCtrl>().bStart)
            Flying.GetComponent<FlyScript>().UpdateChargeBullet();
    }
    // Update is called once per frame
    public void StartDrag(BaseEventData _data)
    {
        //Flying.GetComponent<FlyScript>().UpdateChargeBullet();
    }
    public void EndDrag(BaseEventData _data)
    {
        Flying.GetComponent<FlyScript>().EndChargeBullet();
    }
}
