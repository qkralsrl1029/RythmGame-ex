using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterFlame : MonoBehaviour
{
   
    bool musicStart = false;
    

    public void ResetMusic()
    {
        musicStart = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Note")&&!musicStart)   //첫번째 노트가 프레임에 닿았을때 bgm실행
        {
            AudioManager.instance.PlayBGM("BGM0");
            musicStart = true;
        }
    }
}
