using UnityEngine;
using System.Collections;

public class VirusCtrl : AttackObsParent
{
    public override void Init(GameObject _me)
    {
        base.Init(_me);
    }

    public override void BeginAction()
    {
        base.BeginAction();

        GameObject weapon = GameObject.Instantiate(C_GAMEMANAGER.GetInstance().GetObjectMgr().GetObject("Virus_Weapon"));

        weapon.transform.position = me.transform.position;
    }

    public override void Action()
    {
        base.Action();
    }

    public override void EndActtion()
    {
        base.EndActtion();
    }

    public override void SettingAction()
    {
        base.SettingAction();
    }
}
