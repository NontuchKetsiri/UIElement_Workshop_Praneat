using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class BuffManager : MonoBehaviour
{
    public Player player;
    public BehaviorSubject<List<Buff>> BuffList = new BehaviorSubject<List<Buff>>(new List<Buff>());


    public void AddBuff(BuffType type, float time)
    {
        Buff Buff = null;
        switch (type)
        {
            case BuffType.SpeedBuff:
                Buff = new SpeedBuff();
                break;

            case BuffType.JumpBoostBuff:
                Buff = new JumpBoostBuff();
                break;
        }
        if (Buff != null)
        {
            Buff.ApplyBuff(player);
            var buffList = BuffList.Value;
            buffList.Add(Buff);
            BuffList.OnNext(buffList);
            Observable.Timer(TimeSpan.FromSeconds(time)).Subscribe(obComplete =>
             {
                 Buff.RemoveBuff();
                 RemoveBuffFromList(Buff);
             });
        }
    }

    private void RemoveBuffFromList(Buff target)
    {
        var buffList = BuffList.Value;
        buffList.Remove(target);
        BuffList.OnNext(buffList);
    }
}
