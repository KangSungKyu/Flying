using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchSelectScene : MonoBehaviour {

    Vector2 start;
    Vector2 end;
    int curPage;
    int maxPage = 10;

    public SpriteRenderer sprBG;

    // Use this for initialization
    Dictionary<int, List<GameObject>> dicPage;
    Dictionary<int, string> dicPageBG;

    // Use this for initialization
    void Start ()
    {

        dicPage = new Dictionary<int, List<GameObject>>();
        dicPageBG = new Dictionary<int, string>();

        CreateView("투석기", new Vector2(-2.580f, 1.000f), 0);
        CreateView("새총", new Vector2(-1.180f, 1.000f), 0);
        CreateView("고릴라", new Vector2(-2.580f, 1.000f), 1);
        CreateView("공룡", new Vector2(-1.180f, 1.000f), 1);
        CreateView("물로켓", new Vector2(-2.580f, 1.000f), 2);
        CreateView("선풍기", new Vector2(-1.180f, 1.000f), 2);

        SettingCurrentPage(curPage);
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public void CreateView(string _name, Vector2 _pos, int _page)
    {
        GameObject instCap = C_GAMEMANAGER.GetInstance().GetObjectMgr().GetObject("LauncherSel");

        if (instCap == null)
            return;

        GameObject inst = GameObject.Instantiate<GameObject>(instCap);

        inst.transform.position = _pos;
        inst.name = _name + "_View";

        inst.GetComponent<LauncherSelCtrl>().strLaunchName = _name;
        inst.GetComponent<LauncherSelCtrl>().SettingLauncherSprite();

        if (!C_GAMEMANAGER.GetInstance().GetPlayer().GetIHaveLaunch(_name))
        {
            Color c = new Color(1.0f, 1.0f, 1.0f, 1.0f);

            c /= 2.0f;
            c.a = 1.0f;

            inst.GetComponentInChildren<SpriteRenderer>().color = c;
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

        if (Vector2.Distance(end, start) < 5.0f)
            return;
        
        Debug.Log(dir.y);

        if (dir.y > 0.0f)
        {
            //up slider
            if (curPage < maxPage)
            {
                curPage++;
            }
        }
        else
        {
            //down slider
            if (curPage > 0)
            {
                curPage--;
            }
        }
        SettingCurrentPage(curPage);
    }

    public void SettingCurrentPage(int _page)
    {
        for (int i = 0; i < dicPage.Count; ++i)
        {
            if (i == _page)
            {
                foreach (GameObject o in dicPage[i])
                {
                    o.SetActive(true);
                }
            }
            else
            {
                foreach (GameObject o in dicPage[i])
                {
                    o.SetActive(false);
                }
            }
        }

        string str = C_GAMEMANAGER.GetInstance().GetCurSelBG();
        Sprite spr = C_GAMEMANAGER.GetInstance().GetSpriteMgr().GetSprite(str);

        if (spr != null)
        {
            sprBG.sprite = spr;
            Color col = sprBG.color;
            col.a = 0.3f;
            iTween.ColorTo(sprBG.gameObject, col, 1.0f);
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
