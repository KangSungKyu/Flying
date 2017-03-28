using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FeverMissionParent : MonoBehaviour
{
    protected GameObject player;
    protected Camera myCam;

    protected Vector2 vecBeginMouse;
    protected Vector2 vecEndMouse;
    protected Vector2 vecCurMouse;

    private void Start()
    {

    }
    private void Update()
    {

    }

    public virtual void BeginMotion(BaseEventData _data)
    {
        vecBeginMouse = (_data as PointerEventData).position;
    }
    public virtual void UpdateMoition(BaseEventData _data)
    {
        vecCurMouse = (_data as PointerEventData).position;
    }
    public virtual void EndMotion(BaseEventData _data)
    {
        vecEndMouse = (_data as PointerEventData).position;
    }
    public virtual void CompleteMission()
    {
        this.GetComponent<MissionEvent>().src = null;
        this.GetComponentInChildren<Text>().text = "Wind";
    }

    public void SetPlayer(GameObject _player)
    {
        player = _player;
    }

    public void SetCamera(Camera _cam)
    {
        myCam = _cam;
    }
}

