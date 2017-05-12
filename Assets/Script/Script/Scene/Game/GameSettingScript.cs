using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSettingScript : MonoBehaviour {

    GameObject player;

    Vector3 OriginPos;

    public GameObject blueDot;
    public GameObject redDot1, redDot2;

    //UI elements
    public GameObject ShotButton;
    public GameObject PullButton;
    public GameObject FevButton;
    public Image imgWindMeter;
    public Image imgLifeMeter;
    public Text textMeter;
    public Text textSpeed;
    public Text printEat;
    public Text textHeight;
    public Text textBCount;
    public Text textCBCount;
    public SpriteRenderer renChar;
    public SpriteRenderer renPlane;
    public SpriteRenderer renHand;
    public Button btnBackMain;

    float fITTimer;
    float fLauchPower;
    float faimAngle;
    float fMeter;
    Camera myCam;
    Vector3 vecAim;
    Vector3 vecReverseAim;

    float fMTimer; //mission timer update
    float fMCheck; //mission timer set

    public Vector3 rightPos;
    public Vector3 UpPos;
    public string strCharName;
    public string strPlaneName;
    public string strLauncherName;

    GameObject gobjLaunch;

    FeverMissionParent curFevMission;

    // Use this for initialization
    private void Awake()
    {
    }
    IEnumerator Start ()
    {
        strCharName = C_GAMEMANAGER.GetInstance().strSelectedCharName;
        strPlaneName = C_GAMEMANAGER.GetInstance().strSelectedPlaneName;
        strLauncherName = C_GAMEMANAGER.GetInstance().strSelectedLaunchName;

        C_GAMEMANAGER.GetInstance().GetPlayer().InitPlayer(strCharName, strPlaneName, strLauncherName);

        ChangeCharSprite(strCharName);
        ChangePlaneSprite(strPlaneName);
        ChangeHandSprite(strCharName);

        gobjLaunch = GameObject.Instantiate(C_GAMEMANAGER.GetInstance().GetObjectMgr().GetObject(strLauncherName));

        gobjLaunch.GetComponent<LauncherParent>().SetPlayer(this.gameObject);
        gobjLaunch.GetComponent<BoxCollider2D>().size = new Vector2(10.0f, 10000.0f);
        gobjLaunch.GetComponent<BoxCollider2D>().offset = new Vector2(100.0f, 5000.0f);

        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<FlyScript>().Init();

        AimSetting();

        while (C_GAMEMANAGER.GetInstance().GetPlayer().GetState() == E_PLAYERSTATE.E_PLAYERATTACH)
        {
            PullButton.SetActive(true);
            ShotButton.SetActive(false);
            FevButton.SetActive(false);

            if (Input.GetMouseButton(0))
            {
                gobjLaunch.GetComponent<LauncherParent>().AimEvent();
                AimEvent();
            }

            if (fLauchPower > 0)
                fLauchPower -= 0.01f;
            else if (fLauchPower > 1.0f)
                fLauchPower = 1.0f;

            gobjLaunch.GetComponent<LauncherParent>().SetLaunchPower(fLauchPower);
            gobjLaunch.GetComponent<LauncherParent>().EndAimEvent();

            yield return new WaitForSeconds(0.01f);
        }

        foreach (FeverMissionParent scr in FevButton.GetComponents<FeverMissionParent>())
        {
            scr.SetPlayer(this.gameObject);
            scr.SetCamera(myCam);
        }

        fMTimer = 0.0f;
        fMCheck = 20.0f; //초기 n초후에 시작후 추가로 설정
    }


    void ChangeCharSprite(string strCharName)
    {
        string strCharimage = strCharName + "_Char";
        Sprite spChar = C_GAMEMANAGER.GetInstance().GetSpriteMgr().GetSprite(strCharimage);
        renChar.sprite = spChar;
    }
    void ChangePlaneSprite(string strPlaneName)
    {
        string strPlaneimage = strPlaneName + "_Plane";
        Sprite spPlane = C_GAMEMANAGER.GetInstance().GetSpriteMgr().GetSprite(strPlaneimage);

        renPlane.sprite = spPlane;
    }
    void ChangeHandSprite(string strHandName)
    {
        string strHandimage = strHandName + "_Hand";
        Sprite spHand = C_GAMEMANAGER.GetInstance().GetSpriteMgr().GetSprite(strHandimage);

        Debug.Log(spHand);

        renHand.sprite = spHand;
    }

    void AimEvent()
    {
        vecAim = gobjLaunch.GetComponent<LauncherParent>().GetBlueDot();
        vecReverseAim = gobjLaunch.GetComponent<LauncherParent>().GetBeforeAim();

        blueDot.transform.position = gobjLaunch.GetComponent<LauncherParent>().GetBlueDot();

        blueDot.GetComponent<LineRenderer>().SetPosition(0, gobjLaunch.GetComponent<LauncherParent>().GetBlueDot());
        blueDot.GetComponent<LineRenderer>().SetPosition(1, vecReverseAim);
    }

    void AimSetting()
    {
        Vector3 diagonalPos;

        myCam = GameObject.Find("Main Camera").GetComponent<Camera>();

        OriginPos = player.transform.position;
        C_GAMEMANAGER.GetInstance().GetPlayer().SetState(E_PLAYERSTATE.E_PLAYERATTACH);
        float size = 60.0f;
        fLauchPower = 0.0f;

        gobjLaunch.GetComponent<LauncherParent>().SetOriginPos(OriginPos);

        diagonalPos = new Vector3(0.5f, 0.5f);
        diagonalPos.Normalize();
        diagonalPos = diagonalPos * size + player.transform.position;
        float fMaxDegree = C_GAMEMANAGER.GetInstance().GetPlayer().GetPlayerStats().m_fMaxDegree;
        float fMinDegree = C_GAMEMANAGER.GetInstance().GetPlayer().GetPlayerStats().m_fMinDegree;

        rightPos = Quaternion.Euler(0.0f, 0.0f, fMinDegree) * Vector2.right * size + OriginPos;
        UpPos = Quaternion.Euler(0.0f, 0.0f, fMaxDegree) * Vector2.right * size + OriginPos;


        redDot1 = Instantiate(C_GAMEMANAGER.GetInstance().GetObjectMgr().GetObject("RedDot")) as GameObject;
        redDot2 = Instantiate(C_GAMEMANAGER.GetInstance().GetObjectMgr().GetObject("RedDot")) as GameObject;
        blueDot = Instantiate(C_GAMEMANAGER.GetInstance().GetObjectMgr().GetObject("BlueDot")) as GameObject;

        redDot1.transform.position = rightPos;
        redDot2.transform.position = UpPos;
        blueDot.transform.position = diagonalPos;

        redDot1.GetComponent<LineRenderer>().SetPosition(0, rightPos);
        redDot1.GetComponent<LineRenderer>().SetPosition(1, player.transform.position);

        redDot2.GetComponent<LineRenderer>().SetPosition(0, UpPos);
        redDot2.GetComponent<LineRenderer>().SetPosition(1, player.transform.position);

        blueDot.GetComponent<LineRenderer>().SetPosition(0, diagonalPos);
        blueDot.GetComponent<LineRenderer>().SetPosition(1, player.transform.position);
    }

    public void LauchButton()
    {
        if (C_GAMEMANAGER.GetInstance().GetPlayer().GetState() != E_PLAYERSTATE.E_PLAYERATTACH)
        {
            return;
        }
        if (fLauchPower < 1.0f)
            fLauchPower += 0.15f;

        gobjLaunch.GetComponent<LauncherParent>().SetLaunchPower(fLauchPower);

        if (fLauchPower >= 1.0f)
        {
            fLauchPower = Mathf.Min(fLauchPower, 1.0f);
            LauchEvent();
        }
    }


    public void LauchEvent()
    {
        gobjLaunch.GetComponent<LauncherParent>().BeforeLaunchEvent();

        float MinPow = 0.0f;
        float MaxPow = C_GAMEMANAGER.GetInstance().GetPlayer().GetPlayerStats().m_fMaxSpeed;
        float Pow = Mathf.Lerp(MinPow, MaxPow, fLauchPower);

        vecAim = gobjLaunch.GetComponent<LauncherParent>().GetBlueDot();

        Vector3 vecPower = (vecAim - OriginPos).normalized * Pow * 1.0f;
        float fHorizontalPower = vecPower.x * 2.0f;
        float fVerticalPower = vecPower.y * 2.0f;

        Debug.Log("Pow : " + Pow);
        Debug.Log("HorizonPower : " + fHorizontalPower.ToString());
        Debug.Log("fVerticalPower : " + fVerticalPower.ToString());
        Debug.Log("Angle " + gobjLaunch.GetComponent<LauncherParent>().GetAimAngle().ToString());

        C_GAMEMANAGER.GetInstance().GetPlayer().SetState(E_PLAYERSTATE.E_PLAYERRELEASE);
        C_GAMEMANAGER.GetInstance().GetPlayer().SetRealPos(new Vector3(0.0f, 0.0f, 0.0f));
        C_GAMEMANAGER.GetInstance().GetPlayer().SetCurrentSpeed(fHorizontalPower);
        C_GAMEMANAGER.GetInstance().GetPlayer().SetVerticalSpeed(fVerticalPower);

        Destroy(blueDot);
        Destroy(redDot1);
        Destroy(redDot2);

        gobjLaunch.GetComponent<BoxCollider2D>().size = new Vector2(10.0f, 10000.0f);
        gobjLaunch.GetComponent<BoxCollider2D>().offset = new Vector2(100.0f, 5000.0f);
        gobjLaunch.GetComponent<LauncherParent>().LaunchEvent();

        PullButton.SetActive(false);
        ShotButton.SetActive(true);
        FevButton.SetActive(true);

        StartCoroutine(SpeedReducer());
        StartCoroutine(WindReducer());
    }
    public void WindButton()
    {
        if (C_GAMEMANAGER.GetInstance().GetPlayer().GetState() == E_PLAYERSTATE.E_PLAYERRELEASE &&
            FevButton.GetComponent<MissionEvent>().src == null)
        {
            float fWind = C_GAMEMANAGER.GetInstance().GetPlayer().GetWindMeter();
            if (fWind >= 100)
                return;
            fWind = fWind + 10;
            C_GAMEMANAGER.GetInstance().GetPlayer().SetWindMeter(fWind);
        }
    }

    public IEnumerator SpeedReducer()
    {
        while (C_GAMEMANAGER.GetInstance().GetPlayer().GetCurrentSpeed() >= 0)
        {
            C_GAMEMANAGER.GetInstance().GetPlayer().SetCurrentSpeed(C_GAMEMANAGER.GetInstance().GetPlayer().GetCurrentSpeed()
                - C_GAMEMANAGER.GetInstance().GetPlayer().SpeedReducer());
            C_GAMEMANAGER.GetInstance().GetPlayer().SetVerticalSpeed(C_GAMEMANAGER.GetInstance().GetPlayer().GetPlayerStats().m_fVerticalSpeed
              - C_GAMEMANAGER.GetInstance().GetPlayer().GetGravitySpeed() * player.GetComponent<FlyScript>().fGravityScore);

            int nSpeed = (int)(C_GAMEMANAGER.GetInstance().GetPlayer().GetCurrentSpeed() / 10.0f);
            textSpeed.text = nSpeed.ToString() + "m/s";
            
            fMeter = C_GAMEMANAGER.GetInstance().GetPlayer().GetRealPos().x;

            int nMeter = (int)fMeter;

            if (nMeter < 1000)
            {
                textMeter.text = nMeter.ToString() + "m";
            }
            else
            {
                float fMet = fMeter / 1000;
                textMeter.text = fMet.ToString("N1") + "Km";
            }
            /*
            if (C_GAMEMANAGER.GetInstance().GetPlayer().GetCurrentSpeed() > C_GAMEMANAGER.GetInstance().GetPlayer().SpeedReducer() + 1.0f)
            {
                C_GAMEMANAGER.GetInstance().GetPlayer().SetSpeedReducer(C_GAMEMANAGER.GetInstance().GetPlayer().SpeedReducer() + Time.deltaTime*0.5f);
            }
            if (C_GAMEMANAGER.GetInstance().GetPlayer().GetVerticalSpeed() > C_GAMEMANAGER.GetInstance().GetPlayer().GetGravitySpeed()+1.0f)
            {
                C_GAMEMANAGER.GetInstance().GetPlayer().SetGravitySpeed(C_GAMEMANAGER.GetInstance().GetPlayer().GetGravitySpeed() + Time.deltaTime * 0.5f);
            }
            //*/

            yield return new WaitForSeconds(0.02f);

        }
    }
    public IEnumerator WindReducer()
    {
        while (C_GAMEMANAGER.GetInstance().GetPlayer().GetState() != E_PLAYERSTATE.E_PLAYERDIE)
        {
            float fWind = C_GAMEMANAGER.GetInstance().GetPlayer().GetWindMeter();
            if (C_GAMEMANAGER.GetInstance().GetPlayer().GetWindMeter() > 0)
            {
                fWind = fWind - 0.5f;
                C_GAMEMANAGER.GetInstance().GetPlayer().SetWindMeter(fWind);
            }
            else if (C_GAMEMANAGER.GetInstance().GetPlayer().GetWindMeter() < 0)
            {
                C_GAMEMANAGER.GetInstance().GetPlayer().SetWindMeter(0);
            }
            
            float fRate = fWind / 100;
            imgWindMeter.fillAmount = fRate;
            
            Color32 color = new Color(1 - fRate, 0, fRate, 1);
            imgWindMeter.color = color;

            if (C_GAMEMANAGER.GetInstance().GetPlayer().GetWindMeter() >= 100)
            {
                StartCoroutine(FeverTime());
                break;
            }

            yield return new WaitForSeconds(0.02f);
        }

    }
    IEnumerator FeverTime()
    {
        GetComponent<CutSceneCtrl>().Fever();
        
        C_GAMEMANAGER.GetInstance().GetPlayer().SetState(E_PLAYERSTATE.E_PLAYERFEVERTIME);
        float fSpeedBuffer = C_GAMEMANAGER.GetInstance().GetPlayer().GetCurrentSpeed();
        float fVSpeedBuffer = C_GAMEMANAGER.GetInstance().GetPlayer().GetVerticalSpeed();
        float fSpeed = fSpeedBuffer;
        float fVSpeed = fVSpeedBuffer;
        

        while (C_GAMEMANAGER.GetInstance().GetPlayer().GetWindMeter() > 0)
        {
            float fWind = C_GAMEMANAGER.GetInstance().GetPlayer().GetWindMeter();
            fWind = fWind - 0.5f;
            C_GAMEMANAGER.GetInstance().GetPlayer().SetWindMeter(fWind);
            float fRate = fWind / 100;
            imgWindMeter.fillAmount = fRate;
            fSpeed = fSpeed + 10f * fRate*Time.deltaTime;
            fVSpeed = fVSpeed + 10f * fRate*Time.deltaTime;
            Color32 color = new Color(1 - fRate, 0, fRate, 1);
            imgWindMeter.color = color;

            C_GAMEMANAGER.GetInstance().GetPlayer().SetCurrentSpeed(fSpeed);
            C_GAMEMANAGER.GetInstance().GetPlayer().SetVerticalSpeed(fVSpeed);

            yield return new WaitForSeconds(0.02f);
        }

        C_GAMEMANAGER.GetInstance().GetPlayer().SetState(E_PLAYERSTATE.E_PLAYERRELEASE);
        StartCoroutine(WindReducer());
    }
    // Update is called once per frame
    void Update () {

        if (fITTimer < 1.0f && C_GAMEMANAGER.GetInstance().GetPlayer().GetCurrentEat() != "")
        {
            printEat.text = "Eat : " + C_GAMEMANAGER.GetInstance().GetPlayer().GetCurrentEat();
            fITTimer += Time.deltaTime;
        }
        else
        {
            C_GAMEMANAGER.GetInstance().GetPlayer().SetCurrentEat("");
            printEat.text = "Eat : ";
            fITTimer = 0.0f;
        }

        if (C_GAMEMANAGER.GetInstance().GetPlayer().GetState() != E_PLAYERSTATE.E_PLAYERDIE)
        {
            float fRate = (float)(C_GAMEMANAGER.GetInstance().GetPlayer().GetCurrentHP()) / (float)(C_GAMEMANAGER.GetInstance().GetPlayer().GetMaxHP());
            imgLifeMeter.fillAmount = fRate;

            Color32 color = new Color(1 - fRate, fRate, 0);
            imgLifeMeter.color = color;

            float h = C_GAMEMANAGER.GetInstance().GetPlayer().GetRealPos().y;
            if (h < 1000.0f)
            {
                textHeight.text = h + "m";
            }
            else
            {
                float fMet = h / 1000;
                textHeight.text = fMet.ToString("N1") + "Km";
            }

            textBCount.text = C_GAMEMANAGER.GetInstance().GetPlayer().GetCurrentBulletCount().ToString();
            textCBCount.text = C_GAMEMANAGER.GetInstance().GetPlayer().GetCurrentChargeBulletCount().ToString();

            //==========

            if (fMTimer >= fMCheck && FevButton.GetComponentInChildren<Text>().text == "Wind")
            {
                string[] str =
                {
                    "Roll",
                    "Ver",
                    "Hor"
                };

                int sel = Random.Range(0, str.Length);

                string what = str[sel];
                int i = 0;

                foreach (FeverMissionParent src in FevButton.GetComponents<FeverMissionParent>())
                {
                    src.enabled = i == sel;

                    if (src.enabled)
                        curFevMission = src;

                    i++;
                }

                FevButton.GetComponent<MissionEvent>().src = curFevMission;
                FevButton.GetComponentInChildren<Text>().text = what;

                fMCheck = Random.Range(10.0f, 17.0f);
                fMTimer = 0.0f;
            }

            if (C_GAMEMANAGER.GetInstance().GetPlayer().GetState() == E_PLAYERSTATE.E_PLAYERRELEASE)
                fMTimer += Time.deltaTime;
        }
        if (C_GAMEMANAGER.GetInstance().GetPlayer().GetState() == E_PLAYERSTATE.E_PLAYERDIE)
        {
            StopAllCoroutines();
            textSpeed.text = 0.ToString() + "m/s";
            player.transform.position = Vector2.zero;

            C_GAMEMANAGER.GetInstance().GetPlayer().SetRealPos(Vector3.zero);
            C_GAMEMANAGER.GetInstance().GetPlayer().SetCurrentSpeed(0.0f);
            C_GAMEMANAGER.GetInstance().GetPlayer().SetVerticalSpeed(0.0f);
            C_GAMEMANAGER.GetInstance().SetCMHeight(10.0f);

            btnBackMain.gameObject.SetActive(true);
        }
    }

    private void OnApplicationQuit()
    {
        C_GAMEMANAGER.GetInstance().GetSaveLoadCtr().SaveXML();
    }

    private void OnApplicationPause(bool pause)
    {
        C_GAMEMANAGER.GetInstance().GetSaveLoadCtr().SaveXML();
    }
}
