using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public int m_SN;
    public UnityEngine.UI.Text msgTextWindow;
    public GameObject TalkObj;
    Animator anim;

    public UnityEngine.UI.Text nameText;
    public UnityEngine.UI.Text LevelText;
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }
    public void SendMsg(string msg)
    {
		//Debug.Log(msg);
        TalkObj.SetActive(true);
        msgTextWindow.text = msg;
        CancelInvoke("DisableTalk");
        Invoke("DisableTalk", 20f);
    }

    UnityEngine.AI.NavMeshAgent nav;
    public void TargetMove(Vector3 pos)
    {
		Debug.Log(enabled);
         nav.SetDestination(pos);
    }
    void DisableTalk()
    {
        TalkObj.SetActive(false);
    }
    float Distance;
	void Update ()
    {
        if (nav.remainingDistance <= nav.stoppingDistance)
            anim.SetBool("run", false);
        else
            anim.SetBool("run", true);

    }
}
