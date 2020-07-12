using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class Song       //곡의 정보 저장
{
    public string name;
    public string composer;
    public float bpm;

    public Sprite sprite;
}

public class StageMenu : MonoBehaviour
{
    [SerializeField] GameObject TitleMenu = null;       //백을 눌렀을때 돌아갈 타이틀 메뉴
    [SerializeField] Song[] songList=null;              //노래들 저장
    //각 노래마다의 정보 저장
    [SerializeField] Text txtSongName = null;
    [SerializeField] Text txtSongScore=null;
    [SerializeField] Text txtSongComposer = null;
    [SerializeField] Image imgDisk = null;

    [SerializeField] DatabaseManager theData;       //로드할 정보들이 담겨있는 저장소

    int currentSong = 0;        //현재곡의 배열인덱스 정보 저장

    private void OnEnable()     //점수갱신을 위해 활성화시마다 호출    
    {
        SettingSong();
        
    }

    public void BtnNext()
    {
        AudioManager.instance.PlaySFX("Touch");
        if (++currentSong > songList.Length - 1)        //인덱스 증가후 배열크기 초과시 다시 처음으로
            currentSong = 0;
        SettingSong();
        Debug.Log(currentSong);
    }

    public void BtnPrior()
    {
        AudioManager.instance.PlaySFX("Touch");
        if (--currentSong < 0)        
            currentSong = songList.Length - 1;
        SettingSong();
        Debug.Log(currentSong);
    }

    void SettingSong()
    {
        //변경된 노래의 정보 최신화
        txtSongName.text = songList[currentSong].name;
        txtSongComposer.text = songList[currentSong].composer;
        imgDisk.sprite = songList[currentSong].sprite;
        //브금변경, 노래 변경후 일정시간 대기 후 시작되도록 코루틴 사용
        StartCoroutine(changeBGM());

        theData.SetCurrentSong(currentSong);    //저장소에서 불러올 배열의 인덱스 전달
        theData.LoadData();                     //인덱스에 맞는 노래정보 호출, 이후 DB에서 직접 노래점수 변경
        //txtSongScore.text =string.Format("{0:#,##0}",theData.Scores[currentSong]);
    }

    void SettingForMain()       //코루틴 사용시 메인메뉴로 돌아갈때 오류
    {
        txtSongName.text = songList[currentSong].name;
        txtSongComposer.text = songList[currentSong].composer;
        imgDisk.sprite = songList[currentSong].sprite;
    }

    public void BtnBack()       //뒤로가기 버튼이 눌리면
    {
        AudioManager.instance.StopBGM();    //재생중이던 브금 멈추고
        currentSong = 0;                    //곡 초기화
        SettingSong();
        TitleMenu.SetActive(true);          //스테이지메뉴 비활성화/타이틀메뉴 활성화
        this.gameObject.SetActive(false);
    }

    public void BtnPlay()       //플레이 버튼이 눌리면
    {
        float t_bpm = songList[currentSong].bpm;
        GameManager.instance.GameStart(currentSong,t_bpm);      //게임매니저 호출
        this.gameObject.SetActive(false);                       //스테이지메뉴는 비활성화
    }

    public void BtnRestart()                                    //pause메튜넹서 일시정지버튼메뉴중 게임 리셋시 호출
    {
        GameManager.instance.BtnRestart(currentSong);
    }

    IEnumerator changeBGM()
    {
        AudioManager.instance.StopBGM();
        yield return new WaitForSeconds(0.5f);
        AudioManager.instance.PlayBGM("BGM" + currentSong);     //배경음악 변경
    }

    public void SetScore(string score)      //DB에서 직접 점수 변경
    {
        txtSongScore.text = score;
    }

    public int GetScore()             //DB에서 최고점수인지 판단하도록 현재 최고점수(스테이지메뉴에있는 점수) 전달
    {
        return int.Parse(txtSongScore.text);
    }

    public void resetSong()     //메인메뉴로 돌아갈때 호출
    {
        currentSong = 0;
        SettingForMain();
    }
}
