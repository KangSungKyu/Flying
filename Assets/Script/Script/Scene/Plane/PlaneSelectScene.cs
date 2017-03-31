using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSelectScene : MonoBehaviour {

    Vector2 start;
    Vector2 end;
    int curPage;
    int maxPage = 10;

    public SpriteRenderer sprBG;

    // Use this for initialization
    Dictionary<int, List<GameObject>> dicPage;
    Dictionary<int, string> dicPageBG;


    // Use this for initialization
    void Start () {

        dicPage = new Dictionary<int, List<GameObject>>();
        dicPageBG = new Dictionary<int, string>();

        CreateView("나뭇잎", new Vector2(-2.560f, 1.000f), 0);
        CreateView("단풍잎", new Vector2(-1.180f, 1.000f), 0);
        CreateView("종이비행기", new Vector2(1.00f, 1.000f), 0);
        CreateView("달", new Vector2(-2.560f, 1.000f), 1);
        CreateView("풍선", new Vector2(-1.180f, 1.000f), 1);

        SettingCurrentPage(curPage);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CreateView(string _name, Vector2 _pos, int _page)
    {
        GameObject instCap = C_GAMEMANAGER.GetInstance().GetObjectMgr().GetObject("PlaneSel");

        if (instCap == null)
            return;

        GameObject inst = GameObject.Instantiate<GameObject>(instCap);

        inst.transform.position = _pos;
        inst.name = _name + "_View";

        inst.GetComponent<PlaneSelCtrl>().strPlaneName = _name;
        inst.GetComponent<PlaneSelCtrl>().SettingPlaneSprite();

        if (!C_GAMEMANAGER.GetInstance().GetPlayer().GetIHavePlane(_name))
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

        string str = "";

        if (dicPageBG.TryGetValue(curPage, out str))
        {
            Sprite spr = C_GAMEMANAGER.GetInstance().GetSpriteMgr().GetSprite(str);

            if (spr != null)
                sprBG.sprite = spr;
        }
    }

    public void BackMain()
    {
        C_GAMEMANAGER.GetInstance().ChangeScene("Main");
    }
}
