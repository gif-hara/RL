using System;
using System.Collections.Generic;
using RL.Extensions;
using UnityEngine;

namespace RL.ActorControllers
{
    /// <summary>
    /// エネミーの生成テーブル
    /// </summary>
    [Serializable]
    public sealed class EnemyTable
    {
        [SerializeField]
        private List<Element> elements = new List<Element>();

        public Element Lottery
        {
            get
            {
                return this.elements.Lottery();
            }
        }

        [Serializable]
        public class Element : ILottery
        {
            public int specId;

            [SerializeField]
            public int weight;
            public int Weight { get { return this.weight; } }
        }
    }
}
