using UnityEngine;
using UnityEngine.Assertions;

namespace RL.ActorControllers.AI
{
    /// <summary>
    /// 通常のAI
    /// </summary>
    /// <remarks>
    /// 部屋に存在するときは次の部屋へ移動する
    /// 通路に存在するときは前へ進み、道がない場合はランダムに他の道を探す
    /// 敵対する<see cref="Actor"/>が存在する場合は攻撃する
    /// </remarks>
    [CreateAssetMenu(menuName = "RL/ActorControllers/AI/Basic")]
    public sealed class BasicScriptableAI : ScriptableAI
    {
        public override void Action()
        {
        }
    }
}
