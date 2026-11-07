public class DoubleJumpAbility : Ability
{
    public PlayerMovement movement;
    public override AbilityType Type => AbilityType.DoubleJump;

    public override void OnEquip()
    {
        base.OnEquip();
        movement.allowedJumps = 2;
        movement.remainingJumps = 2;
    }

    public override void OnUnequip()
    {
        base.OnUnequip();
        movement.allowedJumps = 1;
        movement.remainingJumps = 1;
    }
}
