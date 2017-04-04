using System;
using System.Collections.Generic;
using UnityEngine;

public class JewelMeter : MonoBehaviour
{
    Dictionary<string, List<int>> m_dicJewel;//name,(cur/exchange)

    public void Init()
    {
        m_dicJewel = new Dictionary<string, List<int>>();
    }

    public void SetCurrentJewelCount(string _what,int _n)
    {
        List<int> li = null;

        if(m_dicJewel.TryGetValue(_what,out li) && li != null)
        {
            li[0] = _n;
        }
        else
        {
            li = new List<int>();
            li.Add(_n);
            li.Add(_n);

            m_dicJewel.Add(_what, li);
        }
    }
    public void SetExchangeJewelCount(string _what,int _m)
    {
        List<int> li = null;

        if (m_dicJewel.TryGetValue(_what, out li) && li != null)
        {
            li[1] = _m;
        }
        else
        {
            li = new List<int>();
            li.Add(0);
            li.Add(_m);

            m_dicJewel.Add(_what, li);
        }
    }

    public int GetCurrentJewelCount(string _what)
    {
        return m_dicJewel[_what][0];
    }
    public int GetExchangeJewelCount(string _what)
    {
        return m_dicJewel[_what][1];
    }
}

