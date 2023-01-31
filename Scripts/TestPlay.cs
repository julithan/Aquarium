using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JbProject
{
    public class TestPlay : MonoBehaviour
    {
        private void Start()
        {
            Loding();

            Play();
        }

        private void Loding()
        {
            //테이블로딩
            Table_Mgr.Instance.AllLoad();
        }

        private void Play()
        {
            //소환한다 가정하자
            Spawn();
        }


        //스폰테이블을 이용해 불러보자
        private void Spawn()
        {
            if (false == MapTable.Instance.TryGetData(2, out var mapTable))
                return;

            var spawnArr = mapTable.SpawnIds;
            for (int i = 0; i < spawnArr.Length; i++)
            {
                if (false == SpawnTable.Instance.TryGetData(spawnArr[i], out var spawnTable))
                    continue;

                if (false == ActorTable.Instance.TryGetData(spawnTable.Id, out var actorTable))
                    continue;


                ColorDebug.Log( $"name : {actorTable.Name}  path : {actorTable.ModelPath}");
            }


        }
     
    }

}
