using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PowerButtonScript : MonoBehaviour {
    float fPower;
    public Image FillPower;
	
	IEnumerator Start () {
        
        while(true)
        {
            if(fPower > 0)
            {
                fPower -= 0.5f;
            }
            
            yield return new WaitForSeconds(0.02f);
        }
	}
	
	// Update is called once per frame
	void Update () {
        FillPower.fillAmount = fPower / 100;
        
	}
    public void PressButton()
    {
        fPower += 5.0f;
    }
}
