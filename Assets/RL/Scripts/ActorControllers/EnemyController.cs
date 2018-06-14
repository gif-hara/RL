using System.Collections.Generic;
using HK.Framework.EventSystems;
using RL.Events.ActorControllers;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Assertions;

namespace RL.ActorControllers
{
    /// <summary>
    /// 敵アクターを制御するクラス
    /// </summary>
    public sealed class EnemyController
    {
        private readonly Actor actor;

        public static readonly List<EnemyController> Instances = new List<EnemyController>();

        private EnemyController(Actor actor)
        {
            this.actor = actor;
            Actor.Enemies.Add(this.actor);
            Instances.Add(this);
            actor.OnDestroyAsObservable()
                .SubscribeWithState(this, (_, _this) =>
                {
                    Actor.Enemies.Remove(_this.actor);
                    Instances.Remove(_this);
                });
            Broker.Global.Receive<EndTurn>()
                .Where(x => x.Actor.IsPlayer)
                .SubscribeWithState(this, (e, _this) =>
                {
                    var id = new Point(
                        Random.Range(-1, 2),
                        Random.Range(-1, 2)
                    );
                    _this.actor.NextPosition(_this.actor.Id + id);
                })
                .AddTo(this.actor);
        }

        public static EnemyController Create(Actor actor)
        {
            return new EnemyController(actor);
        }
    }
}
