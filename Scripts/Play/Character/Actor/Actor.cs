using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JbProject
{
    //캐릭터 객체의 Base가되는 클래스이다.
    //abstract : 상속받아서 써라
    public abstract class Actor : MonoBehaviour
    {
        public enum eAnimType
        {
            Idle = 0,
            Move,
            Attack,
            Max,
        }

        //애니메이션 상태제어
        public FSM<eAnimType> AnimFSM = new FSM<eAnimType>();


        private void Awake()
        {
            Init_AnimFSM();
        }



        //AnimFsm등록
        private void Init_AnimFSM()
        {
            //등록
            AnimFSM.Regist_State(eAnimType.Idle, gameObject.AddComponent<AnimState_Idle>());
            AnimFSM.Regist_State(eAnimType.Move, gameObject.AddComponent<AnimState_Move>());
            AnimFSM.Regist_State(eAnimType.Attack, gameObject.AddComponent<AnimState_Attack>());

            //실행
            AnimFSM.Start_State(eAnimType.Idle);
        }

        public virtual void OnEventMove()
        {
            AnimFSM.Change_State(eAnimType.Move);
        }

        public virtual void OnEvnetIdle()
        {
            AnimFSM.Change_State(eAnimType.Idle);
        }

        public virtual void OnEventAttack()
        {
            AnimFSM.Change_State(eAnimType.Attack);
        }
    }
}
