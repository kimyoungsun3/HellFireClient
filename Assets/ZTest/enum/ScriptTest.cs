using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
	public enum LOGIN_PROTO : short
	{
		CREATE_ROOM_REQ, //방 만들기 : string 방이름
		CREATE_ROOM_OK, //성공
		CREATE_ROOM_FAILED //실패
	}

	public class ScriptTest : MonoBehaviour, IScript
	{
		public void on_message(Const<byte[]> buffer)
		{
			//throw new System.NotImplementedException();
		}


		// Use this for initialization
		void Start()
		{
			LOGIN_PROTO x = LOGIN_PROTO.CREATE_ROOM_OK;
			Debug.Log(x + " -> " + (int)x);

			int _eee = 0;
			Debug.Log(_eee + " -> " + (LOGIN_PROTO)_eee);
			_eee = 1;
			Debug.Log(_eee + " -> " + (LOGIN_PROTO)_eee);
			_eee = 2;
			Debug.Log(_eee + " -> " + (LOGIN_PROTO)_eee);
		}
	}
}
