using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpStart : MonoBehaviour
{
    PlayerController thePlayer;
    [SerializeField] Transform destination;
    Vector3 WarpDest = new Vector3();
    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
        //WarpDest.Set(destination.position.x, destination.position.y + 3f,destination.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Debug.Log("warp start");
            AudioManager.instance.PlaySFX("Warp");
            thePlayer.transform.position = destination.position + new Vector3(0, 0.6f, 0);
        }

    }
   
}
