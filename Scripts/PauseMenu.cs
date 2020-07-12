using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour      //일시정지 메뉴
{
    [SerializeField] GameObject go_Base;            //메뉴창 활성화
    [SerializeField] PlayerController thePlayer;    //게임 리셋시 플레이어 위치 초기화


    private void Start()
    {
        go_Base.SetActive(false);
    }

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

    public void restart()
    {
        thePlayer.transform.position = Vector3.zero;        //재시작하면 플레이어 위치 초기화
        AudioManager.instance.StopBGM();                    //브금 종료
        CloseMenu();
        StartCoroutine(restartCoroutine());
    }

    IEnumerator restartCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        GameManager.instance.isStartGame = true;
        
    }

    //메인메뉴 버튼은 Result함수에서 호출

}
