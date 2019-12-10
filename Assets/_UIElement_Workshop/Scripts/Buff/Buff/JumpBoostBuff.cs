public class JumpBoostBuff : Buff
{
    private Player playerObject;
    public void ApplyBuff(Player player)
    {
        playerObject = player;
        var normalJump = playerObject.JumpForce.Value;
        playerObject.JumpForce.OnNext(normalJump += 200);
    }

    public void RemoveBuff()
    {
        var normalJump = playerObject.JumpForce.Value;
        playerObject.JumpForce.OnNext(normalJump -= 200);
    }
}
