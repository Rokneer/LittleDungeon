using UnityEngine;

public abstract class DamageableEntity : Entity
{
    [Space]
    public StatusEffect currentStatusEffect;

    [Header("Health")]
    [SerializeField]
    private float _maxHealth = 80;
    public virtual float MaxHealth
    {
        get => _maxHealth;
        set => _maxHealth = value;
    }

    [SerializeField]
    private float _currentHealth = 80;
    public virtual float CurrentHealth
    {
        get => _currentHealth;
        set
        {
            _currentHealth = Mathf.Clamp(value, 0, MaxHealth);
            if (_currentHealth <= 0)
            {
                IsAlive = false;
                Despawn();
            }
        }
    }

    [SerializeField]
    private bool _isAlive = true;
    public bool IsAlive
    {
        get => _isAlive;
        set => _isAlive = value;
    }

    public AudioClip deathSFX;

    [Header("Invicibility")]
    public bool isInvicible = false;
    public float invicibilityTime = 0.25f;
    public float timeSinceHit = 0;

    [Space]
    [SerializeField]
    private AudioClip[] damageSFX;

    [SerializeField]
    private AudioClip[] healSFX;

    protected override void Update()
    {
        if (isInvicible)
        {
            if (timeSinceHit > invicibilityTime)
            {
                isInvicible = false;
                timeSinceHit = 0;
            }
            timeSinceHit += Time.deltaTime;
        }
    }

    public virtual void OnHealthChange(float changeAmount, bool isDamage)
    {
        if (IsAlive && !isInvicible)
        {
            if (isDamage)
            {
                Hit(changeAmount);
                return;
            }
            Heal(changeAmount);
        }
    }

    private void Hit(float damage)
    {
        CurrentHealth -= damage;
        isInvicible = true;
        animator.SetTrigger("hit");
        SoundFXManager.Instance.PlayRandomSoundFXClip(damageSFX, transform, 1f);
    }

    private void Heal(float healing)
    {
        CurrentHealth += healing;
        isInvicible = true;
        SoundFXManager.Instance.PlayRandomSoundFXClip(healSFX, transform, 1f);
    }
}
