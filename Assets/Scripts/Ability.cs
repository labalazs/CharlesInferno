using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public abstract AbilityType Type { get; }

    protected virtual void Awake()
    {
        enabled = false;
    }

    public virtual void OnEquip()
    {
        enabled = true;
    }

    public virtual void OnUnequip()
    {
        enabled = false;
    }
}
