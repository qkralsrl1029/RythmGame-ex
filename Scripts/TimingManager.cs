using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingManager : MonoBehaviour
{
    public List<GameObject> boxNoteList = new List<GameObject>();   //생성된 노트들을 담을 리스트
    [SerializeField] Transform Center = null;
    [SerializeField] RectTransform[] timingRect = null;             //perfect,cool,good,bad
    Vector2[] timingBoxes = null;       //판정범위의 최솟값 최댓값

    int[] judgementRecord = new int[5];     //결과창에 넘겨줄 정보( 퍼펙트 몇개, 쿨 몇개...)

    EffectManager theEffect;
    ScoreManager theScore;
    ComboManager theCombo;
    StageManager theStage;
    PlayerController thePlayer;
    AudioManager theAudio;

    // Start is called before the first frame update
    void Start()
    {
        theEffect = FindObjectOfType<EffectManager>();
        theScore = FindObjectOfType<ScoreManager>();
        theCombo = FindObjectOfType<ComboManager>();
        theStage = FindObjectOfType<StageManager>();
        thePlayer= FindObjectOfType<PlayerController>();
        theAudio = AudioManager.instance;


        timingBoxes = new Vector2[timingRect.Length];
        for (int i = 0; i < timingRect.Length; i++)     //판정범위 설정
        {
            timingBoxes[i].Set(Center.localPosition.x - timingRect[i].rect.width / 2        //센터에서 좌우측으로 이미지의 반을 최소/최대로 설정
                , Center.localPosition.x + timingRect[i].rect.width / 2);
        }
    }

    public void init()
    {
        for (int i = 0; i < judgementRecord.Length; i++)
            judgementRecord[i] = 0;
    }

    public bool CheckTiming()       //노트가 알맞은 타이밍에 눌리는지 체크
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
                    
                   
                    if (CheckCanNextPlate())            //제대로된 발판으로 이동했을때만
                    {
                        theScore.IncreaseScore(j);      //정확도별 점수 증가 
                        theStage.ShowNext();            //노트 판정시 다음 발판 호출
                        theEffect.JudgementEffect(j);   //판정이펙트 호출
                        judgementRecord[j]++;           //정확도별 개수 카운트
                    }

                    theAudio.PlaySFX("Clap");      //노트판정 효과음은 게임 중 가장 많이 호출되므로 미리 선언 후 사용
                    return true;                         //perfect->bad순으로 검사하여 판정범위안에있으면 리턴(이벤트호출시 가장 높은 점수를 리턴)

                }
            }
        }

        //Miss
        theCombo.ResetCombo();             
        theEffect.JudgementEffect(4);
        missCount();
        return false;
    }

    bool CheckCanNextPlate()        //플레이어가 알맞은 발판으로 이동했는지 체크
    {
        if(Physics.Raycast(thePlayer.destination,Vector3.down,out RaycastHit hit,1.1f)) //레이저를 쏴서
        {
            if(hit.transform.CompareTag("BasicPlate"))  //맞은 객체가 발판일때
            {
                BasicPlate temp = hit.transform.GetComponent<BasicPlate>();
                if (temp.flag)
                {
                    temp.flag = false;
                    return true;
                }
            }
        }
        return false;
    }


    public int[] GetRecord()        //판정기록들 넘겨주기
    {
        return judgementRecord;
    }

    public void missCount()
    {
        judgementRecord[4]++;
    }
}
