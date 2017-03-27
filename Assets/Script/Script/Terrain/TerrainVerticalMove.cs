using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainVerticalMove : MonoBehaviour {

    public Transform[] ArrTerrain;
    public Vector3[] ArrBufferPos;
    public float[] ArrMovementScaling;
    
    void Start()
    {
        
        ArrBufferPos = new Vector3[ArrTerrain.Length];
        for(int i =0; i<ArrTerrain.Length; i++)
        {
            ArrBufferPos[i] = ArrTerrain[i].position;
        }
    }

    void Update () {
        
        if (C_GAMEMANAGER.GetInstance().GetPlayer().GetState() != E_PLAYERSTATE.E_PLAYERDIE)
        {
            for (int i = 0; i < ArrTerrain.Length; i++ )
            {

                float fverticalSpeed = C_GAMEMANAGER.GetInstance().GetPlayer().GetPlayerStats().m_fVerticalSpeed / 1000.0f;
                float fHorizontalSpeed = C_GAMEMANAGER.GetInstance().GetPlayer().GetPlayerStats().m_fCurrentSpeed * ArrMovementScaling[i];
                Vector3 AddVec = new Vector3(fHorizontalSpeed,
                    fverticalSpeed, 0);

                float fDiff = ArrBufferPos[i].x - ArrTerrain[i].position.x ;
                if (Mathf.Abs( fDiff) > 25.6f)
                {
                    ArrTerrain[i].position = new Vector3(ArrBufferPos[i].x,
                        ArrTerrain[i].position.y,
                        ArrTerrain[i].position.z);
                }
                if(C_GAMEMANAGER.GetInstance().GetPlayer().GetRealPos().y < -C_GAMEMANAGER.GetInstance().GetCMHeight())
                {
                    //ArrTerrain[i].position = new Vector3(ArrTerrain[i].position.x,
                    //    ArrBufferPos[i].y,
                    //    ArrBufferPos[i].z);
                    C_GAMEMANAGER.GetInstance().GetPlayer().SetState( E_PLAYERSTATE.E_PLAYERDIE);
                    
                }
                ArrTerrain[i].position = ArrTerrain[i].position - AddVec;

                C_GAMEMANAGER.GetInstance().SetAddToPlayer(AddVec);
            }
        }
    }
}
