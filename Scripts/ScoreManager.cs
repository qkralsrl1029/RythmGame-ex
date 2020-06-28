using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    [SerializeField] Text textScore = null;
    [SerializeField] int increaseScore=10;
    [SerializeField] float[] weight = null; //정확도별 가중치
    [SerializeField] int BonusScore = 10;   //콤보별 보너스점수
    int currentScore=0;

    Animator anim;
    ComboManager theCombo;
    // Start is called before the first frame update
    void Start()
    {
        theCombo = FindObjectOfType<ComboManager>();
        anim = GetComponent<Animator>();
        currentScore = 0;
        textScore .text= "0";
    }

    public void IncreaseScore(int hitType)      //판정 참조(퍼펙트...배드)
    {
        theCombo.IncreaseCombo();   //점수오르면 콤보증가
        int combo = theCombo.GetCombo();

        int bonus = (combo / 10) * BonusScore;

        int t_increaseScore = increaseScore+combo;
        t_increaseScore = (int)(t_increaseScore * weight[hitType]);

        currentScore += t_increaseScore;
        textScore.text = string.Format("{0:#,##0}", currentScore);      //문자열 형식

        anim.SetTrigger("ScoreUp");

    }
}
