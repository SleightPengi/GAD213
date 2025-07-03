using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static LachlanM.GAD213.cursors.CursorController;



namespace LachlanM.GAD213.cursors
{

    public class EnemyCursor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private ModeOfCursor modeOfCursor;
        [SerializeField] private ModeOfCursor defaultCursor;
        void Start ()
        {
            
        }


        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("Mouse Entered");
            
            CursorController.Instance.SetToMode(modeOfCursor);
            
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            CursorController.Instance.SetToMode(defaultCursor);

        }


    }
    }
