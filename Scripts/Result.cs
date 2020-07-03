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
    // Start is called before the first frame update
    void Start()
    {
        theScore = FindObjectOfType<ScoreManager>();
        theCombo = FindObjectOfType<ComboManager>();
        theTiming = FindObjectOfType<TimingManager>();
    }

    public void ShowResult()
    {
        AudioManager.instance.StopBGM();    //결과창 호출시 브금 재생 종료
        FindObjectOfType<CenterFlame>().ResetMusic();   

        goUI.SetActive(true);               //결과창 띄우기


        //게임 진행 중 얻은 정보들 텍스트에 저장
        for (int i = 0; i < txt.Length; i++)
        {
            txt[i].text = "0";
        }
        txtCoin.text = "0";
        txtScore.text = "0";
        txtMaxCombo.text = "0";

        int[] tempCount = theTiming.GetRecord();


        for (int i = 0; i < txt.Length; i++)
        {
            txt[i].text = string.Format("{0:#,##0}", tempCount[i]);
        }
        txtScore.text = string.Format("{0:#,##0}", theScore.GetScore());
        txtMaxCombo.text = string.Format("{0:#,##0}", theCombo.GetMaxCombo());
    }

    public void BtnMain()       //메인메뉴 호출 버튼
    {
        goUI.SetActive(false);      //결과창 끄고
        theCombo.ResetCombo();      //콤보도 끄고
        GameManager.instance.MainMenu();    //메인메뉴창 호출
    }
}
