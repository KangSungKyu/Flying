
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using IgaworksUnityAOS;

public class IGAWorkCtrl : MonoBehaviour
{
/*
    private void Awake()
    {
        IgaworksUnityPluginAOS.InitPlugin();
    }
    // Use this for initialization
    void Start()
    {
#if UNITY_5_3_OR_NEWER
        Debug.Log("Current Unity version is UNITY_5_3_OR_NEWER");
#else
        IgaworksUnityPluginAOS.Common.startSession();
#endif

        UserInit();
        PushInit();
        AdBrixInit();
        CouponInit();
        PopconOfferWallInit();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void UserInit()
    {
        IgaworksUnityPluginAOS.Common.setUserId("user100001");
    }
    void PushInit()
    {
        IgaworksUnityPluginAOS.LiveOps.initialize();
        IgaworksUnityPluginAOS.LiveOps.enableService(true);
        IgaworksUnityPluginAOS.LiveOps.flushTargetingData();

        //IgaworksUnityPluginAOS.LiveOps.setNotificationIconStyle("ic_liveops_01", "ic_liveops_02", "ff9d261c");
        //IgaworksUnityPluginAOS.LiveOps.setNotificationOption(IgaworksUnityPluginAOS.AndroidNotificationPriority.PRIORITY_HIGH, IgaworksUnityPluginAOS.AndroidNotificationVisibility.VISIBILITY_PUBLIC);
    }
    void AdBrixInit()
    {
        IgaworksUnityPluginAOS.Adbrix.setAge(30);
        IgaworksUnityPluginAOS.Adbrix.setGender(IgaworksUnityPluginAOS.Gender.MALE);
        IgaworksUnityPluginAOS.Adbrix.firstTimeExperience("LoadMainLogo");
        IgaworksUnityPluginAOS.Adbrix.firstTimeExperience("ContentLoading");
        IgaworksUnityPluginAOS.Adbrix.firstTimeExperience("KakaoTalkConnectSuccess");
        IgaworksUnityPluginAOS.Adbrix.firstTimeExperience("TutorialComplete");
        IgaworksUnityPluginAOS.Adbrix.purchase(
            "oid_1", //주문 아이디
            "pid_1",  //상품 아이디
            "gold_package", //상품명
            1100.00, //상품 단가
            1,  //구매 수량
            IgaworksUnityPluginAOS.Adbrix.Currency.KR_KRW, //화폐 단위
            "cat1.cat2.cat3.cat4.cat5" //카테고리 (총 5단계까지가능)
            );
        IgaworksUnityPluginAOS.Adbrix.retention("openStore");
        IgaworksUnityPluginAOS.Adbrix.retention("stageClear");
        IgaworksUnityPluginAOS.Adbrix.retention("purchaseItemWithVirtualCurrency");
        IgaworksUnityPluginAOS.Adbrix.retention("inviteFriends");
        IgaworksUnityPluginAOS.Adbrix.retention("stageClear", "Tutorial");
    }
    void CouponInit()
    {
        IgaworksUnityPluginAOS.Coupon.showCouponDialog(true);
        IgaworksUnityPluginAOS.OnSendCouponSucceed = OnSendCouponSucceed;
        IgaworksUnityPluginAOS.OnSendCouponFailed = OnSendCouponFailed;
    }
    void PopconOfferWallInit()
    {
        //리워드 지급 정보를 수신하기 위한 이벤트 리스너를 등록합니다.
        IgaworksUnityPluginAOS.Common.setClientRewardEventListener();
        //오퍼월 액션 이벤트를 수산하기 위한 이벤트 리스너를 등록합니다.
        IgaworksUnityPluginAOS.Adpopcorn.setAdpopcornOfferwallEventListener();
        IgaworksUnityPluginAOS.OnGetRewardInfo = OnGetRewardInfo;
        IgaworksUnityPluginAOS.OnDidGiveRewardItemRequestResult = OnDidGiveRewardItemRequestResult;
        IgaworksUnityPluginAOS.OnClosedOfferwallPage = OnClosedOfferwallPage;
    }
    void PushResume()
    {
        IgaworksUnityPluginAOS.LiveOps.resume();
        IgaworksUnityPluginAOS.LiveOps.enableService(true);
        IgaworksUnityPluginAOS.LiveOps.flushTargetingData();
    }
    public void Push()
    {
        IgaworksUnityPluginAOS.LiveOps.setNormalClientPushEvent(
            1,                   // Delay seconds. 몇 초 뒤에 보낼지 설정
            "Test Push!",      // 전송할 메시지
            1,           // Event ID, 취소할 때 쓰기 위한 값.
            true            // 앱이 실행 중일 때에도 보이게 할 것인지 설정
        );
    }
    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            Debug.Log("go to Background");
            IgaworksUnityPluginAOS.Common.endSession();
        }
        else
        {
            Debug.Log("go to Foreground");
            IgaworksUnityPluginAOS.Common.startSession();
            PushResume();
        }
    }

    void OnSendCouponSucceed(string msg, int itemKey, string itemName, long quantity)
    {
        //msg : 쿠폰사용결과
        //itemKey : 사용된 쿠폰에서 지급할 가상화폐의 키, Deprecated							
        //itemName : 사용된 쿠폰에서 지급할 가상화폐
        //quantity : 사용된 쿠폰의 가치
    }

    void OnSendCouponFailed(string msg)
    {
        //msg : 쿠폰사용결과							
    }
    void OnGetRewardInfo(string campaignkey, string campaignname, string quantity, string cv, string rewardkey)
    {
        string ck = campaignkey;
        string cn = campaignname;
        string qt = quantity;
        // 위 정보를 이용하여 유저에게 리워드를 지급합니다.
        // {리워드 지급 처리}

        // didGiveRewardItem API를 호출하여 리워드 지급 처리 완료를 IGAW리워드 서버에 통지합니다.
        IgaworksUnityPluginAOS.Common.didGiveRewardItem(cv, rewardkey);
    }
    void OnDidGiveRewardItemRequestResult(bool isSuccess, string rewardkey)
    {
        // didGiveRewardItem 함수의 처리 결과가 리턴됩니다.
        // 동일한 rewardkey에 대해서 중복지급방지 처리를 합니다
    }

    void OnClosedOfferwallPage()
    {
        IgaworksUnityPluginAOS.Common.getClientPendingRewardItems();
    }
//*/
}
