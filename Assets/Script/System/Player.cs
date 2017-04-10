using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class C_PLAYER
{
    private int m_nID;
    private string m_strSpriteName;
    private S_STATS m_sStats;
    private C_PLANE m_cPlane;
    private C_CHARACTER m_cCharacter;
    private C_LAUCHER m_cLauncher;
    private string m_strCurEat;
    private Vector3 m_vRealPos;
    private Vector3 m_vRealForwardDir;
    private Dictionary<string,bool> m_dicIHaveChar;
    private Dictionary<string, bool> m_dicIHavePlane;
    private Dictionary<string, bool> m_dicIHaveLaunch;
    private Dictionary<string, int> m_dicCharLevel;
    private Dictionary<string, int> m_dicPlaneLevel;
    private Dictionary<string, int> m_dicLaunchLevel;

    public C_PLAYER()
    {
        m_sStats = new S_STATS();
        m_cCharacter = new C_CHARACTER();
        m_cPlane = new C_PLANE();
        m_cLauncher = new C_LAUCHER();

        m_dicIHaveChar = new Dictionary<string, bool>();
        m_dicIHavePlane = new Dictionary<string, bool>();
        m_dicIHaveLaunch = new Dictionary<string, bool>();
        m_dicCharLevel = new Dictionary<string, int>();
        m_dicPlaneLevel = new Dictionary<string, int>();
        m_dicLaunchLevel = new Dictionary<string, int>();

        SetIHaveChar("펭귄", true);
        SetIHaveChar("치킨", false);
        SetIHaveChar("돼지", false);
        SetIHaveChar("양", false);
        SetIHaveChar("사자", false);
        SetIHaveChar("판다", false);
        SetIHaveChar("고양이", false);
        SetIHaveChar("코끼리", false);

        SetIHaveLaunch("새총", true);
        SetIHaveLaunch("투석기", false);
        SetIHaveLaunch("선풍기", false);
        SetIHaveLaunch("고릴라", false);
        SetIHaveLaunch("공룡", false);
        SetIHaveLaunch("물로켓", false);

        SetIHavePlane("종이비행기", true);
        SetIHavePlane("나뭇잎", false);
        SetIHavePlane("낙옆", false);
        SetIHavePlane("달", false);
        SetIHavePlane("풍선", false);
        SetIHavePlane("민들래씨앗", false);

        SetCharLevel("펭귄", 1);
        SetCharLevel("고양이", 1);
        SetCharLevel("돼지", 1);
        SetCharLevel("양", 1);
        SetCharLevel("사자", 1);
        SetCharLevel("치킨", 1);
        SetCharLevel("판다", 1);
        SetCharLevel("코끼리", 1);

        SetPlaneLevel("종이비행기", 1);
        SetPlaneLevel("나뭇잎", 1);
        SetPlaneLevel("낙옆", 1);
        SetPlaneLevel("풍선", 2);
        SetPlaneLevel("달", 3);
        SetPlaneLevel("민들래씨앗", 3);

        SetLauncherLevel("새총", 1);
        SetLauncherLevel("투석기", 1);
        SetLauncherLevel("선풍기", 1);
        SetLauncherLevel("고릴라", 2);
        SetLauncherLevel("공룡", 3);
        SetLauncherLevel("물로켓", 3);
    }


    public void InitPlayer(string strCharacterName, string strPlaneName,string strLauncherName)
    {
        m_sStats.m_fMaxSpeed = 100.0f;
        m_sStats.m_fWindMeter = 0.0f;
        m_cCharacter.LoadDataFromID(strCharacterName);
        m_cPlane.LoadDataFromID(strPlaneName+GetPlaneLevel(strPlaneName));
        m_cLauncher.LoadDataFromID(strLauncherName+GetLauncherLevel(strLauncherName));
		TotalStats ();
    }
    public E_PLAYERSTATE GetState()
    {
        return m_sStats.m_eState;
    }
    public void SetState(E_PLAYERSTATE eState)
    {
        m_sStats.m_eState = eState;
    }
    public void SetRealPos(Vector3 _pos)
    {
        m_vRealPos = _pos;
    }
    public Vector3 GetRealPos()
    {
        return m_vRealPos;
    }
    public void SetRealForwardDir(Vector3 _dir)
    {
        m_vRealForwardDir = _dir;
    }
    public Vector3 GetRealForwardDir()
    {
        return m_vRealForwardDir;
    }
    public float GetMaxSpeed()
    {
        return m_sStats.m_fMaxSpeed;
    }
    public float GetGravitySpeed()
    {
        return m_sStats.m_fGravitySpeed;
    }
    public void SetGravitySpeed(float _f)
    {
        m_sStats.m_fGravitySpeed = _f;
    }
    public float GetCurrentSpeed()
    {
        return m_sStats.m_fCurrentSpeed;
    }
    public void SetCurrentSpeed(float fSpeed)
    {
        if (fSpeed < 0)
        {
            fSpeed = 0;
        }
        m_sStats.m_fCurrentSpeed = fSpeed;
    }

    public void SetIHaveChar(string _who,bool _have)
    {       
        if (m_dicIHaveChar.ContainsKey(_who))
            m_dicIHaveChar[_who] = _have;
        else
            m_dicIHaveChar.Add(_who, _have);
    }
    public bool GetIHaveChar(string _who)
    {
        bool b = false;

        if (m_dicIHaveChar.TryGetValue(_who, out b))
            return b;

        return false;
    }

    public void SetCharLevel(string _who, int _lev)
    {
        m_dicCharLevel[_who] = _lev;
    }
    public int GetCharLevel(string _who)
    {
        return m_dicCharLevel[_who];
    }
    public void SetPlaneLevel(string _who,int _lev)
    {
        m_dicPlaneLevel[_who] = _lev;
    }
    public int GetPlaneLevel(string _who)
    {
        return m_dicPlaneLevel[_who];
    }

    public void SetLauncherLevel(string _who, int _lev)
    {
        m_dicLaunchLevel[_who] = _lev;
    }
    public int GetLauncherLevel(string _who)
    {
        return m_dicLaunchLevel[_who];
    }

    public void SetIHavePlane(string _who, bool _have)
    {
        if (m_dicIHavePlane.ContainsKey(_who))
            m_dicIHavePlane[_who] = _have;
        else
            m_dicIHavePlane.Add(_who, _have);
    }
    public bool GetIHavePlane(string _who)
    {
        bool b = false;

        if (m_dicIHavePlane.TryGetValue(_who, out b))
            return b;

        return false;
    }
    public void SetIHaveLaunch(string _who, bool _have)
    {
        if (m_dicIHaveLaunch.ContainsKey(_who))
            m_dicIHaveLaunch[_who] = _have;
        else
            m_dicIHaveLaunch.Add(_who, _have);
    }
    public bool GetIHaveLaunch(string _who)
    {
        bool b = false;

        if (m_dicIHaveLaunch.TryGetValue(_who, out b))
            return b;

        return false;
    }
    public float SpeedReducer()
	{
		return m_sStats.m_fDecreaseSpeed;
	}
    public void SetSpeedReducer(float _f)
    {
        m_sStats.m_fDecreaseSpeed = _f;
    }

    public void SetWindMeter(float nData)
    {
        m_sStats.m_fWindMeter = nData;
    }
    public float GetWindMeter()
    {
        return m_sStats.m_fWindMeter;
    }
    public void SetCurrentHP(int _hp)
    {
        m_sStats.m_nCurHp = _hp;
    }
    public void SetMaxHP(int _mhp)
    {
        m_sStats.m_nMaxHp = _mhp;
    }
    public int GetCurrentHP()
    {
        return m_sStats.m_nCurHp;
    }
    public int GetMaxHP()
    {
        return m_sStats.m_nMaxHp;
    }
    public void SetReduceHP(int _h)
    {
        m_sStats.m_nConHp = _h;
    }
    public int GetReduceHP()
    {
        return m_sStats.m_nConHp;
    }
    public void SetCurrentBulletCount(int _b)
    {
        m_sStats.m_nCurBulletCount = _b;
    }
    public void SetCurrentChargeBulletCount(int _cb)
    {
        m_sStats.m_nCurChargeCount = _cb;
    }
    public int GetCurrentBulletCount()
    {
        return m_sStats.m_nCurBulletCount;
    }
    public int GetCurrentChargeBulletCount()
    {
        return m_sStats.m_nCurChargeCount;
    }
    public int GetBulletCount()
    {
        return m_sStats.m_nBulletCount;
    }
    public int GetChargeBulletCount()
    {
        return m_sStats.m_nChargeCount;
    }
    public void LoadCharacter(string strID)
    {
        m_cCharacter.LoadDataFromID(strID);
    }

    public void SetCurrentEat(string eat)
    {
        m_strCurEat = eat;
    }
    public string GetCurrentEat()
    {
        return m_strCurEat;
    }

	void TotalStats()
	{
		m_sStats.m_fMaxSpeed = m_cLauncher.GetLauncherStats ().m_fMaxSpeed;
		m_sStats.m_fGravitySpeed = m_cPlane.GetPlaneStats ().m_fDecreseHeight / 100.0f;
		m_sStats.m_fDecreaseSpeed = m_cPlane.GetPlaneStats ().m_fDecreaseSpeed / 100.0f;
		m_sStats.m_fAirMeterIncrease = m_cPlane.GetPlaneStats ().m_fIncreaseAirMeter;
		m_sStats.m_fColliderScale = m_cPlane.GetPlaneStats ().m_fCollisionScale / 100.0f;
		m_sStats.m_fFeverTime = m_cPlane.GetPlaneStats ().m_fFeverTimeHeight;

		m_sStats.m_fBulletPower = m_cCharacter.GetCharacterStats ().m_fBulletPower;
		m_sStats.m_fBulletDegree= m_cCharacter.GetCharacterStats ().m_fBulletDegree;
		m_sStats.m_fBulletSpeed = m_cCharacter.GetCharacterStats ().m_fBulletSpeed;
		m_sStats.m_fBulletAttackSpeed = m_cCharacter.GetCharacterStats ().m_fBulletAttackSpeed;
		m_sStats.m_fBulletScale = m_cCharacter.GetCharacterStats ().m_fBulletScale;

		m_sStats.m_fChargeBulletPower = m_cCharacter.GetCharacterStats ().m_fChargeBulletPower;
		m_sStats.m_fChargeBulletTime= m_cCharacter.GetCharacterStats ().m_fChargeBulletTime;
		m_sStats.m_fChargeBulletDegree= m_cCharacter.GetCharacterStats ().m_fChargeBulletDegree;
		m_sStats.m_fChargeBulletSpeed= m_cCharacter.GetCharacterStats ().m_fChargeBulletSpeed;
		m_sStats.m_fChargeBulletScale= m_cCharacter.GetCharacterStats ().m_fChargeBulletScale;

		m_sStats.m_fMaxPower = m_cLauncher.GetLauncherStats ().m_fMaxPower / 10.0f;
		m_sStats.m_fMaxSpeed= m_cLauncher.GetLauncherStats ().m_fMaxSpeed;
		m_sStats.m_fMaxDegree = m_cLauncher.GetLauncherStats ().m_fMaxDegree;
		m_sStats.m_fMinPower= m_cLauncher.GetLauncherStats ().m_fMinPower / 10.0f;

		m_sStats.m_fMinDegree= m_cLauncher.GetLauncherStats ().m_fMinDegree;
        m_sStats.m_fVerticalSpeed = 0.0f;

        m_sStats.m_nCurHp = m_cCharacter.GetCharacterStats().m_nCurHp;
        m_sStats.m_nMaxHp = m_cCharacter.GetCharacterStats().m_nMaxHp;
        m_sStats.m_nConHp = m_cCharacter.GetCharacterStats().m_nHpReduce;

        m_sStats.m_nBulletCount = m_cCharacter.GetCharacterStats().m_nBulletCount;
        m_sStats.m_nChargeCount = m_cCharacter.GetCharacterStats().m_nChargeCount;
        m_sStats.m_nCurBulletCount = m_cCharacter.GetCharacterStats().m_nCurBulletCount;
        m_sStats.m_nCurChargeCount = m_cCharacter.GetCharacterStats().m_nCurChargeCount;
    }

	public S_STATS GetPlayerStats()
	{
		return m_sStats;
	}
    public void SetVerticalSpeed(float fSpeed)
    {
        m_sStats.m_fVerticalSpeed = fSpeed;
    }
    public float GetVerticalSpeed()
    {
        return m_sStats.m_fVerticalSpeed;
    }
}


public struct S_STATS
{
    //Plane
    public float m_fGravitySpeed;
    public float m_fCurrentSpeed;
	public float m_fDecreaseSpeed;
	public float m_fAirMeterIncrease;
	public float m_fColliderScale;
	public float m_fFeverTime;

	//Character
	public float m_fBulletPower;
	public float m_fBulletDegree;
	public float m_fBulletSpeed;
	public float m_fBulletAttackSpeed;
	public float m_fBulletScale;
	public float m_fChargeBulletPower;
	public float m_fChargeBulletTime;
	public float m_fChargeBulletDegree;
	public float m_fChargeBulletSpeed;
	public float m_fChargeBulletScale;

	//Launcher
	public float m_fMaxPower;
	public float m_fMaxSpeed;
	public float m_fMaxDegree;
	public float m_fMinPower;
	public float m_fMinDegree;


    //VerticalMove
    public float m_fVerticalSpeed;


    //WindMeter
    public float m_fWindMeter;

    //hp
    public int m_nCurHp;
    public int m_nMaxHp;
    public int m_nConHp;

    //bullet count
    public int m_nCurBulletCount;
    public int m_nCurChargeCount;
    public int m_nBulletCount;
    public int m_nChargeCount;


    //OtherStats
    
    public E_PLAYERSTATE m_eState;

}
public enum E_PLAYERSTATE
{
    E_PLAYERATTACH,
    E_PLAYERPOOL,
    E_PLAYERRELEASE,
    E_PLAYERFEVERTIME,
    E_PLAYERDIE
}