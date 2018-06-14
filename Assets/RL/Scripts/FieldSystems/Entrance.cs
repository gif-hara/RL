using UnityEngine;
using UnityEngine.Assertions;

namespace RL.FieldSystems
{
    /// <summary>
    /// 部屋の入り口
    /// </summary>
    /// /// <remarks>
    /// *****
    /// *...*
    /// *...*
    /// *..+.
    /// *****
    /// 上記の+の部分の情報を持つ
    /// </remarks>
    public sealed class Entrance
    {
        /// <summary>
        /// 入り口を担うセル
        /// </summary>
        public CellController CellController { get; private set; }

        /// <summary>
        /// 出口の方向
        /// </summary>
        public Direction ToAisleDirection { get; private set; }

        public Entrance(CellController cellController, Direction toAisleDirection)
        {
            this.CellController = cellController;
            this.ToAisleDirection = toAisleDirection;
        }
    }
}
