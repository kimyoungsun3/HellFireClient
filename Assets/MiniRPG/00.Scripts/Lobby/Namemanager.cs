using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Namemanager : MonoBehaviour {

    public static Namemanager current;
    private void Awake()
    {
        if (current == null)
            current = this;
        else
            Debug.LogError("not singile Namemanager");
    }
    public UnityEngine.UI.InputField inputname;
    public void Changename()
    {
        if (inputname.text != "")
        {
            PlayerPrefs.SetString("id", inputname.text);
            PlayerPrefs.Save();
            RefreshInfo();
            CPacketSender.ID_CHANGE_REQ_SEND(
           PlayerPrefs.GetString("id", "???"),
           PlayerPrefs.GetInt("level", 1));

        }
    }

    public UnityEngine.UI.Text IDText;
    public UnityEngine.UI.Text LevelText;
    public UnityEngine.UI.Text ExpText;
    public UnityEngine.UI.Slider ExpSlider;


    public void SendUserPrint(int OtherSN)
    {
        Player Info= BattleManager.current.FindUser(OtherSN);
        ChatManager.current.MsgPrint(
            "I received a Heart from " + Info.nameText.text,
            new Color(0.95f, 0.56f, 0.14f));
    }
    public void RefreshInfo()
    {
        int iLevel = PlayerPrefs.GetInt("level", 1);
        iCurrentExp = PlayerPrefs.GetInt("HeartExp", 0);
        int MaxExp = expTable(iLevel);

        if(iCurrentExp >= MaxExp)
        {
            iCurrentExp = 0;
            PlayerPrefs.SetInt("level", ++iLevel);
            PlayerPrefs.SetInt("HeartExp", iCurrentExp);
            PlayerPrefs.Save();
            MaxExp = expTable(iLevel);
            CPacketSender.ID_CHANGE_REQ_SEND(
                PlayerPrefs.GetString("id", "noname"),
                PlayerPrefs.GetInt("level", 1));

            MaxExp = expTable(iLevel);

            if (ChatManager.current != null)
                ChatManager.current.MsgPrint("+1 Level Up!", Color.yellow);


        }

        ExpSlider.maxValue = MaxExp;
        ExpSlider.value = iCurrentExp;

        ExpText.text = iCurrentExp + "/" + MaxExp;
        LevelText.text = iLevel.ToString();
        IDText.text = PlayerPrefs.GetString("id", "???");

    }
    int iCurrentExp =0;
    void OnEnable ()
    {
        RefreshInfo();
    }
	
    int expTable(int level)
    {
        return (int)(level * (level * 0.5f)) + 1;
    }

	void Update () {
		
	}
}
