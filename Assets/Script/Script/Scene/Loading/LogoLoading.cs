using UnityEngine;
using System.Collections;

public class LogoLoading : MonoBehaviour {

	public void ButtonEvent()
    {
        C_GAMEMANAGER.GetInstance().ChangeScene("Main");

        Screen.SetResolution(1024, 600, true);
    }
}
