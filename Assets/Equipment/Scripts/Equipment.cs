using UnityEngine;

public abstract class Equipment : Entity
{
    public EquipmentType equipmentType;
    public StatusEffect statusEffect;
    public float multiplier;
    public bool isEquipped;
    public AudioClip pickUpSFX;
    public AudioClip actionSFX;

    public abstract void OnPickUp();
    public abstract void OnUse();
}
