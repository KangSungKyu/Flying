using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatePeaceCtrl : MonoBehaviour {

    GameObject create;
    public GameObject enough;
    Dropdown drop;
    public bool bChar = true;

    Text dia;
    Text rub;
    Text spa;
    Text top;
    Text hob;
    Text eme;

    // Use this for initialization
    void Start ()
    {
        create = this.gameObject;
        create.SetActive(false);
        enough.SetActive(false);
        drop = create.GetComponentInChildren<Dropdown>();

        dia = create.GetComponentsInChildren<Text>()[0];
        rub = create.GetComponentsInChildren<Text>()[1];
        spa = create.GetComponentsInChildren<Text>()[2];
        top = create.GetComponentsInChildren<Text>()[3];
        hob = create.GetComponentsInChildren<Text>()[4];
        eme = create.GetComponentsInChildren<Text>()[5];

        ChangeSelectChar(0);
    }
	
	// Update is called once per frame
	void Update () {

        ChangeSelectChar(drop.value);

        dia.text = "다이아 : " + C_GAMEMANAGER.GetInstance().GetJewelMeter().GetCurrentJewelCount("다이아") + " / " + C_GAMEMANAGER.GetInstance().GetJewelMeter().GetExchangeJewelCount("다이아");
        rub.text = "루비 : " + C_GAMEMANAGER.GetInstance().GetJewelMeter().GetCurrentJewelCount("루비") + " / " + C_GAMEMANAGER.GetInstance().GetJewelMeter().GetExchangeJewelCount("루비");
        spa.text = "사파이어 : " + C_GAMEMANAGER.GetInstance().GetJewelMeter().GetCurrentJewelCount("사파이어") + " / " + C_GAMEMANAGER.GetInstance().GetJewelMeter().GetExchangeJewelCount("사파이어");
        top.text = "토파츠 : " + C_GAMEMANAGER.GetInstance().GetJewelMeter().GetCurrentJewelCount("토파츠") + " / " + C_GAMEMANAGER.GetInstance().GetJewelMeter().GetExchangeJewelCount("토파츠");
        hob.text = "호박 : " + C_GAMEMANAGER.GetInstance().GetJewelMeter().GetCurrentJewelCount("호박") + " / " + C_GAMEMANAGER.GetInstance().GetJewelMeter().GetExchangeJewelCount("호박");
        eme.text = "에메랄드 : " + C_GAMEMANAGER.GetInstance().GetJewelMeter().GetCurrentJewelCount("에메랄드") + " / " + C_GAMEMANAGER.GetInstance().GetJewelMeter().GetExchangeJewelCount("에메랄드");
    }

    public void OnCreatePanel()
    {
        this.gameObject.SetActive(true);
    }
    public void OffCreatePanel()
    {
        this.gameObject.SetActive(false);
    }
    public void ChangeSelectChar(int _n)
    {
        //현재 선택된 캐릭터와 해당 등급에 따라 필요 갯수를 바꿔야함.
        if (bChar)
        {
            C_ExChangeStorage_Char data = new C_ExChangeStorage_Char();
            string strN = drop.options[drop.value].text;
            
            int sd = C_GAMEMANAGER.GetInstance().GetPlayer().GetCharLevel(strN);
            
            data.LoadDataFromID(strN, sd);

            Debug.Log(strN + " "
                    + data.m_nDia + " "
                    + data.m_nRob + " "
                    + data.m_nSpa + " "
                    + data.m_nTop + " "
                    + data.m_nHob + " "
                    + data.m_nEme + " ");

            C_GAMEMANAGER.GetInstance().GetJewelMeter().SetExchangeJewelCount("다이아", data.m_nDia);
            C_GAMEMANAGER.GetInstance().GetJewelMeter().SetExchangeJewelCount("루비", data.m_nRob);
            C_GAMEMANAGER.GetInstance().GetJewelMeter().SetExchangeJewelCount("사파이어", data.m_nSpa);
            C_GAMEMANAGER.GetInstance().GetJewelMeter().SetExchangeJewelCount("토파츠", data.m_nTop);
            C_GAMEMANAGER.GetInstance().GetJewelMeter().SetExchangeJewelCount("호박", data.m_nHob);
            C_GAMEMANAGER.GetInstance().GetJewelMeter().SetExchangeJewelCount("에메랄드", data.m_nEme);
        }
        else
        {
            C_ExChangeStorage_Plane data = new C_ExChangeStorage_Plane();
            string strN = drop.options[drop.value].text;
            int sd = C_GAMEMANAGER.GetInstance().GetPlayer().GetPlaneLevel(strN);

            data.LoadDataFromID(strN, sd);

            Debug.Log(strN + " "
                    + data.m_nDia + " "
                    + data.m_nRob + " "
                    + data.m_nSpa + " "
                    + data.m_nTop + " "
                    + data.m_nHob + " "
                    + data.m_nEme + " ");

            C_GAMEMANAGER.GetInstance().GetJewelMeter().SetExchangeJewelCount("다이아", data.m_nDia);
            C_GAMEMANAGER.GetInstance().GetJewelMeter().SetExchangeJewelCount("루비", data.m_nRob);
            C_GAMEMANAGER.GetInstance().GetJewelMeter().SetExchangeJewelCount("사파이어", data.m_nSpa);
            C_GAMEMANAGER.GetInstance().GetJewelMeter().SetExchangeJewelCount("토파츠", data.m_nTop);
            C_GAMEMANAGER.GetInstance().GetJewelMeter().SetExchangeJewelCount("호박", data.m_nHob);
            C_GAMEMANAGER.GetInstance().GetJewelMeter().SetExchangeJewelCount("에메랄드", data.m_nEme);
        }
    }

    public void CreatePeace()
    {
        int sel = drop.value;

        int dia = 0;
        int rub = 0;
        int spa = 0;
        int top = 0;
        int hob = 0;
        int eme = 0;

        dia = C_GAMEMANAGER.GetInstance().GetJewelMeter().GetCurrentJewelCount("다이아") - C_GAMEMANAGER.GetInstance().GetJewelMeter().GetExchangeJewelCount("다이아");
        rub = C_GAMEMANAGER.GetInstance().GetJewelMeter().GetCurrentJewelCount("루비") - C_GAMEMANAGER.GetInstance().GetJewelMeter().GetExchangeJewelCount("루비");
        spa = C_GAMEMANAGER.GetInstance().GetJewelMeter().GetCurrentJewelCount("사파이어") - C_GAMEMANAGER.GetInstance().GetJewelMeter().GetExchangeJewelCount("사파이어");
        top = C_GAMEMANAGER.GetInstance().GetJewelMeter().GetCurrentJewelCount("토파츠") - C_GAMEMANAGER.GetInstance().GetJewelMeter().GetExchangeJewelCount("토파츠");
        hob = C_GAMEMANAGER.GetInstance().GetJewelMeter().GetCurrentJewelCount("호박") - C_GAMEMANAGER.GetInstance().GetJewelMeter().GetExchangeJewelCount("호박");
        eme = C_GAMEMANAGER.GetInstance().GetJewelMeter().GetCurrentJewelCount("에메랄드") - C_GAMEMANAGER.GetInstance().GetJewelMeter().GetExchangeJewelCount("에메랄드");
        
        if (dia < 0 || rub < 0 || spa < 0 ||
            top < 0 || hob < 0 || eme < 0)
        {
            NotEnoughStarDust();
        }
        else
        {
            if (bChar)
            {
                if (C_GAMEMANAGER.GetInstance().GetCharPeaceMeter().GetPeace(C_GAMEMANAGER.GetInstance().GetCharPeaceMeter().GetIDFromCharName(drop.options[sel].text)) >=
                    C_GAMEMANAGER.GetInstance().GetCharPeaceMeter().GetMaxPeace(C_GAMEMANAGER.GetInstance().GetCharPeaceMeter().GetIDFromCharName(drop.options[sel].text)))
                {
                    return;
                }
            }
            else
            {
                if (C_GAMEMANAGER.GetInstance().GetPlanePeaceMeter().GetPeace(C_GAMEMANAGER.GetInstance().GetPlanePeaceMeter().GetIDFromCharName(drop.options[sel].text)) >=
                    C_GAMEMANAGER.GetInstance().GetPlanePeaceMeter().GetMaxPeace(C_GAMEMANAGER.GetInstance().GetPlanePeaceMeter().GetIDFromCharName(drop.options[sel].text)))
                {
                    return;
                }
            }

            if(dia > 0) C_GAMEMANAGER.GetInstance().GetJewelMeter().SetCurrentJewelCount("다이아", dia);
            if(rub > 0) C_GAMEMANAGER.GetInstance().GetJewelMeter().SetCurrentJewelCount("루비", rub);
            if(spa > 0) C_GAMEMANAGER.GetInstance().GetJewelMeter().SetCurrentJewelCount("사파이어", spa);
            if(top > 0) C_GAMEMANAGER.GetInstance().GetJewelMeter().SetCurrentJewelCount("토파츠", top);
            if(hob > 0) C_GAMEMANAGER.GetInstance().GetJewelMeter().SetCurrentJewelCount("호박", hob);
            if(eme > 0) C_GAMEMANAGER.GetInstance().GetJewelMeter().SetCurrentJewelCount("에메랄드", eme);

            ExChangeStarDust();
        }
    }

    public void NotEnoughStarDust()
    {
        enough.SetActive(true);
    }
    public void ExChangeStarDust()
    {
        string what = drop.options[drop.value].text;

        if (bChar)
        {
            int id = C_GAMEMANAGER.GetInstance().GetCharPeaceMeter().GetIDFromCharName(what);
            int p = C_GAMEMANAGER.GetInstance().GetCharPeaceMeter().GetPeace(id) + 1;

            C_GAMEMANAGER.GetInstance().GetCharPeaceMeter().SetPeace(id, p);
        }
        else
        {
            int id = C_GAMEMANAGER.GetInstance().GetPlanePeaceMeter().GetIDFromCharName(what);
            int p = C_GAMEMANAGER.GetInstance().GetPlanePeaceMeter().GetPeace(id) + 1;

            C_GAMEMANAGER.GetInstance().GetPlanePeaceMeter().SetPeace(id, p);
        }
    }
    public void OkEnough()
    {
        enough.SetActive(false);
    }
}
