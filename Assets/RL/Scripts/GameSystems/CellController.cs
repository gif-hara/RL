using UnityEngine;
using UnityEngine.Assertions;

namespace RL.GameSystems
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class CellController : MonoBehaviour
    {
        private int x, y;

        public void Initialize(int x, int y, Vector2 position, float size)
        {
            this.x = x;
            this.y = y;
            var t = (RectTransform)this.transform;
            t.anchorMin = Vector2.up;
            t.anchorMax = Vector2.up;
            t.pivot = Vector2.up;
            t.sizeDelta = Vector2.one * size;
            t.anchoredPosition = position;
        }
    }
}
