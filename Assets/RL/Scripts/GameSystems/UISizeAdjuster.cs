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
        private RectTransform informationView;

#if UNITY_EDITOR
        void Update()
        {
            this.Adjust();
        }
#endif

        public void Adjust()
        {
            this.AdjustFieldView();
            this.AdjusterInformationView();
        }

        private void AdjustFieldView()
        {
            var up = Vector2.up;
            this.fieldView.anchorMin = up;
            this.fieldView.anchorMax = up;
            this.fieldView.pivot = up;
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

        private void AdjusterInformationView()
        {
            var up = Vector2.up;
            this.informationView.anchorMin = up;
            this.informationView.anchorMax = up;
            this.informationView.pivot = up;
            var width = this.root.rect.width;
            var height = this.root.rect.height;
            if (width > height)
            {
                var size = (width - height);
                this.informationView.sizeDelta = new Vector2(size, height);
                this.informationView.anchoredPosition = new Vector2(height, 0.0f);
            }
            else
            {
                var size = (height - width);
                this.informationView.sizeDelta = new Vector2(width, size);
                this.informationView.anchoredPosition = new Vector2(0.0f, -width);
            }
        }
    }
}
