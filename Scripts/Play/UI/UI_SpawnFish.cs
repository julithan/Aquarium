using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JbProject
{
    public class UI_SpawnFish : SingletonMono<UI_SpawnFish>
    {
        //스폰관리할 부모오브젝트
        public Transform SpawnParent = null;

        private List<int> SpawnList = new List<int>();

        private Dictionary<int, GameObject> SpawnDic = new Dictionary<int, GameObject>();


        //외부에서 호출해서 물고기 한마리씩 꺼내놓는다.
        public void OnEventSpawn(int inIndex)
        {
            //스폰된 ID를 관리한다. 우선은1회만 호출되게 작성한다. 2회이상 필요시 개조해야한다.
            if (SpawnDic.ContainsKey(inIndex))
                return;

            if (false == SpawnTable.Instance.TryGetData(inIndex, out var tempTable))
            {
                ColorDebug.Log($"스폰테이블에 없는 인덱스입니다. {inIndex}");
                return;
            }

            //리소스에서 불러온다.(추후에 번들시스템이 필요하다면 번들로빼야한다)
            string path = $"Prefab/Fish/{tempTable.ResourceName}";
            GameObject obj = Resources.Load<GameObject>(path);
            var target = Instantiate(obj);
            target.transform.SetParent(SpawnParent);

            var pos = tempTable.Pos;
            target.transform.localPosition = new Vector3(pos[0], pos[1], pos[2]);

            //관리Dic에 넣는다.
            SpawnDic.Add(inIndex, target);
        }

        //애니메이션을 재생시킨다.
        public void OnEventAnimation(int inIndex)
        {
            if (false == SpawnDic.ContainsKey(inIndex))
                return;

            ColorDebug.Log($"OnEventAnimation : {inIndex}");

            var targetFlock = SpawnDic[inIndex].GetComponentInChildren<Flock>();
            if (targetFlock == null)
                return;

            //0번은 디폴트값 = 수윔에 해당한다.
            int targetAnim = Random.Range(1, (int)FlockUnit.eAnimType.Max);
            targetFlock.SetAnimation((FlockUnit.eAnimType)targetAnim);
        }


        //테스트코드 : K누르면 1번 소환
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
                OnEventSpawn(1);

            if (Input.GetKeyDown(KeyCode.J))
                OnEventAnimation(1);
        }
    }
}