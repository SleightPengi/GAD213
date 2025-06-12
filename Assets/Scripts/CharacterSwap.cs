using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CharacterSwap : MonoBehaviour
{
   

    [SerializeField] CameraController _camera;

    [SerializeField] Transform _Captain;
    [SerializeField] Transform _Bard;
    [SerializeField] Transform _Shark;


    [Header ("Controllers")]
    [SerializeField] playerController _CaptainController;
    [SerializeField] playerController _BardController;
    [SerializeField] playerController _SharkController;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _camera.target = _Captain;
            _CaptainController.controlled = true;
            _BardController.controlled = false;
            _SharkController.controlled = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _camera.target = _Bard;
            _BardController.controlled = true;
            _CaptainController.controlled = false;
            _SharkController.controlled = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _camera.target = _Shark;
            _SharkController.controlled = true;
            _BardController.controlled = false;
            _CaptainController.controlled = false;
        }
    }
}
