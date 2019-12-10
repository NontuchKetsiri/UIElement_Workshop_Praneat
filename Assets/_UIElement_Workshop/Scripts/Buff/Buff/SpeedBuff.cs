public class SpeedBuff : Buff
{
    private Player playerObject;
    public void ApplyBuff(Player player)
    {
        playerObject = player;
        var normalSpeed = player.WalkSpeed.Value;
        playerObject.WalkSpeed.OnNext(normalSpeed += 400);
    }

    public void RemoveBuff()
    {
        var normalSpeed = playerObject.WalkSpeed.Value;
        playerObject.WalkSpeed.OnNext(normalSpeed -= 400);
    }
}
