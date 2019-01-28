using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatManager : MonoBehaviour {



    public GameObject msgPrefab;
    public Transform ParObj;

    public static ChatManager current;
    void Awake()
    {
        if (current == null)
            current = this;
        else
            Debug.LogError("Not Single ChatManager");
    }

    public void MsgPrint (string msg,Color color)
    {
        GameObject Tmpobj = Instantiate(msgPrefab);
        Chatlogmsg heart = Tmpobj.GetComponent<Chatlogmsg>();
        heart.OnMsg(msg, color);
        heart.transform.SetParent(ParObj);
        heart.transform.localScale = Vector3.one;
        heart.transform.position = Vector3.zero;
        ParObj.Translate(0f, 30f, 0f);

        if(ParObj.GetChildCount() > 30)
        {
            Destroy(ParObj.GetChild(0).gameObject);
        }
    }
}
