using UnityEngine;
using System.Collections;

public class ChatSender : MonoBehaviour {


    string MsgBuffer;

    public void EditMsgBuffer(string msg)
    {
        MsgBuffer = msg;
        OnSendMsg();
    }

	public void OnSendMsg()
    {
        if (MsgBuffer != "")
        {
            ChatManager.current.MsgPrint(
                  BattleManager.current.myPlayer.nameText.text + ":" + MsgBuffer,
                  Color.cyan);

            BattleManager.current.myPlayer.SendMsg(MsgBuffer);
            CPacketSender.CHAT_MSG_REQ_SEND(MsgBuffer);
            MsgBuffer = "";
        }
    }
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
