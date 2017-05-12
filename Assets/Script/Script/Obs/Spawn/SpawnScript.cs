using UnityEngine;
using System.Collections;

public class SpawnScript : MonoBehaviour {
    private C_ObjectPool m_cPool;
    private GameObject Obstacle;
    public int nSize;
    public float fCoolTime;
    public string strObstacleName;
	IEnumerator Start () {

        m_cPool = new C_ObjectPool(nSize, C_GAMEMANAGER.GetInstance().GetObjectMgr().GetObject(strObstacleName));

        while (true)
        {
            if (C_GAMEMANAGER.GetInstance().GetPlayer().GetState() == E_PLAYERSTATE.E_PLAYERRELEASE)
                break;
            yield return new WaitForSeconds(0.02f);
        }


        int nCount = 0;
        while(true)
        {
            yield return new WaitForSeconds(fCoolTime);
            if (nCount >= nSize)
            {
                nCount = 0;
                m_cPool.DestroyAll();
                m_cPool = new C_ObjectPool(nSize, C_GAMEMANAGER.GetInstance().GetObjectMgr().GetObject(strObstacleName));
            }
            GameObject goObject = m_cPool.Alloc();
            goObject.GetComponent<Transform>().position = transform.position + new Vector3(0, Random.Range(-7, 7));
            nCount++;
            if(C_GAMEMANAGER.GetInstance().GetPlayer().GetState() == E_PLAYERSTATE.E_PLAYERDIE)
            {
                m_cPool.DestroyAll();
                break;
            }
        }
    }

    


}
