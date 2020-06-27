using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    //판정이펙트
    [SerializeField] Animator noteHitAnimator = null;
    string hit = "Hit";

    //판정 알리미
    [SerializeField] Animator JudgementAnimator = null;
    [SerializeField] UnityEngine.UI.Image judgementImage = null;
    [SerializeField] Sprite[] JudgementSprite = null;
    public void JudgementEffect(int num)        //판정마다 스프라이트교체
    {
        judgementImage.sprite = JudgementSprite[num];
        JudgementAnimator.SetTrigger(hit);
    }
    public void NoteHitEffect()
    {
        noteHitAnimator.SetTrigger(hit);
    }
   
}
