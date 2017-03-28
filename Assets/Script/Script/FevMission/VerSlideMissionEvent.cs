using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VerSlideMissionEvent : FeverMissionParent
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    public override void BeginMotion(BaseEventData _data)
    {
        base.BeginMotion(_data);
    }
    public override void UpdateMoition(BaseEventData _data)
    {
        base.UpdateMoition(_data);
    }
    public override void EndMotion(BaseEventData _data)
    {
        base.EndMotion(_data);
    }
    public override void CompleteMission()
    {
        base.CompleteMission();
    }
}
