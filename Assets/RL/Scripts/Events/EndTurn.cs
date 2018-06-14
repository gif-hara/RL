using HK.Framework.EventSystems;
using RL.ActorControllers;
using UnityEngine;
using UnityEngine.Assertions;

namespace RL.Events.ActorControllers
{
    /// <summary>
    /// <see cref="Actor"/>の行動が終了した際のイベント
    /// </summary>
    public sealed class EndTurn : Message<EndTurn, Actor>
    {
        /// <summary>
        /// ターン終了した<see cref="Actor"/>
        /// </summary>
        /// <returns></returns>
        public Actor Actor { get { return this.param1; } }
    }
}
