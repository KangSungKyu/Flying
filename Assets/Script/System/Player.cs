using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class C_PLAYER
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

    public C_PLAYER()
    {
        m_sStats = new S_STATS();
        m_cCharacter = new C_CHARACTER();
        m_cPlane = new C_PLANE();
        m_cLauncher = new C_LAUCHER();


    }


    public void InitPlayer(string strCharacterName, string strPlaneName,string strLauncherName)
    {
        m_sStats.m_fMaxSpeed = 100.0f;
        m_sStats.m_fWindMeter = 0.0f;
        m_cCharacter.LoadDataFromID(strCharacterName);
        m_cPlane.LoadDataFromID(strPlaneName);
        m_cLauncher.LoadDataFromID(strLauncherName);
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

	public float SpeedReducer()
	{
		return m_sStats.m_fDecreaseSpeed;
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


struct S_STATS
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