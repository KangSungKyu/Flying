using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RollMissionEvent : FeverMissionParent {

    float prev = 0.0f;
    int count = 0;
	// Use this for initialization
	void Start () {
        prev = 0.0f;
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
        
        Vector2 v = vecCurMouse - (Vector2)this.transform.position;

        float deg = Mathf.Acos(Vector2.Dot(Vector2.right, v.normalized)) * Mathf.Rad2Deg;

        if(deg > 15.0f)
            prev += deg;

        if(prev > 360.0f)
        {
            count++;
            prev = 0.0f;
        }

        if(count > 5)
        {
            count = 0;
            CompleteMission();
        }

        Debug.Log(prev + " " + count);
    }
    public override void EndMotion(BaseEventData _data)
    {
        base.EndMotion(_data);
    }
    public override void CompleteMission()
    {
        C_GAMEMANAGER.GetInstance().GetPlayer().SetWindMeter(C_GAMEMANAGER.GetInstance().GetPlayer().GetWindMeter() + 50.0f);

        base.CompleteMission();
    }
}
