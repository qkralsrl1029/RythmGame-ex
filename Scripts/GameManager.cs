using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] GameObject[] goGameUI=null;
    [SerializeField] GameObject goTitleUI = null;


    //게임 리셋용 클래스 참조
    ComboManager theCombo;
    ScoreManager theScore;
    TimingManager theTiming;
    StatusManager theStatus;
    PlayerController thePlayer;
    StageManager theStage;

    public bool isStartGame = false;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        theCombo = FindObjectOfType<ComboManager>();
        thePlayer = FindObjectOfType<PlayerController>();
        theScore = FindObjectOfType<ScoreManager>();
        theStatus = FindObjectOfType<StatusManager>();
        theTiming = FindObjectOfType<TimingManager>();
        theStage = FindObjectOfType<StageManager>();
    }

    public void GameStart()     //게임 시작, 관련 옵젝들 활성화
    {
        for (int i = 0; i < goGameUI.Length; i++)
        {
            goGameUI[i].SetActive(true);
        }
        isStartGame = true;
        NoteManager.isDone = false;

        theStage.RemoveStage();     //맵 초기화
        theStage.SettingStage();    //맵 생성
        theCombo.ResetCombo();      //콤보리셋
        theScore.init();            //점수 리셋
        thePlayer.init();           //플레이어 위치 리셋
        theStatus.init();           //플레이어 체력 리셋
        theTiming.init();           //점수리셋2
    }

    public void MainMenu()
    {
        for (int i = 0; i < goGameUI.Length; i++)
        {
            goGameUI[i].SetActive(false);
        }
        goTitleUI.SetActive(true);
    }
}
