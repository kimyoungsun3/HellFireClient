using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollower : MonoBehaviour {


	void Start () {
		
	}

  //  Vector3 preMousePos;
	void Update () {


        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Vector2 MovePos = Input.GetTouch(0).deltaPosition;
                if (MovePos.x > 5f)
                    transform.Rotate(new Vector3(0f, Time.deltaTime * 10f * MovePos.x, 0f), Space.World);
                else if (MovePos.x < -5f)
                    transform.Rotate(new Vector3(0f, Time.deltaTime * 10f * MovePos.x, 0f), Space.World);
            }
        }

        {

            //if (Input.GetKeyDown(KeyCode.Mouse0))
            //{
            //    preMousePos = Input.mousePosition;
            //}
            //if (Input.GetKey(KeyCode.Mouse0))
            //{
            //    Vector2 MovePos = Input.mousePosition - preMousePos;
            //    if (MovePos.x > 2f)
            //        transform.Rotate(new Vector3(0f, Time.deltaTime * 10f * MovePos.x, 0f), Space.World);
            //    else if (MovePos.x < -2f)
            //        transform.Rotate(new Vector3(0f, Time.deltaTime * 10f * MovePos.x, 0f), Space.World);

            //    preMousePos = Input.mousePosition;
            //}
        }
    }
}
