using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Test
{
	public interface IScript
	{
		void on_message(Const<byte[]> buffer);
		//void on_removed();
		//void Send(CPacket _msg);
		//void disconnect();
		//void process_user_operation(CPacketSender );
	}
}
