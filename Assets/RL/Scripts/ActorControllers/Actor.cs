using System.Collections.Generic;
using System.Linq;
using HK.Framework.EventSystems;
using RL.Events.ActorControllers;
using RL.FieldSystems;
using RL.GameSystems;
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

        public ActorType Type { get; private set; }

        public IMessageBroker Broker { get; private set; }

        public ActorSpec Spec { get; private set; }

        public ActorParameter CurrentParameter { get; private set; }

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

        public void Initialize(float size, ActorType type, ActorSpec spec)
        {
            this.Type = type;
            this.cachedTransform.sizeDelta = Vector2.one * size;
            this.Spec = spec;
            this.CurrentParameter = new ActorParameter(this.Spec.Parameter);
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
        /// <returns>何かしらの行動を行ったか返す</returns>
        public bool NextPosition(Point nextId)
        {
            var targetActor = FieldController.Cells[nextId.y, nextId.x].RideActor;
            if(targetActor)
            {
                if(this.IsOpponent(targetActor))
                {
                    targetActor.TakeDamage(Calculator.GetDamageFtomAttack(this, targetActor), this);
                    return true;
                }

                Assert.IsTrue(false, $"{this.Spec.Name}は{targetActor.Spec.Name}に対して何もしませんでした");
                return false;
            }
            else if (FieldController.CanMove(this.Id, nextId))
            {
                this.SetPosition(nextId);
                return true;
            }

            return false;
        }

        public void TakeDamage(int damage, Actor attacker)
        {
            this.CurrentParameter.HitPoint -= damage;
            if(this.IsDead)
            {
                this.CellController.Leave(this);
                Destroy(this.gameObject);
                HK.Framework.EventSystems.Broker.Global.Publish(DiedActor.Get(this, attacker));
            }
        }

        /// <summary>
        /// <paramref name="target"/>が敵対する対象であるか
        /// </summary>
        public bool IsOpponent(Actor target)
        {
            if(this.Type == ActorType.Player)
            {
                return target.Type == ActorType.Enemy;
            }
            else if(this.Type == ActorType.Enemy)
            {
                return target.Type == ActorType.Player;
            }

            Assert.IsTrue(false, $"未対応の値です this.Type = {this.Type}");
            return false;
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

        public bool IsDead
        {
            get
            {
                return this.CurrentParameter.HitPoint <= 0;
            }
        }

        public bool IsPlayer
        {
            get
            {
                return this.Type == ActorType.Player;
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
