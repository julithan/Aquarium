using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JbProject
{
    public class UI_Network_Client : MonoBehaviour
    {
        public InputField IpField;
        public Image ConnectImg;

        private string IpAdress = string.Empty;
        private Vector3 m_vecMouseDownPos;

        //스폰과 동시에 해당 물고기를 지울지 여부를 정한다.
        public bool IsSpawnAndDelete = false;

        //애니메이션 버튼 생성관리UI
        public UI_AnimationEvent_Panel AnimationEventPanel = null;

        /// <summary>
        /// 서버IP를 설정한다.
        /// </summary>
        public void OnEvent_IpSet()
        {
            IpAdress = IpField.text;

            Client_Mgr.Instance.SetIp(IpAdress);

            ColorDebug.Log($"OnEvent_IpConfirm : {IpAdress}");
        }

        public void OnEvent_ClientConnect()
        {
            ColorDebug.Log("OnEvent_ClientConnect");
            Client_Mgr.Instance.Start_Client(out bool isConnect);

            if (ConnectImg != null)
            {
                if (isConnect)
                    ConnectImg.color = Color.green;
                else
                    ConnectImg.color = Color.red;
            }
        }


        private void Update()
        {
            touchClick();
        }

        private void touchClick()
        {
#if UNITY_EDITOR
            // 마우스 클릭 시
            if (Input.GetMouseButtonDown(0))
#else
        // 터치 시
        if (Input.touchCount > 0)
#endif
            {

#if UNITY_EDITOR
                m_vecMouseDownPos = Input.mousePosition;
#else
            m_vecMouseDownPos = Input.GetTouch(0).position;
            if(Input.GetTouch(0).phase != TouchPhase.Began)
                return;
#endif
                // 카메라에서 스크린에 마우스 클릭 위치를 통과하는 광선을 반환합니다.
                Ray ray = Camera.main.ScreenPointToRay(m_vecMouseDownPos);
                RaycastHit hit;

                // 광선으로 충돌된 collider를 hit에 넣습니다.
                if (Physics.Raycast(ray, out hit))
                {
                    var targetObj = hit.transform.gameObject.GetComponent<FlockUnit>();
                    if (null != targetObj)
                    {
                        // 오브젝트 별로 코드를 작성할 수 있습니다.
                        SimplePacket newPacket = new SimplePacket();

                        ColorDebug.Log($"name : {hit.collider.name}  index : {targetObj.fishindex}");
                        newPacket.Index = targetObj.fishindex;
                        newPacket.Type = 0;

                        JbProject.Client_Mgr.Instance.Send(newPacket);

                        //패널에 버튼 생성시킨다.
                        AnimationEventPanel.CreateBtn(targetObj.fishindex);

                        //해당bool값이 켜져있다면 소환하고 바로지운다.
                        if (IsSpawnAndDelete)
                        {
                            //지우려고 했더니 에러가 많다.
                            //DestroyImmediate(targetObj.gameObject);
                            targetObj.gameObject.SetActive(false);
                        }
                            
                    }
                }

            }
        }


    }
}