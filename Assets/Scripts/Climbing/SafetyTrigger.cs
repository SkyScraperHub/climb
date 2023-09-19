using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SafetyTrigger : MonoBehaviour
{

    public static event Action<bool> safetyTriggerEvent = delegate { };

    public string targetTag = "XROrigin";

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == targetTag)
            safetyTriggerEvent(false);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == targetTag)
            safetyTriggerEvent(true);
    }
}
