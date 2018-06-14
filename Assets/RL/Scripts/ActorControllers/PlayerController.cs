using HK.Framework.EventSystems;
using RL.Events.ActorControllers;
using RL.Events.FieldSystems;
using RL.FieldSystems;
using RL.GameSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace RL.ActorControllers
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class PlayerController : MonoBehaviour
    {
        private Actor actor;

        void Awake()
        {
            Assert.IsNull(GameController.Instance.PlayerController, $"すでに{typeof(PlayerController).Name}が存在します");
            GameController.Instance.PlayerController = this;

            this.actor = this.GetComponent<Actor>();
            Assert.IsNotNull(this.actor);

            Assert.IsNull(Actor.Player, $"すでにPlayerが存在します");
            Actor.Player = this.actor;

            Broker.Global.Receive<GeneratedField>()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.actor.SetPositionFromRandomRoom();
                })
                .AddTo(this);

            this.actor.Broker.Receive<RideonItem>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    Debug.Log($"TODO Rideon Item itemId = {x.ItemId} cellId = {this.actor.CellController.Id}");
                    _this.actor.CellController.ClearEvent();
                })
                .AddTo(this);
        }

        void Update()
        {
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
                if(isAction)
                {
                    Broker.Global.Publish(EndTurn.Get(this.actor));
                }
            }
        }

        private static bool GetKeyDown(KeyCode keyCode)
        {
            return Input.GetKeyDown(keyCode);
        }
    }
}
