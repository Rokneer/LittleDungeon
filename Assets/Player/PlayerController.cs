using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction pointerPositionAction;
    private InputAction attackAction;
    private InputAction blockAction;
    private InputAction dodgeAction;
    private InputAction changeLeftEquipmentAction;
    private InputAction changeRightEquipmentAction;
    private InputAction interactAction;

    [Header("Movement")]
    private Vector2 moveInput;

    [SerializeField]
    private float movementSpeed = 6f;

    [Header("Equipment")]
    private WeaponParent weaponParent;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        weaponParent = GetComponentInChildren<WeaponParent>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        pointerPositionAction = playerInput.actions["PointerPosition"];
        attackAction = playerInput.actions["Attack"];
        blockAction = playerInput.actions["Block"];
        dodgeAction = playerInput.actions["Dodge"];
        changeLeftEquipmentAction = playerInput.actions["ChangeLeftEquipment"];
        changeRightEquipmentAction = playerInput.actions["ChangeRightEquipment"];
        interactAction = playerInput.actions["Interact"];
    }

    private void Start()
    {
        attackAction.started += OnAttack;
        blockAction.started += OnBlock;
        dodgeAction.started += OnDodge;
        changeLeftEquipmentAction.started += OnChangeLeftEquipment;
        changeRightEquipmentAction.started += OnChangeRightEquipment;
        interactAction.started += OnInteract;
    }

    private void OnDisable()
    {
        attackAction.started -= OnAttack;
        blockAction.started -= OnBlock;
        dodgeAction.started -= OnDodge;
        changeLeftEquipmentAction.started -= OnChangeLeftEquipment;
        changeRightEquipmentAction.started -= OnChangeRightEquipment;
        interactAction.started -= OnInteract;
    }

    private void FixedUpdate()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        rb.velocity = new(moveInput.x * movementSpeed, moveInput.y * movementSpeed);
    }

    private Vector2 GetPointerInput()
    {
        Vector3 mousePosition = pointerPositionAction.ReadValue<Vector2>();
        mousePosition.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        Debug.Log("Attack!");
    }

    private void OnBlock(InputAction.CallbackContext context)
    {
        Debug.Log("Block!");
    }

    private void OnDodge(InputAction.CallbackContext context)
    {
        Debug.Log("Dodge!");
    }

    private void OnChangeLeftEquipment(InputAction.CallbackContext context)
    {
        Debug.Log("Left equipment changed!");
    }

    private void OnChangeRightEquipment(InputAction.CallbackContext context)
    {
        Debug.Log("Right equipment changed!");
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        Debug.Log("Interacted!");
    }
}
