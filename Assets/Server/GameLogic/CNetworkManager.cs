using UnityEngine;
using System;
using System.Collections;
using FreeNet;
using FreeNetUnity;
using LineCrushGameServer;

public class CNetworkManager : MonoBehaviour {

	CFreeNetUnityService gameserver;
	string received_msg;

	public MonoBehaviour message_receiver;

  //  public bool SaveServer = false;
	void Awake()
	{
		this.received_msg = "";

		// 네트워크 통신을 위해 CFreeNetUnityService객체를 추가합니다.
		this.gameserver = gameObject.AddComponent<CFreeNetUnityService>();

		// 상태 변화(접속, 끊김등)를 통보 받을 델리게이트 설정.
		this.gameserver.appcallback_on_status_changed += on_status_changed;

		// 패킷 수신 델리게이트 설정.
		this.gameserver.appcallback_on_message += on_message;

        CPacketSender.network_manager = this;
	}

    //public void SaveServerconnect()
    //{
    //    this.gameserver.connects("ec2-52-197-48-85.ap-northeast-1.compute.amazonaws.com", 49494); //저장서버
    //}
	public void connect()
	{
        this.gameserver.connect("127.0.0.1", 49494);
		//this.gameserver.connect("192.168.0.2", 49494);

        //this.gameserver.connects("ec2-52-196-41-91.ap-northeast-1.compute.amazonaws.com", 49494); //1번서버
    }
    public void Disconnect()
    {
        if (is_connected())
        {
            this.gameserver.QuitDisconnect();
        }
    }
	public bool is_connected()
	{
		return this.gameserver.is_connected();
	}

	/// <summary>
	/// 네트워크 상태 변경시 호출될 콜백 매소드.
	/// </summary>
	/// <param name="server_token"></param>
	void on_status_changed(NETWORK_EVENT status)
	{
        switch (status)
        {
            // 접속 성공.
            case NETWORK_EVENT.connected:
                {
                    CLogManager.log("on connected");
                    this.received_msg += "on connected\n";

                 //   if (SaveServer == false)
                        GetComponent<CMainTitle>().on_connected();
                    //else
                    //    GetComponent<CSaveServerNet>().on_connected();
                }
                break;

            // 연결 끊김.
            case NETWORK_EVENT.disconnected:
                GetComponent<CMainTitle>().on_Disconnected();
                CLogManager.log("disconnected");
                this.received_msg += "disconnected\n";

                Awake();
                break;
        }
	}

	void on_message(CPacket msg)
	{
		this.message_receiver.SendMessage("on_recv", msg);
	}

	public bool send(CPacket msg)
	{
		return this.gameserver.send(msg);
	}
}
