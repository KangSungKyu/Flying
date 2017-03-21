using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FlyScript : MonoBehaviour {

    Rigidbody2D myBody;
    C_ObjectPool bulls;
    C_ObjectPool chars; //충전탄들

    bool bBACheck;

    float fChargeTimer;
    float fLauchPower;
    float faimAngle;
    
    float fVerticalPower;
    float fHorizontalPower;
    string strPrevEat;

    Vector3 OriginPos;

    public GameObject blueDot;
    public GameObject redDot1, redDot2;

    //UI elements
    public GameObject ShotButton;
	public GameObject PullButton;
    public GameObject ToqButton;
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
    public Button btnBackMain;
    float fMeter;

    Vector2 vBCtrl;

    public float fGravityScore;
    public float fToqueCtrlDeg;

    Camera myCam;

    float fITTimer;

    Vector3 vecAim;
    Vector3 vecReverseAim;
	public Vector3 rightPos;
	public Vector3 UpPos;
    public string strCharName;
    public string strPlaneName;
    public string strLauncherName;

    // Use this for initialization
    IEnumerator Start() {
        //파일 이름,찾으려는 데이터의 아이디, 찾으려는 데이터의 항목.

        string strText = C_GAMEMANAGER.GetInstance().GetDataMgr().GetData("Character", "돼지", "B.power");
        //float nDataTest = float.Parse(strText);
        //Debug.Log(nDataTest);
        
        C_GAMEMANAGER.GetInstance().GetPlayer().InitPlayer(strCharName,strPlaneName,strLauncherName);
        ChangeCharSprite(strCharName);
        ChangePlaneSprite(strPlaneName);
        

		GetComponent<BoxCollider2D> ().size = new Vector2 (C_GAMEMANAGER.GetInstance ().GetPlayer ().GetPlayerStats ().m_fColliderScale,
			C_GAMEMANAGER.GetInstance ().GetPlayer ().GetPlayerStats ().m_fColliderScale);

        AimSetting();

        while (C_GAMEMANAGER.GetInstance().GetPlayer().GetState() == E_PLAYERSTATE.E_PLAYERATTACH)
        {
			PullButton.SetActive (true);
			ShotButton.SetActive (false);
            ToqButton.SetActive(false);

            if(Input.GetMouseButton(0))
            {
                AimEvent();
            }
            if (fLauchPower > 0)
                fLauchPower -= 0.01f;
            else if(fLauchPower > 1.0f)
                fLauchPower = 1.0f;
            transform.position = OriginPos + (vecReverseAim - OriginPos ) * fLauchPower;
            yield return new WaitForSeconds(0.01f);
        }

        fITTimer = 0.0f;
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

    void AimEvent()
    {
        Vector3 vecMouseWorld = myCam.ScreenToWorldPoint(Input.mousePosition);
        vecMouseWorld.z = 0.0f;
        
        float fLength = Vector3.Distance(transform.position, vecMouseWorld);
        
        if(fLength > 3.0f)
        {
            return;
        }


        Vector3 vecDir = OriginPos - vecMouseWorld;
        vecDir = Vector3.Normalize(vecDir) * 3.0f;
        
        Vector3 vecBlueDot = OriginPos + vecDir ;
        
        Vector3 vecBeforeDot = OriginPos - vecDir;
        
        Vector3 vecBlueDir = vecBlueDot - OriginPos;
        faimAngle = Mathf.Acos(Vector3.Dot(new Vector3(1,0,0), vecBlueDir.normalized)) * Mathf.Rad2Deg;
      
		float  faimAngle2 = Mathf.Acos(Vector3.Dot(new Vector3(0, 1, 0), vecBlueDir.normalized)) * Mathf.Rad2Deg;

		if(faimAngle2 > C_GAMEMANAGER.GetInstance().GetPlayer().GetPlayerStats().m_fMaxDegree )
        {
			faimAngle = C_GAMEMANAGER.GetInstance().GetPlayer().GetPlayerStats().m_fMinDegree;
			vecBlueDot = rightPos;
			vecBeforeDot = OriginPos - new Vector3(90-C_GAMEMANAGER.GetInstance().GetPlayer().GetPlayerStats().m_fMinDegree,
				C_GAMEMANAGER.GetInstance().GetPlayer().GetPlayerStats().m_fMinDegree).normalized * 3.0f;
        }
		else if(faimAngle >  C_GAMEMANAGER.GetInstance().GetPlayer().GetPlayerStats().m_fMaxDegree )
        {
			faimAngle =  C_GAMEMANAGER.GetInstance().GetPlayer().GetPlayerStats().m_fMaxDegree;
            vecBlueDot = UpPos;
			vecBeforeDot = OriginPos - new Vector3(90-C_GAMEMANAGER.GetInstance().GetPlayer().GetPlayerStats().m_fMaxDegree,
				C_GAMEMANAGER.GetInstance().GetPlayer().GetPlayerStats().m_fMaxDegree).normalized * 3.0f;
        }
        
		vecAim = vecBlueDot;
		vecReverseAim = vecBeforeDot;

        blueDot.transform.position = vecBlueDot;

        blueDot.GetComponent<LineRenderer>().SetPosition(0, vecBlueDot);
        blueDot.GetComponent<LineRenderer>().SetPosition(1, vecBeforeDot);


    }

    
    void AimSetting()
    {
        
        Vector3 diagonalPos;

        myCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        
        OriginPos = transform.position;
        C_GAMEMANAGER.GetInstance().GetPlayer().SetState(E_PLAYERSTATE.E_PLAYERATTACH);
        bulls = new C_ObjectPool(C_GAMEMANAGER.GetInstance().GetPlayer().GetBulletCount(), C_GAMEMANAGER.GetInstance().GetObjectMgr().GetObject(strCharName + "_Bullet"));
        chars = new C_ObjectPool(C_GAMEMANAGER.GetInstance().GetPlayer().GetChargeBulletCount(), C_GAMEMANAGER.GetInstance().GetObjectMgr().GetObject(strCharName + "_Bullet_Charge"));
        fLauchPower = 0.0f;



        diagonalPos = new Vector3(0.5f, 0.5f);
        diagonalPos.Normalize();
        diagonalPos = diagonalPos * 3.0f + transform.position;
		float fMaxDegree = C_GAMEMANAGER.GetInstance ().GetPlayer ().GetPlayerStats().m_fMaxDegree;
		float fMinDegree = C_GAMEMANAGER.GetInstance ().GetPlayer ().GetPlayerStats ().m_fMinDegree;

		rightPos =new Vector3(90-fMinDegree,fMinDegree).normalized * 3.0f + OriginPos;
		UpPos = new Vector3(90-fMaxDegree,fMaxDegree).normalized * 3.0f + OriginPos;


        redDot1 = Instantiate(C_GAMEMANAGER.GetInstance().GetObjectMgr().GetObject("RedDot")) as GameObject;
        redDot2 = Instantiate(C_GAMEMANAGER.GetInstance().GetObjectMgr().GetObject("RedDot")) as GameObject;
        blueDot = Instantiate(C_GAMEMANAGER.GetInstance().GetObjectMgr().GetObject("BlueDot")) as GameObject;


        redDot1.transform.position = rightPos;
        redDot2.transform.position = UpPos;
        blueDot.transform.position = diagonalPos;

        redDot1.GetComponent<LineRenderer>().SetPosition(0, rightPos);
        redDot1.GetComponent<LineRenderer>().SetPosition(1, transform.position);

        redDot2.GetComponent<LineRenderer>().SetPosition(0, UpPos);
        redDot2.GetComponent<LineRenderer>().SetPosition(1, transform.position);

        blueDot.GetComponent<LineRenderer>().SetPosition(0, diagonalPos);
        blueDot.GetComponent<LineRenderer>().SetPosition(1, transform.position);

    }

	public void BeginChargeBullet()
    {
        fChargeTimer = 0.0f;
    }

    public void UpdateChargeBullet()
    {
        if(fChargeTimer < C_GAMEMANAGER.GetInstance().GetPlayer().GetPlayerStats().m_fChargeBulletTime)
        {
            fChargeTimer += Time.deltaTime;
        }
    }

    public void EndChargeBullet()
    {
        if(fChargeTimer < C_GAMEMANAGER.GetInstance().GetPlayer().GetPlayerStats().m_fChargeBulletTime)
        {
            ShotEvent();
        }
        else
        {
            ChargeShotEvent();
        }

        fChargeTimer = 0.0f;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Forward")
        {
            GetComponent<Rigidbody2D>().isKinematic = true;
            float fOriginSpeed = C_GAMEMANAGER.GetInstance().GetPlayer().GetCurrentSpeed();
            float fResultSpeed = fOriginSpeed * 1.5f;
            C_GAMEMANAGER.GetInstance().GetPlayer().SetCurrentSpeed(fResultSpeed);
            GetComponent<Rigidbody2D>().isKinematic = false;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 150.0f));
            Destroy(col.gameObject);
        }
        else if(col.tag == "Backward")
        {
            GetComponent<Rigidbody2D>().isKinematic = true;

            float fOriginSpeed = C_GAMEMANAGER.GetInstance().GetPlayer().GetCurrentSpeed();
            float fResultSpeed = fOriginSpeed * 0.5f;
            C_GAMEMANAGER.GetInstance().GetPlayer().SetCurrentSpeed(fResultSpeed);
            GetComponent<Rigidbody2D>().isKinematic = false;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 150.0f));
            Destroy(col.gameObject);
        }
        else if(col.name == "LaunchPad")
        {
            C_GAMEMANAGER.GetInstance().GetPlayer().SetState(E_PLAYERSTATE.E_PLAYERRELEASE);
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            
            C_GAMEMANAGER.GetInstance().GetPlayer().SetCurrentSpeed(fHorizontalPower);
            C_GAMEMANAGER.GetInstance().GetPlayer().SetVerticalSpeed(fVerticalPower);   

            PullButton.SetActive (false);
			ShotButton.SetActive (true);
            ToqButton.SetActive(true);
            StartCoroutine(SpeedReducer());
            StartCoroutine(WindReducer());

        }


    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Plat")
        {
            float fSpeed = C_GAMEMANAGER.GetInstance().GetPlayer().GetCurrentSpeed();
            float fFriction = fSpeed * 0.4f;
            fSpeed = fSpeed - fFriction;

            C_GAMEMANAGER.GetInstance().GetPlayer().SetCurrentSpeed(fSpeed);

        }
        
    }
    public void LauchButton()
    {
        if(C_GAMEMANAGER.GetInstance().GetPlayer().GetState() != E_PLAYERSTATE.E_PLAYERATTACH)
        {
            return;
        }
        if (fLauchPower < 1.0f)
            fLauchPower += 0.15f;

        if (fLauchPower >= 1.0f)
        {
            fLauchPower = 1.0f;
            LauchEvent();
            
        }
    }

    public void ShotEvent()
    {
        if (C_GAMEMANAGER.GetInstance().GetPlayer().GetCurrentBulletCount() > 0)
        {
            GameObject BulletInstance = bulls.Alloc();

            if (BulletInstance == null)
            {
                bulls.AllFree();

                BulletInstance = bulls.Alloc();

                if (BulletInstance == null)
                    return;
            }

            BulletInstance.GetComponent<Transform>().position = transform.FindChild("ArrowSpot").transform.position;
            float fDegree = C_GAMEMANAGER.GetInstance().GetPlayer().GetPlayerStats().m_fBulletDegree;
            Vector3 dir = C_GAMEMANAGER.GetInstance().GetPlayer().GetRealForwardDir();
            float fBulletShotAng = 0.0f;

            Vector2 jdir = (-ShotButton.GetComponent<JoyStickCtrl>().vecLastDir).normalized;

            if (jdir.y > 0.0f)
                fBulletShotAng = Mathf.Acos(Vector2.Dot(Vector2.right, jdir)) * Mathf.Rad2Deg;
            else
                fBulletShotAng = 360.0f - Mathf.Acos(Vector2.Dot(Vector2.right, jdir)) * Mathf.Rad2Deg;

            dir = Quaternion.Euler(0.0f, 0.0f, fBulletShotAng + fDegree) * dir;


            BulletInstance.transform.rotation = Quaternion.Euler(0.0f, 0.0f, transform.eulerAngles.z + fBulletShotAng + fDegree);
            BulletInstance.GetComponent<Rigidbody2D>().velocity = dir.normalized * C_GAMEMANAGER.GetInstance().GetPlayer().GetPlayerStats().m_fBulletSpeed;

            int b = C_GAMEMANAGER.GetInstance().GetPlayer().GetCurrentBulletCount();

            C_GAMEMANAGER.GetInstance().GetPlayer().SetCurrentBulletCount(b - 1);
        }
	}

    public void ChargeShotEvent()
    {
        if (C_GAMEMANAGER.GetInstance().GetPlayer().GetCurrentChargeBulletCount() > 0)
        {
            GameObject ChargeInst = chars.Alloc();

            if (ChargeInst == null)
            {
                chars.AllFree();

                ChargeInst = chars.Alloc();

                if (ChargeInst == null)
                    return;
            }


            ChargeInst.GetComponent<Transform>().position = transform.FindChild("ArrowSpot").transform.position;
            float fDegree = C_GAMEMANAGER.GetInstance().GetPlayer().GetPlayerStats().m_fChargeBulletDegree;
            Vector3 dir = C_GAMEMANAGER.GetInstance().GetPlayer().GetRealForwardDir();
            float fBulletShotAng = 0.0f;

            Vector2 jdir = (-ShotButton.GetComponent<JoyStickCtrl>().vecLastDir).normalized;

            if (jdir.y > 0.0f)
                fBulletShotAng = Mathf.Acos(Vector2.Dot(Vector2.right, jdir)) * Mathf.Rad2Deg;
            else
                fBulletShotAng = 360.0f - Mathf.Acos(Vector2.Dot(Vector2.right, jdir)) * Mathf.Rad2Deg;

            dir = Quaternion.Euler(0.0f, 0.0f, fBulletShotAng + fDegree) * dir;

            ChargeInst.transform.rotation = Quaternion.Euler(0.0f, 0.0f, transform.eulerAngles.z + fBulletShotAng + fDegree);
            ChargeInst.GetComponent<Rigidbody2D>().velocity = dir.normalized * C_GAMEMANAGER.GetInstance().GetPlayer().GetPlayerStats().m_fChargeBulletSpeed;

            if (ChargeInst.name.Equals("Chicken_Bullet_Charge(Clone)"))
                ChargeInst.GetComponent<ChickenChargeMovement>().Movement();

            int ccb = C_GAMEMANAGER.GetInstance().GetPlayer().GetCurrentChargeBulletCount();

            C_GAMEMANAGER.GetInstance().GetPlayer().SetCurrentChargeBulletCount(ccb - 1);
        }
    }

    public void LauchEvent()
    {
        
		Vector3 vecPower = (vecAim - OriginPos) * C_GAMEMANAGER.GetInstance ().GetPlayer ().GetPlayerStats ().m_fMaxPower * 0.7f;
        fHorizontalPower = vecPower.x;
        fVerticalPower = vecPower.y;
        Debug.Log("HorizonPower : " + fHorizontalPower.ToString());
        Debug.Log("fVerticalPower : " + fVerticalPower.ToString());
        C_GAMEMANAGER.GetInstance().GetPlayer().SetState(E_PLAYERSTATE.E_PLAYERRELEASE);
        GetComponent<Rigidbody2D>().velocity = vecPower;//(vecAim - OriginPos).normalized * 20.0f;
        C_GAMEMANAGER.GetInstance().GetPlayer().SetRealPos(transform.position);

        Destroy(blueDot);
        Destroy(redDot1);
        Destroy(redDot2);
        
    }
    public void WindButton()
    {
        if(C_GAMEMANAGER.GetInstance().GetPlayer().GetState() == E_PLAYERSTATE.E_PLAYERRELEASE)
        {
            float fWind = C_GAMEMANAGER.GetInstance().GetPlayer().GetWindMeter();
            if (fWind >= 100)
                return;
            fWind = fWind + 10;
            C_GAMEMANAGER.GetInstance().GetPlayer().SetWindMeter(fWind);
        }
        
    }
    IEnumerator SpeedReducer()
    {
        while (C_GAMEMANAGER.GetInstance().GetPlayer().GetCurrentSpeed() >= 0)
        {
                
			C_GAMEMANAGER.GetInstance().GetPlayer().SetCurrentSpeed(C_GAMEMANAGER.GetInstance().GetPlayer().GetCurrentSpeed() 
                - C_GAMEMANAGER.GetInstance().GetPlayer().SpeedReducer());
            C_GAMEMANAGER.GetInstance().GetPlayer().SetVerticalSpeed(C_GAMEMANAGER.GetInstance().GetPlayer().GetPlayerStats().m_fVerticalSpeed
              - C_GAMEMANAGER.GetInstance().GetPlayer().GetGravitySpeed() * fGravityScore);

            int nSpeed = (int)(C_GAMEMANAGER.GetInstance().GetPlayer().GetCurrentSpeed()/10.0f);
            textSpeed.text = nSpeed.ToString() + "m/s";

            fMeter += C_GAMEMANAGER.GetInstance().GetPlayer().GetCurrentSpeed() / 500.0f;
            

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
            
            yield return new WaitForSeconds(0.02f);

        }
    }
    IEnumerator WindReducer()
    {
        while (C_GAMEMANAGER.GetInstance().GetPlayer().GetState() != E_PLAYERSTATE.E_PLAYERDIE)
        {
            float fWind = C_GAMEMANAGER.GetInstance().GetPlayer().GetWindMeter();
            if (C_GAMEMANAGER.GetInstance().GetPlayer().GetWindMeter() > 0)
            {
                //fWind = fWind - 0.5f;
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

            if(C_GAMEMANAGER.GetInstance().GetPlayer().GetWindMeter() >= 100)
            {
                StartCoroutine(FeverTime());
                break;
            }

            yield return new WaitForSeconds(0.02f);
        }

    }
    IEnumerator FeverTime()
    {
        C_GAMEMANAGER.GetInstance().GetPlayer().SetState(E_PLAYERSTATE.E_PLAYERFEVERTIME);
        float fSpeedBuffer = C_GAMEMANAGER.GetInstance().GetPlayer().GetCurrentSpeed();
        float fVSpeedBuffer = C_GAMEMANAGER.GetInstance().GetPlayer().GetVerticalSpeed();
        float fSpeed = fSpeedBuffer;
        float fVSpeed = fVSpeedBuffer;
        
        while(C_GAMEMANAGER.GetInstance().GetPlayer().GetWindMeter() > 0)
        {
            float fWind = C_GAMEMANAGER.GetInstance().GetPlayer().GetWindMeter();
            fWind = fWind - 0.5f;
            C_GAMEMANAGER.GetInstance().GetPlayer().SetWindMeter(fWind);
            float fRate = fWind / 100;
            imgWindMeter.fillAmount = fRate;
            fSpeed = fSpeed + 2f * fRate;
            fVSpeed = fVSpeed + 2f * fRate;
            Color32 color = new Color(1 - fRate, 0, fRate, 1);
            imgWindMeter.color = color;

            C_GAMEMANAGER.GetInstance().GetPlayer().SetCurrentSpeed(fSpeed);
            C_GAMEMANAGER.GetInstance().GetPlayer().SetVerticalSpeed(fVSpeed);
            
            yield return new WaitForSeconds(0.02f);
        }

        C_GAMEMANAGER.GetInstance().GetPlayer().SetState(E_PLAYERSTATE.E_PLAYERRELEASE);
        StartCoroutine(WindReducer());
    }

    void Update()
    {

        if (strPrevEat != C_GAMEMANAGER.GetInstance().GetPlayer().GetCurrentEat())
        {
            strPrevEat = C_GAMEMANAGER.GetInstance().GetPlayer().GetCurrentEat();
        }

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

            Color32 color = new Color(1 - fRate,fRate, 0);
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
        }
    }

    
    void FixedUpdate()
    {
        //player forward vector setting
        if (C_GAMEMANAGER.GetInstance().GetPlayer().GetState() == E_PLAYERSTATE.E_PLAYERRELEASE)
        {
            Vector2 pos = C_GAMEMANAGER.GetInstance().GetPlayer().GetRealPos();
            Vector2 ax = Vector2.right;
            pos.Normalize();

            float rad = Mathf.Acos(Vector2.Dot(ax, pos));
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Rad2Deg * rad + fToqueCtrlDeg);
            C_GAMEMANAGER.GetInstance().GetPlayer().SetRealForwardDir(pos);

            float deg = Mathf.Rad2Deg * Mathf.Acos(Vector2.Dot(Vector2.right, pos));

            if (deg < 10.0f && 
                C_GAMEMANAGER.GetInstance().GetPlayer().GetRealPos().y < 500.0f)
            {
                float fSpeed = C_GAMEMANAGER.GetInstance().GetPlayer().GetCurrentSpeed();
                float fVSpeed = C_GAMEMANAGER.GetInstance().GetPlayer().GetVerticalSpeed();

                C_GAMEMANAGER.GetInstance().GetPlayer().SetCurrentSpeed(fSpeed - fSpeed * 0.45f * Time.deltaTime);
                C_GAMEMANAGER.GetInstance().GetPlayer().SetVerticalSpeed(fVSpeed - fVSpeed * 0.45f * Time.deltaTime);
            }
        }
    
        if (C_GAMEMANAGER.GetInstance().GetPlayer().GetState() == E_PLAYERSTATE.E_PLAYERDIE)
        {
            StopAllCoroutines();
            textSpeed.text = 0.ToString() + "m/s";
            float fverticalSpeed = C_GAMEMANAGER.GetInstance().GetPlayer().GetPlayerStats().m_fVerticalSpeed / 1000.0f;
            float fHorizontalSpeed = C_GAMEMANAGER.GetInstance().GetPlayer().GetPlayerStats().m_fCurrentSpeed / 1000.0f;
            Vector3 AddVec = new Vector3(fHorizontalSpeed,
               fverticalSpeed, 0);
            transform.position = transform.position + AddVec;

            btnBackMain.gameObject.SetActive(true);
        }
        
    }
    
}
