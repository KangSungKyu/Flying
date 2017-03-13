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

    void Start()
    {
        player = GameObject.Find("FlyObject");
    }

    public void SetItemSpr()
    {
 
        string[] arrN = { "ForwardWind", "BackwardWind", "치킨_Char","돼지_Char","펭귄_Char","RouletteKey"};


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

        if (strObjectName == "ForwardWind")
        {
            C_GAMEMANAGER.GetInstance().GetPlayer().SetCurrentSpeed(C_GAMEMANAGER.GetInstance().GetPlayer().GetCurrentSpeed() + 10.0f);
        }
        if(strObjectName == "BackwardWind")
        {
            C_GAMEMANAGER.GetInstance().GetPlayer().SetCurrentSpeed(C_GAMEMANAGER.GetInstance().GetPlayer().GetCurrentSpeed() - 10.0f);
        }
        if(strObjectName == "치킨_Char")
        {
            int idx = C_GAMEMANAGER.GetInstance().GetIDFromCharName("치킨");
            int pe = C_GAMEMANAGER.GetInstance().GetPeace(idx);
            
            C_GAMEMANAGER.GetInstance().SetPeace(idx, pe+1);
        }
        if (strObjectName == "돼지_Char")
        {
            int idx = C_GAMEMANAGER.GetInstance().GetIDFromCharName("돼지");
            int pe = C_GAMEMANAGER.GetInstance().GetPeace(idx);

            C_GAMEMANAGER.GetInstance().SetPeace(idx, pe + 1);
        }
        if (strObjectName == "펭귄_Char")
        {
            int idx = C_GAMEMANAGER.GetInstance().GetIDFromCharName("펭귄");
            int pe = C_GAMEMANAGER.GetInstance().GetPeace(idx);
            
            C_GAMEMANAGER.GetInstance().SetPeace(idx, pe + 1);
        }
        if(strObjectName == "RouletteKey")
        {
            C_GAMEMANAGER.GetInstance().SetKeyCount(C_GAMEMANAGER.GetInstance().GetKeyCount() + 1);
        }

        C_GAMEMANAGER.GetInstance().GetPlayer().SetCurrentEat(strObjectName);
    }
}
