using UnityEngine;
using UnityEngine.Assertions;

namespace RL.FieldSystems.Generators.Field
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class ScriptableFieldGenerator : ScriptableObject, IFieldGenerator
    {
        public abstract void Generate();
    }
}
