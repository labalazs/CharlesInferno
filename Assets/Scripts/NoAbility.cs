public class NoAbility : Ability
{
    public override AbilityType Type => AbilityType.None;

    public override void OnEquip()
    {
        base.OnEquip();
    }

    public override void OnUnequip()
    {
        base.OnUnequip();
    }
}
