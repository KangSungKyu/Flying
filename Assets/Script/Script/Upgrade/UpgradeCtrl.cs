using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UpgradeCtrl : MonoBehaviour
{
    GameObject upgrade;
    Dropdown drop;
    public bool bChar = true;
    public GameObject view;
    // Use this for initialization
    void Start()
    {
        upgrade = this.gameObject;
        upgrade.SetActive(false);

        drop = upgrade.GetComponentInChildren<Dropdown>();
        
        ChangeSelectChar(0);
    }

    // Update is called once per frame
    void Update()
    {
        //실시간으로 등급 갱신
    }
    public void Setting()
    {
        ChangeSelectChar(0);
    }
    public void ChangeSelectChar(int _n)
    {
        string strN = drop.options[_n].text;
        //sprite 변경
        if(bChar)
        {
            view.GetComponent<Image>().sprite = C_GAMEMANAGER.GetInstance().GetSpriteMgr().GetSprite(strN+"_CharSel");
        }
        else
        {
            view.GetComponent<Image>().sprite = C_GAMEMANAGER.GetInstance().GetSpriteMgr().GetSprite(strN + "_PlaneSel");
        }
    }
    public void Upgrade()
    {
        string who = drop.options[drop.value].text;

        if (bChar)
        {
            int id = C_GAMEMANAGER.GetInstance().GetCharPeaceMeter().GetIDFromCharName(who);
           
            if(C_GAMEMANAGER.GetInstance().GetCharPeaceMeter().SetNextLevel(id))
            {
                C_GAMEMANAGER.GetInstance().GetPlayer().SetCharLevel(who, C_GAMEMANAGER.GetInstance().GetPlayer().GetCharLevel(who) + 1);
            }
        }
        else
        {
            int id = C_GAMEMANAGER.GetInstance().GetPlanePeaceMeter().GetIDFromCharName(who);

            if(C_GAMEMANAGER.GetInstance().GetPlanePeaceMeter().SetNextLevel(id))
            {
                C_GAMEMANAGER.GetInstance().GetPlayer().SetPlaneLevel(who, C_GAMEMANAGER.GetInstance().GetPlayer().GetPlaneLevel(who) + 1);
            }
        }
    }
    public void OnUpgradePanel()
    {
        upgrade.SetActive(true);
    }
    public void Cancel()
    {
        upgrade.SetActive(false);
    }
}