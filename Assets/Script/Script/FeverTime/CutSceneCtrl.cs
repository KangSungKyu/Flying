using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutSceneCtrl : MonoBehaviour {

    public GameObject cut;
    public SpriteRenderer Body;
    public SpriteRenderer plane;
    public GameObject fever;

    float timer = 0.0f; 
	// Use this for initialization
	void Start () {
    }
    public void Fever()
    {
        cut.SetActive(true);
        fever.SetActive(true);

        Body.sprite = C_GAMEMANAGER.GetInstance().GetSpriteMgr().GetSprite(C_GAMEMANAGER.GetInstance().strSelectedCharName + "_Char");
        plane.sprite = C_GAMEMANAGER.GetInstance().GetSpriteMgr().GetSprite(C_GAMEMANAGER.GetInstance().strSelectedPlaneName + "_Plane");
        cut.GetComponent<CutSceneMoveCtrl>().Set();
    }

    // Update is called once per frame
    void Update () {
        if (cut.activeSelf)
        {
            if (timer > 3.5f)
            {
                cut.SetActive(false);
                fever.SetActive(false);
                cut.GetComponent<CutSceneMoveCtrl>().Reset();

                timer = 0.0f;
            }
            timer += Time.deltaTime;
        }
	}
}
