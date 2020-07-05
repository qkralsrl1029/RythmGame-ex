using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    AudioSource theAudio;
    NoteManager theNote;
    Result theResult;
    // Start is called before the first frame update
    void Start()
    {
        theAudio = GetComponent<AudioSource>();
        theNote = FindObjectOfType<NoteManager>();
        theResult = FindObjectOfType<Result>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //theAudio.Play();        //게임종료 사운드
            PlayerController.isDone = true; //움직임 봉쇄
            NoteManager.isDone = true;      //노트 생성 봉쇄
            theNote.removeNote();
            theResult.ShowResult();
        }
    }
}
