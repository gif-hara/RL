using RL.GameSystems.FieldSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace RL.GameSystems.FieldGenerators
{
    /// <summary>
    /// フィールドを生成する
    /// </summary>
    public interface IFieldGenerator
    {
        void Generate();
    }
}
