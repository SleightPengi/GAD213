using System.Collections;
using System.Collections.Generic;
using LachlanM.GAD213.cursors;
using UnityEngine;
using static LachlanM.GAD213.cursors.CursorController;
using UnityEngine.EventSystems;

public class CrewCursor : MonoBehaviour
{
    [SerializeField] private ModeOfCursor modeOfCursor;
    [SerializeField] private ModeOfCursor defaultCursor;
    void Start()
    {

    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse Entered");
        //if (true) 
        //{
            CursorController.Instance.SetToMode(modeOfCursor);
        //}
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CursorController.Instance.SetToMode(defaultCursor);

    }
}
