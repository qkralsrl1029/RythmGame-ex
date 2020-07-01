using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reload : MonoBehaviour
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
