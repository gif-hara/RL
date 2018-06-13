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
            var actor = this.Spawn(ActorSpecDatabase.Get[0]);
            actor.gameObject.AddComponent<PlayerController>();
        }

        public void SpawnEnemy()
        {

        }

        private Actor Spawn(ActorSpec spec)
        {
            var actor = Instantiate(this.prefab, this.actorParent);
            actor.Initialize(FieldController.Size, spec);
            return actor;
        }
    }
}
