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
        get => base.MaxHealth;
        set
        {
            base.MaxHealth = value;
            stats.maxHealth = value;
        }
    }

    public override float CurrentHealth
    {
        get => base.CurrentHealth;
        set
        {
            base.CurrentHealth = value;
            stats.currentHealth = value;
        }
    }

    public override float MaxStamina
    {
        get => base.MaxStamina;
        set
        {
            base.MaxStamina = value;
            stats.maxStamina = value;
        }
    }
    public override float CurrentStamina
    {
        get => base.CurrentStamina;
        set
        {
            base.CurrentStamina = value;
            stats.currentStamina = value;
        }
    }

    public override float MovementSpeed
    {
        get => base.MovementSpeed;
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
    private bool _canDodge = true;
    public bool CanDodge
    {
        get => _canDodge;
        set => _canDodge = IsAlive && value;
    }

    public override bool IsFacingRight
    {
        get => base.IsFacingRight;
        set
        {
            base.IsFacingRight = value;
            stats.isFacingRight = value;
        }
    }

    public AudioClip dodgeSFX;
    public AudioClip dodgeReadySFX;

    protected override void Awake()
    {
        base.Awake();
        dodgeParticles = GetComponent<ParticleSystem>();
        inventory = GetComponent<InventoryManager>();

        MaxHealth = stats.maxHealth;
        CurrentHealth = stats.currentHealth;
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
    }
}
