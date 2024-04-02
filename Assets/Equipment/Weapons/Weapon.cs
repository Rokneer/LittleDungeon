using UnityEngine;

public class Weapon : Equipment
{
    private WeaponType weaponType;

    private void Awake() 
    {
        equipmentType = EquipmentType.Weapon;
    }

    private void OnDamage() { }

    private void CauseStatusEffect() { }

    public override void OnDespawn()
    {
        Debug.Log("Weapon of type" + weaponType + "despawned");
    }

    public override void OnPickUp()
    {
        Debug.Log("Weapon of type" + weaponType + "was picked up");
    }

    public override void OnUse()
    {
        Debug.Log("Weapon of type" + weaponType + "was used");
    }
}
