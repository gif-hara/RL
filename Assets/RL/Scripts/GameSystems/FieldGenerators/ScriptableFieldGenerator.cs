using RL.GameSystems.FieldSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace RL.GameSystems.FieldGenerators
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class ScriptableFieldGenerator : ScriptableObject, IFieldGenerator
    {
        public abstract void Generate();
    }
}
