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
    [Tooltip("The direction the player is currently facing.")]
    public Vector2 facingDir;

    [Header("Interact Variables")]
    [Tooltip("Whether the player is trying to interact with anything or not.")]
    public bool interactInput;
    [Tooltip("How far away the player can interact with something.")]
    public float rayCastLength;
    [Tooltip("The layer mask that all interactable things will be on.")]
    public LayerMask interactLayerMask;

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
        // Check to see if the player is moving at all...
        if (moveInput.magnitude != 0.0f)
        {
            // Set the facing direction to match the direction they're moving in.
            facingDir = moveInput.normalized;
        }

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
        // Send out a raycast from the player's position, in the direction they're facing and store it in hit.
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position + facingDir, 
            Vector3.up, rayCastLength, interactLayerMask);

        // First check to make sure it hit a collider...
        if (hit.collider != null)
        {
            // Print out the name of the object that was hit to the console.
            Debug.Log(hit.collider.gameObject.name);

            // Check to see if the gameobject that owns the hit collider implements IConversational...
            if (hit.collider.gameObject.GetComponent<IConversational>() != null)
            {
                // Call the StartConversation() method on the gameobject that the raycast hit.
                hit.collider.gameObject.GetComponent<IConversational>().StartConversation();
            }

            if (hit.collider.gameObject.GetComponent<Fightable>() != null)
            {
                hit.collider.gameObject.GetComponent<Fightable>().LoadFight();
            }
        }
    }
}