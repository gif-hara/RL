using RL.ActorControllers;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace RL.FieldSystems
{
    /// <summary>
    /// セルを制御するクラス
    /// </summary>
    public sealed class CellController : MonoBehaviour
    {
        [SerializeField]
        private Image movableImage;

        [SerializeField]
        private Image eventImage;

        [SerializeField]
        private Color movableColor;

        [SerializeField]
        private Color notMovableColor;

        public Point Id { get; private set; }

        /// <summary>
        /// 移動可能か
        /// </summary>
        public bool CanMove { get; private set; } = true;

        /// <summary>
        /// このセルに乗っかっている<see cref="Actor"/>
        /// </summary>
        public Actor RideActor { get; private set; }

        public void Initialize(Point id, Vector2 position, float size)
        {
            this.Id = id;
            var t = (RectTransform)this.transform;
            t.anchorMin = Vector2.up;
            t.anchorMax = Vector2.up;
            t.pivot = Vector2.up;
            t.sizeDelta = Vector2.one * size;
            t.anchoredPosition = position;
        }

        public void SetCanMove(bool value)
        {
            this.CanMove = value;
            this.movableImage.color = this.CanMove ? this.movableColor : this.notMovableColor;
        }

        public void Ride(Actor actor)
        {
            this.RideActor = actor;
        }

        public void Leave(Actor actor)
        {
            if(this.RideActor == null)
            {
                return;
            }
            Assert.AreEqual(actor, this.RideActor);
            this.RideActor = null;
        }
    }
}
