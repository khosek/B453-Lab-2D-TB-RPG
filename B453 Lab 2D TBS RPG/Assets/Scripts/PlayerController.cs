using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement variables")]
    [Tooltip("Movement speed in meters per second. NOTE: World is not to scale, this might need to be very small.")]
    public int moveSpeed;
    [Tooltip("The RigidBody2D on this object.")]
    public Rigidbody2D rb;
    [Tooltip("The X and Y axis input for the player.")]
    public Vector2 moveInput;

    [Header("Interact Variables")]
    public bool interactInput;

    [Header("Character Data")]
    [Tooltip("The Character data object for this character.")]
    public Character myChar;

    private void Start()
    {
        // Grabbing the first Character object from the GameManager's array of Character's.
        myChar = GameManager.Instance.characterArray[0];
        // Change the sprite for this PlayerController to match the sprite held in the GameManager from character creation.
        gameObject.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.characterSprite;
    }

    private void Update()
    {
        // If the player is trying to interact with something...
        if (interactInput)
        {
            // Set attempting to interact to false.
            interactInput = false;
            // Call TryInteract to attempt interacting with something.
            TryInteract();
        }
    }

    private void FixedUpdate()
    {
        // Take player X and Y input, normalize the vector to only receive direction, multiply by speed to add back magnitude, and finally set the player's velocity to this result.
        rb.velocity = moveInput.normalized * moveSpeed;
    }

    // Method for the "new" input system to check for movement input.
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        // Set the result of movement to the variable.
        moveInput = context.ReadValue<Vector2>();
    }

    // Method for the "new" input system to check for attempt at interaction.
    public void OnInteractInput(InputAction.CallbackContext context)
    {
        // If the button is pressed...
        if (context.phase == InputActionPhase.Performed)
        {
            // Set interactInput to true.
            interactInput = true;
        }
    }

    // Method called when the player tries to interact with something in the world.
    private void TryInteract()
    {
        Debug.Log("Pressed interact button");
    }

    private void TryInteractableObject()
    {

    }
}