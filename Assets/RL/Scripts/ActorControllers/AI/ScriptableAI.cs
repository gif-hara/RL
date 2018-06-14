using UnityEngine;
using UnityEngine.Assertions;

namespace RL.ActorControllers.AI
{
    /// <summary>
    /// <see cref="ScriptableObject"/>なAI抽象クラス
    /// </summary>
    public abstract class ScriptableAI : ScriptableObject, IAI
    {
        protected Actor actor;

        public virtual void Initialize(Actor actor)
        {
            this.actor = actor;
        }

        public abstract void Action();
    }
}
