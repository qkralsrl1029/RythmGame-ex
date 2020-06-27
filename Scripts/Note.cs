﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public float noteSpeed = 400;
    UnityEngine.UI.Image noteImage;
    
    // Update is called once per frame
    private void Start()
    {
        noteImage = GetComponent<UnityEngine.UI.Image>();
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
