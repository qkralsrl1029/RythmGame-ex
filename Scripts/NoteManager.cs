using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public int bpm = 0;
    double currentTime = 0d;    //오차를 줄이기위해 더블변수 사용.

    [SerializeField] Transform tfNoteAppear = null;
    

    TimingManager theTimingManager;
    EffectManager theEffectManager;

    private void Start()
    {
        theTimingManager = GetComponent<TimingManager>();
        theEffectManager = FindObjectOfType<EffectManager>();
    }
    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime>=60d/bpm)    //1beat시간
        {
            GameObject t_note = ObjectPool.instance.noteQueue.Dequeue();        //옵젝 풀링
            t_note.transform.position = tfNoteAppear.position;
            t_note.SetActive(true);
        
            theTimingManager.boxNoteList.Add(t_note);       //리스트에 추가
            currentTime -= 60d / bpm;       //그냥 0으로 초기화시켜버리면 프레임별 시간적 오차만큼 손실이 발생함.
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)     //일정거리 밖으로 나가면 노트 파괴
    {
        if (collision.CompareTag("Note"))
        {
            if(collision.GetComponent<Note>().GetNoteFlag())        //이미지가 사라지지않은상태에서(제때 클릭하지않은상태에서) 벽에 닿으면 미스처리
                theEffectManager.JudgementEffect(4);
            theTimingManager.boxNoteList.Remove(collision.gameObject);  //리스트에서 제거하고
            ObjectPool.instance.noteQueue.Enqueue(collision.gameObject);    //다시 큐에 반납
            collision.gameObject.SetActive(false);
            //Destroy(collision.gameObject);

        }
    }
}
