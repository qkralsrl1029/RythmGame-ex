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
        AudioManager.instance.PlayBGM("BGM" + currentSong);     //배경음악 변경
    }

    public void BtnBack()
    {
        TitleMenu.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void BtnPlay()
    {
        GameManager.instance.GameStart();
        this.gameObject.SetActive(false);
    }
}
