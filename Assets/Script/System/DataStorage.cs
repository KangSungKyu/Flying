using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public class C_DATAMANAGER
{
    private Dictionary<string, C_DATASTORAGE> m_dicData;
    public C_DATAMANAGER()
    {
        m_dicData = new Dictionary<string, C_DATASTORAGE>();
    }

    void InsertData(string strDataPath,string strDataName)
    {
        TextAsset textAsset = (TextAsset)Resources.Load(strDataPath) as TextAsset;

        C_DATASTORAGE cData = new C_DATASTORAGE();
        cData.LoadData(textAsset);
        m_dicData.Add(strDataName, cData);

    }
    public void InitMgr()
    {
       
        m_dicData.Clear();
        InsertData("Data/Data_Char", "Character");
        InsertData("Data/Data_Launcher", "Launcher");
        InsertData("Data/Data_Plane", "Plane");

    }
    public bool TryAccessStorage(string strName)
    {
        C_DATASTORAGE m_cData = new C_DATASTORAGE();
        return m_dicData.TryGetValue(strName, out m_cData);
    }
    public C_DATASTORAGE GetStorage(string strName)
    {
        C_DATASTORAGE m_cData = new C_DATASTORAGE();
        m_dicData.TryGetValue(strName, out m_cData);
        return m_cData;
    }
    public string GetData(string strFileName, string strID, string strKey)
    {
        C_DATASTORAGE cStorage;
        if(m_dicData.TryGetValue(strFileName,out cStorage) == false)
        {
            Debug.Log("dic File name Error");
            return null;
        }
        return cStorage.GetData(strID, strKey);
    }
    public string[] GetFilter(string strFileName)
    {
        C_DATASTORAGE cStorage;
        if(m_dicData.TryGetValue(strFileName, out cStorage) == false)
        {
            Debug.Log("dic File name Error");
            return null;
        }
        return cStorage.GetFilter();
    }

}
public class C_DATASTORAGE
{

    //object -> Boxing, UnBoxing의 형태로 데이터를 저장.
    //string으로 저장되어 있기 때문에 나중에 데이터를 꺼내고 다시 구문 분석(parse)을 해야함.
    //ex) string to Int => Int32.Parse or Int32.TryParse

    private string[] m_strFilter;
    private string[] m_strArrData;
    private string m_strFullData;
    

    private Dictionary<string, Dictionary<string, string>> m_dicData;
    public void LoadData(TextAsset text)
    {
        
        m_strFullData = text.text;
        
        m_strArrData = m_strFullData.Split('\n');
        m_strFilter = m_strArrData[0].Split(',');
        
        for(int i = 1; i<m_strArrData.Length; i++)
        {
            
            Dictionary<string, string> dicData = new Dictionary<string, string>();
            string[] strData = m_strArrData[i].Split(',');
            string strID = strData[0];
            
            for(int j = 1; j<strData.Length; j++)
            {
                dicData.Add(m_strFilter[j], strData[j]);
                
            }
            
            m_dicData.Add(strID, dicData);

        }
        

    }
    public string GetData(string strID,string strKey)
    {
        Dictionary<string, string> dicData;

      if(m_dicData.TryGetValue(strID, out dicData) ==false)
        {
            Debug.Log("dicIDError");
            return null;
        }

        string strData;

        if(dicData.TryGetValue(strKey, out strData) == false)
        {
            Debug.Log("dicKeyError");
            return null;
        }

        return strData;

    }


    public C_DATASTORAGE()
    {
        m_dicData = new Dictionary<string, Dictionary<string, string>>();
    }

    public string[] GetFilter()
    {
        return m_strFilter;
    }
}

