using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JbProject
{
    /// <summary>
    /// Fsm조각 : FSM을 쓰기위해선 해당 인터페이스를 상속받은 클래스에만 사용할수 있다.
    /// </summary>
    public interface FSM_Cell
    {
        #region functions
        
        /// <summary>
        /// 초기화 : 등록시1회호출
        /// </summary>
        public void OnInit(System.Enum inType);

        /// <summary>
        /// 해제 : 삭제시1회호출
        /// </summary>
        public void OnRelese();

        /// <summary>
        /// 상태입장 : 상태변경시 1회 호출
        /// </summary>
        public void OnEnter();

        /// <summary>
        /// 상태탈출 : 상태변경시 1회 호출
        /// </summary>
        public void OnExit();

        #endregion
    }

}
