using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RoomTestSystem : MonoBehaviour
{
    public int maxInteractables;

    public GameObject leaveCanvas;

    private int countInteractables;

    private bool disableTriggers = false;

    private void Awake()
    {
        RoomTriggers.roomTriggerEvent += BlockLeave;
    }


    public void InteractablesCheck(SelectExitEventArgs args)
    {
        countInteractables++;

        if (countInteractables >= maxInteractables)
        {
            disableTriggers = true;
        }
    }

    private void BlockLeave(bool isOut)
    {
        if (!disableTriggers)
        {
            leaveCanvas.SetActive(!isOut);
        }
        
    }

}
