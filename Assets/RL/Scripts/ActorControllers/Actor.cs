using System.Collections.Generic;
using System.Linq;
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

        public static List<Actor> Enemies = new List<Actor>();

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
            this.SetPosition(FieldController.RandomCellNotRideActor.Id);
        }

        /// <summary>
        /// 次移動する座標を更新する
        /// </summary>
        /// <remarks>
        /// 移動先のセルに何かしらのイベントがある場合は様々な処理を実行する
        /// 敵対する<see cref="Actor"/>の場合は攻撃を行う
        /// 何もいない場合は移動して<see cref="CellEvent"/>を実行します
        /// </remarks>
        public void NextPosition(Point nextId)
        {
            if(FieldController.Cells[nextId.y, nextId.x].RideActor)
            {
                Debug.Log("Attack");
            }
            else if (FieldController.CanMove(this.Id, nextId))
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

        public static void ClearEnemies()
        {
            for(var i = 0; i < Enemies.Count; ++i)
            {
                var enemy = Enemies[i];
                Destroy(enemy.gameObject);
            }
            Enemies.Clear();
        }
    }
}
