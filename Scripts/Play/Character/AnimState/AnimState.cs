using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JbProject
{
    public abstract class AnimState : MonoBehaviour, FSM_Cell
    {
        protected Animator MyAnimator = null;
        protected string AnimationKey = string.Empty;


        //1.가지고 있는 애니메이터를 등록해놓는다.
        public virtual void OnInit(Enum inType)
        {
            AnimationKey = inType.ToString();
            MyAnimator = this.GetComponentInChildren<Animator>();
        }


        //2.가지고 있던 정보를 다 해제한다.
        public virtual void OnRelese() 
        {
            AnimationKey = string.Empty;
        }

        //3.자기가 해당하는 애니메이션 키를 재생시킨다.
        public virtual void OnEnter() 
        {
            MyAnimator.SetTrigger(AnimationKey);
        }

        //4.자신이 가진 키를 뺀다.
        public virtual void OnExit() 
        {

        }

        
    }
}