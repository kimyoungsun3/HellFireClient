using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FreeNet;
using LineCrushGameServer;
using System;
//using UnityEditor;
public class CMainTitle : MonoBehaviour {

	enum USER_STATE
	{
		NOT_CONNECTED,
		CONNECTED,
		WAITING_MATCHING
	}

	//Texture bg;
	//CBattleRoom battle_room;

    public CNetworkManager network_manager;
	USER_STATE user_state;

	//Texture waiting_img;

  //  public UnityEngine.UI.Text ResultCode;
  //  public GameObject gResultCode;
    void OnEnable()
    {
        network_manager = gameObject.AddComponent<CNetworkManager>();
        this.user_state = USER_STATE.NOT_CONNECTED;
        OnServerEnter();
    }

//    public WindowActive WinAct;
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
            this.network_manager.connect();
        }
        else
        {
            on_connected();
        }
    }

    public void reconnect()
    {
        this.network_manager.connect();
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

    //public bool bTestServer;
    void Update()
    {
        //if (bTestServer)
        //    return;

        //if(network_manager.is_connected() == false)
        //{
        //  //  String test = "서버 접속 실패";
        //    SystemMsg.OnSystemMessage("서버 접속 실패");
        //}
    }


    public GameObject LoadingImage;
    ///// <summary>
    ///// 서버에 접속이 완료되면 호출됨.
    ///// </summary>
    public void on_connected()
    {
        this.user_state = USER_STATE.CONNECTED;
        LoadingImage.SetActive(false);
        StartCoroutine("after_connected");
        CPacketSender.ID_CHANGE_REQ_SEND(
            PlayerPrefs.GetString("id", "noname"),
            PlayerPrefs.GetInt("level", 1));
    }

    public void on_Disconnected()
    {
        LoadingImage.SetActive(true);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    ///// <summary>
    ///// 패킷을 수신 했을 때 호출됨.
    ///// </summary>
    ///// <param name="protocol"></param>
    ///// <param name="msg"></param>
    //리시브
    public void on_recv(CPacket msg)
    {
        // 제일 먼저 프로토콜 아이디를 꺼내온다.
        PROTOCOL protocol_id = (PROTOCOL)msg.pop_protocol_id();

        switch (protocol_id)
        {
            case PROTOCOL.CREATE_ROOM_OK:
                {
                    BattleManager.current.m_MySN = msg.pop_int32();
                    //내 로컬정보
                    BattleManager.current.AddPlayer(BattleManager.current.m_MySN,
                        PlayerPrefs.GetString("id", "noname"),
                        PlayerPrefs.GetInt("level", 1));
                    RoomManager.current.RoomCreateSucces();
                }
                break;
            case PROTOCOL.CREATE_ROOM_FAILED:
                {
                    RoomManager.current.RoomCreateFailed();
                }
                break;
            case PROTOCOL.ROOM_LIST_ACK:
                {
                    string name = "";
                    int currentuser =0;
                    int maxuser =0;

                    RoomManager.current.ClearRoomlist();
                    int RoomCount = msg.pop_int32();
                    for(int i=0; i < RoomCount; ++i)
                    {
                        name = msg.pop_string();
                        currentuser = msg.pop_int32();
                        maxuser = msg.pop_int32();
						Debug.Log(maxuser);
                        RoomManager.current.RoomlistSetting(i,
                            name,
                            currentuser,
                            maxuser);
                        //print(i + msg.pop_string());
                    }
                }
                break;
            case PROTOCOL.ROOM_CONNECT_OK:
                {
                    int sn = 0;
                     string name = "";
                     int level = 0;
                    RoomManager.current.RoomCreateSucces();
                    int UserCount = msg.pop_int32();
                    BattleManager.current.m_MySN = msg.pop_int32();
                    for (int i = 0; i < UserCount; ++i)
                    {
                        sn      = msg.pop_int32();
                        name = msg.pop_string();
                        level   = msg.pop_int32();
                        BattleManager.current.AddPlayer(sn, name, level);
                    }
                }
                break;
            case PROTOCOL.ROOM_CONNECT_FAILED:
                {
                    RoomManager.current.RoomCreateFailed();
                }
                break;
            case PROTOCOL.CHAT_MSG_REQ:
                {
                    int SN = msg.pop_int32();
                    string Msg= msg.pop_string();
					
					Player player= BattleManager.current.FindUser(SN);
                    if (player != null)
                    {
                        player.SendMsg(Msg);

                        ChatManager.current.MsgPrint(
                        player.nameText.text + ":" + Msg, Color.white);
                    }


					//if (iResult == -1)
					//{
					//    MyDrawMsg.OnSystemMessage(0); //이미존재
					//}
					//else
					//{
					//    XmlDataManager.inst().GetChar().SN = iResult;
					//    XmlDataManager.inst().SetSaveData();
					//    MyDrawMsg.OnSystemMessage(1);
					//}
				}
                break;
                case PROTOCOL.ROOM_CONNECT_OTHER:
                {
                    int sn = msg.pop_int32();
                    string name = msg.pop_string();
                    int level = msg.pop_int32();

                    ChatManager.current.MsgPrint(
              name + " entered.",Color.green);

                    BattleManager.current.AddPlayer(sn, name, level);
                    BattleManager.current.CreateFirst();
                }
                break;
                case PROTOCOL.ROOM_EXIT_OTHER:
                {
                    BattleManager.current.DeleteUser(msg.pop_int32());

                }
                break;
            case PROTOCOL.MOVING_USER_REQ:
                {
                    int SN = msg.pop_int32();
                    Vector3 Pos = Vector3.zero;

                    Player obj = BattleManager.current.FindUser(SN);

                    if (obj != null)
                    {
                        Pos.x = msg.pop_float();
                        Pos.y = msg.pop_float();
                        Pos.z = msg.pop_float();

                        //if (Vector3.Distance(obj.transform.position, Pos) > 5f)
                        //    obj.transform.position = Pos;
                        //else
                            obj.TargetMove(Pos);
                      }

                    //BattleManager.current.myPlayer.(msg.pop_int32());
                }
                break;
            case PROTOCOL.HEART_SEND_REQ:
                {
                    int OtherSN = msg.pop_int32();
                    Namemanager.current.SendUserPrint(OtherSN);

                    PlayerPrefs.SetInt("HeartExp",
                    PlayerPrefs.GetInt("HeartExp", 0) + 1);
                    PlayerPrefs.Save();
                    Namemanager.current.RefreshInfo();
                }
                break;

        }
    }
}
