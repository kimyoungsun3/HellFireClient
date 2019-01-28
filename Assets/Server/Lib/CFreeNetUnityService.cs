using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using FreeNet;

namespace FreeNetUnity
{
	/// <summary>
	/// FreeNet엔진과 유니티 어플리케이션을 이어주는 클래스이다.
	/// FreeNet엔진에서 받은 접속 이벤트, 메시지 수신 이벤트등을 어플리케이션으로 전달하는 역할을 하는데
	/// MonoBehaviour를 상속받아 유니티 어플리케이션과 동일한 스레드에서 작동되도록 구현하였다.
	/// 따라서 이 클래스의 콜백 매소드에서 유니티 오브젝트에 접근할 때 별도의 동기화 처리는 하지 않아도 된다.
	/// </summary>
	public class CFreeNetUnityService : MonoBehaviour
	{
		CFreeNetEventManager event_manager;

		// 연결된 게임 서버 객체.
		IPeer gameserver;

		// TCP통신을 위한 서비스 객체.
		CNetworkService service;

		// 접속 완료시 호출되는 델리게이트. 어플리케이션에서 콜백 매소드를 설정하여 사용한다.
		public delegate void StatusChangedHandler(NETWORK_EVENT status);
		public StatusChangedHandler appcallback_on_status_changed;

		// 네트워크 메시지 수신시 호출되는 델리게이트. 어플리케이션에서 콜백 매소드를 설정하여 사용한다.
		public delegate void MessageHandler(CPacket msg);
		public MessageHandler appcallback_on_message;

		void Awake()
		{
			CPacketBufferManager.initialize(10);
			this.event_manager = new CFreeNetEventManager();
		}


        static private IPAddress GetIPV6(string strHostName)
        {
            //if (!System.Net.Sockets.Socket.OSSupportsIPv6)
            //{
            //    return null;
            //}

            IPAddress thisIp = null;

            IPHostEntry iphostentry = Dns.GetHostEntry(strHostName);// Find host name

            foreach (IPAddress ipAddress in iphostentry.AddressList)// Grab the first IP addresses
            {
                if (ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                    thisIp = ipAddress;
            }
            return thisIp;
        }
        static private IPAddress GetIPV4(string strHostName)
        {

            IPAddress thisIp = null;

            IPHostEntry iphostentry = Dns.GetHostEntry(strHostName);// Find host name

            foreach (IPAddress ipAddress in iphostentry.AddressList)// Grab the first IP addresses
            {
                if (ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    thisIp = ipAddress;
            }
            return thisIp;
        }

        public void connect(string host,int port)
        {
            if (this.service != null)
            {
                Debug.LogError("re connected.");
                CConnector connector = new CConnector(service);
                // 접속 성공시 호출될 콜백 매소드 지정.
                connector.connected_callback += on_connected_gameserver;

                //4바이트아이피
                IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse(host), port);
                connector.connect(endpoint, AddressFamily.InterNetwork);
                return;
            }
            else
            {
                // CNetworkService객체는 메시지의 비동기 송,수신 처리를 수행한다.
                this.service = new CNetworkService();

                // endpoint정보를 갖고있는 Connector생성. 만들어둔 NetworkService객체를 넣어준다.
                CConnector connector = new CConnector(service);
                // 접속 성공시 호출될 콜백 매소드 지정.
                connector.connected_callback += on_connected_gameserver;

                //4바이트아이피
                IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse(host), port);
                connector.connect(endpoint, AddressFamily.InterNetwork);
            }

        }

        public void connects(string DnsName,int port)
        {
            if (this.service != null)
            {
                Debug.LogError("re connected.");
                CConnector connector = new CConnector(service);
                // 접속 성공시 호출될 콜백 매소드 지정.
                connector.connected_callback += on_connected_gameserver;

                //IPAddress ipAddr = GetIPV6(DnsName);
                //if (ipAddr != null)
                //{
                //    IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 49495);
                //    connector.connect(ipEndPoint, ipAddr.AddressFamily);
                //}
                //else
                {
                    IPAddress ipAddr = GetIPV4(DnsName);
                    if (ipAddr != null)
                    {
                        IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 49494);
                        connector.connect(ipEndPoint, ipAddr.AddressFamily);
                    }
                }
                return;
            }
            else
            {
                // CNetworkService객체는 메시지의 비동기 송,수신 처리를 수행한다.
                this.service = new CNetworkService();

                // endpoint정보를 갖고있는 Connector생성. 만들어둔 NetworkService객체를 넣어준다.
                CConnector connector = new CConnector(service);
                // 접속 성공시 호출될 콜백 매소드 지정.
                connector.connected_callback += on_connected_gameserver;

                //IPAddress ipAddr = GetIPV6(DnsName);
                //if (ipAddr != null)
                //{
                //    IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 49495);
                //    connector.connect(ipEndPoint, ipAddr.AddressFamily);
                //}
                //else
                {
                    IPAddress ipAddr = GetIPV4(DnsName);
                    if (ipAddr != null)
                    {
                        IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 49494);
                        connector.connect(ipEndPoint, ipAddr.AddressFamily);
                    }
                }
            }

        }

		public void connect(int port)
		{
            if (this.service != null)
            {
                Debug.LogError("re connected.");
                CConnector connector = new CConnector(service);
                // 접속 성공시 호출될 콜백 매소드 지정.
                connector.connected_callback += on_connected_gameserver;

                IPAddress ipAddr = GetIPV6("ec2-52-196-246-78.ap-northeast-1.compute.amazonaws.com");
                if (ipAddr != null)
                {
                    IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 49495);
                    connector.connect(ipEndPoint, ipAddr.AddressFamily);
                }
                else
                {
                    ipAddr = GetIPV4("ec2-52-196-246-78.ap-northeast-1.compute.amazonaws.com");
                    if (ipAddr != null)
                    {
                        IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 49494);
                        connector.connect(ipEndPoint, ipAddr.AddressFamily);
                    }
                }
                return;
            }
            else
            {
                // CNetworkService객체는 메시지의 비동기 송,수신 처리를 수행한다.
                this.service = new CNetworkService();

                // endpoint정보를 갖고있는 Connector생성. 만들어둔 NetworkService객체를 넣어준다.
                CConnector connector = new CConnector(service);
                // 접속 성공시 호출될 콜백 매소드 지정.
                connector.connected_callback += on_connected_gameserver;

                IPAddress ipAddr = GetIPV6("ec2-52-196-246-78.ap-northeast-1.compute.amazonaws.com");
                if (ipAddr != null)
                {
                    IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 49495);
                    connector.connect(ipEndPoint, ipAddr.AddressFamily);
                }
                else
                {
                    ipAddr = GetIPV4("ec2-52-196-246-78.ap-northeast-1.compute.amazonaws.com");
                    if (ipAddr != null)
                    {
                        IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 49494);
                        connector.connect(ipEndPoint, ipAddr.AddressFamily);
                    }
                }
            }

		}



        public void iosconnect(int port)
		{
            if (this.service != null)
            {
                Debug.LogError("re connected.");
                CConnector connector = new CConnector(service);
                // 접속 성공시 호출될 콜백 매소드 지정.
                connector.connected_callback += on_connected_gameserver;

                IPAddress ipAddr = GetIPV6("ec2-52-197-124-209.ap-northeast-1.compute.amazonaws.com");
                if (ipAddr != null)
                {
                    IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 49495);
                    connector.connect(ipEndPoint, ipAddr.AddressFamily);
                }
                else
                {
                    ipAddr = GetIPV4("ec2-52-197-124-209.ap-northeast-1.compute.amazonaws.com");
                    if (ipAddr != null)
                    {
                        IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 49494);
                        connector.connect(ipEndPoint, ipAddr.AddressFamily);
                    }
                }
                return;
            }
            else
            {
                // CNetworkService객체는 메시지의 비동기 송,수신 처리를 수행한다.
                this.service = new CNetworkService();

                // endpoint정보를 갖고있는 Connector생성. 만들어둔 NetworkService객체를 넣어준다.
                CConnector connector = new CConnector(service);
                // 접속 성공시 호출될 콜백 매소드 지정.
                connector.connected_callback += on_connected_gameserver;

                IPAddress ipAddr = GetIPV6("ec2-52-197-124-209.ap-northeast-1.compute.amazonaws.com");
                if (ipAddr != null)
                {
                    IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 49495);
                    connector.connect(ipEndPoint, ipAddr.AddressFamily);
                }
                else
                {
                    ipAddr = GetIPV4("ec2-52-197-124-209.ap-northeast-1.compute.amazonaws.com");
                    if (ipAddr != null)
                    {
                        IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 49494);
                        connector.connect(ipEndPoint, ipAddr.AddressFamily);
                    }
                }
            }

		}

		public bool is_connected()
		{
			return this.gameserver != null;
		}


		/// <summary>
		/// 접속 성공시 호출될 콜백 매소드.
		/// </summary>
		/// <param name="server_token"></param>
		void on_connected_gameserver(CUserToken server_token)
		{
			this.gameserver = new CRemoteServerPeer(server_token);
			((CRemoteServerPeer)this.gameserver).set_eventmanager(this.event_manager);

			// 유니티 어플리케이션으로 이벤트를 넘겨주기 위해서 매니저에 큐잉 시켜 준다.
			this.event_manager.enqueue_network_event(NETWORK_EVENT.connected);
		}

		/// <summary>
		/// 네트워크에서 발생하는 모든 이벤트를 클라이언트에게 알려주는 역할을 Update에서 진행한다.
		/// FreeNet엔진의 메시지 송수신 처리는 워커스레드에서 수행되지만 유니티의 로직 처리는 메인 스레드에서 수행되므로
		/// 큐잉처리를 통하여 메인 스레드에서 모든 로직 처리가 이루어지도록 구성하였다.
		/// </summary>
		void Update()
		{
            if (this.event_manager == null)
                return;

			// 수신된 메시지에 대한 콜백.
			if (this.event_manager.has_message())
			{
				CPacket msg = this.event_manager.dequeue_network_message();
				if (this.appcallback_on_message != null)
				{
					this.appcallback_on_message(msg);
				}
			}

			// 네트워크 발생 이벤트에 대한 콜백.
			if (this.event_manager.has_event())
			{
				NETWORK_EVENT status = this.event_manager.dequeue_network_event();
				if (this.appcallback_on_status_changed != null)
				{
					this.appcallback_on_status_changed(status);
				}
			}
		}

        public void ClearServer()
        {
            this.service = null;
            this.gameserver = null;
        }
		public bool send(CPacket msg)
		{
			try
			{
				this.gameserver.send(msg);
				CPacket.destroy(msg);
			}
			catch (Exception e)
			{
				Debug.LogError(e.Message);

                //  connect("127.0.0.1", 49494);
                //connect("118.223.126.62", 49494);
                //  connect("58.75.21.185", 49494);

                //connect("110.12.241.238", 49494);
                // connect("192.168.12.63", 49494);

               // connects("ec2-54-238-166-148.ap-northeast-1.compute.amazonaws.com", 49494); //내서버
           
                return false;
			}

            return true;
		}


		/// <summary>
		/// 정상적인 종료시에는 OnApplicationQuit매소드에서 disconnect를 호출해 줘야 유니티가 hang되지 않는다.
		/// </summary>
		void OnApplicationQuit()
		{
			if (this.gameserver != null)
			{
				((CRemoteServerPeer)this.gameserver).token.disconnect();
			}
		}
        public void QuitDisconnect()
        {
            if (this.gameserver != null)
            {
                ((CRemoteServerPeer)this.gameserver).token.disconnect();
            }
        }
	}

}

