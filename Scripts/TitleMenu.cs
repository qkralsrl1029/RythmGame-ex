﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMenu : MonoBehaviour
{
    [SerializeField] GameObject goStageUI = null;

    public void BtnPlay()
    {
        AudioManager.instance.StopBGM();
        AudioManager.instance.PlayBGM();
        goStageUI.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
