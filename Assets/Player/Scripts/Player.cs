using UnityEngine;

public class Player : LivingEntity
{
    [HideInInspector]
    public ParticleSystem dodgeParticles;

    [HideInInspector]
    public InventoryManager inventory;

    [Header("Dodge")]
    public AudioClip dodgeSFX;
    public AudioClip dodgeReadySFX;
    public float dodgeImpulse = 20;
    public float dodgeTime = 0.2f;
    public float dodgeCooldown = 1.2f;
    public bool _isDodging = false;
    public bool IsDodging
    {
        get => _isDodging;
        set
        {
            _isDodging = value;
            /* _trail.emitting = value; */
        }
    }
    public bool _canDodge = true;
    public bool CanDodge
    {
        get => _canDodge;
        set => _canDodge = IsAlive && value;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        dodgeParticles = GetComponent<ParticleSystem>();
        inventory = GetComponent<InventoryManager>();
    }
}
