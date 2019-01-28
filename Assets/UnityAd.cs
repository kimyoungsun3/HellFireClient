using UnityEngine;
//using UnityEngine.Advertisements;

public class UnityAd: MonoBehaviour
{
    public GameObject Btn;
    void Update()
    {
		/*
        if(Btn.activeSelf == false)
        {
            bool ready = Advertisement.IsReady("rewardedVideo");
            Btn.SetActive(ready);
        }
        */
    }
    public void ShowRewardedAd()
    {
		/*
		// if (Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);
            

        }
		*/
    }
    public void ActiveBtn()
    {
        //Btn.SetActive(false);
    }
	/*
    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                PlayerPrefs.SetInt("HeartExp",
                PlayerPrefs.GetInt("HeartExp", 0) + 3);

                PlayerPrefs.Save();
                Namemanager.current.RefreshInfo();
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                break;
        }

        ActiveBtn();
    }
        */
}