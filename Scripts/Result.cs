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
        goUI.SetActive(true);

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
}
