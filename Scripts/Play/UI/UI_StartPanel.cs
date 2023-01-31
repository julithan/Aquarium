using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_StartPanel : MonoBehaviour
{
    /// <summary>
    /// 시작버튼 => 씬이동 간단하게
    /// </summary>
    public void OnClick_Start()
    {
        SceneManager.LoadScene("Remote_APK");
    }
}
