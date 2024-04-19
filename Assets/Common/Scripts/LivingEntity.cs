using System;
using UnityEngine;

public abstract class LivingEntity : DamageableEntity
{
    [HideInInspector]
    public Rigidbody2D rb;

    [Header("Stamina")]
    [SerializeField]
    private float _maxStamina = 100;
    public virtual float MaxStamina
    {
        get => _maxStamina;
        set => _maxStamina = value;
    }

    [SerializeField]
    private float _currentStamina = 100;
    public virtual float CurrentStamina
    {
        get => _currentStamina = Mathf.Clamp(_currentStamina, 0, MaxStamina);
        set => _currentStamina = value;
    }

    [SerializeField]
    private float _staminaRegenAmount = 4;
    public virtual float StaminaRegenAmount
    {
        get => _staminaRegenAmount;
        set => _staminaRegenAmount = value;
    }

    [Header("Movement")]
    [SerializeField]
    private bool _canMove = true;
    public bool CanMove
    {
        get => _canMove;
        set => _canMove = value;
    }

    [SerializeField]
    private bool _isMoving = false;
    public bool IsMoving
    {
        get => _isMoving;
        set
        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);
        }
    }

    [SerializeField]
    private float _movementSpeed = 6;
    public virtual float MovementSpeed
    {
        get => _movementSpeed;
        set => _movementSpeed = value;
    }
    public float CurrentMoveSpeed
    {
        get
        {
            if (IsMoving && CanMove)
            {
                return MovementSpeed;
            }
            return 0;
        }
    }

    [Header("Attack")]
    [SerializeField]
    private float _attackPower = 10;
    public virtual float AttackPower
    {
        get => _attackPower;
        set => _attackPower = value;
    }

    [Header("Armor")]
    [SerializeField]
    private float _armorPower = 10;
    public virtual float ArmorPower
    {
        get => _armorPower;
        set => _armorPower = value;
    }

    [Header("Attack")]
    public GameObject rightHand;

    [SerializeField]
    private float _weaponUseDelay = 1;
    public virtual float WeaponUseDelay
    {
        get => _weaponUseDelay;
        set => _weaponUseDelay = Mathf.Clamp(value, 0, 100);
    }

    [SerializeField]
    private bool _isAttacking = false;
    public bool IsAttacking
    {
        get => _isAttacking;
        set => _isAttacking = value;
    }

    [SerializeField]
    private bool _canAttack = true;
    public bool CanAttack
    {
        get => _canAttack;
        set => _canAttack = IsAlive && value;
    }
    public AudioClip attackReadySFX;

    [HideInInspector]
    public Animator weaponAnimator;

    [Header("Block")]
    public GameObject leftHand;

    [SerializeField]
    private float _shieldUseDelay = 1;
    public virtual float ShieldUseDelay
    {
        get => _shieldUseDelay;
        set => _shieldUseDelay = Mathf.Clamp(value, 0, 100);
    }

    [SerializeField]
    private bool _isBlocking = false;
    public bool IsBlocking
    {
        get => _isBlocking;
        set => _isBlocking = IsAlive && value;
    }

    [SerializeField]
    private bool _canBlock = true;
    public bool CanBlock
    {
        get => _canBlock;
        set => _canBlock = IsAlive && value;
    }
    public AudioClip blockReadySFX;

    [HideInInspector]
    public Animator shieldAnimator;

    [Header("Facing Direction")]
    [SerializeField]
    private bool _isFacingRight = true;
    public virtual bool IsFacingRight
    {
        get => _isFacingRight;
        set
        {
            if (_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
    }

    protected override void Start()
    {
        base.Start();
        InvokeRepeating(nameof(RegenStamina), 0, 0.5f);
    }

    private void RegenStamina()
    {
        if (IsAlive && CurrentStamina < MaxStamina)
        {
            CurrentStamina += StaminaRegenAmount;
        }
    }

    public virtual void SetFacingDirection(Vector2 moveInput)
    {
        switch (moveInput.x)
        {
            case > 0 when !IsFacingRight:
                IsFacingRight = true;
                break;
            case < 0 when IsFacingRight:
                IsFacingRight = false;
                break;
            case 0:
                break;
        }
    }

    public float CalculateAttackPower(Equipment weapon, LivingEntity entity)
    {
        return weapon.multiplier * entity.AttackPower;
    }

    public float CalculateDefensePower(Equipment shield, LivingEntity entity)
    {
        return shield.multiplier * entity.ArmorPower;
    }
}
