﻿using UnityEngine;
using System.Collections;

public class LogoLoading : MonoBehaviour {

	public void ButtonEvent()
    {
        C_GAMEMANAGER.GetInstance().ChangeScene("Main");
    }
}
