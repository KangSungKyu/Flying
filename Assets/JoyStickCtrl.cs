using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStickCtrl : MonoBehaviour {

    // Use this for initialization
    public Transform transStick;

    Vector3 vecFirstPos;
    Vector3 vecDir;
    float fRadius;
    Vector3 vecLastPos;

    public bool bStart = false;
    public Vector3 vecLastDir;

	void Start () {
        fRadius = GetComponent<RectTransform>().sizeDelta.y * 0.25f;
        vecFirstPos = transStick.transform.position;

        float ca = transform.parent.GetComponent<RectTransform>().localScale.x;

        fRadius *= ca;
	}

    public void Select()
    {
        bStart = true;
    }

    public void StartDrag(BaseEventData _data)
    {
        PointerEventData point = _data as PointerEventData;
        Vector3 pos = point.position;

        vecDir = (pos - vecFirstPos).normalized;

        float dist = Vector3.Distance(pos, vecFirstPos);

        if (dist < fRadius)
            transStick.position = vecFirstPos + vecDir * dist;
        else
            transStick.position = vecFirstPos + vecDir * fRadius;
    }
    public void EndDrag(BaseEventData _data)
    {
        vecLastPos = transStick.position;
        vecLastDir = vecDir;

        transStick.position = vecFirstPos;
        vecDir = Vector3.zero;

        bStart = false;
    }
    
}
