using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;

namespace JbProject
{
    //클라이언트 : 접속 시도자
    public class Client_Mgr : SingletonMono<Client_Mgr>
    {
        //local IP
        public string serverIp = "192.168.219.103";
        private Socket clientSocket = null;
        private bool IsConnectd = false;

        /// <summary>
        /// 서버 아이피를 세팅한다. 반드시 먼저호출되어야한다.
        /// </summary>
        public void SetIp(string inIpStr)
        {
            serverIp = inIpStr;
        }

        /// <summary>
        /// 클라이언트를 실행시킨다.
        /// </summary>
        public void Start_Client(out bool isConnect)
        {
            isConnect = false;

            //클라이언트에서 사용할 소켓 준비
            this.clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //클라이언트는 바인딩할 필요 없음

            //접속할 서버의 통신지점(목적지)
            IPAddress serverIPAdress = IPAddress.Parse(this.serverIp);
            IPEndPoint serverEndPoint = new IPEndPoint(serverIPAdress, Server_Mgr.PortNumb);

            //서버로 연결 요청
            try
            {
                ColorDebug.Log("Connecting to Server");
                this.clientSocket.Connect(serverEndPoint);
                isConnect = true;
            }
            catch (SocketException e)
            {
                ColorDebug.Log("Connection Failed:" + e.Message);
            }

            //접속되었는지 넣어준다.
            IsConnectd = isConnect;
        }

        private void OnApplicationQuit()
        {
            if (this.clientSocket != null)
            {
                this.clientSocket.Close();
                this.clientSocket = null;
            }
        }

        /// <summary>
        /// 패킷구조체를 보낸다.
        /// </summary>
        /// <param name="packet">해당 패킷구조체에 맞게 보낸다.</param>
        public void Send(SimplePacket packet)
        {
            if (clientSocket == null)
                return;

            if (IsConnectd == false)
                return;


            byte[] sendData = SimplePacket.ToByteArray(packet);
            byte[] prefSize = new byte[1];

            //버퍼의 가장 앞부분에 이 버퍼의 길이에 대한 정보가 있는데 이것을 먼저 보낸다.
            prefSize[0] = (byte)sendData.Length;    
            clientSocket.Send(prefSize);    


            clientSocket.Send(sendData);

            Debug.Log("Send Packet from Client :" + packet.Index.ToString());
        }

    }
}