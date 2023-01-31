using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JbProject
{
    [System.Serializable]
    public class MapTable_Data : TableBase_Data
    {
        public string MapName;
        public int[] SpawnIds;       //스폰되는 Id를 배열로가지고있는다.
    }


    public class MapTable : TableBase<MapTable_Data>
    {
    }
}