using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class SaveLoadCtrl : MonoBehaviour
{
    [System.Serializable]
    [XmlRoot("GameData")]
    public class SaveLoadData
    {
        [System.Serializable]
        public struct SaveLoadPlayer
        {
            //현재 가진 돈
            //현재 가진 플레이횟수
            //캐쉬
            //가지고 있는 캐릭터(등급)
            //가지고 있는 비행기(등급)
            //가지고 있는 발사대(등급)
            //가지고 있는 캐릭터 조각
            //가지고 있는 비행기 조각
            //가지고 있는 발사대 조각
            //가지고 있는 보석 수
            //최고 기록
            public int Money;
            public int PlayCount;
            public int Cash;
            public int CharCnt;
            public bool[] Chars;
            public int[] Char_Levs;
            public int[] Char_Pcs;
            public int PlaneCnt;
            public bool[] Planes;
            public int[] Plane_Levs;
            public int[] Plane_Pcs;
            public int LaunchCnt;
            public bool[] Launchs;
            public int[] Launch_Levs;
            public int[] Launch_Pcs;
            public int JewelCnt;
            public string[] Jewel_Name;
            public int[] Jewel_Cnt;
            public int MaxScore;
        }

        public SaveLoadPlayer data = new SaveLoadPlayer();
    }

    public SaveLoadData svdata = new SaveLoadData();

    private void GetData()
    {
        svdata.data.Money = C_GAMEMANAGER.GetInstance().GetCoin();
        svdata.data.PlayCount = C_GAMEMANAGER.GetInstance().GetPlayCount();
        svdata.data.Cash = C_GAMEMANAGER.GetInstance().GetCash();

        svdata.data.CharCnt = C_GAMEMANAGER.GetInstance().GetCharPeaceMeter().GetIDCount();
        svdata.data.Chars = new bool[svdata.data.CharCnt];
        svdata.data.Char_Levs = new int[svdata.data.CharCnt];
        svdata.data.Char_Pcs = new int[svdata.data.CharCnt];

        for (int i = 0; i < svdata.data.CharCnt; ++i)
        {
            string str = C_GAMEMANAGER.GetInstance().GetCharPeaceMeter().GetCharNameFromID(i);

            svdata.data.Chars[i] = C_GAMEMANAGER.GetInstance().GetPlayer().GetIHaveChar(str);
            svdata.data.Char_Levs[i] = C_GAMEMANAGER.GetInstance().GetPlayer().GetCharLevel(str);
            svdata.data.Char_Pcs[i] = C_GAMEMANAGER.GetInstance().GetCharPeaceMeter().GetPeace(i);
        }

        svdata.data.PlaneCnt = C_GAMEMANAGER.GetInstance().GetPlanePeaceMeter().GetIDCount();
        svdata.data.Planes = new bool[svdata.data.PlaneCnt];
        svdata.data.Plane_Levs = new int[svdata.data.PlaneCnt];
        svdata.data.Plane_Pcs = new int[svdata.data.PlaneCnt];

        for (int i = 0; i < svdata.data.PlaneCnt; ++i)
        {
            string str = C_GAMEMANAGER.GetInstance().GetPlanePeaceMeter().GetCharNameFromID(i);
            svdata.data.Planes[i] = C_GAMEMANAGER.GetInstance().GetPlayer().GetIHavePlane(str);
            svdata.data.Plane_Levs[i] = C_GAMEMANAGER.GetInstance().GetPlayer().GetPlaneLevel(str);
            svdata.data.Plane_Pcs[i] = C_GAMEMANAGER.GetInstance().GetPlanePeaceMeter().GetPeace(i);
        }

        svdata.data.LaunchCnt = C_GAMEMANAGER.GetInstance().GetLauncherPeaceMeter().GetIDCount();
        svdata.data.Launchs = new bool[svdata.data.LaunchCnt];
        svdata.data.Launch_Levs = new int[svdata.data.LaunchCnt];
        svdata.data.Launch_Pcs = new int[svdata.data.LaunchCnt];

        for (int i = 0; i < svdata.data.LaunchCnt; ++i)
        {
            string str = C_GAMEMANAGER.GetInstance().GetLauncherPeaceMeter().GetCharNameFromID(i);
            svdata.data.Launchs[i] = C_GAMEMANAGER.GetInstance().GetPlayer().GetIHaveLaunch(str);
            svdata.data.Launch_Levs[i] = C_GAMEMANAGER.GetInstance().GetPlayer().GetLauncherLevel(str);
            svdata.data.Launch_Pcs[i] = C_GAMEMANAGER.GetInstance().GetLauncherPeaceMeter().GetPeace(i);
        }

        svdata.data.JewelCnt = C_GAMEMANAGER.GetInstance().GetJewelMeter().GetJewelIDCount();
        svdata.data.Jewel_Name = new string[svdata.data.JewelCnt];
        svdata.data.Jewel_Cnt = new int[svdata.data.JewelCnt];

        for (int i = 0; i < svdata.data.JewelCnt; ++i)
        {
            string str = C_GAMEMANAGER.GetInstance().GetJewelMeter().GetJewelName(i); ;
            svdata.data.Jewel_Name[i] = str;
            svdata.data.Jewel_Cnt[i] = C_GAMEMANAGER.GetInstance().GetJewelMeter().GetCurrentJewelCount(str);
        }

        svdata.data.MaxScore = C_GAMEMANAGER.GetInstance().GetScore();
    }

    private void SetData()
    {
        C_GAMEMANAGER.GetInstance().SetCoin(svdata.data.Money);
        C_GAMEMANAGER.GetInstance().SetPlayCount(svdata.data.PlayCount);
        C_GAMEMANAGER.GetInstance().SetCash(svdata.data.Cash);

        for (int i = 0; i < svdata.data.CharCnt; ++i)
        {
            string str = C_GAMEMANAGER.GetInstance().GetCharPeaceMeter().GetCharNameFromID(i);
            C_GAMEMANAGER.GetInstance().GetPlayer().SetIHaveChar(str, svdata.data.Chars[i]);
            C_GAMEMANAGER.GetInstance().GetPlayer().SetCharLevel(str, svdata.data.Char_Levs[i]);
            C_GAMEMANAGER.GetInstance().GetCharPeaceMeter().SetPeace(i, svdata.data.Char_Pcs[i]);
        }

        for (int i = 0; i < svdata.data.PlaneCnt; ++i)
        {
            string str = C_GAMEMANAGER.GetInstance().GetPlanePeaceMeter().GetCharNameFromID(i);
            C_GAMEMANAGER.GetInstance().GetPlayer().SetIHaveChar(str, svdata.data.Planes[i]);
            C_GAMEMANAGER.GetInstance().GetPlayer().SetCharLevel(str, svdata.data.Plane_Levs[i]);
            C_GAMEMANAGER.GetInstance().GetPlanePeaceMeter().SetPeace(i, svdata.data.Plane_Pcs[i]);
        }

        for (int i = 0; i < svdata.data.LaunchCnt; ++i)
        {
            string str = C_GAMEMANAGER.GetInstance().GetLauncherPeaceMeter().GetCharNameFromID(i);
            C_GAMEMANAGER.GetInstance().GetPlayer().SetIHaveLaunch(str, svdata.data.Launchs[i]);
            C_GAMEMANAGER.GetInstance().GetPlayer().SetLauncherLevel(str, svdata.data.Launch_Levs[i]);
            C_GAMEMANAGER.GetInstance().GetLauncherPeaceMeter().SetPeace(i, svdata.data.Launch_Pcs[i]);
        }

        for (int i = 0; i < svdata.data.JewelCnt; ++i)
        {
            string str = C_GAMEMANAGER.GetInstance().GetJewelMeter().GetJewelName(i); 
            C_GAMEMANAGER.GetInstance().GetJewelMeter().SetCurrentJewelCount(str, svdata.data.Jewel_Cnt[i]);
        }

        C_GAMEMANAGER.GetInstance().SetScore(svdata.data.MaxScore);
    }

    public void SaveXML(string _filename = "GameData.xml")
    {
        GetData();

        XmlSerializer ser = new XmlSerializer(typeof(SaveLoadData));
        FileStream stream = new FileStream(_filename, FileMode.Create);
        StreamWriter wri = new StreamWriter(stream, Encoding.UTF8);

        ser.Serialize(wri, svdata);
        stream.Close();

        Debug.Log("Save Xml : " + _filename);
    }

    public void LoadXML(string _filename = "GameData.xml")
    {
        if (!File.Exists(_filename)) return;

        XmlSerializer ser = new XmlSerializer(typeof(SaveLoadData));
        FileStream stream = new FileStream(_filename, FileMode.Open);
        StreamReader red = new StreamReader(stream, Encoding.UTF8);

        svdata = ser.Deserialize(red) as SaveLoadData;

        stream.Close();
        SetData();

        Debug.Log("Load Xml : " + _filename);
    }

    public void SaveBinary(string _filename = "GameData.sav")
    {
        GetData();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = File.Create(_filename);

        bf.Serialize(stream, svdata);
        stream.Close();

        Debug.Log("Save Bin : " + _filename);
    }

    public void LoadBinary(string _filename = "GameData.sav")
    {
        if (!File.Exists(_filename)) return;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = File.Open(_filename, FileMode.Open);

        svdata = bf.Deserialize(stream) as SaveLoadData;

        stream.Close();
        SetData();

        Debug.Log("Load Bin : " + _filename);
    }
}

