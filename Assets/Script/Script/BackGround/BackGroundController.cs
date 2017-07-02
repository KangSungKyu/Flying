using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour {

    public static GameObject Cam;
    public float DownEnd;

    Vector3 mov;

    Vector3 bef;
    Vector3 after;

    Sprite[][] SpriteSet;

    GameObject[] BGS = new GameObject[9];
    int[][] Pos = new int[9][];

    // Use this for initialization
    private void Start()
    {
        Cam = GameObject.Find("Main Camera");
        bef = new Vector3(Cam.transform.position.x, Cam.transform.position.y, Cam.transform.position.z);
        after = new Vector3(Cam.transform.position.x, Cam.transform.position.y, Cam.transform.position.z);

        for(int a=0; a < 9; a++)
        {
            BGS[a]=Cam.transform.Find("BGSet").transform.GetChild(a).gameObject;
            Pos[a] = new int[2];
            Pos[a][0] = a % 3;
            Pos[a][1] = a / 3;
        }
    }


    private void Update()
    {
        after = Cam.transform.position;
        mov = (bef - after)*0.5f;

        for (int a = 0; a < 9; a++)
        {
            BGS[a].transform.Translate(mov);

            if (BGS[a].transform.localPosition.x < -512)
            {
                Debug.LogWarning("3 Actived");
                ChangePos(BGS[a], 3);
            }

            if (BGS[a].transform.localPosition.y > 256)
            {
                Debug.LogWarning("2 Actived");
                ChangePos(BGS[a], 2);
            }

            if (BGS[a].transform.localPosition.y < -256)
            {
                Debug.LogWarning("1 Actived");
                ChangePos(BGS[a], 1);
            }
        }
        bef = after;
	}

    void ChangePos(GameObject obj, int flag)
    {
        int posx=0, posy=0;
        switch (flag)
        {
            case 1:

                posx = obj.GetComponent<BackGroundMover>().posx;
                posy = ++obj.GetComponent<BackGroundMover>().posy;

                obj.transform.Translate(0, 512, 0);

                break;
            case 2:

                posx = obj.GetComponent<BackGroundMover>().posx;
                posy = --obj.GetComponent<BackGroundMover>().posy;

                obj.transform.Translate(0, -512, 0);

                break;
            case 3:

                posx = ++obj.GetComponent<BackGroundMover>().posx;
                posy = obj.GetComponent<BackGroundMover>().posy;

                obj.transform.Translate(1024, 0, 0);

                break;
        }

        if (posx < SpriteSet.Length&&posy<SpriteSet[posx].Length&&posx>=0&&posy>=0)
        {
            obj.GetComponent<SpriteRenderer>().sprite = SpriteSet[posx][posy];
        }
    }
}
