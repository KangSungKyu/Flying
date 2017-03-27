using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SettCharSheet : MonoBehaviour {

    public Canvas canvChar;
    // Use this for initialization
    Dictionary<int, List<GameObject>> dicPage;
    int curPage = 0;
    Vector2 start, end;
    int maxPage = 10;

    void Start ()
    {
        dicPage = new Dictionary<int, List<GameObject>>();
        
        CreateView("펭귄", new Vector2(000.0f, 000.0f),0);
        CreateView("고양이", new Vector2(1.0f, -1.0f), 0);
        CreateView("양", new Vector2(-1.0f, 1.0f), 0);
        CreateView("코끼리", new Vector2(-1.0f, 1.0f), 1);
        CreateView("판다", new Vector2(1.0f, -1.0f), 1);


        SettingCurrentPage(curPage);
    }
	
    public void CreateView(string _name,Vector2 _pos,int _page)
    {
        GameObject instCap = C_GAMEMANAGER.GetInstance().GetObjectMgr().GetObject("CharSel_Man");
        
        if (instCap == null)
            return;

        GameObject inst = GameObject.Instantiate<GameObject>(instCap);

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

    public void BeginDragView(BaseEventData _data)
    {
        start = (_data as PointerEventData).position;
    }
    public void EndDragView(BaseEventData _data)
    {
        end = (_data as PointerEventData).position;

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
    }

    public void BackMain()
    {
        C_GAMEMANAGER.GetInstance().ChangeScene("Main");
    }
}
