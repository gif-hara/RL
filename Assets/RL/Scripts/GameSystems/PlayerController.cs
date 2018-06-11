using RL.FieldSystems;
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

        public Point Id { get; private set; }

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
                this.Move(this.Id + new Point(x, y));
            }
        }

        public void Initialize(float size)
        {
            this.cachedTransform.sizeDelta = Vector2.one * size;
            this.cachedTransform.anchoredPosition = FieldController.GetPosition(this.Id);
        }

        public void SetPosition(Point id)
        {
            Assert.IsTrue(FieldController.Cells[id.y, id.x].CanMove, $"移動できないセルに移動しました id = {id}");
            this.Id = id;
            this.cachedTransform.anchoredPosition = FieldController.GetPosition(this.Id);
        }

        public void SetPositionFromRandomRoom()
        {
            var rooms = FieldController.AllRoom;
            var room = rooms[Random.Range(0, rooms.Count)];
            var cell = room.Cells[Random.Range(0, room.Cells.Length)];
            this.SetPosition(cell.Id);
        }

        private void Move(Point nextId)
        {
            if (FieldController.CanMove(nextId))
            {
                this.SetPosition(nextId);
            }
        }

        private static bool GetKeyDown(KeyCode keyCode)
        {
            return Input.GetKeyDown(keyCode);
        }
    }
}
