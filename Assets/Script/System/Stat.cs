using UnityEngine;
using System.Collections;

struct S_CHAR_STATS
{
    public string m_strID;

    //총알 스텟
    public float m_fBulletPower;
    public float m_fBulletDegree;
    public float m_fBulletSpeed;
    public float m_fBulletAttackSpeed;
    public float m_fBulletScale;

    //충전된 총알 스텟
    public float m_fChargeBulletPower;
    public float m_fChargeBulletTime;
    public float m_fChargeBulletDegree;
    public float m_fChargeBulletSpeed;
    public float m_fChargeBulletScale;

    public int m_nCurHp;
    public int m_nMaxHp;//최대체력
    public int m_nHpReduce;//한번에 깎이는 체력.

}
struct S_PLANE_STATS
{
    public string m_strID;

    public float m_fDecreseHeight;
    public float m_fDecreaseSpeed;
    public float m_fIncreaseAirMeter;
    public float m_fCollisionScale;
    public float m_fFeverTimeHeight;
}
struct S_LAUNCHER_STATS
{
    public string m_strID;

	public float m_fMaxPower;
    public float m_fMaxSpeed;
    public float m_fMaxDegree;
    public float m_fMinPower;
    public float m_fMinSpeed;
    public float m_fMinDegree;

}
struct S_FIELDITEM_STATS
{
    public string m_strID;
    public float m_fTimer; //아이템이 적용되고 있는 시간
    public float m_fMaxTimer; //적용되는 최대시간
}