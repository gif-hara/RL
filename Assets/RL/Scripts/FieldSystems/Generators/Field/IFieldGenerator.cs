using UnityEngine;
using UnityEngine.Assertions;

namespace RL.FieldSystems.Generators.Field
{
    /// <summary>
    /// フィールドを生成する
    /// </summary>
    public interface IFieldGenerator
    {
        void Generate();
    }
}
