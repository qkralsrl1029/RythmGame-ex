using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCube : BasicPlate
{
    [SerializeField] float moveOffset;
    [SerializeField] float moveSpeed;
    [SerializeField] GameObject Destination;
    PlayerController thePlayer;
    bool isUp = false;
    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
       
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hitinfo;
        if (Physics.Raycast(transform.position, UnityEngine.Vector3.up, out hitinfo, 1.1f))
        {
            if(hitinfo.transform.tag=="Player")
            {
                thePlayer.transform.SetParent(this.transform);
                StartCoroutine(MoveCube());
                
            }
        }
    }

    IEnumerator MoveCube()
    {
        yield return new WaitForSeconds(0.3f);
        if(Vector3.Distance(transform.position,Destination.transform.position)>0.1f)
            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0, moveOffset, 0), moveSpeed * Time.deltaTime);
        yield return new WaitForSeconds(0.3f);
        thePlayer.transform.parent = null;
    }
}
