using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reload : MonoBehaviour     //추락하면 처음위치에서 로드
{
    

    private void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player"&&StatusManager.isAlive)
        {
            other.GetComponentInParent<PlayerController>().ResetFalling();
        }
    }
}
