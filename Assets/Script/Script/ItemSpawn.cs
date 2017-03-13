using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    private C_ObjectPool m_cPool;
    private GameObject Obstacle;
    public int nSize;
    public float fCoolTime;
    public string strObstacleName;

    IEnumerator Start()
    {
        GameObject oo = C_GAMEMANAGER.GetInstance().GetObjectMgr().GetObject("EmptyItem_IM");

        if (oo == null)
        {
            Debug.Log("Failed!~!");
            yield return new WaitForSeconds(0.0f);
        }

        m_cPool = new C_ObjectPool(nSize, oo);

        while (true)
        {
            if (C_GAMEMANAGER.GetInstance().GetPlayer().GetState() == E_PLAYERSTATE.E_PLAYERRELEASE)
                break;
            yield return new WaitForSeconds(0.02f);
        }


        int nCount = 0;
        while (true)
        {
            yield return new WaitForSeconds(fCoolTime);
            if (nCount >= nSize)
            {
                nCount = 0;
                m_cPool.DestroyAll();

                GameObject jj = C_GAMEMANAGER.GetInstance().GetObjectMgr().GetObject("EmptyItem_IM");

                if(jj == null)
                {
                    break;
                }

                m_cPool = new C_ObjectPool(nSize, jj);
            }
            GameObject goObject = m_cPool.Alloc();

            ItemScript iss = goObject.GetComponent<ItemScript>();
            
            if(iss != null)
                iss.SetItemSpr();

            goObject.GetComponent<Transform>().position = transform.position + new Vector3(0, Random.Range(-7, 7));
            nCount++;

            if (C_GAMEMANAGER.GetInstance().GetPlayer().GetState() == E_PLAYERSTATE.E_PLAYERDIE)
            {
                m_cPool.DestroyAll();
                break;
            }


        }

    }



}
