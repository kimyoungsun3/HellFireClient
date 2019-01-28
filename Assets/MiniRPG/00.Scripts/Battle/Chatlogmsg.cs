using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chatlogmsg : MonoBehaviour {

    public UnityEngine.UI.Text msgBox;

    public void OnMsg(string msg,Color color)
    {
        msgBox.text = msg;
        msgBox.color = color;
    }
}
