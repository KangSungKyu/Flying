using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class C_ANIMATIONMANAGER
{
    private Dictionary<string, Animation> m_Dic;
    public C_ANIMATIONMANAGER()
    {
        m_Dic = new Dictionary<string, Animation>();
        m_Dic.Clear();
    }
    public  void InsertAnimation(string strKey,Animation anim)
    {
        m_Dic.Add(strKey, anim);
        Debug.Log(strKey + " Load Success!");
    }
    public Animation GetAnimation(string strKey)
    {
        Animation outSprite;
        if(!m_Dic.TryGetValue(strKey,out outSprite))
        {
            return null;
        }
        return outSprite;
    }
}

public class C_ANIMATORMANAGER
{
    private Dictionary<string, Animator> m_Dic;
    public C_ANIMATORMANAGER()
    {
        m_Dic = new Dictionary<string, Animator>();
        m_Dic.Clear();
    }
    public  void InsertAnimator(string strKey, Animator anim)
    {
        m_Dic.Add(strKey, anim);
        Debug.Log(strKey + " Load Success!");
    }
    public Animator GetAnimator(string strKey)
    {
        Animator outSprite;
        if(!m_Dic.TryGetValue(strKey,out outSprite))
        {
            return null;
        }
        return outSprite;
    }
}