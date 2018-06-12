using UnityEngine;
using UnityEngine.Assertions;

namespace RL.FieldSystems.CellEvents
{
    /// <summary>
    /// セルを踏んだ際のイベントのインターフェイス
    /// </summary>
    public interface ICellEvent
    {
        void Initialize(CellController owner);

        void Invoke();
    }
}
