using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Player player;

    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction pointerPositionAction;
    private InputAction attackAction;
    private InputAction blockAction;
    private InputAction dodgeAction;
    private InputAction changeLeftEquipmentAction;
    private InputAction changeRightEquipmentAction;
    private InputAction interactAction;
    private InputAction openInventoryAction;

    private Vector2 moveInput;

    private WeaponParent weaponParent;
    private Vector2 pointerInput;

    private void Awake()
    {
        player = GetComponent<Player>();
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
        openInventoryAction = playerInput.actions["OpenInventory"];
    }

    private void Start()
    {
        attackAction.started += OnAttack;
        blockAction.started += OnBlock;
        dodgeAction.started += OnDodge;
        changeLeftEquipmentAction.started += OnChangeLeftEquipment;
        changeRightEquipmentAction.started += OnChangeRightEquipment;
        interactAction.started += OnInteract;
        openInventoryAction.started += OnOpenInventory;
    }

    private void OnDisable()
    {
        attackAction.started -= OnAttack;
        blockAction.started -= OnBlock;
        dodgeAction.started -= OnDodge;
        changeLeftEquipmentAction.started -= OnChangeLeftEquipment;
        changeRightEquipmentAction.started -= OnChangeRightEquipment;
        interactAction.started -= OnInteract;
        openInventoryAction.started -= OnOpenInventory;
    }

    private void Update()
    {
        pointerInput = GetPointerInput();
        weaponParent.pointerPosition = pointerInput;
    }

    private void FixedUpdate()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        float currentMovementSpeed = player.movementSpeed;
        player.rb.velocity = new(
            moveInput.x * currentMovementSpeed,
            moveInput.y * currentMovementSpeed
        );
    }

    private Vector2 GetPointerInput()
    {
        Vector3 mousePosition = pointerPositionAction.ReadValue<Vector2>();
        mousePosition.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        if (player.inventory.rightHandEquipment.type is EquipmentType.Weapon)
        {
            Debug.Log(
                $"Attacked with {player.inventory.rightHandEquipment.itemName} for {player.CalculateAttackPower(player.inventory.rightHandEquipment, player)}!"
            );
        }
    }

    private void OnBlock(InputAction.CallbackContext context)
    {
        if (player.inventory.leftHandEquipment.type is EquipmentType.Shield)
        {
            Debug.Log(
                $"Blocked with {player.inventory.leftHandEquipment.itemName} for {player.CalculateDefensePower(player.inventory.leftHandEquipment, player)}!"
            );
        }
    }

    private void OnDodge(InputAction.CallbackContext context)
    {
        StartCoroutine(Dodge());
    }

    private IEnumerator Dodge()
    {
        Debug.Log("Dodging!");
        player.CanDodge = false;
        player.IsDodging = true;
        //* Add dodge impulse
        SoundFXManager.Instance.PlaySoundFXClip(player.dodgeSFX, transform, 1f);
        yield return new WaitForSeconds(player.dodgeTime);
        player.IsDodging = false;
        Debug.Log("Dodge done!");
        yield return new WaitForSeconds(player.dodgeCooldown);
        SoundFXManager.Instance.PlaySoundFXClip(player.dodgeReadySFX, transform, 1f);
        player.dodgeParticles.Play();
        player.CanDodge = true;
        Debug.Log("Dodge recharged!");
    }

    private void OnChangeLeftEquipment(InputAction.CallbackContext context)
    {
        player.inventory.ChangeEquipment(EquipmentSide.Left);
        Debug.Log($"Left equipment changed to {player.inventory.leftHandEquipment.itemName}!");
    }

    private void OnChangeRightEquipment(InputAction.CallbackContext context)
    {
        player.inventory.ChangeEquipment(EquipmentSide.Right);
        Debug.Log($"Right equipment changed to {player.inventory.rightHandEquipment.itemName}!");
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        Debug.Log($"Interacted with a thing!");
    }

    private void OnOpenInventory(InputAction.CallbackContext context)
    {
        Debug.Log("Opened inventory!");
        Debug.Log($"Weapons: {player.inventory.weapons}");
        Debug.Log($"Shields: {player.inventory.shields}");
        Debug.Log($"Potions: {player.inventory.potions}");
    }
}
