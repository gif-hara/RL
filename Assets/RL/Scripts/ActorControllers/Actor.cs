﻿using RL.FieldSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace RL.ActorControllers
{
    /// <summary>
    /// キャラクターの根底部分を司るクラス
    /// </summary>
    public class Actor : MonoBehaviour
    {
        protected RectTransform cachedTransform;

        protected Point Id { get; private set; }

        protected virtual void Awake()
        {
            this.cachedTransform = (RectTransform)this.transform;
        }

        public void Initialize(float size)
        {
            this.cachedTransform.sizeDelta = Vector2.one * size;
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
            this.Move(cell.Id, true);
        }

        protected void Move(Point nextId, bool isForce)
        {
            if (isForce || FieldController.CanMove(this.Id, nextId))
            {
                this.SetPosition(nextId);
            }
        }
    }
}
