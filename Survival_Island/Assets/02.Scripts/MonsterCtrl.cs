using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterCtrl : MonoBehaviour
{
    [Header("컴포넌트")]
    public NavMeshAgent agent;
    public Transform Player;
    public Transform thisMonster;
    public Animator animator;

    [Header("관련 변수")]
    public float traceDis = 20.0f;
    public float attackDis = 3.0f;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Player = GameObject.FindWithTag("Player").transform;
        thisMonster = transform;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float  distance = Vector3.Distance(Player.position, thisMonster.position);

        if(distance <= attackDis)
        {
            agent.isStopped = true;
            animator.SetBool("isAttack", true);
        }
        else if (distance <= traceDis)
        {
            agent.isStopped = false;
            animator.SetBool("isTrace", true);
            animator.SetBool("isAttack",false);
            agent.destination = Player.position;
        }
        else
        {
            agent.isStopped = true;
            animator.SetBool("isTrace", false);
            animator.SetBool("isAttack", false);

        }
    }
}
