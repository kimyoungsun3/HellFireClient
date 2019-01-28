using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomObj : MonoBehaviour {

    int iRoomNum;
    public UnityEngine.UI.Text RoomText;

    public void CreateRoomInfo(int _Roomnum,string _text)
    {
        iRoomNum = _Roomnum;
        RoomText.text = iRoomNum + ":"+  _text;
    }

    public UnityEngine.UI.Button connectbtn;
    public void DisableConnectBtn()
    {
        connectbtn.interactable = false;
    }
    public void ConnectRoom()
    {
        CPacketSender.ROOM_CONNECT_REQ_SEND(iRoomNum);
        RoomManager.current.EneableList(false);
    }
    void Start () {
		
	}
	
	void Update () {
		
	}
}
