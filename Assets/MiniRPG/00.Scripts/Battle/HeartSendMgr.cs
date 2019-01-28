using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartSendMgr : MonoBehaviour {

    public static HeartSendMgr current;
    private void OnEnable()
    {
        Sendbtn.interactable = false;
        heartTimer = 60;
        HeartTimertext.text = heartTimer + "s";
    }
    private void Awake()
    {
        if (current == null)
            current = this;
        else
            Debug.LogError("Not Single HeartSendMgr");

        Sendbtn.interactable = false;
        heartTimer = 60;
        HeartTimertext.text = heartTimer + "s";
    }

    public UnityEngine.UI.Button Sendbtn;
    public UnityEngine.UI.Text HeartTimertext;
    int heartTimer = 60;
    float ftimer = 0;
    public void spendheart()
    {
        Sendbtn.interactable = false;
        heartTimer = 60;
        Sendwindow.SetActive(false);
    }
    private void Update()
    {
        ftimer += Time.deltaTime;
        if (ftimer >= 1f && heartTimer > 0)
        {
            --heartTimer;

            if (heartTimer <= 0)
            {
                Sendbtn.interactable = true;
                HeartTimertext.text = "send";
            }
            else
            {
                HeartTimertext.text = heartTimer + "s";
            }
            ftimer = 0f;
        }

    }
    public GameObject HeartBtnPrefab;

    public GameObject Sendwindow;
    public Transform BtnPar;
	public void OnClickBtn ()
    {
        Sendwindow.SetActive(true);
        for (int i=0; i < BtnPar.transform.GetChildCount(); ++i)
        {
            Destroy(BtnPar.transform.GetChild(i).gameObject);
        }
        List<Player> PlayerArray = BattleManager.current.GetPlayerList();

        GameObject Tmpobj = null;
        for(int i=0; i < PlayerArray.Count; ++i)
        {
            if(BattleManager.current.m_MySN != PlayerArray[i].m_SN)
            {
                Tmpobj = Instantiate(HeartBtnPrefab);
                HeartBtn heart = Tmpobj.GetComponent<HeartBtn>();
                heart.SetUserInfo(PlayerArray[i].m_SN,
                     PlayerArray[i].LevelText.text + "."+ PlayerArray[i].nameText.text);

                heart.transform.SetParent(BtnPar);
                heart.transform.localScale = Vector3.one;
                heart.transform.position = Vector3.zero;
            }
        }



    }

}
