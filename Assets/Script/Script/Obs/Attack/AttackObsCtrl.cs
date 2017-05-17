using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackObsCtrl : MonoBehaviour {

    public string strSel = "";
    public float fHP = 20.0f;
    public float fMaxHP = 20.0f;
    public float fSpeed = 10.0f;
    public float fAttack = 5.0f;

    AttackObsParent Ctrl;

    Dictionary<string, AttackObsParent> dicCtrls;

	// Use this for initialization
	void Start () {
        Ctrl = null;

        dicCtrls = new Dictionary<string, AttackObsParent>();

        dicCtrls.Add("술취한새",new DrunkBirdCtrl());
        dicCtrls.Add("외계인", new AlianCtrl());
        dicCtrls.Add("먼지", new DustCtrl());
        dicCtrls.Add("바이러스", new VirusCtrl());
        dicCtrls.Add("바이러스무기", new VirusWeaponCtrl());
        dicCtrls.Add("운석", new MeteoCtrl());
        dicCtrls.Add("스패너", new SpanorCtrl());

        Setting();
	}
	
	// Update is called once per frame
	void Update () {
		if(Ctrl != null)
        {
            Ctrl.Action();

            Ctrl.fHP = fHP;
            Ctrl.fMaxHP = fMaxHP;
            Ctrl.fSpeed = fSpeed;

            if (Ctrl.IsDone())
                Ctrl.EndActtion();
        }
	}

    public void Setting()
    {
        Ctrl = dicCtrls[strSel];

        if (Ctrl != null)
        {
  
            Ctrl.Init(this.gameObject);
            Ctrl.SettingAction();
            Ctrl.BeginAction();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlayerAttack")
        {
            float dmg = C_GAMEMANAGER.GetInstance().GetPlayer().GetPlayerStats().m_fBulletPower;

            if(other.gameObject.name.IndexOf("_Charge") != 0)
            {
                dmg = C_GAMEMANAGER.GetInstance().GetPlayer().GetPlayerStats().m_fChargeBulletPower;
            }

            fHP = Mathf.Max(fHP - dmg,0.0f);
        }
    }
}
