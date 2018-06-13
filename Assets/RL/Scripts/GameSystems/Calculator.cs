using UnityEngine;
using UnityEngine.Assertions;
using RL.ActorControllers;

namespace RL.GameSystems
{
    /// <summary>
    /// 各種計算を行う
    /// </summary>
    public static class Calculator
    {
        public static int GetDamageFtomAttack(Actor attacker, Actor receiver)
        {
            return attacker.CurrentParameter.Strength - receiver.CurrentParameter.Defence;
        }
    }
}
