using UnityEngine;
using UnityEngine.Assertions;

namespace RL.FieldSystems.CellEvents
{
    /// <summary>
    /// セルイベントの抽象クラス
    /// </summary>
    public abstract class CellEvent : ICellEvent
    {
        protected CellController owner;

        public virtual void Initialize(CellController owner)
        {
            this.owner = owner;
        }

        public abstract void Invoke();
    }
}
