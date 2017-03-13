using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class C_SPRITEMANAGER
{
    private Dictionary<string, Sprite> m_Dic;
    public C_SPRITEMANAGER()
    {
        m_Dic = new Dictionary<string, Sprite>();
        m_Dic.Clear();
    }
    public  void InsertSprite(string strKey, Sprite sprite)
    {
        m_Dic.Add(strKey, sprite);
        Debug.Log(strKey + " Load Success!");
    }
    public Sprite GetSprite(string strKey)
    {
        Sprite outSprite;
        if(!m_Dic.TryGetValue(strKey,out outSprite))
        {
            return null;
        }
        return outSprite;
    }
}