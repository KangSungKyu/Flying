using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        CreateView("치킨", new Vector2(100.0f, 400.0f),0);
        CreateView("돼지", new Vector2(300.0f, 400.0f),0);
        CreateView("펭귄", new Vector2(100.0f, 400.0f),1);
    }
	
    public void CreateView(string _name,Vector2 _pos,int _page)
    {
        GameObject instCap = C_GAMEMANAGER.GetInstance().GetObjectMgr().GetObject("CharView");

        if (instCap == null)
            return;

        GameObject inst = GameObject.Instantiate<GameObject>(instCap, canvChar.transform);
        
        inst.GetComponent<SetupCharView>().SettingChild(_name, _pos);
        inst.SetActive(false);

        inst.name = _name+"_View";
        inst.GetComponent<RectTransform>().localScale.Set(1.0f, 1.0f, 1.0f);
        Image img = inst.GetComponent<SetupCharView>().imgCh;
        Text txt = inst.GetComponent<SetupCharView>().txtCh;

        txt.text = "0/30";
        img.sprite = C_GAMEMANAGER.GetInstance().GetSpriteMgr().GetSprite(_name+"_Char");
        
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

    public void BeginDragView()
    {
        if (Input.touchCount > 0)
            start = Input.touches[0].position;
        else
            start = Input.mousePosition;
    }
    public void EndDragView()
    {
        if (Input.touchCount > 0)
        {
            end = Input.touches[0].position;
        }
        else
        {
            end = Input.mousePosition;
        }

        Vector2 dir = end - start;

        dir.Normalize();

        if (dir.y < 0.0f)
        {
            curPage = curPage + 1;

            if (curPage > maxPage)
                curPage = maxPage;
        }
        else
        {
            curPage = curPage - 1;

            if (curPage < 0)
                curPage = 0;
        }

        for (int i = 0; i < maxPage; ++i)
        {
            if (i == curPage)
            {
                foreach (GameObject g in dicPage[curPage])
                {
                    g.SetActive(true);
                }
            }
            else
            {
                foreach (GameObject g in dicPage[i])
                {
                    g.SetActive(false);
                }
            }
        }
    }

    public void BackMain()
    {
        C_GAMEMANAGER.GetInstance().ChangeScene("Main");
    }
}
