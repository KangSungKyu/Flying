using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetupCharView : MonoBehaviour {

    public string id;
    public Image imgCh;
    public Text txtCh;
	// Use this for initialization
	void Start () {
	}
	
    public void SettingChild(string _id,Vector2 _pos)
    {
        transform.position = _pos;
        id = _id;
        imgCh = gameObject.GetComponentInChildren<Image>();
        txtCh = gameObject.GetComponentInChildren<Text>();
    }
	// Update is called once per frame
	void Update () {
        int idx = C_GAMEMANAGER.GetInstance().GetIDFromCharName(id);
        txtCh.text = C_GAMEMANAGER.GetInstance().GetPeace(idx) + " / " + C_GAMEMANAGER.GetInstance().GetMaxPeace(idx);
	}
}
