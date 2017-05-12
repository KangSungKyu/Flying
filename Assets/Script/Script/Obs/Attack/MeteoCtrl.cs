using UnityEngine;
using System.Collections;

public class MeteoCtrl : AttackObsParent
{
    public override void Init(GameObject _me)
    {
        bRotate = true;

        base.Init(_me);
    }

    public override void BeginAction()
    {
        base.BeginAction();
    }

    public override void Action()
    {
        base.Action();

        me.transform.Rotate(new Vector3(0.0f, 0.0f, 360.0f * Time.deltaTime));
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
