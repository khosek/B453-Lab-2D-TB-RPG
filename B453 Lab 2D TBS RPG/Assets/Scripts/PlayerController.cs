using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement variables")]
    public int moveSpeed;
    public Rigidbody2D rb;
    public Vector2 moveInput;

    [Header("Interact Variables")]
    public bool interactInput;

    [Header("Character Data")]
    public Character myChar;

    private void Start()
    {
        myChar = GameManager.Instance.characterArray[0];
        gameObject.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.characterSprite;
    }

    private void Update()
    {
        if (interactInput)
        {
            interactInput = false;
            TryInteract();
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = moveInput.normalized * moveSpeed;
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            interactInput = true;
        }
    }

    private void TryInteract()
    {
        Debug.Log("Pressed interact button");
    }
}
