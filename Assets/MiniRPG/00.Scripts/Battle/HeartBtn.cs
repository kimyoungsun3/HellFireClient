using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBtn : MonoBehaviour {

    public UnityEngine.UI.Text IdText;

    int m_SN;

   public void SetUserInfo(int Sn,string name)
    {
        m_SN = Sn;
        IdText.text = name;
    }

    public void OnSendHeart()
    {
        HeartSendMgr.current.spendheart();
        CPacketSender.HEART_SEND_REQ_SEND(m_SN);
    }
	void Start () {
		
	}
	
	void Update () {
		
	}
}
