using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRoomBtn : MonoBehaviour {

    public UnityEngine.UI.InputField InputText;
    public UnityEngine.UI.Dropdown dropdown;
    public void OnCreate()
    {
        if (InputText.text == "")
        {
            InputText.text = "Room";
        }
        RoomManager.current.Createroom(InputText.text, dropdown.value + 2);
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
