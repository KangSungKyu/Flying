using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingCtrl : MonoBehaviour {

    public Image img;
    public Text txt;
    AsyncOperation sync;

    // Use this for initialization
    void Start ()
    {
        sync = SceneManager.LoadSceneAsync("Main");
        sync.allowSceneActivation = false;
        StartCoroutine(Loading());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Loading()
    {
        while (true)
        {
            float rate = sync.progress;

            Color col = img.color;

            col.r = Random.Range(0.0f, rate);
            col.g = Random.Range(0.0f, rate);
            col.b = Random.Range(0.0f, rate);
            col.a = 1.0f;

            img.color = col;
            txt.text = "Loading\n" + rate * 100.0f + "%!";

            if(rate >= 0.9f)
            {
                sync.allowSceneActivation = true;
                break;
            }

            yield return null;
        }
    }
}
