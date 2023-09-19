using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class Teleporter : XRBaseInteractable
{
    public GameObject XROrigin;
    public Transform teleportPoint;

    protected override void OnEnable()
    {
        base.OnEnable();
        selectEntered.AddListener(StartGrab);

    }

    protected override void OnDisable()
    {
        base.OnDisable();
        selectEntered.RemoveListener(StartGrab);
    }
    
    void StartGrab(SelectEnterEventArgs args)
    {
        Teleport();
    }


    void Teleport()
    {
        XROrigin.transform.position = teleportPoint.position;
        XROrigin.transform.rotation = teleportPoint.rotation;
    }
}
