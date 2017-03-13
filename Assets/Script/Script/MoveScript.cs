using UnityEngine;
using System.Collections;

public class MoveScript : MonoBehaviour {
    public Animator[] anim;
    public float[] Arr_SpeedMulti;
	
	
	void Update () {
        int nCount = 0;
        while (nCount != anim.Length)
        {
            anim[nCount].speed = Arr_SpeedMulti[nCount] * C_GAMEMANAGER.GetInstance().GetPlayer().GetCurrentSpeed();
            nCount++;
        }
        
	}
}
