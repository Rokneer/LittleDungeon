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

    private Vector2 pointerInput;

    private void Awake()
    {
        player = GetComponent<Player>();

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
        blockAction.started += OnBlockStart;
        blockAction.canceled += OnBlockEnd;
        dodgeAction.started += OnDodge;
        changeLeftEquipmentAction.started += OnChangeLeftEquipment;
        changeRightEquipmentAction.started += OnChangeRightEquipment;
        interactAction.started += OnInteract;
        openInventoryAction.started += OnOpenInventory;
    }

    private void OnDisable()
    {
        attackAction.started -= OnAttack;
        blockAction.started -= OnBlockStart;
        blockAction.canceled -= OnBlockEnd;
        dodgeAction.started -= OnDodge;
        changeLeftEquipmentAction.started -= OnChangeLeftEquipment;
        changeRightEquipmentAction.started -= OnChangeRightEquipment;
        interactAction.started -= OnInteract;
        openInventoryAction.started -= OnOpenInventory;
    }

    private void Update()
    {
        if (!player.IsAttacking)
        {
            pointerInput = GetPointerInput();
        }
        player.SetFacingDirection(pointerInput);
    }

    private void FixedUpdate()
    {
        if (!player.IsDodging)
        {
            if (player.IsAlive)
            {
                player.IsMoving = moveInput != Vector2.zero;
                moveInput = moveAction.ReadValue<Vector2>();
            }
            else
            {
                player.IsMoving = false;
            }
            player.rb.velocity = new(
                moveInput.x * player.CurrentMoveSpeed,
                moveInput.y * player.CurrentMoveSpeed
            );
        }
    }

    private Vector2 GetPointerInput()
    {
        Vector3 mousePosition = pointerPositionAction.ReadValue<Vector2>();
        mousePosition.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        if (
            player.inventory.rightHandItem.Equipment.type is EquipmentType.Weapon
            && player.IsAlive
            && player.CanAttack
        )
        {
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        player.CanAttack = false;
        player.IsAttacking = true;

        player.weaponAnimator.SetTrigger(AnimationStrings.Attack);
        SoundFXManager
            .Instance
            .PlaySoundFXClip(player.inventory.rightHandItem.Equipment.actionSFX, transform, 1f);

        yield return new WaitForSeconds(player.WeaponUseDelay);
        SoundFXManager.Instance.PlaySoundFXClip(player.attackReadySFX, transform, 1f);
        player.CanAttack = true;
    }

    public void ResetIsAttacking()
    {
        player.IsAttacking = false;
    }

    private void OnBlockStart(InputAction.CallbackContext context)
    {
        if (
            player.inventory.leftHandItem.Equipment.type is EquipmentType.Shield
            && player.IsAlive
            && player.CanBlock
        )
        {
            player.CanBlock = false;
            player.IsBlocking = true;

            player.shieldAnimator.SetBool(AnimationStrings.isBlocking, true);
            SoundFXManager
                .Instance
                .PlaySoundFXClip(player.inventory.leftHandItem.Equipment.actionSFX, transform, 1f);
        }
    }

    private void OnBlockEnd(InputAction.CallbackContext context)
    {
        if (
            player.inventory.leftHandItem.Equipment.type is EquipmentType.Shield
            && player.IsAlive
            && !player.CanBlock
        )
        {
            StartCoroutine(EndBlock());
        }
    }

    private IEnumerator EndBlock()
    {
        player.IsBlocking = false;
        player.shieldAnimator.SetBool(AnimationStrings.isBlocking, false);

        yield return new WaitForSeconds(
            player.inventory.leftHandItem.Equipment.useDelayMultiplier * 1
        );

        player.CanBlock = true;

        SoundFXManager
            .Instance
            .PlaySoundFXClip(player.inventory.leftHandItem.Equipment.actionSFX, transform, 1f);
    }

    private void OnDodge(InputAction.CallbackContext context)
    {
        if (player.IsAlive && player.CanDodge)
        {
            StartCoroutine(Dodge());
        }
    }

    private IEnumerator Dodge()
    {
        player.CanDodge = false;
        player.CanBlock = false;
        player.CanAttack = false;
        player.IsDodging = true;
        player.rb.velocity = new(transform.localScale.x * player.DodgeImpulse, 0f);
        player.dodgeParticles.Play();
        SoundFXManager.Instance.PlaySoundFXClip(player.dodgeSFX, transform, 1f);

        yield return new WaitForSeconds(player.DodgeTime);
        player.IsDodging = false;

        yield return new WaitForSeconds(player.DodgeCooldown);
        SoundFXManager.Instance.PlaySoundFXClip(player.dodgeReadySFX, transform, 1f);
        player.CanDodge = true;
        player.CanBlock = true;
        player.CanAttack = true;
    }

    private void OnChangeLeftEquipment(InputAction.CallbackContext context)
    {
        player.inventory.ChangeEquipment(EquipmentSide.Left);
    }

    private void OnChangeRightEquipment(InputAction.CallbackContext context)
    {
        player.inventory.ChangeEquipment(EquipmentSide.Right);
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
