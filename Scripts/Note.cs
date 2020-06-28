using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public float noteSpeed = 400;
    UnityEngine.UI.Image noteImage;

    // Update is called once per frame
    private void OnEnable()     //옵젝풀링기법을 사용해서 매번 활성/비활성을 반복하기때문에 활성화될때마다 초기화 시켜줘야함
    {
        if (noteImage == null)
            noteImage = GetComponent<UnityEngine.UI.Image>();

        noteImage.enabled = true;
    }
    
    void Update()
    {
        transform.localPosition += Vector3.right * noteSpeed*Time.deltaTime;
    }

    public void HideNote()
    {
        noteImage.enabled = false;
    }

    public bool GetNoteFlag()
    {
        return noteImage.enabled;
    }
}
