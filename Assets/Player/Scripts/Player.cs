using UnityEngine;

public class Player : LivingEntity
{
    [Space]
    public PlayerStats stats;

    [HideInInspector]
    public ParticleSystem dodgeParticles;

    [HideInInspector]
    public InventoryManager inventory;

    public override float MaxHealth
    {
        set
        {
            base.MaxHealth = value;
            stats.maxHealth = value;
        }
    }

    public override float CurrentHealth
    {
        set
        {
            base.CurrentHealth = value;
            stats.currentHealth = value;
        }
    }

    public override float MaxStamina
    {
        set
        {
            base.MaxStamina = value;
            stats.maxStamina = value;
        }
    }
    public override float CurrentStamina
    {
        set
        {
            base.CurrentStamina = value;
            stats.currentStamina = value;
        }
    }

    public override float MovementSpeed
    {
        set
        {
            base.MovementSpeed = value;
            stats.movementSpeed = value;
        }
    }

    public override float AttackPower
    {
        get => base.AttackPower;
        set
        {
            base.AttackPower = value;
            stats.attackPower = value;
        }
    }

    public override float ArmorPower
    {
        get => base.ArmorPower;
        set
        {
            base.ArmorPower = value;
            stats.armorPower = value;
        }
    }
    public override float ArmorDurability
    {
        get => base.ArmorDurability;
        set
        {
            base.ArmorDurability = value;
            stats.armorDurability = value;
        }
    }
    public override float WeaponUseDelay
    {
        set
        {
            base.WeaponUseDelay = Mathf.Clamp(
                value * inventory.rightHandItem.Equipment.useDelayMultiplier,
                0,
                float.PositiveInfinity
            );
            stats.weaponUseDelay = value;
        }
    }
    public override float ShieldUseDelay
    {
        set
        {
            base.ShieldUseDelay = Mathf.Clamp(
                value * inventory.leftHandItem.Equipment.useDelayMultiplier,
                0,
                float.PositiveInfinity
            );
            stats.shieldUseDelay = value;
        }
    }

    [Header("Dodge")]
    [SerializeField]
    private float _dodgeImpulse = 20;
    public float DodgeImpulse
    {
        get => _dodgeImpulse;
        set
        {
            _dodgeImpulse = value;
            stats.dodgeImpulse = value;
        }
    }

    [SerializeField]
    private float _dodgeTime = 0.2f;
    public float DodgeTime
    {
        get => _dodgeTime;
        set
        {
            _dodgeTime = value;
            stats.dodgeTime = value;
        }
    }

    [SerializeField]
    private float _dodgeCooldown = 1.2f;
    public float DodgeCooldown
    {
        get => _dodgeCooldown;
        set
        {
            _dodgeCooldown = value;
            stats.dodgeCooldown = value;
        }
    }

    [SerializeField]
    private bool _isDodging = false;
    public bool IsDodging
    {
        get => _isDodging;
        set
        {
            _isDodging = value;
            /* _trail.emitting = value; */
        }
    }

    [SerializeField]
    private bool _canDodge = true;
    public bool CanDodge
    {
        get => _canDodge;
        set => _canDodge = IsAlive && value;
    }
    public AudioClip dodgeSFX;
    public AudioClip dodgeReadySFX;

    protected override void Awake()
    {
        base.Awake();
        dodgeParticles = GetComponent<ParticleSystem>();
        inventory = GetComponent<InventoryManager>();
        weaponAnimator = rightHand.GetComponentInChildren<Animator>();
        shieldAnimator = leftHand.GetComponentInChildren<Animator>();

        MaxHealth = stats.maxHealth;
        CurrentHealth = stats.currentHealth == 0 ? stats.maxHealth : stats.currentHealth;
        MaxStamina = stats.maxStamina;
        CurrentStamina = stats.currentStamina;
        MovementSpeed = stats.movementSpeed;
        AttackPower = stats.attackPower;
        ArmorPower = stats.armorPower;
        ArmorDurability = stats.armorDurability;
        DodgeImpulse = stats.dodgeImpulse;
        DodgeTime = stats.dodgeTime;
        DodgeCooldown = stats.dodgeCooldown;
        IsFacingRight = stats.isFacingRight;
        WeaponUseDelay = stats.weaponUseDelay;
        ShieldUseDelay = stats.shieldUseDelay;
    }

    public override void SetFacingDirection(Vector2 pointerInput)
    {
        Vector2 distance = pointerInput - (Vector2)transform.position;

        switch (distance.x)
        {
            case > 0:
                rightHand.transform.right = distance;
                if (!IsFacingRight)
                {
                    IsFacingRight = true;
                }
                break;
            case < 0:
                rightHand.transform.right = -distance;
                if (IsFacingRight)
                {
                    IsFacingRight = false;
                }
                break;
            default:
                break;
        }
    }
}
