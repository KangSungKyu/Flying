using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCtrl : MonoBehaviour
{
    GameObject upgrade;
    Dropdown drop;
    public bool bChar = true;
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
    public void ChangeSelectChar(int _n)
    {
        //sprite 변경
    }
    public void Upgrade()
    {
        string who = drop.options[drop.value].text;

        if (bChar)
        {
            int id = C_GAMEMANAGER.GetInstance().GetCharPeaceMeter().GetIDFromCharName(who);

            C_GAMEMANAGER.GetInstance().GetCharPeaceMeter().SetNextLevel(id);
        }
        else
        {
            int id = C_GAMEMANAGER.GetInstance().GetPlanePeaceMeter().GetIDFromCharName(who);

            C_GAMEMANAGER.GetInstance().GetPlanePeaceMeter().SetNextLevel(id);
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