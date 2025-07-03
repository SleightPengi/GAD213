using System.Collections;
using System.Collections.Generic;
using LachlanM.GAD213.cursors;
using Unity.VisualScripting;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public enum InteractableType { Enemy, Item, Player}

   // public Actor myActor;
    public InteractableType interactionType;
    private bool inRange;

    private void Awake()
    {
        if (interactionType == InteractableType.Enemy)
        {
            //myActor = GetComponent<actor>();
        }

        if (interactionType == InteractableType.Item)
        {
           
            Debug.Log("Clicked");
            if (inRange == true)
            {
                StartCoroutine(ItemInteraction());

            }
            
        }

        /*if (interactionType == InteractableType.Player)
        {
            if ()
            {

            }
        }*/

        

    }
    IEnumerator ItemInteraction()
    {
        yield return new WaitForSeconds(2);

        if (inRange == true)
        {
            Destroy(this.gameObject);
        }
    }





    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided");
        if (collision.transform.tag == "Player")
        {
            Debug.Log("player collided");
            inRange = true;
        }
    }
}
