using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LoadingScript : MonoBehaviour {
    char[] SzArr;
    public string strInputText;
    public string strOutText;
	// Use this for initialization
	IEnumerator Start () {
        SceneManager.LoadScene(2);
        int nCount = 0;
        while(true)
        {
            if(nCount == strInputText.Length)
            {
                nCount = 0;
                strOutText = "";
            }
            strOutText = strOutText + strInputText[nCount];
            nCount++;
            GetComponent<Text>().text = strOutText;
            yield return new WaitForSeconds(0.25f);
            
        }
        
        
	}
	
	// Update is called once per frame
	void Update () {
	}
}
