using UnityEngine;
using System.Collections;
using admob;
public class admobdemo : MonoBehaviour {


	// Use this for initialization
	void Start () {
        Debug.Log("start unity demo-------------");
         initAdmob();
        //배너 시작하자마자
        Admob.Instance().showBannerRelative(AdSize.SmartBanner, AdPosition.BOTTOM_CENTER, 0);

        //   ShowAd();
    }

    public void Quit()
    {
        ad.removeAllBanner();
        Application.Quit();
    }
    // Update is called once per frame
    void Update () {
	    if (Input.GetKeyUp (KeyCode.Escape)) {
      

         //   Debug.Log(KeyCode.Escape+"-----------------");
         //ad.removeAllBanner();
         //Application.Quit();
        }
    }
    Admob ad;
    //bool isAdmobInited = false;
    void initAdmob()
    {
          //  isAdmobInited = true;
             ad = Admob.Instance();
            ad.bannerEventHandler += onBannerEvent;
            ad.interstitialEventHandler += onInterstitialEvent;
            //ad.rewardedVideoEventHandler += onRewardedVideoEvent;
            //ad.nativeBannerEventHandler += onNativeBannerEvent;
        ad.initAdmob("ca-app-pub-8885442409308081/3508865453", "ca-app-pub-8885442409308081/6462331852");

        //ad.initAdmob("ca-app-pub-3940256099942544/2934735716", "ca-app-pub-3940256099942544/4411468910");
         // ad.setTesting(true);


        //if (Random.Range(0, 2) ==0)
        //    ad.setGender(AdmobGender.FEMAIL);
        //else
        //    ad.setGender(AdmobGender.MALE);

        //    string[] keywords = { "game", "couple", "community" };
        //    ad.setKeywords(keywords);
        //    Debug.Log("admob inited -------------");
    }

    public void ShowAd()
    {
        if (ad.isInterstitialReady())
        {
            ad.showInterstitial();
        }
        else
        {
            ad.loadInterstitial();
        }
    }
    //void OnGUI(){
    //       if (GUI.Button(new Rect(120, 0, 100, 60), "showInterstitial"))
    //       {

    //           if (ad.isInterstitialReady())
    //           {
    //               ad.showInterstitial();
    //           }
    //           else
    //           {
    //               ad.loadInterstitial();
    //           }
    //       }
    //       if (GUI.Button(new Rect(240, 0, 100, 60), "showRewardVideo"))
    //       {

    //           if (ad.isRewardedVideoReady())
    //           {
    //               ad.showRewardedVideo();
    //           }
    //           else
    //           {

    //               ad.loadRewardedVideo("ca-app-pub-3940256099942544/xxxxxxxxxx");
    //           }
    //       }
    //       if (GUI.Button(new Rect(0, 100, 100, 60), "showbanner"))
    //       {
    //           Admob.Instance().showBannerRelative(AdSize.SmartBanner, AdPosition.BOTTOM_CENTER, 0);
    //       }
    //       if (GUI.Button(new Rect(120, 100, 100, 60), "showbannerABS"))
    //       {
    //           Admob.Instance().showBannerAbsolute(AdSize.Banner, 0, 300);
    //       }
    //       if (GUI.Button(new Rect(240, 100, 100, 60), "removebanner"))
    //       {
    //           Admob.Instance().removeBanner();
    //       }

    //       string nativeBannerID = "ca-app-pub-3940256099942544/2562852117";//google
    //       //string nativeBannerID = "ca-app-pub-8885442409308081/3508865453";//내꺼
    //       if (GUI.Button(new Rect(0, 200, 100, 60), "showNative"))
    //       {

    //           Admob.Instance().showNativeBannerRelative(new AdSize(320,120), AdPosition.BOTTOM_CENTER, 0,nativeBannerID);
    //       }
    //       if (GUI.Button(new Rect(120, 200, 100, 60), "showNativeABS"))
    //       {
    //           Admob.Instance().showNativeBannerAbsolute(new AdSize(320,120), 0, 300, nativeBannerID);
    //       }
    //       if (GUI.Button(new Rect(240, 200, 100, 60), "removeNative"))
    //       {
    //           Admob.Instance().removeNativeBanner();
    //       }
    //}
    void onInterstitialEvent(string eventName, string msg)
    {
        Debug.Log("handler onAdmobEvent---" + eventName + "   " + msg);
        if (eventName == AdmobEvent.onAdLoaded)
        {
            Admob.Instance().showInterstitial();
        }
    }
    void onBannerEvent(string eventName, string msg)
    {
        Debug.Log("handler onAdmobBannerEvent---" + eventName + "   " + msg);
    }
    void onRewardedVideoEvent(string eventName, string msg)
    {
        Debug.Log("handler onRewardedVideoEvent---" + eventName + "   " + msg);
    }
    void onNativeBannerEvent(string eventName, string msg)
    {
        Debug.Log("handler onAdmobNativeBannerEvent---" + eventName + "   " + msg);
    }
}
