using UnityEngine;
using UnityEngine.Assertions;
using HK.Framework.EventSystems;

namespace RL.Events.FieldSystems
{
    /// <summary>
    /// アイテムの上に乗った際のイベント
    /// </summary>
    public sealed class RideonItem : Message<RideonItem, int>
    {
        /// <summary>
        /// アイテムID
        /// </summary>
        public int ItemId { get { return this.param1; } }
    }
}
