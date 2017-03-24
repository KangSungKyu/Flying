using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

class C_GAMEMANAGER
{
    private C_OBJECTMANGER m_cObjectMgr;
    private S_SCENEDATA m_sScene;
    private C_PLAYER m_cPlayer;
    private C_DATAMANAGER m_cDataMgr;
    private C_SPRITEMANAGER m_cSpriteMgr;
    private Dictionary<string,int> m_dicID;
    private Dictionary<int, string> m_dicName;
    private Dictionary<int, int> m_dicMaxFromLevel; //cur max -> next max
    private List<int> m_arrPeace;
    private List<int> m_arrMaxPeace;
    private int m_nKey;

    bool m_bInit = false;

    public string strSelectedCharName = "";
    public string strSelectedPlaneName = "";
    public string strSelectedLaunchName = "";

    #region ApplySingleton
    private static C_GAMEMANAGER m_cInstance;
    private C_GAMEMANAGER()
    {
        m_cObjectMgr = new C_OBJECTMANGER();
        m_cDataMgr = new C_DATAMANAGER();
        m_sScene = new S_SCENEDATA();
        m_cPlayer = new C_PLAYER();
        m_cSpriteMgr = new C_SPRITEMANAGER();

        if (m_dicID != null)
            m_dicID.Clear();
        if (m_dicID == null)
            m_dicID = new Dictionary<string, int>();

        if (m_dicName != null)
            m_dicName.Clear();
        if (m_dicName == null)
            m_dicName = new Dictionary<int, string>();

        AddID("치킨", 0);
        AddID("펭귄", 1);
        AddID("돼지", 2);

        if (m_dicMaxFromLevel != null)
            m_dicMaxFromLevel.Clear();
        if (m_dicMaxFromLevel == null)
        {
            m_dicMaxFromLevel = new Dictionary<int, int>();
            m_dicMaxFromLevel[0] = 30;
            m_dicMaxFromLevel[30] = 50;
            m_dicMaxFromLevel[50] = 70;
            m_dicMaxFromLevel[70] = 90;
        }

        if (m_arrPeace == null)
        {
            m_arrPeace = new List<int>();

            for (int i = 0; i < m_dicID.Count; ++i)
                m_arrPeace.Add(0);
        }
        if (m_arrMaxPeace == null)
        {
            m_arrMaxPeace = new List<int>();

            for (int i = 0; i < m_dicID.Count; ++i)
                m_arrMaxPeace.Add(m_dicMaxFromLevel[m_arrPeace[i]]);
        }

    }
    public static C_GAMEMANAGER GetInstance()
    {
        if(m_cInstance == null)
        {
            m_cInstance = new C_GAMEMANAGER();
        }
        return m_cInstance;
    }
    #endregion

    public C_OBJECTMANGER GetObjectMgr()
    {
        return m_cObjectMgr;
    }
    public C_SPRITEMANAGER GetSpriteMgr()
    {
        return m_cSpriteMgr;
    }
    public C_DATAMANAGER GetDataMgr()
    {
        return m_cDataMgr;
    }
    public void AddID(string _name,int _id)
    {
        int id = 0;

        if (!m_dicID.TryGetValue(_name, out id))
        {
            m_dicID.Add(_name, _id);
            m_dicName.Add(_id, _name);
        }
    }
    public int GetIDCount()
    {
        return m_dicID.Count;
    }
    public void SetKeyCount(int _n)
    {
        m_nKey = _n;
    }
    public int GetKeyCount()
    {
        return m_nKey;
    }
    public void SetPeace(int _who, int _peace)
    {
        if (0 <= _who && _who < m_arrPeace.Count)
        {
            m_arrPeace[_who] = _peace;

            if (m_arrPeace[_who] >= m_arrMaxPeace[_who])
            {
                m_arrMaxPeace[_who] = m_dicMaxFromLevel[m_arrPeace[_who]];
                m_arrPeace[_who] = 0;
            }
        }
    }
    public void SetMaxPeace(int _who, int _peace)
    {
        if(0 <= _who && _who < m_arrMaxPeace.Count)
            m_arrMaxPeace[_who] = _peace;
    }
    public int GetPeace(int _who)
    {
        if(0 <= _who && _who < m_arrPeace.Count)
            return m_arrPeace[_who];
        return 0;
    }
    public int GetMaxPeace(int _who)
    {
        if(0 <= _who && _who < m_arrMaxPeace.Count)
            return m_arrMaxPeace[_who];
        return 0;
    }
    public string GetCharNameFromID(int _id)
    {
        return m_dicName[_id];
    }
    public void InitMgr()
    {
        if (m_bInit == true)
            return;


        m_cObjectMgr.InsertObject("Flying", Resources.Load("FlyingObject", typeof(GameObject)) as GameObject);
        m_cObjectMgr.InsertObject("Guide", Resources.Load("Prefab/Guid", typeof(GameObject)) as GameObject);
        m_cObjectMgr.InsertObject("Bullet", Resources.Load("Prefab/Bullet", typeof(GameObject)) as GameObject);
        m_cObjectMgr.InsertObject("Box_OB", Resources.Load("Prefab/Obstacle", typeof(GameObject)) as GameObject);
        m_cObjectMgr.InsertObject("Cloud", Resources.Load("Prefab/Cloud", typeof(GameObject)) as GameObject);
        m_cObjectMgr.InsertObject("CloudParticle", Resources.Load("Prefab/CloudParticle", typeof(GameObject)) as GameObject);
        m_cObjectMgr.InsertObject("EmptyItem_IM", Resources.Load("Prefab/Item", typeof(GameObject)) as GameObject);
        m_cObjectMgr.InsertObject("Penguin_Bullet_Particle", Resources.Load("Prefab/IceBullet_Part", typeof(GameObject)) as GameObject);
        m_cObjectMgr.InsertObject("Cat_Bullet_Particle", Resources.Load("Prefab/CatBullet_Part", typeof(GameObject)) as GameObject);
        m_cObjectMgr.InsertObject("Monkey_Bullet_F_Particle", Resources.Load("Prefab/MonkeyBullet_F_Part", typeof(GameObject)) as GameObject);
        m_cObjectMgr.InsertObject("Monkey_Bullet_Particle", Resources.Load("Prefab/MonkeyBullet_Part", typeof(GameObject)) as GameObject);

        m_cObjectMgr.InsertObject("돼지_Bullet", Resources.Load("Prefab/Char/Pig_Bullet", typeof(GameObject)) as GameObject);
        m_cObjectMgr.InsertObject("펭귄_Bullet", Resources.Load("Prefab/Char/Penguin_Bullet", typeof(GameObject)) as GameObject);
        m_cObjectMgr.InsertObject("치킨_Bullet", Resources.Load("Prefab/Char/Chicken_Bullet", typeof(GameObject)) as GameObject);
        m_cObjectMgr.InsertObject("판다_Bullet", Resources.Load("Prefab/Char/Panda_Bullet", typeof(GameObject)) as GameObject);
        m_cObjectMgr.InsertObject("사자_Bullet", Resources.Load("Prefab/Char/Lion_Bullet", typeof(GameObject)) as GameObject);
        m_cObjectMgr.InsertObject("코끼리_Bullet", Resources.Load("Prefab/Char/Elepha_Bullet", typeof(GameObject)) as GameObject);
        m_cObjectMgr.InsertObject("원숭이_Bullet", Resources.Load("Prefab/Char/Monkey_Bullet", typeof(GameObject)) as GameObject);
        m_cObjectMgr.InsertObject("고양이_Bullet", Resources.Load("Prefab/Char/Cat_Bullet", typeof(GameObject)) as GameObject);
        m_cObjectMgr.InsertObject("양_Bullet", Resources.Load("Prefab/Char/Sheep_Bullet", typeof(GameObject)) as GameObject);
        m_cObjectMgr.InsertObject("돼지_Bullet_Charge", Resources.Load("Prefab/Char/Pig_Bullet_Charge", typeof(GameObject)) as GameObject);
        m_cObjectMgr.InsertObject("펭귄_Bullet_Charge", Resources.Load("Prefab/Char/Penguin_Bullet_Charge", typeof(GameObject)) as GameObject);
        m_cObjectMgr.InsertObject("치킨_Bullet_Charge", Resources.Load("Prefab/Char/Chicken_Bullet_Charge", typeof(GameObject)) as GameObject);
        m_cObjectMgr.InsertObject("판다_Bullet_Charge", Resources.Load("Prefab/Char/Panda_Bullet_Charge", typeof(GameObject)) as GameObject);
        m_cObjectMgr.InsertObject("사자_Bullet_Charge", Resources.Load("Prefab/Char/Lion_Bullet_Charge", typeof(GameObject)) as GameObject);
        m_cObjectMgr.InsertObject("코끼리_Bullet_Charge", Resources.Load("Prefab/Char/Elepha_Bullet_Charge", typeof(GameObject)) as GameObject);
        m_cObjectMgr.InsertObject("원숭이_Bullet_Charge", Resources.Load("Prefab/Char/Monkey_Bullet_Charge", typeof(GameObject)) as GameObject);
        m_cObjectMgr.InsertObject("고양이_Bullet_Charge", Resources.Load("Prefab/Char/Cat_Bullet_Charge", typeof(GameObject)) as GameObject);
        m_cObjectMgr.InsertObject("양_Bullet_Charge", Resources.Load("Prefab/Char/Sheep_Bullet_Charge", typeof(GameObject)) as GameObject);

       
        m_cObjectMgr.InsertObject("CharSel_Man", Resources.Load("Prefab/CharSel_Man", typeof(GameObject)) as GameObject);
        m_cObjectMgr.InsertObject("RouletteBoardPeace", Resources.Load("Prefab/BoardPeace", typeof(GameObject)) as GameObject);

        m_cObjectMgr.InsertObject("RedDot", Resources.Load("Prefab/RedDot", typeof(GameObject)) as GameObject);
        m_cObjectMgr.InsertObject("BlueDot", Resources.Load("Prefab/BlueDot", typeof(GameObject)) as GameObject);


        //char
        //m_cSpriteMgr.InsertSprite("펭귄_Pack", Resources.Load("Texture/Character/Penguin_Pack", typeof(Sprite)) as Sprite);
        m_cSpriteMgr.InsertSprite("펭귄_Char", Resources.LoadAll("Texture/Character/Penguin_Char", typeof(Sprite))[0] as Sprite);
        //hand
        m_cSpriteMgr.InsertSprite("펭귄_Hand", Resources.LoadAll("Texture/Character/Penguin_Char", typeof(Sprite))[2] as Sprite);
        //char ver select
        m_cSpriteMgr.InsertSprite("펭귄_CharSel", Resources.LoadAll("Texture/Character/Penguin_Char", typeof(Sprite))[1] as Sprite);
        //leg
        m_cSpriteMgr.InsertSprite("펭귄_Leg", Resources.LoadAll("Texture/Character/Penguin_Char", typeof(Sprite))[3] as Sprite);

        m_cSpriteMgr.InsertSprite("돼지_Char", Resources.Load("Texture/Character/Pig_Char", typeof(Sprite)) as Sprite);
        //m_cSpriteMgr.InsertSprite("펭귄_Char", Resources.Load("Texture/Character/Penguin_Char", typeof(Sprite)) as Sprite);
        m_cSpriteMgr.InsertSprite("치킨_Char", Resources.Load("Texture/Character/Chicken_Char", typeof(Sprite)) as Sprite);
        m_cSpriteMgr.InsertSprite("사자_Char", Resources.Load("Texture/Character/Lion_Char", typeof(Sprite)) as Sprite);
        m_cSpriteMgr.InsertSprite("판다_Char", Resources.Load("Texture/Character/Panda_Char", typeof(Sprite)) as Sprite);
        m_cSpriteMgr.InsertSprite("코끼리_Char", Resources.Load("Texture/Character/Elepha_Char", typeof(Sprite)) as Sprite);
        m_cSpriteMgr.InsertSprite("원숭이_Char", Resources.Load("Texture/Character/Monkey_Char", typeof(Sprite)) as Sprite);
        m_cSpriteMgr.InsertSprite("고양이_Char", Resources.Load("Texture/Character/Cat_Char", typeof(Sprite)) as Sprite);
        m_cSpriteMgr.InsertSprite("양_Char", Resources.Load("Texture/Character/Sheep_Char", typeof(Sprite)) as Sprite);

        m_cSpriteMgr.InsertSprite("cloudHit", Resources.Load("Texture/cloudHit", typeof(Sprite)) as Sprite);
        m_cSpriteMgr.InsertSprite("cloud", Resources.Load("Texture/cloud1", typeof(Sprite)) as Sprite);

        m_cSpriteMgr.InsertSprite("ForwardWind", Resources.Load("Texture/ForwardWind", typeof(Sprite)) as Sprite);
        m_cSpriteMgr.InsertSprite("BackwardWind", Resources.Load("Texture/BackwardWind", typeof(Sprite)) as Sprite);
        m_cSpriteMgr.InsertSprite("RouletteKey", Resources.Load("Texture/RouletteKey", typeof(Sprite)) as Sprite);
        m_cSpriteMgr.InsertSprite("BulletGet", Resources.Load("Texture/BulletGet", typeof(Sprite)) as Sprite);
        m_cSpriteMgr.InsertSprite("ChargeGet", Resources.Load("Texture/ChargeGet", typeof(Sprite)) as Sprite);
        m_cSpriteMgr.InsertSprite("UpGo", Resources.Load("Texture/UpGo", typeof(Sprite)) as Sprite);
        m_cSpriteMgr.InsertSprite("DownGo", Resources.Load("Texture/DownGo", typeof(Sprite)) as Sprite);

        m_cSpriteMgr.InsertSprite("나뭇잎_Plane", Resources.Load("Texture/Leaf1", typeof(Sprite)) as Sprite);
        m_cSpriteMgr.InsertSprite("단풍잎_Plane", Resources.Load("Texture/Leaf2", typeof(Sprite)) as Sprite);

        m_cDataMgr.InitMgr();

        m_bInit = true;
    }
    public C_PLAYER GetPlayer()
    {
        return m_cPlayer;
    }
    public void ChangeScene(int nData)
    {
        m_sScene.m_nSceneNum = nData;
         SceneManager.LoadSceneAsync(m_sScene.m_nSceneNum);
    }
    public void ChangeScene(string name)
    {
        SceneManager.LoadSceneAsync(name);
        m_sScene.m_nSceneNum = SceneManager.GetSceneByName(name).buildIndex;
    }


    public int GetIDFromCharName(string _name)
    {
        int num = 0;

        if(m_dicID.TryGetValue(_name,out num))
        {
            return num;
        }
        return 0;
    }
}
 struct S_SCENEDATA
{
    public int m_nSceneNum;
    public float m_fProgress;
}
