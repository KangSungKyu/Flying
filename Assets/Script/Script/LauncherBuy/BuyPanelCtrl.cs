using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyPanelCtrl : MonoBehaviour {

    GameObject buy;
    Dropdown drop;
    public GameObject view;
    public Text cost;
    // Use this for initialization
    void Start () {

        buy = this.gameObject;
        buy.SetActive(false);

        drop = buy.GetComponentInChildren<Dropdown>();

        ChangeSelectChar(0);
    }
	
	// Update is called once per frame
	void Update () {
        string strN = drop.options[drop.value].text;

        strN += C_GAMEMANAGER.GetInstance().GetPlayer().GetLauncherLevel(strN);

        cost.text = C_GAMEMANAGER.GetInstance().GetCoin() + " / " + C_GAMEMANAGER.GetInstance().GetLauncherCost().GetCost(strN);
	}

    public void Setting()
    {
        ChangeSelectChar(0);
    }
    public void ChangeSelectChar(int _n)
    {
        string strN = drop.options[_n].text;
        view.GetComponent<Image>().sprite = C_GAMEMANAGER.GetInstance().GetSpriteMgr().GetSprite(strN + "_LauncherSel");
    }

    public void OnBuyPanel()
    {
        buy.SetActive(true);
    }
    public void Buy()
    {
        string strN = drop.options[drop.value].text;

        int coin = C_GAMEMANAGER.GetInstance().GetCoin();
        int cost = C_GAMEMANAGER.GetInstance().GetLauncherCost().GetCost(strN+ C_GAMEMANAGER.GetInstance().GetPlayer().GetLauncherLevel(strN));

        if(coin >= cost && !C_GAMEMANAGER.GetInstance().GetPlayer().GetIHaveLaunch(strN))
        {
            C_GAMEMANAGER.GetInstance().GetPlayer().SetIHaveLaunch(strN, true);
            C_GAMEMANAGER.GetInstance().SetCoin(coin - cost);
        }
        else
        {
            DontBuy();
        }
    }
    public void DontBuy()
    {

    }
    public void Cancel()
    {
        buy.SetActive(false);

    }
}
