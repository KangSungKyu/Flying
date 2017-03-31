using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HorSlideMissionEvent : FeverMissionParent {
    
    int count = 0;
    int maxCount = 5;
    // Use this for initialization
    void Start ()
    {
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

        Vector2 p = vecPrvMouse;
        Vector2 v = vecCurMouse - p;

        float dist = v.magnitude;

        if (Mathf.Abs(p.y - vecCurMouse.y) < 1.5f &&
            dist > 3.0f)
        {
            count++;
        }

        if (count > maxCount)
        {
            count = 0;
            CompleteMission();
        }

        vecPrvMouse = vecEndMouse;

        Debug.Log(dist + " " + count);
    }
    public override void CompleteMission()
    {
        base.CompleteMission();

        C_GAMEMANAGER.GetInstance().GetPlayer().SetWindMeter(C_GAMEMANAGER.GetInstance().GetPlayer().GetWindMeter() + 70.0f);

    }
}
