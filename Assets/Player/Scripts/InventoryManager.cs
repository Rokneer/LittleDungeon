using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [Header("Equipment")]
    public GameObject rightHand;
    public GameObject leftHand;

    [HideInInspector]
    public Item rightHandItem;

    [HideInInspector]
    public Item leftHandItem;

    private int rightHandIndex = 0;

    private int leftHandIndex = 0;

    [Header("Inventory")]
    public List<Equipment> weapons;
    public List<Equipment> shields;
    public List<Potion> potions;
    private int _healthPotionsAmount = 0;
    private int HealthPotionsAmount
    {
        get => _healthPotionsAmount;
        set
        {
            _healthPotionsAmount = value;
            PlayerUIManager.Instance.UpdatePotionUI(PotionType.Health, value);
        }
    }
    private int _staminaPotionsAmount = 0;
    private int StaminaPotionsAmount
    {
        get => _staminaPotionsAmount;
        set
        {
            _staminaPotionsAmount = value;
            PlayerUIManager.Instance.UpdatePotionUI(PotionType.Stamina, value);
        }
    }
    private int _curePotionsAmount = 0;
    private int CurePotionsAmount
    {
        get => _curePotionsAmount;
        set
        {
            _curePotionsAmount = value;
            PlayerUIManager.Instance.UpdatePotionUI(PotionType.Cure, value);
        }
    }
    private int _armorPotionsAmount = 0;
    private int ArmorPotionsAmount
    {
        get => _armorPotionsAmount;
        set
        {
            _armorPotionsAmount = value;
            PlayerUIManager.Instance.UpdatePotionUI(PotionType.Armor, value);
        }
    }

    private void Awake()
    {
        rightHandItem = rightHand.GetComponent<Item>();
        rightHandItem.Equipment = weapons[0];

        leftHandItem = leftHand.GetComponent<Item>();
        leftHandItem.Equipment = shields[0];
    }

    private void Start()
    {
        if (potions.Count != 0)
        {
            foreach (Potion potion in potions)
            {
                switch (potion.potionType)
                {
                    case PotionType.Health:
                        HealthPotionsAmount++;
                        break;
                    case PotionType.Stamina:
                        StaminaPotionsAmount++;
                        break;
                    case PotionType.Cure:
                        CurePotionsAmount++;
                        break;
                    case PotionType.Armor:
                        ArmorPotionsAmount++;
                        break;
                }
            }
        }
    }

    public void ChangeEquipment(EquipmentSide side)
    {
        switch (side)
        {
            case EquipmentSide.Right:
                if (weapons.Count <= 1)
                {
                    return;
                }
                rightHandIndex = RotateEquipmentIndex(weapons, rightHandIndex);
                rightHandItem.Equipment = weapons[rightHandIndex];
                break;
            case EquipmentSide.Left:
                if (shields.Count <= 1)
                {
                    return;
                }
                leftHandIndex = RotateEquipmentIndex(shields, leftHandIndex);
                leftHandItem.Equipment = shields[leftHandIndex];
                break;
            default:
                Debug.LogError("ERROR: Invalid equipment side");
                break;
        }
    }

    private int RotateEquipmentIndex(List<Equipment> equipmentList, int currentEquipmentIndex)
    {
        if (currentEquipmentIndex < equipmentList.Count - 1)
        {
            currentEquipmentIndex++;
        }
        else
        {
            currentEquipmentIndex = 0;
        }
        return currentEquipmentIndex;
    }

    public void AddEquipment(Equipment equipment)
    {
        FilterEquipment(
            equipment,
            () => weapons.Add(equipment),
            () => shields.Add(equipment),
            () => potions.Add((Potion)equipment)
        );
    }

    public void RemoveEquipment(Equipment equipment)
    {
        FilterEquipment(
            equipment,
            () => weapons.Remove(equipment),
            () => shields.Remove(equipment),
            () => potions.Remove((Potion)equipment)
        );
    }

    private void FilterEquipment(
        Equipment equipment,
        Action weaponAction,
        Action shieldAction,
        Action potionAction
    )
    {
        switch (equipment.type)
        {
            case EquipmentType.Weapon:
                weaponAction();
                break;
            case EquipmentType.Shield:
                shieldAction();
                break;
            case EquipmentType.Potion:
                potionAction();
                break;
            default:
                Debug.LogError("ERROR: Invalid equipment type");
                break;
        }
    }

    private int ChangePotionAmount(PotionType potionType, bool isPositive)
    {
        return isPositive ? 1 : -1;
    }
}
