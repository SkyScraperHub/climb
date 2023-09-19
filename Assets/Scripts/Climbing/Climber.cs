using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Climber : MonoBehaviour
{
    private CharacterController character;
    private ActionBasedContinuousMoveProvider moveProvider;
    //private ActionBasedSnapTurnProvider snapProvider;
    public static ActionBasedController climbingHand;

    public GameObject safetyCanvas;

    public GameObject gameOverCanvas;

    public static bool onGround;

    public float gameOverVelocity = -5f;

    public InputActionProperty leftVelocity;
    public InputActionProperty rightVelocity;

    private bool isLeftClacked;

    private bool isRightClacked;

    private void Awake()
    {
        CarabinerSystem.carabinerClacked += SafetySystem;
        SafetyTrigger.safetyTriggerEvent += TriggerSave;
        GroungCheck.groundEvent += GameOver;
    }

    void Start()
    {
        character = GetComponent<CharacterController>();
        moveProvider = GetComponent<ActionBasedContinuousMoveProvider>();
        //snapProvider= GetComponent<ActionBasedSnapTurnProvider>();
    }


    void FixedUpdate()
    {
        if (climbingHand)
        {
            EnableProviders(false);
            Climb();
        }
        if (!climbingHand)
        {
            EnableProviders(true);

        }
         //ApplyGravity();
    }

    void Climb()
    {
        Vector3 velocity = climbingHand.tag == "Left" ? leftVelocity.action.ReadValue<Vector3>() : rightVelocity.action.ReadValue<Vector3>();

        character.Move(transform.rotation * -velocity * Time.fixedDeltaTime);
    }

    /*void ApplyGravity()
    {
        if (!character.isGrounded && !climbingHand)
        {
            character.SimpleMove(new Vector3());
        }
    } */

    void EnableProviders(bool active)
    {
            moveProvider.moveSpeed = active && !(isLeftClacked || isRightClacked) ? 1 : 0;
            //snapProvider.enabled = active && !(isLeftClacked || isRightClacked);

        if (!active)
            moveProvider.useGravity = active;

        if (active)
            moveProvider.useGravity = active && !(isLeftClacked || isRightClacked);

    }

    public void SafetySystem(bool isClacked, string tag)
    {
        if (tag == "Left")
            isLeftClacked = isClacked;

        if (tag == "Right")
            isRightClacked = isClacked;

        if (!(isLeftClacked || isRightClacked))
            ShowMessage(false);

        gameOverCanvas.SetActive(false);
    }

    private void TriggerSave(bool isOut)
    {

        if (isOut && isLeftClacked || isRightClacked)
        {
            ShowMessage(isOut);
            moveProvider.useGravity = !isOut;
        }
        
        if (!isOut)
        {
            ShowMessage(isOut);
            moveProvider.useGravity = !isOut;
        }
    }



    private void ShowMessage(bool active)
    {
            safetyCanvas.SetActive(active);
    }

    private void GameOver(float velocity)
    {
        if (velocity < gameOverVelocity)
        {
            gameOverCanvas.SetActive(true);
        }
    }

}
