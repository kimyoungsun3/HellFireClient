using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {

    public void EnterLobby()
    {
        CPacketSender.ROOM_EXIT_REQ_SEND();
        gameObject.SetActive(true);
        for (int i = 0; i < Battlelist.Length; ++i)
            Battlelist[i].SetActive(false);

    }
    public GameObject QuitBtn;
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            QuitBtn.SetActive(true);
        }
    }

    public static RoomManager current;
    void Awake()
    {
        if (current == null)
            current = this;
        else
            Debug.LogError("Not Single Roommanager");

        Invoke("Refresh", 1f);
    }
    public GameObject[] Lobbylist;

    public GameObject[] Battlelist;
    public Transform roomList;

    //방 생성 성공
    public void RoomCreateSucces()
    {
        EneableList(true);
        gameObject.SetActive(false);

        for (int i = 0; i < Battlelist.Length; ++i)
            Battlelist[i].SetActive(true);
    }
    public void RoomCreateFailed()
    {
        EneableList(true);
    }
    public void EneableList(bool active)
    {
        for (int i = 0; i < Lobbylist.Length; ++i)
            Lobbylist[i].SetActive(active);
    }
    public void Createroom(string roomname,int maxUser)
    {
        CPacketSender.CREATE_ROOM_REQ_SEND(roomname, maxUser);
        EneableList(false);
    }
    public GameObject RoomPrefab;

    bool RefreshAble = true;
    void ActiveRefresh()
    {
        RefreshAble = true;
    }
    public void Refresh()
    {
        if (RefreshAble)
        {
            CPacketSender.ROOM_LIST_REQ_SEND();
            RefreshAble = false;
            Invoke("ActiveRefresh", 2f);
        }

    }
	

    public void ClearRoomlist()
    {
        int Count = roomList.childCount;
        for(int i=0; i < Count; ++i)
        {
            Destroy(roomList.GetChild(i).gameObject);
        }
    }
    public void RoomlistSetting(int _Roomnum, string _text,
        int CurrentUser,int MaxUser)
    {
        bool Full = false;
        if (CurrentUser < MaxUser)
            _text += "-" + CurrentUser + "/ " + MaxUser;
        else
        {
            Full = true;
            _text += "-Full";
        }

        GameObject roomobj =  Instantiate(RoomPrefab);
        roomobj.GetComponent<RoomObj>().CreateRoomInfo(_Roomnum, _text);
        if(Full)
        {
            roomobj.GetComponent<RoomObj>().DisableConnectBtn();
        }
        roomobj.transform.SetParent(roomList);
        roomobj.transform.localScale = Vector3.one;
        roomobj.transform.position = Vector3.zero;

    }

}
