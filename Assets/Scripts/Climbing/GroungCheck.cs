using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroungCheck : MonoBehaviour
{
    public CharacterController character;

    public static event Action<float> groundEvent = delegate { };

    private void Start()
    {
        character = character.GetComponent<CharacterController>();
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Ground")
            groundEvent(character.velocity.y);
    }

}
