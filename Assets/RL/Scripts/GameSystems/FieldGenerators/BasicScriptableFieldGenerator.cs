using RL.GameSystems.FieldSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace RL.GameSystems.FieldGenerators
{
    /// <summary>
    /// 通常のダンジョンフィールドを生成する
    /// </summary>
    [CreateAssetMenu(menuName = "RL/FieldGenerator/Basic")]
    public sealed class BasicScriptableFieldGenerator : ScriptableFieldGenerator
    {
        [SerializeField]
        private Point roomMin;

        [SerializeField]
        private Point roomMax;

        [SerializeField]
        private Point paddingMin;

        [SerializeField]
        private Point paddingMax;

        public override void Generate()
        {
            FieldController.Rooms.Clear();

            var horizontal = Random.Range(this.roomMin.x, this.roomMax.x + 1);
            var vertical = Random.Range(this.roomMin.y, this.roomMax.y + 1);
            var horizontalSize = FieldController.XMax / horizontal;
            var verticalSize = FieldController.YMax / vertical;
            for (var y = 0; y < vertical; y++)
            {
                for (var x = 0; x < horizontal; x++)
                {
                    var room = new Room(
                        new Point(x * horizontalSize, y * verticalSize),
                        new Point(horizontalSize, verticalSize),
                        this.paddingMin,
                        this.paddingMax
                        );
                    FieldController.Rooms.Add(room);
                }
            }
        }
    }
}
