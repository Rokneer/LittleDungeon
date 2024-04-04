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
    public List<Equipment> potions;

    private void Awake()
    {
        rightHandItem = rightHand.GetComponent<Item>();
        rightHandItem.Equipment = weapons[0];

        leftHandItem = leftHand.GetComponent<Item>();
        leftHandItem.Equipment = shields[0];
    }

    public void ChangeEquipment(EquipmentSide side)
    {
        switch (side)
        {
            case EquipmentSide.Right:
                if (weapons.Count <= 1)
                {
                    Debug.Log("No more weapons in inventory");
                    return;
                }
                rightHandIndex = RotateEquipmentIndex(weapons, rightHandIndex);
                rightHandItem.Equipment = weapons[rightHandIndex];
                break;
            case EquipmentSide.Left:
                if (shields.Count <= 1)
                {
                    Debug.Log("No more shields in inventory");
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
            () => potions.Add(equipment)
        );
    }

    public void RemoveEquipment(Equipment equipment)
    {
        FilterEquipment(
            equipment,
            () => weapons.Remove(equipment),
            () => shields.Remove(equipment),
            () => potions.Remove(equipment)
        );
    }

    private void FilterEquipment(
        Equipment equipment,
        Action weaponAction,
        Action shieldAction,
        [Optional] Action potionAction
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
}
