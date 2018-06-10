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

        public int X { get; private set; }

        public int Y { get; private set; }

        /// <summary>
        /// 移動可能か
        /// </summary>
        public bool CanMove { get; private set; } = true;

        public void Initialize(int x, int y, Vector2 position, float size)
        {
            this.X = x;
            this.Y = y;
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
