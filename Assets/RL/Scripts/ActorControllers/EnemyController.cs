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

        public EnemyController(Actor actor)
        {
            this.actor = actor;
        }
    }
}
