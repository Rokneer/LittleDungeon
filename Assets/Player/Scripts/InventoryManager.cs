using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [Header("Equipment")]
    public GameObject rightHandEquipmentPrefab;
    public GameObject leftHandEquipmentPrefab;

    [HideInInspector]
    public Equipment rightHandEquipment;

    [HideInInspector]
    public Equipment leftHandEquipment;
    private readonly int rightHandEquipmentIndex = 0;
    private readonly int leftHandEquipmentIndex = 0;

    [Header("Inventory")]
    public List<Equipment> weapons;
    public List<Equipment> shields;
    public List<Equipment> potions;

    private void Awake()
    {
        Item[] items = GetComponentsInChildren<Item>();
        foreach (Item item in items)
        {
            Equipment equipment = item.Equipment;
            FilterEquipment(
                equipment,
                () =>
                {
                    rightHandEquipment = equipment;
                    rightHandEquipmentPrefab = equipment.prefab;
                    weapons.Add(equipment);
                },
                () =>
                {
                    leftHandEquipment = equipment;
                    leftHandEquipmentPrefab = equipment.prefab;
                    shields.Add(equipment);
                }
            );
        }
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
                rightHandEquipment = ReplaceEquipment(weapons, rightHandEquipmentIndex);
                rightHandEquipmentPrefab = rightHandEquipment.prefab;
                break;
            case EquipmentSide.Left:
                if (shields.Count <= 1)
                {
                    Debug.Log("No more shields in inventory");
                    return;
                }
                leftHandEquipment = ReplaceEquipment(shields, leftHandEquipmentIndex);
                leftHandEquipmentPrefab = leftHandEquipment.prefab;
                break;
            default:
                Debug.LogError("ERROR: Invalid equipment side");
                break;
        }
    }

    private Equipment ReplaceEquipment(List<Equipment> equipmentList, int equipmentIndex)
    {
        equipmentIndex = equipmentIndex > equipmentList.Count ? 0 : equipmentIndex++;
        Equipment currentEquipment = equipmentList[equipmentIndex];

        return currentEquipment;
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
