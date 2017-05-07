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
    private int m_nKey;
    private float m_fCheckMaxHeight = 10.0f;
    private int m_nCoin = 0;
    private int m_nPlayCount = 5;
    private int m_nMaxPlayCount = 5;
    private int m_nScore = 0;
    private int m_nCash = 0;
    private string m_strCurSelBG = "CharSel_BG";

    bool m_bInit = false;

    Vector2 vecAddToPlayer = Vector2.zero;

    public string strSelectedCharName = "";
    public string strSelectedPlaneName = "";
    public string strSelectedLaunchName = "";

    private PeaceMeter CharPeaceM;
    private PeaceMeter PlanePeaceM;
    private PeaceMeter LauncherPeaceM;
    private JewelMeter JewelM;
    private LauncherCost LauncherCostM;
    private SaveLoadCtrl SaveLoadCtrlM;

    private Vector2 vecJoystickDir;

    #region ApplySingleton
    private static C_GAMEMANAGER m_cInstance;
    private C_GAMEMANAGER()
    {
        m_cObjectMgr = new C_OBJECTMANGER();
        m_cDataMgr = new C_DATAMANAGER();
        m_sScene = new S_SCENEDATA();
        m_cPlayer = new C_PLAYER();
        m_cSpriteMgr = new C_SPRITEMANAGER();

        CharPeaceM = new PeaceMeter();
        PlanePeaceM = new PeaceMeter();
        LauncherPeaceM = new PeaceMeter();
        JewelM = new JewelMeter();
        LauncherCostM = new LauncherCost();
        SaveLoadCtrlM = new SaveLoadCtrl();

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
    public void SetCMHeight(float _h)
    {
        m_fCheckMaxHeight = _h;
    }
    public float GetCMHeight()
    {
        return m_fCheckMaxHeight;
    }
    public void SetKeyCount(int _n)
    {
        m_nKey = _n;
    }
    public int GetKeyCount()
    {
        return m_nKey;
    }
    public void SetAddToPlayer(Vector2 _v)
    {
        vecAddToPlayer = _v;
    }
    public Vector2 GetAddToPlayer()
    {
        return vecAddToPlayer;
    }
    public void SetCoin(int _c)
    {
        m_nCoin = _c;
    }
    public int GetCoin()
    {
        return m_nCoin;
    }
    public void SetPlayCount(int _play)
    {
        m_nPlayCount = _play;
    }
    public int GetPlayCount()
    {
        return m_nPlayCount;
    }
    public int GetMaxPlayCount()
    {
        return m_nMaxPlayCount;
    }
    public void SetScore(int _s)
    {
        m_nScore = _s;
    }
    public int GetScore()
    {
        return m_nScore;
    }
    public void SetCash(int _cs)
    {
        m_nCash = _cs;
    }
    public int GetCash()
    {
        return m_nCash;
    }
    public void SetJoyStickDir(Vector2 _dir)
    {
        vecJoystickDir = _dir;
    }
    public Vector2 GetJoyStickDir()
    {
        return vecJoystickDir;
    }

    public PeaceMeter GetCharPeaceMeter()
    {
        return CharPeaceM;
    }
    public PeaceMeter GetPlanePeaceMeter()
    {
        return PlanePeaceM;
    }
    public PeaceMeter GetLauncherPeaceMeter()
    {
        return LauncherPeaceM;
    }
    public JewelMeter GetJewelMeter()
    {
        return JewelM;
    }
    public LauncherCost GetLauncherCost()
    {
        return LauncherCostM;
    }
    public SaveLoadCtrl GetSaveLoadCtr()
    {
        return SaveLoadCtrlM;
    }
    public void SetCurSelBG(string _name)
    {
        m_strCurSelBG = _name;
    }
    public string GetCurSelBG()
    {
        return m_strCurSelBG;
    }

    public void InitPeaceMeter()
    {
        CharPeaceM.Init();
        PlanePeaceM.Init();
        LauncherPeaceM.Init();
        JewelM.Init();

        CharPeaceM.m_cPlayer = PlanePeaceM.m_cPlayer = LauncherPeaceM.m_cPlayer = m_cPlayer;

        CharPeaceM.AddID("펭귄", 0);
        CharPeaceM.AddID("고양이", 1);
        CharPeaceM.AddID("양", 2);
        CharPeaceM.AddID("판다", 3);
        CharPeaceM.AddID("코끼리", 4);
        CharPeaceM.AddID("돼지", 5);
        CharPeaceM.AddID("치킨", 6);
        CharPeaceM.AddID("사자", 7);

        PlanePeaceM.AddID("종이비행기", 0);
        PlanePeaceM.AddID("나뭇잎", 1);
        PlanePeaceM.AddID("낙옆", 2);
        PlanePeaceM.AddID("풍선", 3);
        PlanePeaceM.AddID("달", 4);
        PlanePeaceM.AddID("민들래씨앗", 5);

        LauncherPeaceM.AddID("새총", 0);
        LauncherPeaceM.AddID("투석기", 1);
        LauncherPeaceM.AddID("공룡", 2);
        LauncherPeaceM.AddID("고릴라", 3);
        LauncherPeaceM.AddID("물로켓", 4);
        LauncherPeaceM.AddID("선풍기", 5);

        JewelM.SetCurrentJewelCount("다이아", 0);
        JewelM.SetCurrentJewelCount("루비", 0);
        JewelM.SetCurrentJewelCount("사파이어", 0);
        JewelM.SetCurrentJewelCount("토파츠", 0);
        JewelM.SetCurrentJewelCount("호박", 0);
        JewelM.SetCurrentJewelCount("에메랄드", 0);

        JewelM.SetExchangeJewelCount("다이아", 10);
        JewelM.SetExchangeJewelCount("루비", 15);
        JewelM.SetExchangeJewelCount("사파이어", 10);
        JewelM.SetExchangeJewelCount("토파츠", 15);
        JewelM.SetExchangeJewelCount("호박", 10);
        JewelM.SetExchangeJewelCount("에메랄드", 20);
    }

    public bool LoadPrefab(string _name, string _path)
    {
        GameObject obj = Resources.Load(_path, typeof(GameObject)) as GameObject;

        if(obj != null)
        {
            m_cObjectMgr.InsertObject(_name, obj);

            return true;
        }

        return false;
    }
    public bool LoadSprite(string _name,string _path)
    {
        Sprite spr = Resources.Load(_path, typeof(Sprite)) as Sprite;

        if(spr != null)
        {
            m_cSpriteMgr.InsertSprite(_name, spr);

            return true;
        }

        return false;
    }
    public bool LoadAllSprite(string _name,string _path,int _idx)
    {
        Sprite spr = Resources.LoadAll(_path, typeof(Sprite))[_idx] as Sprite;

        if(spr != null)
        {
            m_cSpriteMgr.InsertSprite(_name, spr);

            return true;
        }

        return false;
    }

    public void InitMgr()
    {
        if (m_bInit == true)
            return;

        InitPeaceMeter();

        //*
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

        //bullet
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
        m_cObjectMgr.InsertObject("PlaneSel", Resources.Load("Prefab/PlaneSel", typeof(GameObject)) as GameObject);
        m_cObjectMgr.InsertObject("LauncherSel", Resources.Load("Prefab/LauncherSel", typeof(GameObject)) as GameObject);
        m_cObjectMgr.InsertObject("RouletteBoardPeace", Resources.Load("Prefab/BoardPeace", typeof(GameObject)) as GameObject);

        m_cObjectMgr.InsertObject("RedDot", Resources.Load("Prefab/RedDot", typeof(GameObject)) as GameObject);
        m_cObjectMgr.InsertObject("BlueDot", Resources.Load("Prefab/BlueDot", typeof(GameObject)) as GameObject);

        //launcher
        m_cObjectMgr.InsertObject("새총", Resources.Load("Prefab/BirdGun", typeof(GameObject)) as GameObject);
        m_cObjectMgr.InsertObject("투석기", Resources.Load("Prefab/Catapult", typeof(GameObject)) as GameObject);
        m_cObjectMgr.InsertObject("선풍기", Resources.Load("Prefab/Pan", typeof(GameObject)) as GameObject);
        m_cObjectMgr.InsertObject("공룡", Resources.Load("Prefab/Dino", typeof(GameObject)) as GameObject);
        m_cObjectMgr.InsertObject("고릴라", Resources.Load("Prefab/Gorilla", typeof(GameObject)) as GameObject);
        m_cObjectMgr.InsertObject("물로켓", Resources.Load("Prefab/WRocket", typeof(GameObject)) as GameObject);


        //char
        m_cSpriteMgr.InsertSprite("펭귄_Char", Resources.LoadAll("Texture/Character/Penguin_Char", typeof(Sprite))[0] as Sprite);
        m_cSpriteMgr.InsertSprite("고양이_Char", Resources.LoadAll("Texture/Character/Cat_Char", typeof(Sprite))[0] as Sprite);
        m_cSpriteMgr.InsertSprite("양_Char", Resources.LoadAll("Texture/Character/Sheep_Char", typeof(Sprite))[0] as Sprite);
        m_cSpriteMgr.InsertSprite("판다_Char", Resources.LoadAll("Texture/Character/Panda_Char", typeof(Sprite))[0] as Sprite);
        m_cSpriteMgr.InsertSprite("코끼리_Char", Resources.LoadAll("Texture/Character/Elepha_Char", typeof(Sprite))[0] as Sprite);
        //hand
        m_cSpriteMgr.InsertSprite("펭귄_Hand", Resources.LoadAll("Texture/Character/Penguin_Char", typeof(Sprite))[2] as Sprite);
        m_cSpriteMgr.InsertSprite("고양이_Hand", Resources.LoadAll("Texture/Character/Cat_Char", typeof(Sprite))[2] as Sprite);
        m_cSpriteMgr.InsertSprite("양_Hand", Resources.LoadAll("Texture/Character/Sheep_Char", typeof(Sprite))[2] as Sprite);
        m_cSpriteMgr.InsertSprite("판다_Hand", Resources.LoadAll("Texture/Character/Panda_Char", typeof(Sprite))[2] as Sprite);
        m_cSpriteMgr.InsertSprite("코끼리_Hand", Resources.LoadAll("Texture/Character/Elepha_Char", typeof(Sprite))[2] as Sprite);
        //char ver select
        m_cSpriteMgr.InsertSprite("펭귄_CharSel", Resources.LoadAll("Texture/Character/Penguin_Char", typeof(Sprite))[1] as Sprite);
        m_cSpriteMgr.InsertSprite("고양이_CharSel", Resources.LoadAll("Texture/Character/Cat_Char", typeof(Sprite))[1] as Sprite);
        m_cSpriteMgr.InsertSprite("양_CharSel", Resources.LoadAll("Texture/Character/Sheep_Char", typeof(Sprite))[1] as Sprite);
        m_cSpriteMgr.InsertSprite("판다_CharSel", Resources.LoadAll("Texture/Character/Panda_Char", typeof(Sprite))[1] as Sprite);
        m_cSpriteMgr.InsertSprite("코끼리_CharSel", Resources.LoadAll("Texture/Character/Elepha_Char", typeof(Sprite))[1] as Sprite);
        //leg
        m_cSpriteMgr.InsertSprite("펭귄_Leg", Resources.LoadAll("Texture/Character/Penguin_Char", typeof(Sprite))[3] as Sprite);
        m_cSpriteMgr.InsertSprite("고양이_Leg", Resources.LoadAll("Texture/Character/Cat_Char", typeof(Sprite))[3] as Sprite);
        m_cSpriteMgr.InsertSprite("양_Leg", Resources.LoadAll("Texture/Character/Sheep_Char", typeof(Sprite))[3] as Sprite);
        m_cSpriteMgr.InsertSprite("판다_Leg", Resources.LoadAll("Texture/Character/Panda_Char", typeof(Sprite))[3] as Sprite);
        m_cSpriteMgr.InsertSprite("코끼리_Leg", Resources.LoadAll("Texture/Character/Elepha_Char", typeof(Sprite))[3] as Sprite);


        m_cSpriteMgr.InsertSprite("돼지_Char", Resources.Load("Texture/Character/Pig_Char", typeof(Sprite)) as Sprite);
        m_cSpriteMgr.InsertSprite("치킨_Char", Resources.Load("Texture/Character/Chicken_Char", typeof(Sprite)) as Sprite);
        m_cSpriteMgr.InsertSprite("사자_Char", Resources.Load("Texture/Character/Lion_Char", typeof(Sprite)) as Sprite);
        m_cSpriteMgr.InsertSprite("원숭이_Char", Resources.Load("Texture/Character/Monkey_Char", typeof(Sprite)) as Sprite);
        
        //obs
        m_cSpriteMgr.InsertSprite("cloudHit", Resources.Load("Texture/cloudHit", typeof(Sprite)) as Sprite);
        m_cSpriteMgr.InsertSprite("cloud", Resources.Load("Texture/cloud1", typeof(Sprite)) as Sprite);

        //item
        m_cSpriteMgr.InsertSprite("ForwardWind", Resources.Load("Texture/ForwardWind", typeof(Sprite)) as Sprite);
        m_cSpriteMgr.InsertSprite("BackwardWind", Resources.Load("Texture/BackwardWind", typeof(Sprite)) as Sprite);
        m_cSpriteMgr.InsertSprite("RouletteKey", Resources.Load("Texture/RouletteKey", typeof(Sprite)) as Sprite);
        m_cSpriteMgr.InsertSprite("BulletGet", Resources.Load("Texture/BulletGet", typeof(Sprite)) as Sprite);
        m_cSpriteMgr.InsertSprite("ChargeGet", Resources.Load("Texture/ChargeGet", typeof(Sprite)) as Sprite);
        m_cSpriteMgr.InsertSprite("UpGo", Resources.Load("Texture/UpGo", typeof(Sprite)) as Sprite);
        m_cSpriteMgr.InsertSprite("DownGo", Resources.Load("Texture/DownGo", typeof(Sprite)) as Sprite);
        m_cSpriteMgr.InsertSprite("다이아", Resources.Load("Texture/Diamond", typeof(Sprite)) as Sprite);
        m_cSpriteMgr.InsertSprite("루비", Resources.Load("Texture/Ruby", typeof(Sprite)) as Sprite);
        m_cSpriteMgr.InsertSprite("사파이어", Resources.Load("Texture/Sapphire", typeof(Sprite)) as Sprite);
        m_cSpriteMgr.InsertSprite("토파츠", Resources.Load("Texture/Topaz", typeof(Sprite)) as Sprite);
        m_cSpriteMgr.InsertSprite("호박", Resources.Load("Texture/Amber", typeof(Sprite)) as Sprite);
        m_cSpriteMgr.InsertSprite("에메랄드", Resources.Load("Texture/Emerald", typeof(Sprite)) as Sprite);
        m_cSpriteMgr.InsertSprite("코인", Resources.Load("Texture/CoinIcon", typeof(Sprite)) as Sprite);

        //Plane
        m_cSpriteMgr.InsertSprite("나뭇잎_Plane", Resources.Load("Texture/Plane/Leaf1", typeof(Sprite)) as Sprite);
        m_cSpriteMgr.InsertSprite("낙옆_Plane", Resources.Load("Texture/Plane/Leaf2", typeof(Sprite)) as Sprite);
        m_cSpriteMgr.InsertSprite("종이비행기_Plane", Resources.Load("Texture/Plane/Paper", typeof(Sprite)) as Sprite);
        m_cSpriteMgr.InsertSprite("달_Plane", Resources.Load("Texture/Plane/Moon", typeof(Sprite)) as Sprite);
        m_cSpriteMgr.InsertSprite("풍선_Plane", Resources.Load("Texture/Plane/Balloon", typeof(Sprite)) as Sprite);
        m_cSpriteMgr.InsertSprite("민들래씨앗_Plane", Resources.Load("Texture/Plane/Flower", typeof(Sprite)) as Sprite);
        m_cSpriteMgr.InsertSprite("민들래씨앗2_Plane", Resources.Load("Texture/Plane/Flower2", typeof(Sprite)) as Sprite);


        //select plane
        m_cSpriteMgr.InsertSprite("나뭇잎_PlaneSel", Resources.Load("Texture/Selected/Plane/Leaf_PlaneSel", typeof(Sprite)) as Sprite);
        m_cSpriteMgr.InsertSprite("낙옆_PlaneSel", Resources.Load("Texture/Selected/Plane/Maple_PlaneSel", typeof(Sprite)) as Sprite);
        m_cSpriteMgr.InsertSprite("종이비행기_PlaneSel", Resources.Load("Texture/Selected/Plane/Paper_PlaneSel", typeof(Sprite)) as Sprite);
        m_cSpriteMgr.InsertSprite("달_PlaneSel", Resources.Load("Texture/Selected/Plane/Moon_PlaneSel", typeof(Sprite)) as Sprite);
        m_cSpriteMgr.InsertSprite("풍선_PlaneSel", Resources.Load("Texture/Selected/Plane/Balloon_PlaneSel", typeof(Sprite)) as Sprite);
        m_cSpriteMgr.InsertSprite("민들래씨앗_PlaneSel", Resources.Load("Texture/Selected/Plane/Flower_PlaneSel", typeof(Sprite)) as Sprite);
        //select launch
        m_cSpriteMgr.InsertSprite("새총_LauncherSel", Resources.Load("Texture/Selected/Launcher/BirdGun_LaunchSel", typeof(Sprite)) as Sprite);
        m_cSpriteMgr.InsertSprite("투석기_LauncherSel", Resources.Load("Texture/Selected/Launcher/Catapult_LaunchSel", typeof(Sprite)) as Sprite);
        m_cSpriteMgr.InsertSprite("선풍기_LauncherSel", Resources.Load("Texture/Selected/Launcher/Pan_LaunchSel", typeof(Sprite)) as Sprite);
        m_cSpriteMgr.InsertSprite("공룡_LauncherSel", Resources.Load("Texture/Selected/Launcher/Dino_LaunchSel", typeof(Sprite)) as Sprite);
        m_cSpriteMgr.InsertSprite("고릴라_LauncherSel", Resources.Load("Texture/Selected/Launcher/Gorilla_LaunchSel", typeof(Sprite)) as Sprite);
        m_cSpriteMgr.InsertSprite("물로켓_LauncherSel", Resources.Load("Texture/Selected/Launcher/WRocket_LaunchSel", typeof(Sprite)) as Sprite);

        //Select Scene BG
        m_cSpriteMgr.InsertSprite("CharSel_BG", Resources.Load("Texture/UI/CharSelBG", typeof(Sprite)) as Sprite);
        //*/

        m_cDataMgr.InitMgr();

        m_bInit = true;
        
        /*
        C_GAMEMANAGER.GetInstance().GetJewelMeter().SetCurrentJewelCount("다이아", 999999);
        C_GAMEMANAGER.GetInstance().GetJewelMeter().SetCurrentJewelCount("루비", 999999);
        C_GAMEMANAGER.GetInstance().GetJewelMeter().SetCurrentJewelCount("사파이어",999999);
        C_GAMEMANAGER.GetInstance().GetJewelMeter().SetCurrentJewelCount("토파츠",999999);
        C_GAMEMANAGER.GetInstance().GetJewelMeter().SetCurrentJewelCount("호박",999999);
        C_GAMEMANAGER.GetInstance().GetJewelMeter().SetCurrentJewelCount("에메랄드", 999999);
        C_GAMEMANAGER.GetInstance().SetCoin(9999999);
        C_GAMEMANAGER.GetInstance().SetPlayCount(999);
        //*/
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

}
 struct S_SCENEDATA
{
    public int m_nSceneNum;
    public float m_fProgress;
}
