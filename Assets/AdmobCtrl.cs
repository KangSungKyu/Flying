using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdmobCtrl : MonoBehaviour
{
    //*
    public string and_banner;
    public string ios_banner;
    public string and_inter;
    public string ios_inter;

    BannerView banner = null;
    InterstitialAd inter = null;
    // Use this for initialization
    void Start()
    {
        RequestBanner();
        RequestInterstitialAd();
        ShowBanner();
    }

    public void RequestBanner()
    {
        string str = "";
#if UNITY_ANDROID
        str = and_banner;
#elif UNITY_IOS
        str = ios_banner;
#endif
        banner = new BannerView(str, AdSize.SmartBanner, AdPosition.Top);
        AdRequest.Builder builder = new AdRequest.Builder();

        AdRequest req = builder.AddTestDevice(AdRequest.TestDeviceSimulator).AddTestDevice("3FB7AE355826BE23").Build();
        //AdRequest req = builder.Build();

        banner.LoadAd(req);
    }
    public void RequestInterstitialAd()
    {
        string str = "";
#if UNITY_ANDROID
        str = and_inter;
#elif UNITY_IOS
        str = ios_inter;
#endif
        inter = new InterstitialAd(str);
        AdRequest.Builder builder = new AdRequest.Builder();

        AdRequest req = builder.AddTestDevice(AdRequest.TestDeviceSimulator).AddTestDevice("3FB7AE355826BE23").Build();
        //AdRequest req = builder.Build();

        inter.LoadAd(req);
        inter.OnAdClosed += EventAdClose;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowBanner()
    {
        banner.Show();
    }
    public void ShowInter()
    {
       if(!inter.IsLoaded())
        {
            RequestInterstitialAd();
            return;
        }
        inter.Show();
    }

    public void OnInter()
    {
        ShowInter();
    }

    public void EventAdClose(object _sender, System.EventArgs _arg)
    {
        inter.Destroy();
        RequestInterstitialAd();
    }
    //*/
}
