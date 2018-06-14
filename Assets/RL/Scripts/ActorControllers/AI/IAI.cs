using UnityEngine;
using UnityEngine.Assertions;

namespace RL.ActorControllers.AI
{
    /// <summary>
    /// <see cref="Actor"/>を行動させるAIのインターフェイス
    /// </summary>
    public interface IAI
    {
        void Initialize(Actor actor);

        /// <summary>
        /// 行動する
        /// </summary>
        void Action();
    }
}
