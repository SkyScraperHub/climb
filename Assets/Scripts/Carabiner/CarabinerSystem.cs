using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System;
using UnityEngine.Rendering;

public class CarabinerSystem : MonoBehaviour
{
    [SerializeField]
    private Renderer leftMarker;

    [SerializeField]
    private Renderer rightMarker;

    [SerializeField]
    private GameObject leftCarabiner;

    [SerializeField]
    private GameObject rightCarabiner;

    [SerializeField]
    private string leftTag = "Left";

    [SerializeField]
    private string rightTag = "Right";

    private bool leftInTrigger = false;

    private bool rightInTrigger = false;

    public static event Action<bool, string> carabinerClacked = delegate { };

    private void Awake()
    {
        CarabinerPoint.triggerEvent += GetTrigger;

        CarabinerPoint.activateMarkersEvent += ActivateMarkers;

        Carabiner.activateCarabiner += ActivateCarabiner;

        Carabiner.handPosEvent += ChangeCarabinerPos;

    }


    public void GetTrigger(Vector3 markerPos, Transform triggerPos, string tag)
    {

        if (tag == leftTag)
        {
            leftMarker.transform.position = markerPos;
            leftMarker.transform.rotation = Quaternion.LookRotation(triggerPos.up, triggerPos.right);
        }

        if (tag == rightTag)
        {
            rightMarker.transform.position = markerPos;
            leftMarker.transform.rotation = Quaternion.LookRotation(triggerPos.up, triggerPos.right);
        }
    }

    public void ActivateMarkers(string tag, bool active)
    {


        if (tag == leftTag)
        {
            leftMarker.enabled = active;
            leftInTrigger = active;
        }


        if (tag == rightTag)
        {
            rightMarker.enabled = active;
            rightInTrigger = active;
        }

    }

    public void ActivateCarabiner(string tag, bool active)
    {
        if (active == true && tag == leftTag)
        {
            leftCarabiner.SetActive(active);
            carabinerClacked(!active, tag);
        }


        if (active == true && tag == rightTag)
        {
            rightCarabiner.SetActive(active);
            carabinerClacked(!active, tag);
        }


        if (active == false && leftInTrigger)
        {
            leftCarabiner.transform.position = leftMarker.transform.position;
            leftCarabiner.transform.rotation = leftMarker.transform.rotation;
            leftMarker.enabled = active;

            carabinerClacked(!active, tag);
        }
        
        if (active == false && tag == leftTag && !leftInTrigger)
        {
            leftCarabiner.SetActive(active);
        }

        if (active == false && rightInTrigger)
        {
            rightCarabiner.transform.position = rightMarker.transform.position;
            rightCarabiner.transform.rotation = rightMarker.transform.rotation;
            rightMarker.enabled = active;

            carabinerClacked(!active, tag);
        }
        
        if (active == false && tag == rightTag && !rightInTrigger)
        {
            rightCarabiner.SetActive(active);
        }

        

    }

    public void ChangeCarabinerPos(Transform transform, string tag)
    {
        if (tag == leftTag)
        {
            leftCarabiner.transform.position = transform.position;
            leftCarabiner.transform.rotation = transform.rotation;
        }

        if (tag == rightTag)
        {
            rightCarabiner.transform.position = transform.position;
            rightCarabiner.transform.rotation = transform.rotation;
        }
    }

}
