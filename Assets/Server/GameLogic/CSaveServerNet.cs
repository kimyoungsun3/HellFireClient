using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FreeNet;
using LineCrushGameServer;
using System;
//using UnityEditor;
public class CSaveServerNet : MonoBehaviour
{

	enum USER_STATE
	{
		NOT_CONNECTED,
		CONNECTED,
		WAITING_MATCHING
	}
    public void FILE_SEND_REG_SEND()
    {
        //int Pos = 0;
        //byte[] Data = XmlDataManager.inst().GetFileDate(out Pos);

        //{
        //    CPacket start = CPacket.create((short)PROTOCOL.FILE_SEND_BEGIN);
        //    start.push(Data.Length);
        //    start.push(XmlDataManager.inst().GetChar().SN);
        //    network_manager.send(start);
        //}

        //int iCopyIndex = 0;
        //while (iCopyIndex < Data.Length)
        //{
        //    byte[] SubData;

        //    int iDist = Data.Length - iCopyIndex;
        //    if (iDist < 1020)
        //    {
        //        SubData = new byte[iDist];
        //        Array.Copy(Data, iCopyIndex, SubData, 0, iDist);
        //        iCopyIndex += iDist;
        //    }
        //    else
        //    {
        //        SubData = new byte[1020];
        //        Array.Copy(Data, iCopyIndex, SubData, 0, 1020);
        //        iCopyIndex += 1020;
        //    }

        //    CPacket msg = CPacket.create((short)PROTOCOL.FILE_SEND_REG);
        //    msg.push(SubData);
        //    network_manager.send(msg);
        //}

        //{
        //    CPacket start = CPacket.create((short)PROTOCOL.FILE_SEND_END);
        //    network_manager.send(start);
        //}
    }
    //public void FILE_LOAD_BEGIN_SEND(int SN)
    //{
    //    CPacket start = CPacket.create((short)PROTOCOL.FILE_LOAD_BEGIN);
    //    start.push(SN);
    //    network_manager.send(start);
    //}

    public CNetworkManager network_manager;
	USER_STATE user_state;

	//Texture waiting_img;

    public UnityEngine.UI.Text ResultCode;
    public GameObject gResultCode;
    void OnEnable()
    {

        network_manager = gameObject.AddComponent<CNetworkManager>();
        //network_manager.SaveServer = true;
        this.user_state = USER_STATE.NOT_CONNECTED;
    }

    public void OnServerEnter()
    {
        enter();
    }
    void OnDisable()
    {
        this.network_manager.Disconnect();
    }


    public void enter()
    {
        StopCoroutine("after_connected");

        //매니저에 리시버 등록
        this.network_manager.message_receiver = this;

        if (!this.network_manager.is_connected())
        {
            this.user_state = USER_STATE.CONNECTED;
        //    this.network_manager.SaveServerconnect();
        }
        else
        {
            on_connected();
        }
    }


    ///// <summary>
    ///// 서버에 접속된 이후에 처리할 루프.
    ///// 마우스 입력이 들어오면 ENTER_GAME_ROOM_REQ프로토콜을 요청하고 
    ///// 중복 요청을 방지하기 위해서 현재 코루틴을 중지 시킨다.
    ///// </summary>
    ///// <returns></returns>
    IEnumerator after_connected()
    {
        // CBattleRoom의 게임오버 상태에서 마우스 입력을 통해 메인 화면으로 넘어오도록 되어 있는데,
        // 한 프레임 내에서 이 코루틴이 실행될 경우 아직 마우스 입력이 남아있는것으로 판단되어
        // 메인 화면으로 돌아오자 마자 ENTER_GAME_ROOM_REQ패킷을 보내는 일이 발생한다.
        // 따라서 강제로 한 프레임을 건너뛰어 다음 프레임부터 코루틴의 내용이 수행될 수 있도록 한다.
        yield return new WaitForEndOfFrame();

        while (true)
        {
            if (this.user_state == USER_STATE.CONNECTED)
            {
                //if (Input.GetMouseButtonDown(0))
                //{
                    this.user_state = USER_STATE.WAITING_MATCHING;

                    StopCoroutine("after_connected");
               // }
            }

            yield return 0;
        }
    }

    public bool bTestServer;
    void Update()
    {
        if (bTestServer)
            return;

        //if(network_manager.is_connected() == false)
        //{
        //  //  String test = "서버 접속 실패";
        //    SystemMsg.OnSystemMessage("서버 접속 실패");
        //}
    }


    ///// <summary>
    ///// 서버에 접속이 완료되면 호출됨.
    ///// </summary>
    public void on_connected()
    {
        this.user_state = USER_STATE.CONNECTED;

        StartCoroutine("after_connected");
    }

    ///// <summary>
    ///// 패킷을 수신 했을 때 호출됨.
    ///// </summary>
    ///// <param name="protocol"></param>
    ///// <param name="msg"></param>

    void NextLevel()
    {
        Application.LoadLevel(1);
    }

   // public DrawMsg MyDrawMsg;
    byte[] FileArray = null;
    int fileCount;


    public GameObject LoadStick;
    //리시브
    public void on_recv(CPacket msg)
    {
        // 제일 먼저 프로토콜 아이디를 꺼내온다.
        PROTOCOL protocol_id = (PROTOCOL)msg.pop_protocol_id();

        //switch (protocol_id)
        //{
           
        //     break;
        //}
    }
}
