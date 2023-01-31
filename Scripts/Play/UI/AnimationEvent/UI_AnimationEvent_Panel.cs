using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JbProject
{
    public class UI_AnimationEvent_Panel : MonoBehaviour
    {
        public Transform Parent;
        public GameObject BtnPrefab;

        //관리목적
        private Dictionary<int, UI_AnimationEvent_BtnItem> BtnDic = new Dictionary<int, UI_AnimationEvent_BtnItem>();

        private void Start()
        {
            //테이블을 로드해놓는다.
            Table_Mgr.Instance.AllLoad();
        }

        public void CreateBtn(int inIndex)
        {
            //중복은 막는다.
            if (BtnDic.ContainsKey(inIndex))
                return;

            var target = Instantiate(BtnPrefab);
            if (target == null)
                return;

            var btnItem = target.GetComponent<UI_AnimationEvent_BtnItem>();
            if (btnItem == null)
                return;
            
            //초기화
            btnItem.Init(inIndex);

            //부모세팅
            target.transform.SetParent(Parent);

            //관리등록
            BtnDic.Add(inIndex, btnItem);
        }
    }
}