using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlate : MonoBehaviour
{
    [SerializeField] GameObject movingPlate;
    [SerializeField] GameObject destination;


    private void OnEnable()
    {
        movingPlate.SetActive(true);
        destination.SetActive(true);
    }
}
