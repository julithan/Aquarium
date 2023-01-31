using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JbProject
{

    public class UI_Network_Server : MonoBehaviour
    {
        public Text ServerIp;

        private void Start()
        {
            ColorDebug.Log("Start_Server");
            ServerIp.text = Server_Mgr.Instance.Start_Server();


            //테이블 읽어놓는다.
            Table_Mgr.Instance.AllLoad();
        }
    }
}