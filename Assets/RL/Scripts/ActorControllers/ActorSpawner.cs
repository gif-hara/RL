using HK.Framework.EventSystems;
using RL.FieldSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace RL.ActorControllers
{
    /// <summary>
    /// <see cref="Actor"/>を生成するクラス
    /// </summary>
    public sealed class ActorSpawner : MonoBehaviour
    {
        [SerializeField]
        private RectTransform actorParent;

        [SerializeField]
        private Actor prefab;

        public void SpawnPlayer()
        {
            this.Spawn(ActorType.Player, ActorSpecDatabase.Get[0]);
        }

        public void SpawnEnemy(ActorSpec spec, Point position)
        {
            var actor = this.Spawn(ActorType.Enemy, spec);
            actor.SetPosition(position);
            EnemyController.Create(actor);
        }

        private Actor Spawn(ActorType type, ActorSpec spec)
        {
            var actor = Instantiate(this.prefab, this.actorParent);
            actor.Initialize(FieldController.Size, type, spec);
            return actor;
        }
    }
}
