using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class ZombieDamage : MonoBehaviour
{
    [Header("컴퍼넌트")]
    public Rigidbody rb;
    public CapsuleCollider capCol;
    public Animator animator;

    [Header("사용 변수")]
    public string playerTag = "Player"; //이것은 "Player" 문자열을 동적할당 한것이다.
    public string bulletTag = "BULLET";
    public string hitTrigger = "HitTrigger";
    public string dieTrigger = "DieTrigger";
    public int hitCount = 0;
    public bool isDie = false; // 죽음 감지했을 때 true로 업데이트
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
        capCol = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision col) //충돌을 감지할 때 isTrigger체크 안했을때 사용
    {
        if(col.gameObject.CompareTag(playerTag)) //col.gameObject.CompareTag == "Player"은 동적할당과 비교를 동시에 실행, 런타임에서 할일이 많음 메모리, 속도등 다양한 방면에서 불리함.
        {//동적 할당과 비교를 따로하는 이 방법을 더 주로 쓴다.
            rb.mass = 8000f; //플레이와 충돌하면 무게가 5000으로 늘어난다. -> 플레이어와 충돌하더라도 안밀리도록 설정
            rb.freezeRotation = false; //부딫히면 회전 제한하기.
        }
        else if(col.gameObject.CompareTag(bulletTag))
        {
            //Print("아야");
            Destroy(col.gameObject); //충돌한 오브젝트 삭제
            animator.SetTrigger(hitTrigger);
            if(++hitCount == 5)
            {
                Zombie_Die();
            }
        }
    }
    private void OnCollisionExit(Collision col)
    {
        rb.mass = 75f;
        rb.freezeRotation = true;
    }

    void Zombie_Die()
    {
        animator.SetTrigger(dieTrigger);
        capCol.enabled = false; //콜라이더 비활성화
        rb.isKinematic = true; //오브젝트의 물리력을 제거함.
        isDie = true;
    }
}
