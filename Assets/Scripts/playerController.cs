using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
public class playerController : MonoBehaviour
{


    const string WALK = "Walk";
    const string IDLE = "Idle";
    const string JUMP = "Jump";

    public bool jumping = false;
    public bool controlled;

    
    

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
                agent.destination = hit.point;
                if (clickEffect != null)
                {
                    Instantiate(clickEffect, hit.point += new Vector3(0, 0.1f, 0), clickEffect.transform.rotation);
                    
                }
            }
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
        FaceTarget();
        SetAnimations();
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
