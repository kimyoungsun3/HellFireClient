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

        //�Ŵ����� ���ù� ���
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
    ///// ������ ���ӵ� ���Ŀ� ó���� ����.
    ///// ���콺 �Է��� ������ ENTER_GAME_ROOM_REQ���������� ��û�ϰ� 
    ///// �ߺ� ��û�� �����ϱ� ���ؼ� ���� �ڷ�ƾ�� ���� ��Ų��.
    ///// </summary>
    ///// <returns></returns>
    IEnumerator after_connected()
    {
        // CBattleRoom�� ���ӿ��� ���¿��� ���콺 �Է��� ���� ���� ȭ������ �Ѿ������ �Ǿ� �ִµ�,
        // �� ������ ������ �� �ڷ�ƾ�� ����� ��� ���� ���콺 �Է��� �����ִ°����� �ǴܵǾ�
        // ���� ȭ������ ���ƿ��� ���� ENTER_GAME_ROOM_REQ��Ŷ�� ������ ���� �߻��Ѵ�.
        // ���� ������ �� �������� �ǳʶپ� ���� �����Ӻ��� �ڷ�ƾ�� ������ ����� �� �ֵ��� �Ѵ�.
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
        //  //  String test = "���� ���� ����";
        //    SystemMsg.OnSystemMessage("���� ���� ����");
        //}
    }


    public GameObject LoadingImage;
    ///// <summary>
    ///// ������ ������ �Ϸ�Ǹ� ȣ���.
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
    ///// ��Ŷ�� ���� ���� �� ȣ���.
    ///// </summary>
    ///// <param name="protocol"></param>
    ///// <param name="msg"></param>
    //���ú�
    public void on_recv(CPacket msg)
    {
        // ���� ���� �������� ���̵� �����´�.
        PROTOCOL protocol_id = (PROTOCOL)msg.pop_protocol_id();

        switch (protocol_id)
        {
            case PROTOCOL.CREATE_ROOM_OK:
                {
                    BattleManager.current.m_MySN = msg.pop_int32();
                    //�� ��������
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
					//    MyDrawMsg.OnSystemMessage(0); //�̹�����
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
