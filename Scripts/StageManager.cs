using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] GameObject stage = null;
    GameObject currentStage;
    Transform[] stagePlates;

    [SerializeField] float offsetY = -3f;        //발판 등장시 연출효과 부여
    float offsetSpeed = 10f;


    int stepCount = 0;  //플레이어의 이동 횟수
    int totalPlateCount = 0;
    // Start is called before the first frame update
    public void SettingStage()      //호출시 초기화/생성
    {
        stepCount = 0;
        currentStage = Instantiate(stage, UnityEngine.Vector3.zero, UnityEngine.Quaternion.identity);
        stagePlates = currentStage.GetComponent<Stage>().platess;      //해당 스테이지의 비활성화된 발판들을 저장
        totalPlateCount = stagePlates.Length;

        for (int i = 0; i < totalPlateCount; i++)               //초기 발판 위치 설정, 활성화될때 다시 올라오게 설정
        {
            stagePlates[i].position = new UnityEngine.Vector3(stagePlates[i].position.x, stagePlates[i].position.y+offsetY, stagePlates[i].position.z);
        }
    }

    public void RemoveStage()
    {
        if (currentStage != null)
            Destroy(currentStage);
    }

   public void ShowNext()
   {
        if (stepCount < totalPlateCount)
            StartCoroutine(MoveCoroutine(stepCount++));
   }

    IEnumerator MoveCoroutine(int num)
    {
        stagePlates[num].gameObject.SetActive(true);
        
        //도착지 설정
        UnityEngine.Vector3 Destination = new UnityEngine.Vector3(stagePlates[num].position.x, 
                                                                  stagePlates[num].position.y - offsetY, 
                                                                  stagePlates[num].position.z);

        //점진적 이동
        while(UnityEngine.Vector3.SqrMagnitude(stagePlates[num].position-Destination)>=0.01f)
        {
            stagePlates[num].position = UnityEngine.Vector3.Lerp(stagePlates[num].position, Destination, offsetSpeed * Time.deltaTime);
            yield return null;
        }

        stagePlates[num].position = Destination; 
    }
}
