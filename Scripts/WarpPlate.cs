using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpPlate : BasicPlate
{
    [SerializeField] Transform StartPlate;
    [SerializeField] Transform EndPlate;
    PlayerController thePlayer;


    private void OnEnable()
    {
        StartPlate.gameObject.SetActive(true);
        EndPlate.gameObject.SetActive(true);
    }
}
