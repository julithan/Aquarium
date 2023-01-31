using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JbProject
{
    public class UI_AnimationEvent_BtnItem : MonoBehaviour
    {
        public Text FishName;
        private int FishId = 0;

        public void Init(int inId)
        {
            //0번 ID는 존재하지 않는다.
            if (inId <= 0)
                return;

            if (false == SpawnTable.Instance.TryGetData(inId, out var tempTable))
                return;

            //버튼이름설정
            FishName.text = tempTable.FishName;

            //등록
            FishId = inId;
        }

        public void OnEvent_ClickSelf()
        {
            //0번 ID는 존재하지 않는다.
            if (FishId <= 0)
                return;

            // 오브젝트 별로 코드를 작성할 수 있습니다.
            SimplePacket newPacket = new SimplePacket();
            newPacket.Index = FishId;
            newPacket.Type = 1; //요게핵심
            JbProject.Client_Mgr.Instance.Send(newPacket);
        }
    }
}