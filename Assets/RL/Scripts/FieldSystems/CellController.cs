using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace RL.FieldSystems
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class CellController : MonoBehaviour
    {
        [SerializeField]
        private Image image;

        [SerializeField]
        private Color movableColor;

        [SerializeField]
        private Color notMovableColor;

        public Point Id { get; private set; }

        public int X { get { return this.Id.x; } }

        public int Y { get { return this.Id.y; } }

        /// <summary>
        /// 移動可能か
        /// </summary>
        public bool CanMove { get; private set; } = true;

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
            this.image.color = this.CanMove ? this.movableColor : this.notMovableColor;
        }
    }
}
