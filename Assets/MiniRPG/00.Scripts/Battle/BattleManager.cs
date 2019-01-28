using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour {

    public int m_MySN; //내꺼 SN
    public static BattleManager current;
    public Player myPlayer;

    public Transform camTrans;
    void Awake()
    {
        if (current == null)
            current = this;
        else
            Debug.LogError("Not Single BattleManager");
        gameObject.SetActive(false);

    }
    List<Player> PlayerArray = new List<Player>();

    public List<Player> GetPlayerList()
    {
        return PlayerArray;
    }
    public GameObject playerPrefab;

    public GameObject[] CharPrefabs;
    public void AddPlayer(int _SN,string Name,int Level)
    {
        int Chartype = _SN % 8;

        GameObject Obj = Instantiate(playerPrefab);
        GameObject CharObj = Instantiate(CharPrefabs[Chartype]);
        CharObj.transform.SetParent(Obj.transform);
        CharObj.transform.position = Vector3.zero;
        CharObj.transform.rotation = Quaternion.identity;
        CharObj.transform.localScale = Vector3.one;


        Obj.transform.Translate(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
        Player tmp = Obj.GetComponent<Player>();
        tmp.m_SN = _SN;
        tmp.nameText.text = Name;
        tmp.LevelText.text = Level.ToString();
        if (m_MySN == _SN)
            myPlayer = tmp;

        PlayerArray.Add(tmp);
    }

    public void DeleteUser(int _SN)
    {
        for (int i = 0; i < PlayerArray.Count; ++i)
        {
            if (PlayerArray[i].m_SN == _SN)
            {
                ChatManager.current.MsgPrint(
                    PlayerArray[i].nameText.text + " has left.", Color.red);

                Destroy(PlayerArray[i].gameObject);
                PlayerArray.RemoveAt(i);
                return;
            }
        }

    }

    public Player FindUser(int _SN)
    {
        for(int i=0; i < PlayerArray.Count; ++i)
        {
            if (PlayerArray[i].m_SN == _SN)
                return PlayerArray[i];
        }

        return null;
    }
    public void SetuserSN(int PlayerCount,int SN)
    {
       // PlayerArray[PlayerCount].m_SN = SN;
    }
    void AllClearUser()
    {
        for (int i = 0; i < PlayerArray.Count; ++i)
        {
            if (PlayerArray[i] != null)
            {
                Destroy(PlayerArray[i].gameObject);
                PlayerArray[i] = null;
            }
        }
        PlayerArray.Clear();
    }
    bool First = false;

    public admobdemo ad;
    void OnEnable ()
    {
     //   if(First)
        {
            ad.ShowAd();
        }
        First = true;
        // ID_CHANGE_REQ_SEND
        //  AllClearUser();
    }

    void OnDisable()
    {
        AllClearUser();
    }

    float fSendTimer = 0;

    public GameObject QuitBtn;

    void Update () {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            QuitBtn.SetActive(true);
        }

        if (myPlayer != null)
        {
            fSendTimer += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (fSendTimer >= 0.5f)
                {
                    RaycastHit hitObj;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                    if (Physics.Raycast(ray, out hitObj, 100f, LayerMask.GetMask("Floor")))
                    {
                        myPlayer.TargetMove(hitObj.point);
                        CPacketSender.MOVING_USER_REQ_SEND(m_MySN, hitObj.point);
                        fSendTimer = 0f;
                    }
                }
            }
            camTrans.position = Vector3.Lerp(camTrans.position, myPlayer.transform.position, Time.deltaTime * 5f) ;
        }
    }
    public void CreateFirst()
    {
        if(myPlayer != null)
            CPacketSender.MOVING_USER_REQ_SEND(m_MySN, myPlayer.transform.position);
    }
}
