using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainCtrl : MonoBehaviour {

    public GameObject[] terbgs;
    public int wcount;
    public int hcount;
    GameObject player;
    public float width;
    public float height;
    int prevIdx = -1;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        //width = terbgs[0].GetComponent<SpriteRenderer>().size.x;
        //height = terbgs[0].GetComponent<SpriteRenderer>().size.y;

        for (int i = 0; i < terbgs.Length; ++i)
            terbgs[i].SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        SettingTerrain();
	}

    void SettingTerrain()
    {
        Vector2 vPos = C_GAMEMANAGER.GetInstance().GetPlayer().GetRealPos();
        int x = (int)(vPos.x / width);
        int y = (int)(vPos.y / height);
        int idx = y * wcount + x;

        terbgs[idx].SetActive(true);

        Debug.Log(idx);

        if(prevIdx != -1 && idx != prevIdx)
        {
            terbgs[prevIdx].SetActive(false);
        }

        prevIdx = idx;
    }
}
