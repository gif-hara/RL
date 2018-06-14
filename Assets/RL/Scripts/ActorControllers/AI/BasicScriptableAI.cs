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
        private Actor targetActor;

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
                if(this.targetActor != null)
                {
                    // 狙っているアクターが別の部屋にいる場合は狙わなくなる
                    if(this.targetActor.CellController.Room != cell.Room)
                    {
                        this.targetActor = null;
                        this.targetCell = null;
                    }
                    else
                    {
                        this.targetCell = this.targetActor.CellController;
                    }
                }
                if(this.targetActor == null)
                {
                    var opponents = cell.Room.Cells.RideOpponents(this.actor).Select(c => c.RideActor);
                    if(opponents.Any())
                    {
                        this.targetActor = opponents.Random();
                        this.targetCell = this.targetActor.CellController;
                    }
                }

                // 次の移動先が決まってない場合は決める
                if (this.targetCell == null)
                {
                    Entrance entrance;
                    if(cell.Room.Entrances.Count <= 1)
                    {
                        entrance = cell.Room.Entrances[0];
                    }
                    else
                    {
                        // 自分がいるセルを除く入り口を取得する
                        var entrances = cell.Room.Entrances.Where(e => e.CellController != cell);
                        entrance = entrances.Random();
                    }
                    
                    this.targetCell = entrance.AisleCell;
                }
            }
            // 通路にいるときの挙動
            else
            {
                var movableCells = FieldController.GetCellCross(cell.Id, 1)
                    .Where(c => c.CanMove)
                    .Where(c => c.Id != cell.Id);
                var opponents = movableCells
                    .RideOpponents(this.actor)
                    .Select(c => c.RideActor);

                // 敵対するアクターが存在する場合は優先的に狙う
                if(opponents.Any())
                {
                    this.targetActor = opponents.Random();
                    this.targetCell = this.targetActor.CellController;
                }
                else
                {
                    movableCells = movableCells
                        .Where(c => c.Id != cell.Id + this.lastMoveDirection.Invert().ToPoint()) // 逆方向へは移動しない
                        .Where(c => c.RideActor == null);
                    if(movableCells.Any())
                    {
                        this.targetCell = movableCells.ElementAt(Random.Range(0, movableCells.Count()));
                    }
                    // 移動可能なセルがない場合は後ろへ戻る
                    else
                    {
                        var targetId = cell.Id + this.lastMoveDirection.Invert().ToPoint();
                        this.targetCell = FieldController.Cells[targetId.y, targetId.x];
                    }
                }
            }

            // 目標のセルへ移動する
            Assert.IsTrue(this.targetCell.CanMove, $"{this.actor.Spec.Name}が移動不可能なセルへ移動しようとしています cellId = {this.targetCell.Id}");
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
