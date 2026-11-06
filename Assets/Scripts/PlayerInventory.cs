using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventory : MonoBehaviour
{
    public static readonly AbilityType[] slots = { AbilityType.None, AbilityType.DoubleJump, AbilityType.GrapplingHook };
    public event Action changed;
    public int selectedIndex { get; private set; } = 0;

    private readonly HashSet<AbilityType> unlocked = new HashSet<AbilityType>()
    {
        AbilityType.None
    };
    private InputAction ability1Action;
    private InputAction ability2Action;
    private InputAction ability3Action;

    private void Start()
    {
        ability1Action = InputSystem.actions.FindAction("Ability1");
        ability2Action = InputSystem.actions.FindAction("Ability2");
        ability3Action = InputSystem.actions.FindAction("Ability3");
    }

    private void Update()
    {
        if (ability1Action.ReadValue<float>() == 1 && HasAbility(slots[0]))
        {
            Select(0);
        }
        if (ability2Action.ReadValue<float>() == 1 && HasAbility(slots[1]))
        {
            Select(1);
        }
        if (ability3Action.ReadValue<float>() == 1 && HasAbility(slots[2]))
        {
            Select(2);
        }
    }

    private void HandleAbilityCollected(AbilityType type)
    {
        unlocked.Add(type);
        changed?.Invoke();
    }

    private void OnEnable()
    {
        ChestController.onAbilityCollected += HandleAbilityCollected;
    }

    private void OnDisable()
    {
        ChestController.onAbilityCollected -= HandleAbilityCollected;
    }

    public bool HasAbility(AbilityType type)
    {
        return unlocked.Contains(type);
    }

    public void Select(int index)
    {
        if (index < 0 || index >= slots.Length)
        {
            return;
        }
        if (!HasAbility(slots[index]))
        {
            return;
        }
        selectedIndex = index;
        changed?.Invoke();
    }
}
