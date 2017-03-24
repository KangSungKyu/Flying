using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSelManCtrl : MonoBehaviour {

    public string strCharName;
    // Use this for initialization
    Vector3 prev;
    string curPlayPath;
    bool bStop = true;
    bool bClick = false;

    void Start () {
       
        GetComponent<SpriteRenderer>().sprite = C_GAMEMANAGER.GetInstance().GetSpriteMgr().GetSprite(strCharName + "_CharSel");
        GetComponentsInChildren<SpriteRenderer>()[2].sprite = C_GAMEMANAGER.GetInstance().GetSpriteMgr().GetSprite(strCharName + "_Leg");
        GetComponentsInChildren<SpriteRenderer>()[3].sprite = C_GAMEMANAGER.GetInstance().GetSpriteMgr().GetSprite(strCharName + "_Leg");

        if(C_GAMEMANAGER.GetInstance().GetPlayer().GetIHaveChar(strCharName))
            SettingMovement();
    }
	void SettingMovement()
    {
        string[] strp =
        {
            "CharSelMove1",
            "CharSelMove2",
            "CharSelMove3",
            "CharSelMove4",
        };
        curPlayPath = strp[Random.Range(0, strp.Length)];

        Vector3[] path = iTweenPath.GetPath(curPlayPath);
        for (int i = 0; i < path.Length; ++i)
        {
            path[i] += this.gameObject.transform.position;
        }

        iTween.MoveTo(this.gameObject, iTween.Hash("path", path, "speed", 0.35f, "loopType", iTween.LoopType.pingPong));

        prev = transform.position;

        bStop = false;

        GetComponent<Animator>().SetTrigger("IdleToMove");
        GetComponent<Animator>().ResetTrigger("MoveToIdle");
    }
	// Update is called once per frame
	void Update () {

        bool bXFlip = (prev - transform.position).x > 0.0f;

        GetComponent<SpriteRenderer>().flipX = bXFlip;
        GetComponentsInChildren<SpriteRenderer>()[1].flipX = bXFlip;
        GetComponentsInChildren<SpriteRenderer>()[2].flipX = bXFlip;
        GetComponentsInChildren<SpriteRenderer>()[3].flipX = bXFlip;
        
        if(bXFlip)
        {
            GetComponentsInChildren<SpriteRenderer>()[2].sortingOrder = 2;
            GetComponentsInChildren<SpriteRenderer>()[3].sortingOrder = 1;

        }
        else
        {
            GetComponentsInChildren<SpriteRenderer>()[2].sortingOrder = 1;
            GetComponentsInChildren<SpriteRenderer>()[3].sortingOrder = 2;
        }

        prev = transform.position;
    }
    private void OnMouseDown()
    {
        if (!bClick)
            bClick = true;
    }
    private void OnMouseUp()
    {
        if (bClick)
        {
            if (!bStop)
                ItsMe();
            else
                SettingMovement();

            bClick = false;
        }
    }
    public void ItsMe()
    {
        if (!C_GAMEMANAGER.GetInstance().GetPlayer().GetIHaveChar(strCharName))
            return;

        iTween.Stop(this.gameObject);
        bStop = true;
        iTween.PunchPosition(this.gameObject, new Vector3(0.0f, 2.5f, 0.0f), 4.0f);

        GetComponent<Animator>().ResetTrigger("IdleToMove");
        GetComponent<Animator>().SetTrigger("MoveToIdle");

        C_GAMEMANAGER.GetInstance().strSelectedCharName = strCharName;
    }
}
