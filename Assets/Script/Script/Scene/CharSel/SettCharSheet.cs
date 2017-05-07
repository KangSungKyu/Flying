using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SettCharSheet : MonoBehaviour {

    public Canvas canvChar;
    public Dropdown drop;
    public Dropdown drop_u;
    public SpriteRenderer sprBG;

    // Use this for initialization
    Dictionary<int, List<GameObject>> dicPage;
    Dictionary<int, string> dicPageBG;
    int curPage = 0;
    Vector2 start, end;
    int maxPage = 10;

    void Start ()
    {
        dicPage = new Dictionary<int, List<GameObject>>();
        dicPageBG = new Dictionary<int, string>();
        
        CreateView("펭귄", new Vector2(000.0f, -2.0f),0);
        CreateView("고양이", new Vector2(1.0f, -2.0f), 0);
        CreateView("양", new Vector2(-1.0f, -2.0f), 0);
        CreateView("코끼리", new Vector2(-1.0f, -2.0f), 1);
        CreateView("판다", new Vector2(1.0f, -2.0f), 1);

        dicPageBG.Add(0, "CharSel_BG");
        
        SettingCurrentPage(curPage);

        List<string> opt = new List<string>();

        opt.Add("펭귄");
        opt.Add("고양이");
        opt.Add("양");
        opt.Add("판다");
        opt.Add("코끼리");
        opt.Add("돼지");
        opt.Add("치킨");
        opt.Add("사자");

        drop.transform.parent.gameObject.GetComponent<CreatePeaceCtrl>().bChar = true;
        drop.ClearOptions();
        drop.AddOptions(opt);
        
        drop_u.transform.parent.gameObject.GetComponent<UpgradeCtrl>().bChar = true;
        drop_u.ClearOptions();
        drop_u.AddOptions(opt);
    }
	
    public void CreateView(string _name,Vector2 _pos,int _page)
    {
        GameObject instCap = C_GAMEMANAGER.GetInstance().GetObjectMgr().GetObject("CharSel_Man");
        
        if (instCap == null)
            return;

        GameObject inst = GameObject.Instantiate<GameObject>(instCap);
        Text peace = inst.GetComponentInChildren<Canvas>().GetComponentInChildren<Text>();

        peace.name = _name + "_Peace";

        inst.GetComponent<CharSelManCtrl>().peace = peace;

        inst.transform.position = _pos;
        inst.name = _name+"_View";

        inst.GetComponent<CharSelManCtrl>().strCharName = _name;
        inst.GetComponent<CharSelManCtrl>().SettingCharSprite();

        if(!C_GAMEMANAGER.GetInstance().GetPlayer().GetIHaveChar(_name))
        {
            Color c = new Color(1.0f, 1.0f, 1.0f, 1.0f);

            c /= 2.0f;
            c.a = 1.0f;

            inst.GetComponent<SpriteRenderer>().color = c;
            inst.GetComponentsInChildren<SpriteRenderer>()[1].color = c;
            inst.GetComponentsInChildren<SpriteRenderer>()[2].color = c;
            inst.GetComponentsInChildren<SpriteRenderer>()[3].color = c;

            inst.GetComponent<CharSelManCtrl>().Stop();
        }
        List<GameObject> li;

        dicPage.TryGetValue(_page, out li);

        if (li == null)
        {
            li = new List<GameObject>();
            li.Add(inst);

            dicPage.Add(_page, li);
        }
        else
        {
            li.Add(inst);
            dicPage[_page] = li;
        }
    }
	// Update is called once per frame
	void Update () {
 
	}
    
    public void BeginDragView(Vector2 _data)
    {
        start = _data;

        Debug.Log(start);
    }

    public void EndDragView(Vector2 _data)
    {
        end = _data;

        Debug.Log(end);

        Vector2 dir = (end - start).normalized;

        Debug.Log(dir.y);

        if(dir.y > 0.0f)
        {
            //up slider
            if(curPage < maxPage)
            {
                curPage++;
            }
        }
        else
        {
            //down slider
            if(curPage > 0)
            {
                curPage--;
            }
        }
        SettingCurrentPage(curPage);
    }

    public void SettingCurrentPage(int _page)
    {
        for(int i = 0; i < dicPage.Count; ++i)
        {
            if(i == _page)
            {
                foreach(GameObject o in dicPage[i])
                {
                    o.SetActive(true);
                }
            }
            else
            {
                foreach(GameObject o in dicPage[i])
                {
                    o.SetActive(false);
                }
            }
        }

        string str = "";

        if(dicPageBG.TryGetValue(curPage,out str))
        {
            Sprite spr = C_GAMEMANAGER.GetInstance().GetSpriteMgr().GetSprite(str);

            if (spr != null)
            {
                sprBG.sprite = spr;
                C_GAMEMANAGER.GetInstance().SetCurSelBG(str);
            }
        }
    }

    public void BackMain()
    {
        C_GAMEMANAGER.GetInstance().ChangeScene("Main");
    }

    private void OnApplicationQuit()
    {
        C_GAMEMANAGER.GetInstance().GetSaveLoadCtr().SaveXML();
    }
    private void OnApplicationPause(bool pause)
    {
        C_GAMEMANAGER.GetInstance().GetSaveLoadCtr().SaveXML();
    }
}
