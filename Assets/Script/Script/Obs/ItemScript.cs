using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemScript : MonoBehaviour
{
    float fPlayerSpeed;
    float Speed;
    SpriteRenderer renObj;
    public string strObjectName;
    public string strObjectHitName;
    public Sprite sp;
    public Sprite spHit;
    Vector3 vecDir;

    GameObject player;

    delegate void ItemFunc();
    Dictionary<string, ItemFunc> m_dicIFunc;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        m_dicIFunc = new Dictionary<string, ItemFunc>();

        m_dicIFunc.Add("ForwardWind", ForwardWindFunc);
        m_dicIFunc.Add("BackwardWind", BackwardWindFunc);
        m_dicIFunc.Add("치킨_Char", CharPeaceFunc);
        m_dicIFunc.Add("돼지_Char", CharPeaceFunc);
        m_dicIFunc.Add("펭귄_Char", CharPeaceFunc);
        m_dicIFunc.Add("양_Char", CharPeaceFunc);
        m_dicIFunc.Add("사자_Char", CharPeaceFunc);
        m_dicIFunc.Add("코끼리_Char", CharPeaceFunc);
        m_dicIFunc.Add("판다_Char", CharPeaceFunc);
        m_dicIFunc.Add("고양이_Char", CharPeaceFunc);
        m_dicIFunc.Add("원숭이_Char", CharPeaceFunc);
        m_dicIFunc.Add("RouletteKey", RouletteKeyFunc);
        m_dicIFunc.Add("BulletGet", BulletGetFunc);
        m_dicIFunc.Add("ChargeGet", ChargeGetFunc);
        m_dicIFunc.Add("UpGo", UpGoFunc);
        m_dicIFunc.Add("DownGo", DownGoFunc);
    }


    public void SetItemSpr()
    {
 
        string[] arrN = {
                "ForwardWind", "BackwardWind", "치킨_Char","돼지_Char",
            "펭귄_Char","사자_Char","고양이_Char","판다_Char",
            "양_Char","원숭이_Char", "RouletteKey","BulletGet",
            "ChargeGet", "UpGo", "DownGo",
                        };


        string ok = arrN[Random.Range(0, arrN.Length)];

        Debug.Log("Item sett!" + ok);
        
        strObjectName = ok;
        strObjectHitName = ok;
        spHit = C_GAMEMANAGER.GetInstance().GetSpriteMgr().GetSprite(strObjectHitName);
        sp = C_GAMEMANAGER.GetInstance().GetSpriteMgr().GetSprite(strObjectName);
        renObj = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        if (C_GAMEMANAGER.GetInstance().GetPlayer().GetState() == E_PLAYERSTATE.E_PLAYERRELEASE
            || C_GAMEMANAGER.GetInstance().GetPlayer().GetState() == E_PLAYERSTATE.E_PLAYERFEVERTIME)
        {

            fPlayerSpeed = C_GAMEMANAGER.GetInstance().GetPlayer().GetCurrentSpeed();
            float fverticalSpeed = C_GAMEMANAGER.GetInstance().GetPlayer().GetPlayerStats().m_fVerticalSpeed / 1500.0f;
            Speed = fPlayerSpeed / 1500.0f;
            transform.position = transform.position - new Vector3(Speed, fverticalSpeed);

            if (transform.position.x <= -30.00f)
            {
                Destroy(this.gameObject);
            }
        }
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "PlayerAttack")
        {
            StopAllCoroutines();
            StartCoroutine(HitAction());
        }
    }

    IEnumerator HitAction()
    {
        renObj.sprite = spHit;
        renObj.color = new Color(renObj.color.r, renObj.color.g, renObj.color.b, 0.25f);
        yield return new WaitForSeconds(0.125f);
        renObj.color = new Color(renObj.color.r, renObj.color.g, renObj.color.b, 1.0f);
        renObj.sprite = sp;

        this.gameObject.SetActive(false);

        C_GAMEMANAGER.GetInstance().GetPlayer().SetCurrentEat(strObjectName);

        ItemFunc func = null;

        if (m_dicIFunc.TryGetValue(strObjectName, out func))
            if(func != null)
                func();
    }


    void ForwardWindFunc()
    {
        C_GAMEMANAGER.GetInstance().GetPlayer().SetCurrentSpeed(C_GAMEMANAGER.GetInstance().GetPlayer().GetCurrentSpeed() + 30.0f);
    }

    void BackwardWindFunc()
    {
        C_GAMEMANAGER.GetInstance().GetPlayer().SetCurrentSpeed(C_GAMEMANAGER.GetInstance().GetPlayer().GetCurrentSpeed() - 30.0f);
    }

    void CharPeaceFunc()
    {
        string str = strObjectName;

        str = str.Substring(0, str.IndexOf("_"));

        int idx = C_GAMEMANAGER.GetInstance().GetCharPeaceMeter().GetIDFromCharName(str);
        int pe = C_GAMEMANAGER.GetInstance().GetCharPeaceMeter().GetPeace(idx);

        C_GAMEMANAGER.GetInstance().GetCharPeaceMeter().SetPeace(idx, pe + 1);
    }
    void RouletteKeyFunc()
    {
        C_GAMEMANAGER.GetInstance().SetKeyCount(C_GAMEMANAGER.GetInstance().GetKeyCount() + 1);
    }
    void BulletGetFunc()
    {
        int b = C_GAMEMANAGER.GetInstance().GetPlayer().GetCurrentBulletCount();

        b = b + C_GAMEMANAGER.GetInstance().GetPlayer().GetBulletCount();

        C_GAMEMANAGER.GetInstance().GetPlayer().SetCurrentBulletCount(b);
    }
    void ChargeGetFunc()
    {
        int b = C_GAMEMANAGER.GetInstance().GetPlayer().GetCurrentChargeBulletCount();

        b = b + C_GAMEMANAGER.GetInstance().GetPlayer().GetChargeBulletCount();

        C_GAMEMANAGER.GetInstance().GetPlayer().SetCurrentChargeBulletCount(b);
    }
    void UpGoFunc()
    {
        //45deg
        GameObject fly = GameObject.FindGameObjectWithTag("Player");

        fly.GetComponent<FlyScript>().fToqueCtrlDeg += 15.0f;

        float curS = C_GAMEMANAGER.GetInstance().GetPlayer().GetCurrentSpeed();
        float curV = C_GAMEMANAGER.GetInstance().GetPlayer().GetVerticalSpeed();
        float power = 70.0f;
        
        C_GAMEMANAGER.GetInstance().GetPlayer().SetCurrentSpeed(curS + power);
        C_GAMEMANAGER.GetInstance().GetPlayer().SetVerticalSpeed(curV + power);
    }
    void DownGoFunc()
    {
        //90deg
        GameObject fly = GameObject.FindGameObjectWithTag("Player");

        //fly.GetComponent<FlyScript>().fToqueCtrlDeg -= 30.0f;
        
        float curV = C_GAMEMANAGER.GetInstance().GetPlayer().GetVerticalSpeed();
        float power = 100.0f;
        
        C_GAMEMANAGER.GetInstance().GetPlayer().SetVerticalSpeed(curV - power);
    }
}
