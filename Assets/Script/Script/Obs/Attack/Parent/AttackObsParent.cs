using UnityEngine;
using System.Collections;

public class AttackObsParent 
{
    protected bool bRotate = false;
    protected GameObject me;
    protected GameObject player;
    protected bool bDone = false;
    protected float fDist = 0.0f;
    public float fHP;
    public float fMaxHP;
    public float fSpeed;

    public virtual void Init(GameObject _me)
    {
        me = _me;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public virtual void SettingAction() //init
    {

    }
    public virtual void BeginAction() //start
    {

    }
    public virtual void Action() //update
    {
        if (bDone == true)
            return;

        //homming player
        Vector2 vPlayer = player.transform.position;
        Vector2 vMe = me.transform.position;
        Vector2 dir = (vPlayer - vMe).normalized;
        float rate = 0.7f;
        float deg = Mathf.Acos(Vector2.Dot(Vector2.right, dir)) * Mathf.Rad2Deg;

        if (vPlayer.y < vMe.y)
            deg = 360.0f - deg;

        Vector3 d = (dir) * fSpeed * Time.deltaTime;
        d.z = 0.0f;

        if (bRotate == false)
            me.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180.0f-deg * rate);

        me.transform.position += d;
        fDist = Vector2.Distance(vPlayer, vMe);
    
        if (fDist < 0.001f)
            bDone = true;
    }
    public virtual void EndActtion() //done
    {
    }
    public bool IsDone()
    {
        return bDone;
    }
}
