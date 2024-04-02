using System.Collections.Generic;
using UnityEngine;

public class Player : LivingEntity
{
    [HideInInspector]
    public ParticleSystem dodgeParticles;

    [Header("Equipment")]
    public Equipment activeRightHandEquipment;
    public Equipment activeLeftHandEquipment;
    public List<Weapon> weaponsInInventory;
    public List<Shield> shieldsInInventory;
    public List<Potion> potionsInInventory;

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
        foreach (Equipment equipment in GetComponentsInChildren<Equipment>())
        {
            switch (equipment)
            {
                case Weapon:
                    activeRightHandEquipment = equipment;
                    break;
                case Shield:
                case Potion:
                    activeLeftHandEquipment = equipment;
                    break;
                default:
                    Debug.LogError($"ERROR: Invalid equipment of type {equipment}");
                    break;
            }
        }
    }

    public override void OnDespawn()
    {
        throw new System.NotImplementedException();
    }
}