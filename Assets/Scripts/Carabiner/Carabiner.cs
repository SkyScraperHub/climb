using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.XR;

public class Carabiner : MonoBehaviour
{
    public bool isLeft;

    private XRIDefaultInputActions action;

    private bool isActive = false;

    public static event Action<string, bool> activateCarabiner = delegate { };

    public static event Action<Transform, string> handPosEvent = delegate { };


    private void Awake()
    {
        action = new XRIDefaultInputActions();

        if (isLeft)
        {
            action.XRILeftHandInteraction.Activate.performed += Activate;
            action.XRILeftHandInteraction.Activate.canceled += Deactivate;
        }
        else
        {
            action.XRIRightHandInteraction.Activate.performed += Activate;
            action.XRIRightHandInteraction.Activate.canceled += Deactivate;
        }
    }

    private void OnEnable()
    {
        action.Enable();
    }

    private void OnDisable()
    {
        action.Disable();
    }

    public void Activate(InputAction.CallbackContext context)
    {   
        activateCarabiner(gameObject.tag, true);
        isActive = true;
    }

    public void Deactivate(InputAction.CallbackContext context)
    {
        activateCarabiner(gameObject.tag, false);
        isActive = false;
    }

    private void Update()
    {
        if (isActive)
        {
            handPosEvent(gameObject.transform, gameObject.tag);
        }
    }

}
