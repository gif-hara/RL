using System.Collections.Generic;
using RL.GameSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace RL.ActorControllers
{
    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(menuName = "RL/ActorControllers/ActorSpecDatabase")]
    public sealed class ActorSpecDatabase : ScriptableObject
    {
        public List<ActorSpec> Specs;

        public static List<ActorSpec> Get
        {
            get
            {
                return GameController.Instance.ActorSpecDatabase.Specs;
            }
        }
    }
}
