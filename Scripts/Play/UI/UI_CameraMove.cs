using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JbProject
{ 

public class UI_CameraMove : MonoBehaviour
{
        private Vector3 StartRotation;
        private Vector3 RightEndPos;
        private Vector3 LeftEndPos;

        //속도
        private float Speed = 0.01f;

        //움직일각도
        private float Angle = 20f;
        private float Right;
        private float Left;

        private float TimeCheck = 0f;
        private bool Reverse = false;


        private void Awake()
        {
            StartRotation = this.transform.rotation.eulerAngles;

            //RightEndPos = new Vector3(StartRotation.x, StartRotation.y + Angle, StartRotation.z);
            //LeftEndPos = new Vector3(StartRotation.x, StartRotation.y - Angle, StartRotation.z);

            Right = Speed;
            Left = Speed * -1;
        }


        private void LateUpdate()
        {
            TimeCheck += Time.deltaTime;
            if (TimeCheck > 5f)
            {
                TimeCheck = 0f;
                Reverse = !Reverse;
            }

            if (Reverse)
                Speed = Right;
            else
                Speed = Left;


            //if (RightEndPos.y < transform.rotation.eulerAngles.y)
            //    Speed = Left;
            //else if (LeftEndPos.y > transform.rotation.eulerAngles.y)
            //    Speed = Right;

            transform.Rotate(0f, Speed, 0f, Space.World);
        }      
    }
}