using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettCharSheet : MonoBehaviour {

    public Canvas canvChar;
    // Use this for initialization
    void Start ()
    {
        CreateView("치킨", new Vector2(100.0f, 400.0f));
        CreateView("돼지", new Vector2(300.0f, 400.0f));
        CreateView("펭귄", new Vector2(500.0f, 400.0f));
    }
	
    public void CreateView(string _name,Vector2 _pos)
    {
        GameObject instCap = C_GAMEMANAGER.GetInstance().GetObjectMgr().GetObject("CharView");

        if (instCap == null)
            return;

        GameObject inst = GameObject.Instantiate<GameObject>(instCap, canvChar.transform);
        
        inst.GetComponent<SetupCharView>().SettingChild(_name, _pos);

        inst.name = _name+"_View";
        inst.GetComponent<RectTransform>().localScale.Set(1.0f, 1.0f, 1.0f);
        Image img = inst.GetComponent<SetupCharView>().imgCh;
        Text txt = inst.GetComponent<SetupCharView>().txtCh;

        txt.text = "0/30";
        img.sprite = C_GAMEMANAGER.GetInstance().GetSpriteMgr().GetSprite(_name+"_Char");
    }
	// Update is called once per frame
	void Update () {
		
	}

    public void BackMain()
    {
        C_GAMEMANAGER.GetInstance().ChangeScene("Main");
    }
}
