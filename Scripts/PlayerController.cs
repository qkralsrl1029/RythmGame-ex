using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    TimingManager theTimingManager;
    CameraController theCam;
    StatusManager theStatus;
    Rigidbody rigid;


    [SerializeField] float moveSpeed = 3f;      //큐브움직임
    UnityEngine.Vector3 dir = new UnityEngine.Vector3();
    public UnityEngine.Vector3 destination = new UnityEngine.Vector3();
    UnityEngine.Vector3 originPos = new UnityEngine.Vector3();          //리젠될때 불러올 원위치

    [SerializeField] float spinSpeed = 270;     //회전속도
    UnityEngine.Vector3 rotDir = new UnityEngine.Vector3();             //회전방향
    UnityEngine.Quaternion destRot = new UnityEngine.Quaternion();      //목표 회전값

    [SerializeField] Transform fakeCube = null; //가짜 큐브를 먼저 회번시키고 그 값을 목표 회전값으로 삼음
    [SerializeField] Transform realCube = null;

    [SerializeField] float recoilPosY = 0.25f;    //반동효과
    float recoilSpeed = 1.5f;

    bool canMove = true;        //중복 실행 방지
    public static bool isDone = false;  //게임 종료 감지

    private void Start()
    {
        theTimingManager = FindObjectOfType<TimingManager>();
        theCam = FindObjectOfType<CameraController>();
        theStatus = FindObjectOfType<StatusManager>();
        rigid = GetComponentInChildren<Rigidbody>();

        originPos = transform.position;       
    }

    public void init()
    {
        transform.position = UnityEngine.Vector3.zero;
        destination = UnityEngine.Vector3.zero;
        realCube.localPosition = UnityEngine.Vector3.zero;
        canMove = true;
        isDone = false;

        rigid.useGravity = false;
        rigid.isKinematic = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isStartGame)
        {
            CheckFalling();     //발판에서 벗어나는지 체크
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W))
            {
                if (canMove && !isDone)
                {
                    Calc();
                    if (theTimingManager.CheckTiming())
                    {
                        startAtion();       //올바른 타이밍에 키를 눌렀을때만 움직임, 움직이는 도중엔 못움직임
                    }
                }
            }
        }
    }
    void CheckFalling()
    {
        if (!Physics.Raycast(transform.position, UnityEngine.Vector3.down, 1.1f)&&!isDone)
        {
            rigid.useGravity = true;
            rigid.isKinematic = false;
        }
    }
    public void ResetFalling()
    {
        theStatus.DecreaseHp(1);
        AudioManager.instance.PlaySFX("Falling");
        if (StatusManager.isAlive)
        {
            rigid.useGravity = false;
            rigid.isKinematic = true;
            transform.position = originPos;
            realCube.localPosition = new UnityEngine.Vector3(0, 0, 0);      //외형모형인 큐브도 초기화
        }
    }
    void Calc()
    {
        dir.Set(Input.GetAxisRaw("Vertical"), 0, Input.GetAxisRaw("Horizontal"));   //입력키대로 방향 설정

        destination = transform.position + new UnityEngine.Vector3(dir.x, 0, -dir.z);             //방향대로 목적지 설정
        rotDir = new UnityEngine.Vector3(dir.z, 0, dir.x);

        fakeCube.RotateAround(transform.position, rotDir, spinSpeed);       //자기자신을 중심으로 이동방향을 축으로 하여 회전
        destRot = fakeCube.rotation;
    }
    void startAtion()
    {
      
        StartCoroutine(MoveCoroutine());
        StartCoroutine(SpinCoroutine());
        StartCoroutine(recoilCoroutine());

        StartCoroutine(theCam.ZoomCam());
    }

    IEnumerator MoveCoroutine()
    {
        canMove = false;
        while(UnityEngine.Vector3.SqrMagnitude(transform.position-destination)>=0.01)       //distance함수보다 가벼움.제곱근 구하는 함수
        {
            transform.position = UnityEngine.Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = destination;       //코루틴에서의 오차 상쇄
    }

    IEnumerator SpinCoroutine()
    {
        while(UnityEngine.Quaternion.Angle(realCube.rotation,destRot)>0.5f)     //목표점과 자신의 각도 차이 계산
        {
            realCube.rotation = UnityEngine.Quaternion.RotateTowards(realCube.rotation, destRot, spinSpeed*Time.deltaTime);
            yield return null;
        }

        realCube.rotation = destRot;        //오차 상쇄
        canMove = true;
    }

    IEnumerator recoilCoroutine()
    {
        while(realCube.position.y<recoilPosY)       //올라갔다
        {
            realCube.position += new UnityEngine.Vector3(0, recoilSpeed * Time.deltaTime, 0);
            yield return null;
        }
        while (realCube.position.y >0)              //내려오기
        {
            realCube.position -= new UnityEngine.Vector3(0, recoilSpeed * Time.deltaTime, 0);
            yield return null;
        }

        realCube.localPosition = new UnityEngine.Vector3(0, 0, 0);

    }

 
}
