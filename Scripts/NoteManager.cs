using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public int bpm = 0;
    double currentTime = 0d;    //오차를 줄이기위해 더블변수 사용.

    [SerializeField] Transform tfNoteAppear = null;
    [SerializeField] GameObject goNote = null;
    

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime>=60d/bpm)    //1beat시간
        {
            GameObject t_note = Instantiate(goNote, tfNoteAppear.position, Quaternion.identity);
            t_note.transform.SetParent(this.transform);     //이미지는 캔버스 안에서만 가시화되기때문에 부모객체 설정
            currentTime -= 60d / bpm;       //그냥 0으로 초기화시켜버리면 프레임별 시간적 오차만큼 손실이 발생함.
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Note"))
            Destroy(collision.gameObject);
    }
}
