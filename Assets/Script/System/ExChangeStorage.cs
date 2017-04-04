using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

abstract public class C_ExChangeStorage 
{
    protected float floatParseWord(string strFileName, string strID, string strFillterWord)
    {
        string strData = C_GAMEMANAGER.GetInstance().GetDataMgr().GetData(strFileName, strID, strFillterWord);
        float fData = float.Parse(strData);
        return fData;
    }

    protected int intParseWord(string strFileName, string strID, string strFillterWord)
    {
        string strData = C_GAMEMANAGER.GetInstance().GetDataMgr().GetData(strFileName, strID, strFillterWord);
        int nData = Int32.Parse(strData);
        return nData;
    }
    
    public int m_nDia = 0;
    public int m_nRob = 0;
    public int m_nSpa = 0;
    public int m_nTop = 0;
    public int m_nHob = 0;
    public int m_nEme = 0;
}

public class C_ExChangeStorage_Char : C_ExChangeStorage
{
    public void LoadDataFromID(string strID,int _star)
    {
        string strFileName = "Char_"+_star+"S";

        Debug.Log(strFileName);

        string[] strArrFiliter = C_GAMEMANAGER.GetInstance().GetDataMgr().GetStorage(strFileName).GetFilter();

        m_nDia = intParseWord(strFileName, strID, strArrFiliter[1]);
        m_nRob = intParseWord(strFileName, strID, strArrFiliter[2]);
        m_nSpa = intParseWord(strFileName, strID, strArrFiliter[3]);
        m_nTop = intParseWord(strFileName, strID, strArrFiliter[4]);
        m_nHob = intParseWord(strFileName, strID, strArrFiliter[5]);
        m_nEme = intParseWord(strFileName, strID, strArrFiliter[6]);

        Debug.Log(
            strID + "\n" +
            "dia : " + m_nDia + "\n" +
            "rob : " + m_nRob + "\n" +
            "spa : " + m_nSpa + "\n" +
            "top : " + m_nTop + "\n" +
            "hob : " + m_nHob + "\n" +
            "eme : " + m_nEme + "\n");
    }
}

public class C_ExChangeStorage_Plane : C_ExChangeStorage
{
    public void LoadDataFromID(string strID, int _star)
    {
        string strFileName = "Plane_" + _star + "S";
        string[] strArrFiliter = C_GAMEMANAGER.GetInstance().GetDataMgr().GetStorage(strFileName).GetFilter();

        m_nDia = intParseWord(strFileName, strID, strArrFiliter[1]);
        m_nRob = intParseWord(strFileName, strID, strArrFiliter[2]);
        m_nSpa = intParseWord(strFileName, strID, strArrFiliter[3]);
        m_nTop = intParseWord(strFileName, strID, strArrFiliter[4]);
        m_nHob = intParseWord(strFileName, strID, strArrFiliter[5]);
        m_nEme = intParseWord(strFileName, strID, strArrFiliter[6]);

        Debug.Log(
            strID + "\n" +
            "dia : " + m_nDia + "\n" +
            "rob : " + m_nRob + "\n" +
            "spa : " + m_nSpa + "\n" +
            "top : " + m_nTop + "\n" +
            "hob : " + m_nHob + "\n" +
            "eme : " + m_nEme + "\n");
    }
}
