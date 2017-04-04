using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RouletteSetScript : MonoBehaviour {
    public Canvas can;
    public Image img;
    bool bStart = false;
    public GameObject peace;
    public Image sel;
    public Text keyText;

    int nSel = -1;
    List<GameObject> peaces;

    Rigidbody2D body;
    
    // Use this for initialization
    void Start () {

        body = img.GetComponent<Rigidbody2D>();

        //갯수만큼 균등하게 분할해서 선을 그려줌
        int num = C_GAMEMANAGER.GetInstance().GetCharPeaceMeter().GetIDCount();
        float fDegPeace = 360.0f / (float)(num);

        peaces = new List<GameObject>(0);

        for(int i = 0; i < num; ++i)
        {
            float fDeg = fDegPeace * i;
            GameObject ob = GameObject.Instantiate(peace,img.transform);

            peaces.Add(ob);

            peaces[i].transform.rotation = Quaternion.Euler(0.0f, 0.0f, fDeg);
            peaces[i].transform.position = can.transform.position;
            peaces[i].GetComponentInChildren<Text>().text = C_GAMEMANAGER.GetInstance().GetCharPeaceMeter().GetCharNameFromID(i);
            peaces[i].GetComponentInChildren<Image>().color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1.0f);
        }
        
	}
	
	// Update is called once per frame
    void Update () {
        if(nSel != -1 && bStart)
        {
            Debug.Log("You Got it!" + nSel);

            int pe = C_GAMEMANAGER.GetInstance().GetCharPeaceMeter().GetPeace(nSel);
            C_GAMEMANAGER.GetInstance().GetCharPeaceMeter().SetPeace(nSel, pe + 10);

            nSel = -1;
            bStart = false;

        }

        keyText.text = "Key : " + C_GAMEMANAGER.GetInstance().GetKeyCount();
	}

    private void FixedUpdate()
    {
        List<float> dist = new List<float>();

        for (int i = 0; i < peaces.Count; ++i)
        {
            dist.Add(Vector2.Distance(peaces[i].GetComponentInChildren<Image>().transform.position, sel.transform.position));
        }

        float d = Mathf.Min(dist.ToArray());
        int id = dist.IndexOf(d);

        if (body.angularVelocity <= 0.0f && body.rotation != 0.0f)
        {
            nSel = id;
            body.rotation = 0.0f;

            Debug.Log(dist[0] + " " + dist[1] + " " + dist[2] + " " + nSel);
        }
    }

    public void RouletteStart()
    {
        if (bStart == false)
        {
            if (C_GAMEMANAGER.GetInstance().GetKeyCount() > 0)
            {
                body.AddTorque(Random.Range(1000.0f, 1500.0f));
                bStart = true;
                C_GAMEMANAGER.GetInstance().SetKeyCount(C_GAMEMANAGER.GetInstance().GetKeyCount() - 1);
            }
        }
    }

    public void BackMain()
    {
        C_GAMEMANAGER.GetInstance().ChangeScene("Main");
    }
}
