using UnityEngine;
using System.Collections;

public class DustCtrl : AttackObsParent
{
    public override void Init(GameObject _me)
    {
        base.Init(_me);
    }

    public override void BeginAction()
    {
        base.BeginAction();
    }

    public override void Action()
    {
        base.Action();

        if (fDist < 0.0001f)
            bDone = true;
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
