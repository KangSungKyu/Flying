using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;


public class PeaceMeter : MonoBehaviour
{
    public C_PLAYER m_cPlayer;

    private Dictionary<string, int> m_dicID;
    private Dictionary<int, string> m_dicName;
    private Dictionary<int, int> m_dicMaxFromLevel; //cur max -> next max
    private List<int> m_arrPeace;
    private List<int> m_arrMaxPeace;

    public void Init()
    {
        if (m_dicID != null)
            m_dicID.Clear();
        if (m_dicID == null)
            m_dicID = new Dictionary<string, int>();

        if (m_dicName != null)
            m_dicName.Clear();
        if (m_dicName == null)
            m_dicName = new Dictionary<int, string>();

        if (m_dicMaxFromLevel != null)
            m_dicMaxFromLevel.Clear();
        if (m_dicMaxFromLevel == null)
        {
            m_dicMaxFromLevel = new Dictionary<int, int>();
            m_dicMaxFromLevel.Add(0,30);
            m_dicMaxFromLevel.Add(30,50);
            m_dicMaxFromLevel.Add(50,70);
            m_dicMaxFromLevel.Add(70,90);
        }

        if (m_arrPeace == null)
        {
            m_arrPeace = new List<int>();
        }
        if (m_arrMaxPeace == null)
        {
            m_arrMaxPeace = new List<int>();
        }
    }


    public void AddID(string _name, int _id)
    {
        int id = 0;

        if (!m_dicID.TryGetValue(_name, out id))
        {
            m_dicID.Add(_name, _id);
            m_dicName.Add(_id, _name);

            m_arrPeace.Add(0);
            m_arrMaxPeace.Add(m_dicMaxFromLevel[0]);
        }
        else
        {
            m_arrPeace[id] = 0;
            m_arrMaxPeace[id] = m_dicMaxFromLevel[0];
        }
    }
    public int GetIDCount()
    {
        return m_dicID.Count;
    }

    public bool SetNextLevel(int _who)
    {
        if (IsNextLevel(_who))
        {
            int sub = m_arrPeace[_who] - m_arrMaxPeace[_who];

            m_arrMaxPeace[_who] = m_dicMaxFromLevel[m_arrPeace[_who]];
            m_arrPeace[_who] = sub;

            return true;
        }

        return false;
    }
    public bool IsNextLevel(int _who)
    {
        return m_arrPeace[_who] >= m_arrMaxPeace[_who];
    }

    public void SetPeace(int _who, int _peace)
    {
        if (m_dicID.ContainsValue(_who))
        {
            m_arrPeace[_who] = _peace;
        }
    }
    public void SetMaxPeace(int _who, int _peace)
    {
        m_arrMaxPeace[_who] = _peace;
    }
    public int GetPeace(int _who)
    {
        return m_arrPeace[_who];
    }
    public int GetMaxPeace(int _who)
    {
        return m_arrMaxPeace[_who];
    }
    public string GetCharNameFromID(int _id)
    {
        return m_dicName[_id];
    }
    public int GetIDFromCharName(string _name)
    {
        return m_dicID[_name];
    }
}

