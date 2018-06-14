using HK.Framework.EventSystems;
using RL.Events.ActorControllers;
using RL.Events.FieldSystems;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Assertions;

namespace RL.ActorControllers.AI
{
    /// <summary>
    /// ユーザーの入力によって行動するAI
    /// </summary>
    [CreateAssetMenu(menuName = "RL/ActorControllers/AI/UserInput")]
    public sealed class UserInputAI : ScriptableAI
    {
        public override ScriptableAI Clone
        {
            get
            {
                var clone = CreateInstance<UserInputAI>();
                return clone;
            }
        }

        public override void Initialize(Actor actor)
        {
            base.Initialize(actor);
            
            Assert.IsNull(Actor.Player, $"すでにPlayerが存在します");
            Actor.Player = this.actor;

            Broker.Global.Receive<GeneratedField>()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.actor.SetPositionFromRandomRoom();
                })
                .AddTo(this.actor);

            this.actor.Broker.Receive<RideonItem>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    Debug.Log($"TODO Rideon Item itemId = {x.ItemId} cellId = {this.actor.CellController.Id}");
                    _this.actor.CellController.ClearEvent();
                })
                .AddTo(this.actor);

            actor.UpdateAsObservable()
                .Where(_ => actor.isActiveAndEnabled)
                .SubscribeWithState(actor, (_, a) =>
                {
                    // 足踏み
                    if(GetKeyDown(KeyCode.S) || Input.GetKey(KeyCode.R))
                    {
                        Broker.Global.Publish(EndTurn.Get(this.actor));
                        return;
                    }

                    int x = 0, y = 0;

                    if (GetKeyDown(KeyCode.W) || GetKeyDown(KeyCode.Q) || GetKeyDown(KeyCode.E))
                    {
                        y = -1;
                    }
                    else if (GetKeyDown(KeyCode.X) || GetKeyDown(KeyCode.Z) || GetKeyDown(KeyCode.C))
                    {
                        y = 1;
                    }
                    if (GetKeyDown(KeyCode.A) || GetKeyDown(KeyCode.Q) || GetKeyDown(KeyCode.Z))
                    {
                        x = -1;
                    }
                    else if (GetKeyDown(KeyCode.D) || GetKeyDown(KeyCode.E) || GetKeyDown(KeyCode.C))
                    {
                        x = 1;
                    }

                    if (x != 0 || y != 0)
                    {
                        var isAction = this.actor.NextPosition(this.actor.Id + new Point(x, y));
                        if (isAction)
                        {
                            Broker.Global.Publish(EndTurn.Get(this.actor));
                        }
                    }
                })
                .AddTo(this.actor);
        }

        public override void Action()
        {
            // 何もしない
        }

        private static bool GetKeyDown(KeyCode keyCode)
        {
            return Input.GetKeyDown(keyCode);
        }
    }
}
