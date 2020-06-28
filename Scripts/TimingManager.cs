using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingManager : MonoBehaviour
{
    public List<GameObject> boxNoteList = new List<GameObject>();   //생성된 노트들을 담을 리스트
    [SerializeField] Transform Center = null;
    [SerializeField] RectTransform[] timingRect = null;             //perfect,cool,good,bad
    Vector2[] timingBoxes = null;       //판정범위의 최솟값 최댓값

    EffectManager theEffect;
    ScoreManager theScore;

    // Start is called before the first frame update
    void Start()
    {
        theEffect = FindObjectOfType<EffectManager>();
        theScore = FindObjectOfType<ScoreManager>();

        timingBoxes = new Vector2[timingRect.Length];
        for (int i = 0; i < timingRect.Length; i++)     //판정범위 설정
        {
            timingBoxes[i].Set(Center.localPosition.x - timingRect[i].rect.width / 2        //센터에서 좌우측으로 이미지의 반을 최소/최대로 설정
                , Center.localPosition.x + timingRect[i].rect.width / 2);
        }
    }

    public void CheckTiming()
    {
        for (int i = 0; i < boxNoteList.Count; i++)
        {
            float t_notePosX = boxNoteList[i].transform.localPosition.x;
            for (int j = 0; j < timingBoxes.Length; j++)
            {
                if(timingBoxes[j].x<=t_notePosX&&t_notePosX<=timingBoxes[j].y)      //해당 노트가 판정범위안에 들어왔는지 검사 
                {
                    //노트제거
                    boxNoteList[i].GetComponent<Note>().HideNote();
                    if(j<timingBoxes.Length-1)      //bad판정이 아닐때 이펙트 호출
                        theEffect.NoteHitEffect();
                    boxNoteList.RemoveAt(i);
                    theEffect.JudgementEffect(j);
                    theScore.IncreaseScore(j);      //정확도별 점수 증가
                    return;                                                         //perfect->bad순으로 검사하여 판정범위안에있으면 리턴(이벤트호출시 가장 높은 점수를 리턴)

                }
            }
        }
        theEffect.JudgementEffect(4);       //Miss
    }
}
