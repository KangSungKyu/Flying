using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

class C_ITEM
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

}
abstract class C_PLAYERITEM : C_ITEM
{
    
}
class C_PLANE : C_PLAYERITEM
{
    private S_PLANE_STATS m_sStats;

    public C_PLANE()
    {

    }

    public void LoadDataFromID(string strID)
    {
        string strFileName = "Plane";
        string[] strArrFiliter = C_GAMEMANAGER.GetInstance().GetDataMgr().GetStorage(strFileName).GetFilter();

        m_sStats.m_strID = strID;
        m_sStats.m_fDecreseHeight = floatParseWord(strFileName, strID, strArrFiliter[1]);
        m_sStats.m_fDecreaseSpeed = floatParseWord(strFileName, strID, strArrFiliter[2]);
        m_sStats.m_fIncreaseAirMeter = floatParseWord(strFileName, strID, strArrFiliter[3]);
        m_sStats.m_fCollisionScale = floatParseWord(strFileName, strID, strArrFiliter[4]);
        m_sStats.m_fFeverTimeHeight = floatParseWord(strFileName, strID, strArrFiliter[5]);

        Debug.Log("Type : Plane \n"
            + "ID : " + m_sStats.m_strID + "\n"
            + "Decrease Height : " + m_sStats.m_fDecreseHeight + "\n"
            + "Decrease Speed : " + m_sStats.m_fDecreaseSpeed + "\n"
            + "Increase Air Meter : " + m_sStats.m_fIncreaseAirMeter + "\n"
            + "CollisionScale : " + m_sStats.m_fCollisionScale + "\n"
            + "FeverTimeHeight : " + m_sStats.m_fFeverTimeHeight + "\n");

    }
	public S_PLANE_STATS GetPlaneStats()
	{
		return m_sStats;
	}
}
class C_CHARACTER : C_PLAYERITEM
{
    private S_CHAR_STATS m_sStats;
    
    
    public C_CHARACTER()
    {
        
    }

    public void LoadDataFromID(string strID)
    {
        string strFileName = "Character";
        string[] strArrFiliter = C_GAMEMANAGER.GetInstance().GetDataMgr().GetStorage(strFileName).GetFilter();


        m_sStats.m_strID = strID;
        m_sStats.m_fBulletPower = floatParseWord(strFileName, strID, strArrFiliter[1]);
        m_sStats.m_fBulletDegree = floatParseWord(strFileName, strID, strArrFiliter[2]);
        m_sStats.m_fBulletSpeed = floatParseWord(strFileName, strID, strArrFiliter[3]);
        m_sStats.m_fBulletAttackSpeed = floatParseWord(strFileName, strID, strArrFiliter[4]);
        m_sStats.m_fBulletScale = floatParseWord(strFileName, strID, strArrFiliter[5]);
        


        m_sStats.m_fChargeBulletPower = floatParseWord(strFileName, strID, strArrFiliter[6]);
        m_sStats.m_fChargeBulletTime = floatParseWord(strFileName, strID, strArrFiliter[7]);
        m_sStats.m_fChargeBulletDegree = floatParseWord(strFileName, strID, strArrFiliter[8]);
        m_sStats.m_fChargeBulletSpeed = floatParseWord(strFileName, strID, strArrFiliter[9]);
        m_sStats.m_fChargeBulletScale = floatParseWord(strFileName, strID, strArrFiliter[10]);


        m_sStats.m_nMaxHp = intParseWord(strFileName, strID, strArrFiliter[11]);
        m_sStats.m_nHpReduce = intParseWord(strFileName, strID, strArrFiliter[12]);
        m_sStats.m_nCurHp = m_sStats.m_nMaxHp;

        Debug.Log("Type : Character \n"
            +"ID : " + strID+ "\n"
            +"Bullet Power : " + m_sStats.m_fBulletPower.ToString() + "\n"
        + "Bullet Degree : " + m_sStats.m_fBulletDegree.ToString() + "\n"
        + "Bullet speed : " + m_sStats.m_fBulletSpeed.ToString() + "\n"
        + "Bullet AttackSpeed : " + m_sStats.m_fBulletAttackSpeed.ToString() + "\n"
        + "Bullet scale : " + m_sStats.m_fBulletScale.ToString() + "\n"

        + "Charge Power : " + m_sStats.m_fChargeBulletPower.ToString() + "\n"
        + "Charge Time : " + m_sStats.m_fChargeBulletTime.ToString() + "\n"
        + "Charge Degree : " + m_sStats.m_fChargeBulletDegree.ToString() + "\n"
        + "Charge Speed : " + m_sStats.m_fChargeBulletSpeed.ToString() + "\n"
        + "Charge Scale : " + m_sStats.m_fChargeBulletScale.ToString() + "\n"
        
        + "Max Health : " + m_sStats.m_nMaxHp.ToString() + "\n"
        + "Health Reduce : " + m_sStats.m_nHpReduce.ToString());



    }

    public S_CHAR_STATS GetCharacterStats()
    {
        return m_sStats;
    }

  


}
class C_LAUCHER : C_PLAYERITEM
{
    private S_LAUNCHER_STATS m_sStats;
    public C_LAUCHER()
    {

    }
    public void LoadDataFromID(string strID)
    {
        string strFileName = "Launcher";
        string[] strArrFiliter = C_GAMEMANAGER.GetInstance().GetDataMgr().GetStorage(strFileName).GetFilter();


        m_sStats.m_strID = strID;
		m_sStats.m_fMaxPower = floatParseWord(strFileName, strID, strArrFiliter[1]);
        m_sStats.m_fMaxSpeed = floatParseWord(strFileName, strID, strArrFiliter[2]);
        m_sStats.m_fMaxDegree = floatParseWord(strFileName, strID, strArrFiliter[3]);

        m_sStats.m_fMinPower = floatParseWord(strFileName, strID, strArrFiliter[4]);
        m_sStats.m_fMinSpeed = floatParseWord(strFileName, strID, strArrFiliter[5]);
        m_sStats.m_fMinDegree = floatParseWord(strFileName, strID, strArrFiliter[6]);

        Debug.Log("Type : Launcher\n"
            + "ID : " + m_sStats.m_strID + "\n"
			+ "Max Height : " + m_sStats.m_fMaxPower.ToString() + "\n"
            + "Max Speed : " + m_sStats.m_fMaxSpeed.ToString() + "\n"
            + "Max Degree : " + m_sStats.m_fMaxDegree.ToString() + "\n"
			+ "Min Height : " + m_sStats.m_fMinPower.ToString() + "\n"
            + "Min Speed : " + m_sStats.m_fMinSpeed.ToString() + "\n"
            + "Min Degree : " + m_sStats.m_fMinDegree.ToString() + "\n"
            );


    }

	public S_LAUNCHER_STATS GetLauncherStats()
	{
		return m_sStats;
	}

}
class FIELDTIEM : C_ITEM
{
    private S_FIELDITEM_STATS m_sStats;

    public FIELDTIEM()
    {

    }
    public void LoadDataFromID(string strID)
    {
        string strFileName = "FieldItem";
        string[] strArrFiliter = C_GAMEMANAGER.GetInstance().GetDataMgr().GetStorage(strFileName).GetFilter();


        m_sStats.m_strID = strID;
        m_sStats.m_fTimer = floatParseWord(strFileName, strID, strArrFiliter[1]);
        m_sStats.m_fMaxTimer = floatParseWord(strFileName, strID, strArrFiliter[2]);

        Debug.Log("Type : FieldItem\n"
                + "ID : " + m_sStats.m_strID + "\n"
                + "Timer : " + m_sStats.m_fTimer.ToString() + "\n"
                + "Max Timer : " + m_sStats.m_fMaxTimer.ToString() + "\n"
                );


    }

    public S_FIELDITEM_STATS GetFieldItemStats()
    {
         return m_sStats;
    }
    
}

