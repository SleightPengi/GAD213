using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using static Interactable;
public class playerController : MonoBehaviour
{


    const string WALK = "Walk";
    const string IDLE = "Idle";
    const string JUMP = "Jump";

    public bool jumping = false;
    public bool controlled;

    Interactable target;
    bool playerBusy = false;

    [SerializeField]PlayerActions input;

    [SerializeField]NavMeshAgent agent;
    Animator animator;

    [SerializeField]float lookRotationSpeed;


    [Header("Movement")]
    [SerializeField] ParticleSystem clickEffect;
    [SerializeField] LayerMask clickableLayers;



    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        input = new PlayerActions();
        AssignInputs();
    }


    void FollowTarget()
    {
        
        if (target == null) return;
        
        if (Vector3.Distance(target.transform.position, transform.position) <= 1)
        { ReachDistance(); }
        else
        { agent.SetDestination(target.transform.position); }
    }

    void AssignInputs()
    {
        input.Main.Move.performed += ctx => ClickToMove();
    }


    void ClickToMove()
    {
        if (controlled == true)
        {

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, clickableLayers))
            {
                
                if (hit.transform.CompareTag("Interactable"))
                {
                    
                    target = hit.transform.GetComponent<Interactable>();
                    if (clickEffect != null)
                    { Instantiate(clickEffect, hit.transform.position + new Vector3(0, 0.1f, 0), clickEffect.transform.rotation); }
                }
                else
                {
                    target = null;

                    agent.destination = hit.point;
                    if (clickEffect != null)
                    { Instantiate(clickEffect, hit.point + new Vector3(0, 0.1f, 0), clickEffect.transform.rotation); }
                }
            }
        }
    }

    void ReachDistance()
    {
        
        agent.SetDestination(transform.position);

        if (playerBusy) return;

        playerBusy = true;

        switch (target.interactionType)
        {
            case InteractableType.Enemy:

                break;
            case InteractableType.Item:
               
                target.InteractWithItem();
                target = null;

                Invoke(nameof(ResetBusyState), 0.5f);
                break;
        }
    }




    void OnEnable()
    {
        input.Enable();
    }

    void OnDisable()
    {
        input.Disable();
    }

    void Update()
    {
        FollowTarget();
        FaceTarget();
        SetAnimations();
    }

    /*
    void FaceTarget()
    {
        


        {
            if (agent.velocity != Vector3.zero)
            {
                Vector3 direction = (agent.destination - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lookRotationSpeed);
            }
        }

    }
    */

    void FaceTarget()
    {
        if (agent.destination == transform.position) return;

        Vector3 facing = Vector3.zero;
        if (target != null)
        { facing = target.transform.position; }
        else
        { facing = agent.destination; }

        Vector3 direction = (facing - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lookRotationSpeed);
    }

    void ResetBusyState()
    {
        playerBusy = false;
        SetAnimations();
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
