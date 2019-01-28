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

        //�Ŵ����� ���ù� ���
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

    public bool bTestServer;
    void Update()
    {
        if (bTestServer)
            return;

        //if(network_manager.is_connected() == false)
        //{
        //  //  String test = "���� ���� ����";
        //    SystemMsg.OnSystemMessage("���� ���� ����");
        //}
    }


    ///// <summary>
    ///// ������ ������ �Ϸ�Ǹ� ȣ���.
    ///// </summary>
    public void on_connected()
    {
        this.user_state = USER_STATE.CONNECTED;

        StartCoroutine("after_connected");
    }

    ///// <summary>
    ///// ��Ŷ�� ���� ���� �� ȣ���.
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
    //���ú�
    public void on_recv(CPacket msg)
    {
        // ���� ���� �������� ���̵� �����´�.
        PROTOCOL protocol_id = (PROTOCOL)msg.pop_protocol_id();

        //switch (protocol_id)
        //{
           
        //     break;
        //}
    }
}
