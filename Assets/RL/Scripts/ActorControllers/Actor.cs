using System.Collections.Generic;
using HK.Framework.EventSystems;
using RL.FieldSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace RL.ActorControllers
{
    /// <summary>
    /// キャラクターの根底部分を司るクラス
    /// </summary>
    public sealed class Actor : MonoBehaviour
    {
        [SerializeField]
        private RawImage image;
        public RawImage Image
        {
            get
            {
                return this.image;
            }
        }

        private RectTransform cachedTransform;

        public Point Id { get; private set; }

        public IMessageBroker Broker { get; private set; }

        public ActorSpec Spec { get; private set; }

        public static readonly List<Actor> Instances = new List<Actor>();

        public static Actor Player { get; set; }

        void Awake()
        {
            this.cachedTransform = (RectTransform)this.transform;
            Instances.Add(this);

            this.Broker = HK.Framework.EventSystems.Broker.GetGameObjectBroker(this.gameObject);
        }

        void OnDestroy()
        {
            Instances.Remove(this);
        }

        public void Initialize(float size, ActorSpec spec)
        {
            this.cachedTransform.sizeDelta = Vector2.one * size;
            this.Spec = spec;
            this.image.texture = this.Spec.Texture;
            this.image.color = this.Spec.Color;
        }

        public void SetPosition(Point id)
        {
            Assert.IsTrue(FieldController.Cells[id.y, id.x].CanMove, $"移動できないセルに移動しました id = {id}");
            this.CellController.Leave(this);
            this.Id = id;
            this.cachedTransform.anchoredPosition = FieldController.GetPosition(this.Id);
            this.CellController.Ride(this);
        }

        public void SetPositionFromRandomRoom()
        {
            this.SetPosition(FieldController.RandomPosition);
        }

        public void Move(Point nextId, bool isForce)
        {
            if (isForce || FieldController.CanMove(this.Id, nextId))
            {
                this.SetPosition(nextId);
            }
        }

        /// <summary>
        /// 今乗っているセルを返す
        /// </summary>
        public CellController CellController
        {
            get
            {
                return FieldController.Cells[this.Id.y, this.Id.x];
            }
        }
    }
}
