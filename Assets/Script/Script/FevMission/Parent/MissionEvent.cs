using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MissionEvent : MonoBehaviour {

    public FeverMissionParent src;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BeginDrag(BaseEventData _data)
    {
        if(src)
            src.BeginMotion(_data);
    }
    public void EndDrag(BaseEventData _data)
    {
        if (src)
            src.EndMotion(_data);
    }
    public void Drag(BaseEventData _data)
    {
        if (src)
            src.UpdateMoition(_data);
    }
}
