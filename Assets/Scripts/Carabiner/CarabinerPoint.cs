using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarabinerPoint : MonoBehaviour
{
    public static event Action<Vector3, Transform, string> triggerEvent = delegate { };

    public static event Action<string, bool> activateMarkersEvent = delegate { };

    private Vector3 markerPos = new Vector3();

    public bool xAxis;
    public bool yAxis;
    public bool zAxis;



    private void OnTriggerEnter(Collider other)
    {
        activateMarkersEvent(other.tag, true);
    }

    private void OnTriggerExit(Collider other)
    {
        activateMarkersEvent(other.tag, false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (xAxis)
        {
            float x = other.transform.position.x;

            markerPos = transform.position;

            markerPos.x = x;

            triggerEvent(markerPos, transform, other.tag);
        }

        if (yAxis)
        {
            float y = other.transform.position.y;

            markerPos = transform.position;

            markerPos.y = y;

            triggerEvent(markerPos, transform, other.tag);
        }

        if (zAxis)
        {
            float z = other.transform.position.z;

            markerPos = transform.position;

            markerPos.z = z;

            triggerEvent(markerPos, transform, other.tag);
        }


    }
}
