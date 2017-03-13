using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class C_ObjectPool {

    private int m_Cnt;
    private int m_Size;
    private List<GameObject> m_Pool;
    

    public C_ObjectPool(int _size,GameObject _prop = null)
    {
        m_Cnt = 0;
        m_Size = _size;
        m_Pool = new List<GameObject>();

        for(int i = 0; i < m_Size; i++)
        {
            m_Pool.Add( _prop ? GameObject.Instantiate( _prop) : new GameObject("Empty_Obj_"+i.ToString()+"_In_Pool"));

            m_Pool[i].SetActive(false);
            //DontDestroyOnLoad(m_Pool[i]);
        }        
    }
    public GameObject Alloc()
    {
        GameObject obj = null;

        if (0 <= m_Cnt && m_Cnt < m_Size)
        {
            obj = m_Pool[m_Cnt];
            m_Cnt++;

            obj.SetActive(true);
        }

        return obj;
    }
    
    public void Free( GameObject _obj)
    {
        if(_obj && m_Cnt > 0)
        {
            _obj.SetActive(false);
            m_Cnt--;
        }
    }
    public void AllFree()
    {
        for(int i = 0; i < m_Size; ++i)
        {
            if(!m_Pool[i].Equals(null))
            {
                m_Pool[i].SetActive(false);
                m_Cnt--;
            }
        }
        if (m_Cnt > 0)
            m_Cnt = 0;
    }
    public GameObject SearchObject(int nIndex)
    {
        return m_Pool[nIndex];
    }
    public int GetMax()
    {
        return m_Size;
    }
    public void DestroyAll()
    {
        int nCount = 0;
        while(nCount != m_Size)
        {
            Object.Destroy(m_Pool[nCount]);
            nCount++;
        }
        m_Pool.Clear();
    }
}
