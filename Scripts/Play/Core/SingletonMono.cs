using UnityEngine;

namespace JbProject
{
    //상속받아서 처리하게한다.
    public abstract class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour
    {
        private const string TargetObjectName = "SingletonMono_Obj";

        private static T instance = null;
        public static T Instance
        {
            get
            {
                if (null == instance)
                    instance = CreateInstance();

                return instance;
            }
        }

        protected static T CreateInstance()
        {
            //1.게임오브젝트찾기
            GameObject tempObj = GameObject.Find(TargetObjectName);

            //2.없으면 새로만들어주기
            if (null == tempObj)
            {
                tempObj = new GameObject(TargetObjectName);
                DontDestroyOnLoad(tempObj);
                //ColorDebug.Log($"매니저 오브젝트 최초생성");
            }


            //3.컴포넌트찾기
            T target = FindObjectOfType<T>();

            //4.없으면 새로 만들어주기
            if (null == target)
            {
                target = tempObj.AddComponent<T>();
                //ColorDebug.Log($"싱글톤 생성확인 : {typeof(T).ToString()}");
            }
            else
            {
                if (target.gameObject != tempObj)
                    ColorDebug.Log("나와서는 안되는 에러이다.");
            }

            return target;
        }
    }
}