using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LachlanM.GAD213.cursors
{ 
public class CursorController : MonoBehaviour
{
    [SerializeField] private Texture2D MoveCursor;
    [SerializeField] private Texture2D TargetCursor;
    [SerializeField] private Texture2D HealCursor;

    [SerializeField] Vector2 clickPosition = Vector2.zero;

        public static CursorController Instance { get; private set; }


        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
        Cursor.SetCursor(MoveCursor, clickPosition, CursorMode.Auto);
        }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            {
                Cursor.SetCursor(TargetCursor, clickPosition, CursorMode.Auto);
            }
    }



    public void SetToMode(ModeOfCursor modeOfCursor)
    {
        switch (modeOfCursor)
        {
            case ModeOfCursor.Move:
                Cursor.SetCursor(MoveCursor, clickPosition, CursorMode.Auto);
                break;
            case ModeOfCursor.Target:
                Cursor.SetCursor(TargetCursor, clickPosition, CursorMode.Auto); 
                break;
            case ModeOfCursor.Heal:
                Cursor.SetCursor(HealCursor, clickPosition, CursorMode.Auto);
                break;
            default:
                Cursor.SetCursor(MoveCursor, clickPosition, CursorMode.Auto);
                break;


        }
    }





    public enum ModeOfCursor
    {
        Move, 
        Target, 
        Heal
    }


}
}
