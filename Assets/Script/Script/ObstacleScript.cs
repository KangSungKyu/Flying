using UnityEngine;
using System.Collections;
using System.Collections.Generic;

 class ObstacleScript : MonoBehaviour {
    float fPlayerSpeed;
    float Speed;
    SpriteRenderer renObj;
    public string strObjectName;
    public string strObjectHitName;
    public Sprite sp;
    public Sprite spHit;
    Vector3 vecDir;
    Dictionary<string,string> partDic;
    List<string> dontList;


    void Start()
    {
        spHit = C_GAMEMANAGER.GetInstance().GetSpriteMgr().GetSprite(strObjectHitName);
        sp = C_GAMEMANAGER.GetInstance().GetSpriteMgr().GetSprite(strObjectName);
        renObj = GetComponent<SpriteRenderer>();

        partDic = new Dictionary<string, string>();
        dontList = new List<string>();

        partDic.Add("Penguin_Bullet(Clone)", "Penguin_Bullet_Particle(Clone)");
        partDic.Add("Cat_Bullet(Clone)", "Cat_Bullet_Particle(Clone)");

        dontList.Add("Chicken_Bullet_Charge(Clone)");
        dontList.Add("Monkey_Bullet(Clone)");
        dontList.Add("Pig_Bullet_Charge(Clone)");
        dontList.Add("Sheep_Bullet_Charge(Clone)");
        dontList.Add("Penguin_Bullet_Charge(Clone)");
        dontList.Add("Panda_Bullet_Charge(Clone)");
        dontList.Add("Lion_Bullet_Charge(Clone)");
        dontList.Add("Cat_Bullet_Charge(Clone)");
        dontList.Add("Monkey_Bullet_Charge(Clone)");
    }
	
	void Update () {
        if (C_GAMEMANAGER.GetInstance().GetPlayer().GetState() == E_PLAYERSTATE.E_PLAYERRELEASE
            || C_GAMEMANAGER.GetInstance().GetPlayer().GetState() == E_PLAYERSTATE.E_PLAYERFEVERTIME)
        {
            
            fPlayerSpeed = C_GAMEMANAGER.GetInstance().GetPlayer().GetCurrentSpeed();
            float fverticalSpeed = C_GAMEMANAGER.GetInstance().GetPlayer().GetPlayerStats().m_fVerticalSpeed / 1500.0f;
            Speed = fPlayerSpeed / 1500.0f;
            transform.position = transform.position - new Vector3(Speed, fverticalSpeed);

            if (transform.position.x <= -15.00f)
            {
                Destroy(this.gameObject);
            }
        }
        
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "PlayerAttack")
        {
            //Destroy(col.gameObject);
            
            string ou = "";
            if(partDic.TryGetValue(col.gameObject.name,out ou))
            {
                if (GameObject.Find(ou) == null)
                {
                    GameObject par = C_GAMEMANAGER.GetInstance().GetObjectMgr().GetObject(ou.Substring(0,ou.IndexOf("(")));

                    par = GameObject.Instantiate(par);
                    par.transform.position = this.transform.position;
                    par.GetComponent<ParticleSystem>().Play();

                }
                else
                {
                    this.GetComponent<GameObject>().transform.position = this.transform.position;
                    this.GetComponent<GameObject>().GetComponent<ParticleSystem>().Play();
                }
            }

            if (dontList.IndexOf(col.gameObject.name) == -1)
                col.gameObject.SetActive(false);

            GameObject.FindGameObjectWithTag("Player").GetComponent<FlyScript>().WindButton();
            
            StopAllCoroutines();
            StartCoroutine(HitAction());
        }
        if(col.gameObject.tag == "Player")
        {
            StopAllCoroutines();
            StartCoroutine(PlayerHitAction());
        }
    }

    IEnumerator HitAction()
    {
        renObj.sprite = spHit;
        yield return new WaitForSeconds(0.05f);
        renObj.sprite = sp;
        Destroy(gameObject);
    }
    IEnumerator PlayerHitAction()
    {
        renObj.sprite = spHit;
        yield return new WaitForSeconds(0.05f);
        renObj.sprite = sp;
        Destroy(gameObject);

        int hp = C_GAMEMANAGER.GetInstance().GetPlayer().GetCurrentHP() - C_GAMEMANAGER.GetInstance().GetPlayer().GetReduceHP();
        C_GAMEMANAGER.GetInstance().GetPlayer().SetCurrentHP(hp);

        if (hp < 0)
            C_GAMEMANAGER.GetInstance().GetPlayer().SetState(E_PLAYERSTATE.E_PLAYERDIE);
    }
}


enum E_PARTICLEMODE
{
    E_IDLE,
    E_ENVIRONMENT,
    E_OBSTACLE,

}

