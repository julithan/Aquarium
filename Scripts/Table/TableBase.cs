using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace JbProject
{
    /// <summary>
    /// 모든 테이블 데이터는 해당 클래스를 상속받는다.
    /// </summary>
    public abstract class TableBase_Data
    {
        public int Id; //기본 keyIndex로 사용한다.
    }



    /// <summary>
    /// 싱글톤으로 만들어서 가지고 있는다 = 언제든 접근이 용이해진다.
    /// </summary>
    public class TableBase<T> : Singleton<TableBase<T>> where T : TableBase_Data
    {
        //미리파싱해서 가지고있는다.
        public Dictionary<int, T> TableDic = new Dictionary<int, T>();
        private bool IsInit = false;

        //테이블이 해체할때 사용한다.(사용안할듯하다)
        public virtual void Relese() { }

        //모든테이블이 로드되고 나서 호출된다. => 다른 테이블을 참조해서 가지고오고싶은 데이터가 있다면 이곳을 사용한다.
        public virtual void CreateExtraData() { }

        //TableMgr에서 호출된다.
        public void LoadTable()
        {
            //두번 호출되는일이 없도록한다.
            if (IsInit)
                return;

            //테이블 이름 = class이름과 같게한다.
            string tempName = typeof(T).Name;

            //데이터를 가지고올 경로
            //string tempPath = string.Format(BundleName.TablePath, tempName);
            string tempPath = string.Format(BundleName.TablePath2, tempName);

            //Json정보를 받아온다.
            //var target = Json_Mgr.Instance.LoadJson<T>(tempPath);
            var target = Json_Mgr.Instance.LoadJson2<T>(tempPath);

            if (null == target || 0 > target.Length)
                return;

            //만들어진 데이터를 넣어준다.
            for (int i = 0; i < target.Length; i++)
                TableDic.Add(target[i].Id, target[i]);


            //두번 호출되는일이 없도록한다.
            IsInit = true;
        }

        //Id에 해당하는 Data를 반환해준다.
        public T GetData(int inId)
        {
            //두번 호출되는일이 없도록한다.
            if (false == IsInit)
            {
                ColorDebug.Log("해당테이블이 로드되지않았다.");
                return null;
            }

            if (false == TableDic.ContainsKey(inId))
                return null;

            return TableDic[inId];
        }

        //Id에 해당하는 Data를 반환해준다. 예외체크를 바로해준다.
        public bool TryGetData(int inId , out T Data)
        {
            Data = null;
            if (false == IsInit)
                return false;

            if (false == TableDic.ContainsKey(inId))
                return false;

            Data = TableDic[inId];
            return true;
        }
    }
}

