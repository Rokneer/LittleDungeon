using UnityEngine;

public class Item : Entity
{
    [SerializeField]
    private Equipment _equipment;
    public Equipment Equipment
    {
        get => _equipment;
        set
        {
            _equipment = value;
            entityName = value.itemName;
            Sprite = value.sprite;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<InventoryManager>(out InventoryManager inventory))
        {
            OnPickUp(inventory);
        }
    }

    protected virtual void OnPickUp(InventoryManager inventory)
    {
        inventory.AddEquipment(Equipment);
        SoundFXManager.Instance.PlaySoundFXClip(Equipment.pickUpSFX, transform, 1f);
        Despawn();
    }

    public virtual void OnUse()
    {
        Debug.Log($"{entityName} was used");
    }
}
