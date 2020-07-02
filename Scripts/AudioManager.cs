using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]       //데이터 직렬화
public class Sound
{
    public string name;
    public AudioClip clip;
}


public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;        //접근성 용이, 자기자신 인스턴스화

    [SerializeField] Sound[] sfx = null;
    [SerializeField] Sound[] bgm = null;

    [SerializeField] AudioSource bgmPlayer = null;  //브금은 한개씩만 플레이
    [SerializeField] AudioSource[] sfxPlayer = null;


    private void Start()
    {
        instance = this;
    }

    public void PlayBGM(string bgmName)
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            if (bgmName == bgm[i].name)     //파라매터로 넘어온 이름이 있는지 검사
            {
                bgmPlayer.clip = bgm[i].clip;
                bgmPlayer.Play();           //있으면 플레이
                return;
            }
        }
    }
    public void StopBGM()
    {
        bgmPlayer.Stop();
    }



    public void PlaySFX(string sfxName)
    {
        for (int i = 0; i < sfx.Length; i++)
        {
            if (sfxName == sfx[i].name)     //같은 이름이 있는지 검사하고
            {
                for (int j = 0; j < sfxPlayer.Length; j++)
                {
                    if(!sfxPlayer[j].isPlaying)     //남아있는 오디오 소스가 있는 지 검사
                    {
                        sfxPlayer[j].clip = sfx[i].clip;
                        sfxPlayer[j].Play();
                        return;
                    }
                }
                Debug.Log("남아있는 오디오소스가 없습니다");
                return;
            }
        }
        Debug.Log("해당 곡이 존재하지않습니다");
    }
    
}
