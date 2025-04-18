using UnityEngine.AI;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBehaviour : StateMachineBehaviour
{
    float timer;
    int random;
    List<Transform> points = new List<Transform>();
    NavMeshAgent agent;
    Transform player;
    float chaseRange = 10f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        Transform pointsObject = GameObject.FindGameObjectWithTag("Points").transform;
        foreach (Transform t in pointsObject)
            points.Add(t);

        random = Random.Range(0, 9);
        agent = animator.GetComponent<NavMeshAgent>();
        agent.SetDestination(points[random].position);
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
            agent.SetDestination(points[Random.Range(0, points.Count)].position);

        timer += Time.deltaTime;
        if (timer > 10)
        {
            animator.SetBool("isPatrolling", false);
        }

        float distance = Vector3.Distance(animator.transform.position, player.position);

        if (distance < chaseRange)
        {
            animator.SetBool("isChasing", true);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);
    }
}