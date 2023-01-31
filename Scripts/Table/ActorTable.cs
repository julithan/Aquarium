using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JbProject
{
    [System.Serializable]
    public class ActorTable_Data : TableBase_Data
    {   
        public string Name = "";
        public string ModelPath = "";

    }

    public class ActorTable : TableBase<ActorTable_Data>
    {

    }
}