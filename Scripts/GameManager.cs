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
    NoteManager theNote;
    Result theResult;
    DatabaseManager theData;
    [SerializeField] CenterFlame theMusic=null;      //비활성화된 객체는 find로 찾을수 없음!
    public bool isStartGame = false;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        theNote = FindObjectOfType<NoteManager>();
        theCombo = FindObjectOfType<ComboManager>();
        thePlayer = FindObjectOfType<PlayerController>();
        theScore = FindObjectOfType<ScoreManager>();
        theStatus = FindObjectOfType<StatusManager>();
        theTiming = FindObjectOfType<TimingManager>();
        theStage = FindObjectOfType<StageManager>();
        theResult = FindObjectOfType<Result>();
        theData = FindObjectOfType<DatabaseManager>();
    }

    public void GameStart(int p_songNum, float p_bpm)     //게임 시작, 관련 옵젝들 활성화
    {
        for (int i = 0; i < goGameUI.Length; i++)
        {
            goGameUI[i].SetActive(true);
        }

        isStartGame = true;
        AudioManager.instance.StopBGM();
        NoteManager.isDone = false;
        
        theNote.bpm = p_bpm;        //bpm초기화
        theMusic.BGMName = "BGM" + p_songNum;   //재생곡 초기화
        theMusic.musicStart = false;//브금 초기설정
        theStage.RemoveStage();     //맵 초기화
        theStage.SettingStage(p_songNum);    //맵 생성
        theCombo.ResetCombo();      //콤보리셋
        theScore.init();            //점수 리셋
        thePlayer.init();           //플레이어 위치 리셋
        theStatus.init();           //플레이어 체력 리셋
        theTiming.init();           //점수리셋2
        theData.SetCurrentSong(p_songNum);
        
    }

    public void MainMenu()
    {
        for (int i = 0; i < goGameUI.Length; i++)
        {
            goGameUI[i].SetActive(false);
        }
        goTitleUI.SetActive(true);
    }

    public void BtnRestart(int p_SongNum)
    {
        theStage.RemoveStage();     //맵 초기화
        theStage.SettingStage(p_SongNum);    //맵 생성
    }
    
}
