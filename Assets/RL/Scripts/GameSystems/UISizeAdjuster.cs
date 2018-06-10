using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace RL.GameSystems
{
    /// <summary>
    /// UIのサイズを調整する
    /// </summary>
    [ExecuteInEditMode]
    public sealed class UISizeAdjuster : MonoBehaviour
    {
        [SerializeField]
        private RectTransform root;

        [SerializeField]
        private RectTransform fieldView;

        [SerializeField]
        private RectTransform leftView;

        [SerializeField]
        private RectTransform rightView;

#if UNITY_EDITOR
        void Update()
        {
            this.Adjust();
        }
#endif

        public void Adjust()
        {
            this.AdjustFieldView();
            this.AdjusterLeftView();
            this.AdjusterRightView();
        }

        private void AdjustFieldView()
        {
            Vector2 center = Vector2.one * 0.5f;
            this.fieldView.anchorMin = center;
            this.fieldView.anchorMax = center;
            this.fieldView.pivot = center;
            this.fieldView.anchoredPosition = Vector2.zero;
            var width = this.root.rect.width;
            var height = this.root.rect.height;
            if (width > height)
            {
                this.fieldView.sizeDelta = Vector2.one * height;
            }
            else
            {
                this.fieldView.sizeDelta = Vector2.one * width;
            }
        }

        private void AdjusterLeftView()
        {
            this.leftView.anchorMin = Vector2.up;
            this.leftView.anchorMax = Vector2.up;
            this.leftView.pivot = Vector2.up;
            this.leftView.anchoredPosition = Vector2.zero;
            var width = this.root.rect.width;
            var height = this.root.rect.height;
            if (width > height)
            {
                var size = (width - height) * 0.5f;
                this.leftView.sizeDelta = new Vector2(size, height);
            }
            else
            {
                var size = (height - width) * 0.5f;
                this.leftView.sizeDelta = new Vector2(width, size);
            }
        }

        private void AdjusterRightView()
        {
            this.rightView.anchorMin = Vector2.up;
            this.rightView.anchorMax = Vector2.up;
            this.rightView.pivot = Vector2.up;
            var width = this.root.rect.width;
            var height = this.root.rect.height;
            if (width > height)
            {
                var size = (width - height) * 0.5f;
                this.rightView.sizeDelta = new Vector2(size, height);
                this.rightView.anchoredPosition = new Vector2(height + size, 0.0f);
            }
            else
            {
                var size = (height - width) * 0.5f;
                this.rightView.sizeDelta = new Vector2(width, size);
                this.rightView.anchoredPosition = new Vector2(0.0f, -(width + size));
            }
        }
    }
}
