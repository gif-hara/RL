using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace RL.ActorControllers
{
    /// <summary>
    /// アクターのパラメータ
    /// </summary>
    [Serializable]
    public sealed class ActorParameter
    {
        public int HitPoint;

        public int Strength;

        public int Defence;

        public int Speed;

        public ActorParameter(ActorParameter other)
        {
            this.HitPoint = other.HitPoint;
            this.Strength = other.Strength;
            this.Defence = other.Defence;
            this.Speed = other.Speed;
        }
    }
}
