using UnityEngine;
using UnityEngine.Assertions;

namespace RL.GameSystems
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class PlayerController : MonoBehaviour
    {
        private RectTransform cachedTransform;

        private int x, y;

        void Awake()
        {
            this.cachedTransform = (RectTransform)this.transform;
        }

        void Update()
        {
            int x = 0, y = 0;

            if (GetKeyDown(KeyCode.W) || GetKeyDown(KeyCode.Q) || GetKeyDown(KeyCode.E))
            {
                y = -1;
            }
            else if (GetKeyDown(KeyCode.X) || GetKeyDown(KeyCode.Z) || GetKeyDown(KeyCode.C))
            {
                y = 1;
            }
            if (GetKeyDown(KeyCode.A) || GetKeyDown(KeyCode.Q) || GetKeyDown(KeyCode.Z))
            {
                x = -1;
            }
            else if (GetKeyDown(KeyCode.D) || GetKeyDown(KeyCode.E) || GetKeyDown(KeyCode.C))
            {
                x = 1;
            }

            if (x != 0 || y != 0)
            {
                this.Move(this.x + x, this.y + y);
            }
        }

        public void Initialize(int x, int y, float size)
        {
            this.x = x;
            this.y = y;
            this.cachedTransform.sizeDelta = Vector2.one * size;
            this.cachedTransform.anchoredPosition = FieldController.GetPosition(this.x, this.y);
        }

        private void Move(int nextX, int nextY)
        {
            if (FieldController.CanMove(nextX, nextY))
            {
                this.x = nextX;
                this.y = nextY;
                this.cachedTransform.anchoredPosition = FieldController.GetPosition(this.x, this.y);
            }
        }

        private static bool GetKeyDown(KeyCode keyCode)
        {
            return Input.GetKeyDown(keyCode);
        }
    }
}
