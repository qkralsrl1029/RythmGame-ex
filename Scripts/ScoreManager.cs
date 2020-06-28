using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    [SerializeField] Text textScore = null;
    [SerializeField] int increaseScore=10;
    [SerializeField] float[] weight = null; //콤보별 가중치
    int currentScore=0;

    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        currentScore = 0;
        textScore .text= "0";
    }

    public void IncreaseScore(int hitType)      //판정 참조(퍼펙트...배드)
    {
        int t_increaseScore = increaseScore;
        t_increaseScore = (int)(t_increaseScore * weight[hitType]);

        currentScore += t_increaseScore;
        textScore.text = string.Format("{0:#,##0}", currentScore);      //문자열 형식

        anim.SetTrigger("ScoreUp");

    }
}
