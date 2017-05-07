using UnityEngine;
using System.Collections;

public class LogoLoading : MonoBehaviour {

	public void ButtonEvent()
    {
        C_GAMEMANAGER.GetInstance().ChangeScene("Loading");

        Screen.SetResolution(1280, 720, true);
    }
}
