using System.Collections.Generic;
using UnityEngine;
using System.IO;


namespace JbProject
{
    public class Json_Mgr : SingletonMono<Json_Mgr>
    {
        [System.Serializable]
        public class Wrapper<T>
        {
            public T[] Items;
        }

        //파일 경로를 받아서 Json형태로 바꾼다.
        //주의할점은 반드시 Json파일이  Items 이름을가진 배열형태여야한다.
        public T[] LoadJson<T>(string inPath)
        {        
            //1.파일을 읽는다.
            FileInfo loadFile = new FileInfo(inPath);
            if (loadFile.Exists == false)
            {
                ColorDebug.Log($"파일 없음 path : {inPath}");
                return null;
            }
            
            //2.Json형태로 추출해준다.
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(File.ReadAllText(loadFile.FullName));
            if (null == wrapper)
                ColorDebug.Log($"Json로드 실패 : {typeof(T).Name}");

            return wrapper.Items;
        }

        //파일 경로를 받아서 Json형태로 바꾼다.
        //주의할점은 반드시 Json파일이  Items 이름을가진 배열형태여야한다.
        public T[] LoadJson2<T>(string inPath)
        {
            //1.
            var jsonText = Resources.Load<TextAsset>(inPath);


            //2.Json형태로 추출해준다.
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(jsonText.text);
            if (null == wrapper)
                ColorDebug.Log($"Json로드 실패 : {typeof(T).Name}");

            return wrapper.Items;
        }


        //Json파일 읽기 - 경로
        public string LoadFileData(string inPath)
        {   
            FileInfo loadFile = new FileInfo(inPath);
            if (loadFile.Exists == false)
            {
                ColorDebug.Log($"파일 없음 path : {inPath}");
                return string.Empty;
            }

            return File.ReadAllText(loadFile.FullName);
        }

        //해당클래스로 반환핸준다.
        //JsonUtility을 다른곳에서 호출하지않고 이곳에서만 호출하기위해만든다.
        public T[] LoadJsonData<T>(string inJsonStr) where T : class
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(inJsonStr);
            if (null == wrapper)
                ColorDebug.Log($"Json로드 실패 : {typeof(T).Name}");

            return wrapper.Items;
        }


        #region Json파일 저장하는함수 : 만들었지만 사용예정은 없다.(에디터 개발시 사용하면될듯하다)

        //배열데이터를 제이슨 형태로 변환
        public string SaveJsonArray<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, true);
        }
     
        //제이슨정보를 파일로 저장
        public void SaveJsonData<T>(T inData , string inPath) where T : class
        {
            //제이슨변환
            string jsonData = JsonUtility.ToJson(inData);

            //파일저장
            System.IO.FileInfo file = new System.IO.FileInfo(inPath);
            file.Directory.Create();
            File.WriteAllText(file.FullName, jsonData);
        }
        #endregion
    }
}