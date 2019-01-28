using UnityEngine;
using System.Collections;

public class MyRankInfo : MonoBehaviour {

    public UnityEngine.UI.Text Name;
    public UnityEngine.UI.Text SDW;
    public UnityEngine.UI.Text MSG;

    public UnityEngine.UI.Image LogoImage;

    public UnityEngine.UI.Text Mmr;
    public void OnUpdateData()
    {
        //MSG.text = XmlDataManager.inst().GetChar().strMSG;
        //Name.text = XmlDataManager.inst().GetChar().ID;
        ////SDW.text = XmlDataManager.inst().GetChar().strFirst + " " + XmlDataManager.inst().GetChar().strSecond + " " + XmlDataManager.inst().GetChar().strThird;
        //SDW.text = XmlDataManager.inst().GetChar().strThird + XmlDataManager.inst().GetChar().iDongRank + "짱";

        //Mmr.text = "평판 " + XmlDataManager.inst().GetChar().mmr.ToString();
        //LogoImage.sprite = LocalLogoSprite.current.GetLogoImage(XmlDataManager.inst().GetChar().iFirst);
    }
	void OnEnable () {
        OnUpdateData();
	}
	
	void Update () {
	
	}
    //0부터 동 구 시 전국
    public void SetTab(int Type)
    {
        //OnUpdateData();

        //if (Type == 0)
        //    SDW.text = XmlDataManager.inst().GetChar().strThird + " " + (XmlDataManager.inst().GetChar().iDongRank) + "짱";
        //else if(Type == 1)
        //    SDW.text = XmlDataManager.inst().GetChar().strSecond + " " + (XmlDataManager.inst().GetChar().iDoRank) + "짱";
        //else if (Type == 2)
        //    SDW.text = XmlDataManager.inst().GetChar().strFirst + " " + (XmlDataManager.inst().GetChar().iSiRank) + "짱";
        //else if (Type == 3)
        //    SDW.text = "전국 " + (XmlDataManager.inst().GetChar().iAllRank) + "짱";
    }
}
