using UnityEngine;
using UnityEngine.Assertions;

namespace RL.FieldSystems.CellEvents
{
    /// <summary>
    /// アイテムを取得するセルイベント
    /// </summary>
    public sealed class AcquireItem : CellEvent
    {
        private readonly int itemId;

        public AcquireItem(int itemId)
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
            this.owner.RideActor.Broker.Publish(Events.FieldSystems.AcquireItem.Get(this.itemId));
        }
    }
}
