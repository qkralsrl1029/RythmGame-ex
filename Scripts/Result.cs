using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    [SerializeField] GameObject goUI = null;
    [SerializeField] Text[] txt=null;
    [SerializeField] Text txtCoin = null;
    [SerializeField] Text txtScore = null;
    [SerializeField] Text txtMaxCombo = null;

    ScoreManager theScore;
    ComboManager theCombo;
    TimingManager theTiming;
    DatabaseManager theData;
    [SerializeField]StageMenu theStage;     //게임진행중엔 스테이지메뉴는 비활성상태여서 find로 찾기 안됨

    

    // Start is called before the first frame update
    void Start()
    {
        theScore = FindObjectOfType<ScoreManager>();
        theCombo = FindObjectOfType<ComboManager>();
        theTiming = FindObjectOfType<TimingManager>();
        theData = FindObjectOfType<DatabaseManager>();
    }

    public void ShowResult()
    {
        AudioManager.instance.StopBGM();    //결과창 호출시 브금 재생 종료
        FindObjectOfType<CenterFlame>().ResetMusic();   //게임 처음부터 시작시 브금이 초기화될수있도록

        goUI.SetActive(true);               //결과창 띄우기

        int sCore = theScore.GetScore();    //점수 참조

        //게임 진행 중 얻은 정보들 텍스트에 저장
        for (int i = 0; i < txt.Length; i++)
        {
            txt[i].text = "0";
        }
        txtCoin.text = "0";
        txtScore.text = "0";
        txtMaxCombo.text = "0";

        int[] tempCount = theTiming.GetRecord();    //각 정확도별 얼마 기록했는지 참조

        //콤보,점수,정확도 결과창에 띄우기
        for (int i = 0; i < txt.Length; i++)
        {
            txt[i].text = string.Format("{0:#,##0}", tempCount[i]);
        }
        txtScore.text = string.Format("{0:#,##0}", sCore);
        txtMaxCombo.text = string.Format("{0:#,##0}", theCombo.GetMaxCombo());

        //현재점수 전달, 최고점수일시 최신화
        theData.SaveData(sCore);
    }

    public void BtnMain()       //메인메뉴 호출 버튼
    {
        theStage.resetSong();       //선택곡 초기화
        AudioManager.instance.StopBGM();
        goUI.SetActive(false);      //결과창 끄고
        theCombo.ResetCombo();      //콤보도 끄고
        GameManager.instance.MainMenu();    //메인메뉴창 호출
    }
}
