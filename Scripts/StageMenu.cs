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
    [SerializeField] GameObject TitleMenu = null;
    [SerializeField] Song[] songList=null;
    [SerializeField] Text txtSongName = null;
    [SerializeField] Text txtSongComposer = null;
    [SerializeField] Image imgDisk = null;

    int currentSong = 0;        //현재곡의 배열인덱스 정보 저장

    private void Start()
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
        txtSongName.text = songList[currentSong].name;
        txtSongComposer.text = songList[currentSong].composer;
        imgDisk.sprite = songList[currentSong].sprite;
        StartCoroutine(changeBGM());
    }

    public void BtnBack()       //뒤로가기 버튼이 눌리면
    {
        AudioManager.instance.StopBGM();    //재생중이던 브금 멈추고
        currentSong = 0;                    //곡 초기화
        SettingSong();
        TitleMenu.SetActive(true);          //스테이지메뉴 비활성화/타이틀메뉴 활성화
        this.gameObject.SetActive(false);
    }

    public void BtnPlay()
    {
        float t_bpm = songList[currentSong].bpm;
        GameManager.instance.GameStart(currentSong,t_bpm);
        this.gameObject.SetActive(false);
    }

    IEnumerator changeBGM()
    {
        AudioManager.instance.StopBGM();
        yield return new WaitForSeconds(0.5f);
        AudioManager.instance.PlayBGM("BGM" + currentSong);     //배경음악 변경
    }


    public void resetSong()
    {
        currentSong = 0;
        SettingSong();
    }
}
