using System.Collections.Generic;
using UnityEngine;

namespace JbProject
{
    public class Table_Mgr : SingletonMono<Table_Mgr>
    {
        /// <summary>
        /// 테이블 정보를 전부 읽는다. , 로딩부분에서 1회만 호출시킨다.
        /// </summary>
        public void AllLoad()
        {
            //각각의 테이블은 매니저형태를 가진다. 여기서는 단지 로드를 호출해줇뿐이다.
            SpawnTable.Instance.LoadTable();
            MapTable.Instance.LoadTable();
            ActorTable.Instance.LoadTable();

            //로딩끝난후 호출
            CompleteLoad();
        }

        /// <summary>
        /// 모든 테이블의 로딩이 끝나고 호출해준다.
        /// </summary>
        private void CompleteLoad()
        {
            SpawnTable.Instance.CreateExtraData();
            MapTable.Instance.CreateExtraData();
            ActorTable.Instance.CreateExtraData();
        }


        /// <summary>
        /// 전부해지한다. = 내용구현은 필요한다.
        /// </summary>
        public void AllRelese()
        {
            SpawnTable.Instance.Relese();
            MapTable.Instance.Relese();
            ActorTable.Instance.Relese();
        }

    }
}