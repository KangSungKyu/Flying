using UnityEngine;
using System.Collections.Generic;
using System.Collections;

class C_OBJECTMANGER
{
    private Dictionary<string, GameObject> m_Dic;
    public C_OBJECTMANGER()
    {
        m_Dic = new Dictionary<string, GameObject>();
        m_Dic.Clear();
    }
    public GameObject GetObject(string strKey)
    {
        GameObject outObject;
        if (!m_Dic.TryGetValue(strKey, out outObject))
        {
            return null;
        }
        return outObject;
    }
    public void InsertObject(string strKey, GameObject cObject)
    {
        if (!GetObject(strKey))
        {
            m_Dic.Add(strKey, cObject);
            Debug.Log(strKey + " Insert Success");
        }
        else
        {
            Debug.Log("Fail Insert!, " + strKey + " is Added In Dic!");
        }
    }
    
}
