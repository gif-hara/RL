using UnityEngine;
using UnityEngine.Assertions;
using HK.Framework.EventSystems;

namespace RL.Events.FieldSystems
{
    /// <summary>
    /// アイテムを取得した際のイベント
    /// </summary>
    public sealed class AcquireItem : Message<AcquireItem, int>
    {
        /// <summary>
        /// アイテムID
        /// </summary>
        public int ItemId { get { return this.param1; } }
    }
}
