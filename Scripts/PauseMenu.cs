using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject go_Base;


    public void CallMenu()
    {
        
        go_Base.gameObject.SetActive(true);
        Time.timeScale = 0f;        //일시정지창 실행시 타임스케일 0, 시간흐름 정지
    }

    public void CloseMenu()
    {
        
        go_Base.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

}
