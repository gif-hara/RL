using HK.Framework.EventSystems;
using RL.ActorControllers;
using UnityEngine;
using UnityEngine.Assertions;

namespace RL.Events.ActorControllers
{
    /// <summary>
    /// アクターが死亡した際のイベント
    /// </summary>
    public sealed class DiedActor : Message<DiedActor, Actor, Actor>
    {
        /// <summary>
        /// 死亡したアクター
        /// </summary>
        public Actor Died { get { return this.param1; } }

        /// <summary>
        /// <see cref="Died"/>を倒したアクター
        /// </summary>
        public Actor Attacker { get { return this.param2; } }
    }
}
