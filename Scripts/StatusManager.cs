using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusManager : MonoBehaviour
{
    Result theResult;                   //hp가 0이되면 결과창 호출

    public static bool isAlive = true;

    int maxHp = 3;
    int currentHp = 3;

    int maxShield = 3;
    int currentShield = 0;

    [SerializeField] GameObject[] hpImage=null;
    

    float blinkSpeed = 0.15f;       //체력감소시 깜빡이는 효과
    int blinkCount = 10;
    int currentBlinkCount=0;
    [SerializeField] MeshRenderer theMesh = null;
    bool isBlink = false;

    private void Start()
    {
        theResult = FindObjectOfType<Result>();
    }

    public void init()      //초기화
    {
        currentHp = maxHp;
        isAlive = true;
        SettingHpImage();
    }

    public void DecreaseHp(int num)
    {
        if (!isBlink)
        {
            currentHp -= num;
            SettingHpImage();
            if (currentHp <= 0)
            {
                isAlive = false;
                theResult.ShowResult();
                AudioManager.instance.StopBGM();
            }
            else
                StartCoroutine(BlinkCoroutine());
        }
    }

    void SettingHpImage()
    {
        for (int i = 0; i < hpImage.Length; i++)    
        {
            if (i < currentHp)              //현재 hp만큼 이미지 활성화
                hpImage[i].SetActive(true);
            else
                hpImage[i].SetActive(false);
        }
    }

    IEnumerator BlinkCoroutine()            //체력감소시 일정시간 무적효과
    {
        isBlink = true;
        while(currentBlinkCount<blinkCount)
        {
            theMesh.enabled = !theMesh.enabled;
            yield return new WaitForSeconds(blinkSpeed);
            currentBlinkCount++;
        }
        theMesh.enabled = true;
        isBlink = false;
        currentBlinkCount = 0;
    }
}
