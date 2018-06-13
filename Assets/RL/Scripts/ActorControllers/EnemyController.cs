using System.Collections.Generic;
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

        // TODO: 死亡したらRemoveする
        public static readonly List<EnemyController> Instances = new List<EnemyController>();

        private EnemyController(Actor actor)
        {
            this.actor = actor;
            Instances.Add(this);
        }

        public static EnemyController Create(Actor actor)
        {
            return new EnemyController(actor);
        }
    }
}
