using System.Collections.Generic;
using System.Linq;
using RL.ActorControllers;
using RL.FieldSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace RL.Extensions
{
    /// <summary>
    /// <see cref="List"/>に関する拡張関数
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// 抽選を行う
        /// </summary>
        public static T Lottery<T>(this List<T> self) where T : ILottery
        {
            var max = 0;
            for(var i = 0; i < self.Count; ++i)
            {
                max += self[i].Weight;
            }
            var r = Random.Range(0, max);
            var current = 0;
            for(var i = 0; i < self.Count; ++i)
            {
                var t = self[i];
                if(current <= r && r < current + t.Weight)
                {
                    return t;
                }
                current += t.Weight;
            }

            Assert.IsTrue(false, $"未定義の動作です max = {max}, r = {r}");
            return default(T);
        }

        /// <summary>
        /// 敵対する<see cref="Actor"/>が存在する<see cref="CellController"/>を返す
        /// </summary>
        public static IEnumerable<CellController> RideOpponents(this IEnumerable<CellController> self, Actor actor)
        {
            return self
                .Where(c => c.RideActor != null && actor.IsOpponent(c.RideActor));
        }

        /// <summary>
        /// ランダムな要素を取得する
        /// </summary>
        public static T Random<T>(this IEnumerable<T> self) => self.ElementAt(UnityEngine.Random.Range(0, self.Count()));
    }
}
