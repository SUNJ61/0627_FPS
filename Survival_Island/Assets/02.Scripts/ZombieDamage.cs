using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class ZombieDamage : MonoBehaviour
{
    [Header("���۳�Ʈ")]
    public Rigidbody rb;
    public CapsuleCollider capCol;
    public Animator animator;

    [Header("��� ����")]
    public string playerTag = "Player"; //�̰��� "Player" ���ڿ��� �����Ҵ� �Ѱ��̴�.
    public string bulletTag = "BULLET";
    public string hitTrigger = "HitTrigger";
    public string dieTrigger = "DieTrigger";
    public int hitCount = 0;
    public bool isDie = false; // ���� �������� �� true�� ������Ʈ
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
        capCol = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision col) //�浹�� ������ �� isTriggerüũ �������� ���
    {
        if(col.gameObject.CompareTag(playerTag)) //col.gameObject.CompareTag == "Player"�� �����Ҵ�� �񱳸� ���ÿ� ����, ��Ÿ�ӿ��� ������ ���� �޸�, �ӵ��� �پ��� ��鿡�� �Ҹ���.
        {//���� �Ҵ�� �񱳸� �����ϴ� �� ����� �� �ַ� ����.
            rb.mass = 8000f; //�÷��̿� �浹�ϸ� ���԰� 5000���� �þ��. -> �÷��̾�� �浹�ϴ��� �ȹи����� ����
            rb.freezeRotation = false; //�΋H���� ȸ�� �����ϱ�.
        }
        else if(col.gameObject.CompareTag(bulletTag))
        {
            //Print("�ƾ�");
            Destroy(col.gameObject); //�浹�� ������Ʈ ����
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
        capCol.enabled = false; //�ݶ��̴� ��Ȱ��ȭ
        rb.isKinematic = true; //������Ʈ�� �������� ������.
        isDie = true;
    }
}
