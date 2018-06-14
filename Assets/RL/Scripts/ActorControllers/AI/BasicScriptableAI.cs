using System.Linq;
using RL.Extensions;
using RL.FieldSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace RL.ActorControllers.AI
{
    /// <summary>
    /// 通常のAI
    /// </summary>
    /// <remarks>
    /// 部屋に存在するときは次の部屋へ移動する
    /// 通路に存在するときは前へ進み、道がない場合はランダムに他の道を探す
    /// 敵対する<see cref="Actor"/>が存在する場合は攻撃する
    /// </remarks>
    [CreateAssetMenu(menuName = "RL/ActorControllers/AI/Basic")]
    public sealed class BasicScriptableAI : ScriptableAI
    {
        private CellController targetCell;

        private Direction lastMoveDirection;

        public override ScriptableAI Clone
        {
            get
            {
                var clone = CreateInstance<BasicScriptableAI>();
                return clone;
            }
        }

        public override void Action()
        {
            var cell = this.actor.CellController;
            // 部屋にいるときの挙動
            if(cell.IsRoom)
            {
                // 次の移動先が決まってない場合は決める
                if (this.targetCell == null)
                {
                    // 自分がいるセルを除く入り口を取得する
                    var entrances = cell.Room.Entrances.Where(e => e.CellController != cell);
                    var entrance = entrances.ElementAt(Random.Range(0, entrances.Count()));
                    this.targetCell = entrance.AisleCell;
                }
            }
            // 通路にいるときの挙動
            else
            {
                var movableCells = FieldController.GetCellCross(cell.Id, 1)
                    .Where(c => c.CanMove)
                    .Where(c => c.Id != cell.Id)
                    .Where(c => c.Id != cell.Id + this.lastMoveDirection.Invert().ToPoint()); // 逆方向へは移動しない
                this.targetCell = movableCells.ElementAt(Random.Range(0, movableCells.Count()));
            }

            var velocity = FieldController.GetTargetPointSign(cell.Id, this.targetCell.Id);
            this.lastMoveDirection = velocity.ToDirection();
            this.actor.NextPosition(cell.Id + velocity);

            // 通路から部屋へ入った場合
            if(!cell.IsRoom && this.actor.CellController.IsRoom)
            {
                this.targetCell = null;
            }
        }
    }
}
