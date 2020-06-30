using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform thePlayer;       //카메라가 따라갈 타겟
    [SerializeField] float followSpeed = 15f;   //따라갈 속도

    float hitDistance = 0;

    Vector3 DistanceGap = new Vector3();        //극적 효과 연출을 위해 노트 적중시마다 카메라 줌아웃/줌인
    [SerializeField] float zoomDistance = -1.25f;

    // Start is called before the first frame update
    void Start()
    {
        DistanceGap = transform.position - thePlayer.position;  //위치차 저장
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 t_destPos = thePlayer.position + DistanceGap+(transform.forward*hitDistance);
        transform.position = Vector3.Lerp(transform.position, t_destPos, followSpeed * Time.deltaTime);
    }

    public IEnumerator ZoomCam()        //노트 적중시마다 호출(플레이어컨트롤러)
    {
        hitDistance = zoomDistance;

        yield return new WaitForSeconds(0.15f);

        hitDistance = 0;
    }
}
