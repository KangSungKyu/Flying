using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TogueCtrl : MonoBehaviour {

    public GameObject Flying;

    public Slider slider;
    
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeValue()
    {
        float power = 0.0f;

        if (slider.value < 0.0f)
            power = 75.0f;
        else
            power = 75.0f;

        float curS = C_GAMEMANAGER.GetInstance().GetPlayer().GetCurrentSpeed();
        float curV = C_GAMEMANAGER.GetInstance().GetPlayer().GetVerticalSpeed();

        Flying.GetComponent<FlyScript>().fToqueCtrlDeg = 15.0f * slider.value;
        C_GAMEMANAGER.GetInstance().GetPlayer().SetCurrentSpeed(curS+power);
        C_GAMEMANAGER.GetInstance().GetPlayer().SetVerticalSpeed(curV+slider.value * power);
        //Flying.GetComponent<Rigidbody2D>().velocity += new Vector2(0.0f, slider.value);

        slider.value = 0.0f;
    }
}
