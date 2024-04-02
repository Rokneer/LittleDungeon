using UnityEngine;

public abstract class Equipment : Entity
{
    [HideInInspector]
    public EquipmentType equipmentType;
    public StatusEffect statusEffect;
    public float multiplier;
    public bool isEquipped;
    public AudioClip pickUpSFX;
    public AudioClip actionSFX;

    public abstract void OnPickUp();
    public abstract void OnUse();
}
