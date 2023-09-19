using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTriggers : MonoBehaviour
{
    public static event Action<bool> roomTriggerEvent = delegate { };

    public string targetTag = "XROrigin";

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == targetTag)
            roomTriggerEvent(false);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == targetTag)
            roomTriggerEvent(true);
    }
}
