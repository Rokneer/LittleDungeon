using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
  private PlayerInput playerInput;
  private InputAction moveAction;
  private InputAction dodgeAction;
  private InputAction attackAction;
  private InputAction changeLeftEquipmentAction;
  private InputAction changeRightEquipmentAction;
  private InputAction interactAction;


  private void Awake() {
    playerInput = GetComponent<PlayerInput>();
    moveAction = playerInput.actions["Move"];
    dodgeAction = playerInput.actions["Dodge"];
    attackAction = playerInput.actions["Attack"];
    changeLeftEquipmentAction = playerInput.actions["ChangeLeftEquipment"];
    changeRightEquipmentAction = playerInput.actions["ChangeRightEquipment"];
    interactAction = playerInput.actions["Interact"];
  }
  private void Start() {
    moveAction.started += Move;
    dodgeAction.started += OnDodge;
    attackAction.started += OnAttack;
    changeLeftEquipmentAction.started += OnChangeLeftEquipment;
    changeRightEquipmentAction.started += OnChangeRightEquipment;
    interactAction.started += OnInteract;
  }
  private void OnDisable()
  {
    moveAction.started -= Move;
    dodgeAction.started -= OnDodge;
    attackAction.started -= OnAttack;
    changeLeftEquipmentAction.started -= OnChangeLeftEquipment;
    changeRightEquipmentAction.started -= OnChangeRightEquipment;
    interactAction.started -= OnInteract;
  }

  public void Move(InputAction.CallbackContext context){
    Debug.Log(context.ReadValue<Vector2>());
  }
  public void OnDodge(InputAction.CallbackContext context){
    Debug.Log(context.ReadValue<Boolean>());
  }
  public void OnAttack(InputAction.CallbackContext context){
    Debug.Log(context.ReadValue<Boolean>());
  }
  public void OnChangeLeftEquipment(InputAction.CallbackContext context){
    Debug.Log(context.ReadValue<Boolean>());
  }
  public void OnChangeRightEquipment(InputAction.CallbackContext context){
    Debug.Log(context.ReadValue<Boolean>());
  }
  public void OnInteract(InputAction.CallbackContext context){
    Debug.Log(context.ReadValue<Boolean>());
  }
}
