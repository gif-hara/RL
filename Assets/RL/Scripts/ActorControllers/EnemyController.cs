using System.Collections.Generic;
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
        }

        public static EnemyController Create(Actor actor)
        {
            return new EnemyController(actor);
        }
    }
}
