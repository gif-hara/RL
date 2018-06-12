using RL.Events.FieldSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace RL.FieldSystems.CellEvents
{
    /// <summary>
    /// アイテムが存在するセルイベント
    /// </summary>
    public sealed class Item : CellEvent
    {
        private readonly int itemId;

        public Item(int itemId)
        {
            this.itemId = itemId;
        }

        public override void Initialize(CellController owner)
        {
            base.Initialize(owner);
            owner.EventImage.color = Color.green;
        }

        public override void Invoke()
        {
            this.owner.RideActor.Broker.Publish(RideonItem.Get(this.itemId));
        }
    }
}
