using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JbProject
{
    [System.Serializable]
    public class SpawnTable_Data : TableBase_Data
    {
        public string FishName;         //물고기이름 ui표시용
        public string ResourceName;     //소환할 프리펩이름
        public float[] Pos;               //x,y,z의 포지션
    }


    public class SpawnTable : TableBase<SpawnTable_Data>
    {
    }
}