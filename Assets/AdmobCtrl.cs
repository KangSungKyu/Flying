using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using GoogleMobileAds.Api;

public class AdmobCtrl : MonoBehaviour
{
    /*
    BannerView banner = null;
    InterstitialAd inter = null;
    // Use this for initialization
    void Start()
    {
        inter = new InterstitialAd("ca-app-pub-9973222948315812/2362197480");
        inter.AdClosed += EventAdClose;

        banner = new BannerView("ca-app-pub-9973222948315812/9885464289", AdSize.SmartBanner, AdPosition.Bottom);

        AdRequest.Builder builder = new AdRequest.Builder();

        AdRequest req = builder.AddTestDevice(AdRequest.TestDeviceSimulator).AddTestDevice("3FB7AE355826BE23").Build();
        //builder.Build();

        inter.LoadAd(req);
        banner.LoadAd(req);
        banner.Show();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnGUI()
    {
        if(GUI.Button(new Rect(300,200,300,100),"inter show"))
        {
            inter.Show();
        }
    }

    public void EventAdClose(object _sender, System.EventArgs _arg)
    {
        AdRequest.Builder builder = new AdRequest.Builder();

        AdRequest req = builder.AddTestDevice(AdRequest.TestDeviceSimulator).AddTestDevice("3FB7AE355826BE23").Build();

        inter.LoadAd(req);
    }
    //*/
}
