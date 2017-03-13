using UnityEngine;
using System.Collections.Generic;
using System.Collections;

class C_SOUNDMANAGER
{
    private Dictionary<string, AudioSource> m_Dic;
    public C_SOUNDMANAGER()
    {
        m_Dic = new Dictionary<string, AudioSource>();
        m_Dic.Clear();
    }
    public AudioSource GetAudio(string strKey)
    {
        AudioSource outSource;
        if(!m_Dic.TryGetValue(strKey,out outSource))
        {
            return null;
        }
        return outSource;
    }
    public void InsertAudio(string strKey, AudioSource audio)
    {
        m_Dic.Add(strKey, audio);
    }
    
    
}

