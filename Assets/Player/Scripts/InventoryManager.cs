using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [Header("Equipment")]
    public GameObject rightHandEquipmentPrefab;
    public GameObject leftHandEquipmentPrefab;

    [SerializeField]
    private Transform rightHandEquipmentTransform;

    [SerializeField]
    private Transform leftHandEquipmentTransform;

    [HideInInspector]
    public Equipment rightHandEquipment;

    [HideInInspector]
    public Equipment leftHandEquipment;

    private int rightHandEquipmentIndex = 0;

    private int leftHandEquipmentIndex = 0;

    [Header("Inventory")]
    public List<Equipment> weapons;
    public List<Equipment> shields;
    public List<Equipment> potions;

    private void Awake()
    {
        rightHandEquipment = rightHandEquipmentPrefab.GetComponent<Item>().Equipment;
        weapons.Add(rightHandEquipment);
        Instantiate(rightHandEquipmentPrefab, rightHandEquipmentTransform);

        leftHandEquipment = leftHandEquipmentPrefab.GetComponent<Item>().Equipment;
        shields.Add(leftHandEquipment);
        Instantiate(leftHandEquipmentPrefab, leftHandEquipmentTransform);
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

                rightHandEquipmentIndex = RotateEquipmentIndex(weapons, rightHandEquipmentIndex);
                rightHandEquipment = weapons[rightHandEquipmentIndex];
                rightHandEquipmentPrefab = rightHandEquipment.prefab;
                break;
            case EquipmentSide.Left:
                if (shields.Count <= 1)
                {
                    Debug.Log("No more shields in inventory");
                    return;
                }

                leftHandEquipmentIndex = RotateEquipmentIndex(shields, leftHandEquipmentIndex);
                leftHandEquipment = shields[leftHandEquipmentIndex];
                leftHandEquipmentPrefab = leftHandEquipment.prefab;
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
