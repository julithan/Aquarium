using UnityEngine;

namespace JbProject
{
    //상속받아서 처리하게한다.
    public abstract class Singleton<T> where T : Singleton<T>, new()
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new T();
                    instance.Init();
                }

                return instance;
            }
        }

        protected virtual void Init() { }
    }
}