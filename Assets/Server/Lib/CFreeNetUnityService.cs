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
	/// FreeNet������ ����Ƽ ���ø����̼��� �̾��ִ� Ŭ�����̴�.
	/// FreeNet�������� ���� ���� �̺�Ʈ, �޽��� ���� �̺�Ʈ���� ���ø����̼����� �����ϴ� ������ �ϴµ�
	/// MonoBehaviour�� ��ӹ޾� ����Ƽ ���ø����̼ǰ� ������ �����忡�� �۵��ǵ��� �����Ͽ���.
	/// ���� �� Ŭ������ �ݹ� �żҵ忡�� ����Ƽ ������Ʈ�� ������ �� ������ ����ȭ ó���� ���� �ʾƵ� �ȴ�.
	/// </summary>
	public class CFreeNetUnityService : MonoBehaviour
	{
		CFreeNetEventManager event_manager;

		// ����� ���� ���� ��ü.
		IPeer gameserver;

		// TCP����� ���� ���� ��ü.
		CNetworkService service;

		// ���� �Ϸ�� ȣ��Ǵ� ��������Ʈ. ���ø����̼ǿ��� �ݹ� �żҵ带 �����Ͽ� ����Ѵ�.
		public delegate void StatusChangedHandler(NETWORK_EVENT status);
		public StatusChangedHandler appcallback_on_status_changed;

		// ��Ʈ��ũ �޽��� ���Ž� ȣ��Ǵ� ��������Ʈ. ���ø����̼ǿ��� �ݹ� �żҵ带 �����Ͽ� ����Ѵ�.
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
                // ���� ������ ȣ��� �ݹ� �żҵ� ����.
                connector.connected_callback += on_connected_gameserver;

                //4����Ʈ������
                IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse(host), port);
                connector.connect(endpoint, AddressFamily.InterNetwork);
                return;
            }
            else
            {
                // CNetworkService��ü�� �޽����� �񵿱� ��,���� ó���� �����Ѵ�.
                this.service = new CNetworkService();

                // endpoint������ �����ִ� Connector����. ������ NetworkService��ü�� �־��ش�.
                CConnector connector = new CConnector(service);
                // ���� ������ ȣ��� �ݹ� �żҵ� ����.
                connector.connected_callback += on_connected_gameserver;

                //4����Ʈ������
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
                // ���� ������ ȣ��� �ݹ� �żҵ� ����.
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
                // CNetworkService��ü�� �޽����� �񵿱� ��,���� ó���� �����Ѵ�.
                this.service = new CNetworkService();

                // endpoint������ �����ִ� Connector����. ������ NetworkService��ü�� �־��ش�.
                CConnector connector = new CConnector(service);
                // ���� ������ ȣ��� �ݹ� �żҵ� ����.
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
                // ���� ������ ȣ��� �ݹ� �żҵ� ����.
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
                // CNetworkService��ü�� �޽����� �񵿱� ��,���� ó���� �����Ѵ�.
                this.service = new CNetworkService();

                // endpoint������ �����ִ� Connector����. ������ NetworkService��ü�� �־��ش�.
                CConnector connector = new CConnector(service);
                // ���� ������ ȣ��� �ݹ� �żҵ� ����.
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
                // ���� ������ ȣ��� �ݹ� �żҵ� ����.
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
                // CNetworkService��ü�� �޽����� �񵿱� ��,���� ó���� �����Ѵ�.
                this.service = new CNetworkService();

                // endpoint������ �����ִ� Connector����. ������ NetworkService��ü�� �־��ش�.
                CConnector connector = new CConnector(service);
                // ���� ������ ȣ��� �ݹ� �żҵ� ����.
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
		/// ���� ������ ȣ��� �ݹ� �żҵ�.
		/// </summary>
		/// <param name="server_token"></param>
		void on_connected_gameserver(CUserToken server_token)
		{
			this.gameserver = new CRemoteServerPeer(server_token);
			((CRemoteServerPeer)this.gameserver).set_eventmanager(this.event_manager);

			// ����Ƽ ���ø����̼����� �̺�Ʈ�� �Ѱ��ֱ� ���ؼ� �Ŵ����� ť�� ���� �ش�.
			this.event_manager.enqueue_network_event(NETWORK_EVENT.connected);
		}

		/// <summary>
		/// ��Ʈ��ũ���� �߻��ϴ� ��� �̺�Ʈ�� Ŭ���̾�Ʈ���� �˷��ִ� ������ Update���� �����Ѵ�.
		/// FreeNet������ �޽��� �ۼ��� ó���� ��Ŀ�����忡�� ��������� ����Ƽ�� ���� ó���� ���� �����忡�� ����ǹǷ�
		/// ť��ó���� ���Ͽ� ���� �����忡�� ��� ���� ó���� �̷�������� �����Ͽ���.
		/// </summary>
		void Update()
		{
            if (this.event_manager == null)
                return;

			// ���ŵ� �޽����� ���� �ݹ�.
			if (this.event_manager.has_message())
			{
				CPacket msg = this.event_manager.dequeue_network_message();
				if (this.appcallback_on_message != null)
				{
					this.appcallback_on_message(msg);
				}
			}

			// ��Ʈ��ũ �߻� �̺�Ʈ�� ���� �ݹ�.
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

               // connects("ec2-54-238-166-148.ap-northeast-1.compute.amazonaws.com", 49494); //������
           
                return false;
			}

            return true;
		}


		/// <summary>
		/// �������� ����ÿ��� OnApplicationQuit�żҵ忡�� disconnect�� ȣ���� ��� ����Ƽ�� hang���� �ʴ´�.
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

