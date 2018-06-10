using UnityEngine;
using UnityEngine.Assertions;

namespace RL.FieldSystems.Generators.Aisle
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class ScriptableAisleGenerator : ScriptableObject, IAisle
    {
        public abstract void Generate();
    }
}
