using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JbProject
{
    /// <summary>
    /// 상태머신을 간단하게 만들어본다. 반드시 아래순서로 호출되어야한다.
    /// 1.Regist
    /// 2.Start
    /// 3.Change(변경필요시)
    /// 4.Relese(다사용후 완전비울때)
    /// </summary>
    /// <typeparam name="eType"> key로 사용할 enum값이다. </typeparam>
    public class FSM<eType> where eType : System.Enum
    {
        #region variables
        //Start함수를 호출할때 변경된다. 이후에 Change함수 호출이 가능해진다.
        private bool IsStart = false;
        
        //현재상태
        private eType CurrentState;

        private Dictionary<eType, FSM_Cell> StateDic = null;
        #endregion


        #region functions
        //1. 상태 등록
        public void Regist_State(eType inType, FSM_Cell inClass)
        {
            //없으면 만들어주기
            if (null == StateDic) { StateDic = new Dictionary<eType, FSM_Cell>(); }

            //예외처리
            if (true == StateDic.ContainsKey(inType)) 
            { 
                Debug.Log("키중복"); return; 
            }
            else if (null == inClass) 
            { 
                Debug.Log("Null Class"); return; 
            }


            //최종등록완료
            StateDic.Add(inType, inClass);

            //Init호출
            StateDic[inType].OnInit(inType);
        }

        //2. 상태 시작 : 최초의 상태를 지정해준다.
        public void Start_State(eType inType)
        {

            if (null == StateDic)
                return;

            if (false == StateDic.ContainsKey(inType))
                return;

            CurrentState = inType;
            StateDic[CurrentState].OnEnter();
            IsStart = true;
        }

        //3. 상태 변경 : 필요시 호출, 기존걸 빼고 새로들어온 상태를 넣어준다.
        public void Change_State(eType inType)
        {
            //해당변수를 둠으로 인해 반드시 currentState가 올바르게 정의되있음을 확정한다.
            if (false == IsStart)
                return;

            if (CurrentState.Equals(inType))
                return;

            //기존상태에서 나간다.
            StateDic[CurrentState].OnExit();

            //새로운 상태를 넣어준다.
            StateDic[inType].OnEnter();

            //마지막으로 바뀐상태를 등록해놓는다.
            CurrentState = inType;
        }

        //4. 상태 제거 : 전부삭제한다.
        public void Relese_State()
        {
            Clear_State();
            StateDic = null;
        }

        //etc : 비우기 : 데이터 정리가 필요할때 호출한다.
        public void Clear_State()
        {
            if (null == StateDic)
                return;

            //전부정리
            foreach (var item in StateDic)
                item.Value.OnRelese();

            //최종비우기
            StateDic.Clear();

            //등록취소
            IsStart = false;
        }
        #endregion

    }
}