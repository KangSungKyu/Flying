using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AngleScript : MonoBehaviour {

    public Vector3 mousePos;
    public Image Image;
    public Vector3 ImageVec;
    bool isPress;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
       

        if(isPress)
        {
            mousePos = Input.mousePosition;
            ImageVec = Image.rectTransform.position;

            Vector2 vecOrigin = new Vector3(1, 0);
            Vector2 vecDir = mousePos - ImageVec;
            vecDir.Normalize();


            float fDot = Vector2.Dot(vecOrigin, vecDir);
            float fAngle = Mathf.Acos(fDot);
            if (mousePos.y > ImageVec.y)
                fAngle = Mathf.Rad2Deg * fAngle;
            else
                fAngle = 360 - Mathf.Rad2Deg * fAngle;
            
            Image.rectTransform.rotation = Quaternion.Euler(new Vector3(0, 0, fAngle));
        }
        
        

    }
    public void ButtonPress(bool isButton)
    {
        isPress = isButton;
    }
    
    
}
