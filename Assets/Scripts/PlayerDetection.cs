using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class PlayerDetection : MonoBehaviour
{

    const string WALK = "Walk";
    const string IDLE = "Idle";
    const string JUMP = "Jump";



    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator animator;

    [SerializeField] float lookRotationSpeed;

    bool aggro = false;
    public bool jumping = false;

    GameObject currentTarget;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if (aggro == true)
        {
            agent.destination = currentTarget.transform.position;
        }




        FollowTarget();
        FaceTarget();
        SetAnimations();
    }

    void FollowTarget()
    {
        if (currentTarget == null) return;

        if (Vector3.Distance(currentTarget.transform.position, transform.position) <= 1.3)
        { agent.SetDestination(transform.position); }
        else
        { agent.SetDestination(currentTarget.transform.position); }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (currentTarget == null)
        {
            if (collision.tag == "Player")
            {
                aggro = true;
                currentTarget = collision.gameObject;
            }
        }
    }

    

    void FaceTarget()
    {
        /*Vector3 direction = (agent.destination - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lookRotationSpeed);
        */


        {
            if (agent.velocity != Vector3.zero)
            {
                Vector3 direction = (agent.destination - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lookRotationSpeed);
            }
        }

    }



    void SetAnimations()
    {
        if (jumping == true)
        {
            animator.Play(JUMP);
        }
        else if (agent.velocity == Vector3.zero)
        {
            animator.Play(IDLE);
        }
        else if (jumping == false)
        {
            animator.Play(WALK);
        }

        
    }

}
