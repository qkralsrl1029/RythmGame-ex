using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterFlame : MonoBehaviour
{
    AudioSource theAudio;
    bool musicStart = false;
    // Start is called before the first frame update
    void Start()
    {
        theAudio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Note")&&!musicStart)   //첫번째 노트가 프레임에 닿았을때 bgm실행
        {
            theAudio.Play();
            musicStart = true;
        }
    }
}
