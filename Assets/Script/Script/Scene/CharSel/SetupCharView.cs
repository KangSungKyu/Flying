using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetupCharView : MonoBehaviour {

    public string id;
    public Text txtPeace;
    public GameObject gobj;

	// Use this for initialization
	void Start () {
	}
	
    public void SettingChild(string _id)
    {
        id = _id;
    }
	// Update is called once per frame
	void Update () {
        int idx = C_GAMEMANAGER.GetInstance().GetCharPeaceMeter().GetIDFromCharName(id);
        txtPeace.text = C_GAMEMANAGER.GetInstance().GetCharPeaceMeter().GetPeace(idx) + " / " + C_GAMEMANAGER.GetInstance().GetCharPeaceMeter().GetMaxPeace(idx);

        Vector3 v = gobj.transform.position;

        v.y += 2.5f;

        txtPeace.transform.position = v;
    }
}
