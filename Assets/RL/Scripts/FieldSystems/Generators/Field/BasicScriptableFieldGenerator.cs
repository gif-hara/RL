using System.Collections.Generic;
using RL.ActorControllers;
using RL.FieldSystems;
using RL.FieldSystems.CellEvents;
using RL.FieldSystems.Generators.Aisle;
using RL.GameSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace RL.FieldSystems.Generators.Field
{
    /// <summary>
    /// 通常のダンジョンフィールドを生成する
    /// </summary>
    [CreateAssetMenu(menuName = "RL/FieldSystem/Generator/Field/Basic")]
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

        [SerializeField]
        private Point offsetSize;

        [SerializeField]
        private Point itemNumber;

        [SerializeField]
        private Point enemyNumber;

        [SerializeField]
        private ScriptableAisleGenerator aisleGenerator;

#if UNITY_EDITOR
        void OnValidate()
        {
            this.offsetSize.x = Mathf.Clamp(this.offsetSize.x, 0, this.paddingMax.x);
            this.offsetSize.y = Mathf.Clamp(this.offsetSize.y, 0, this.paddingMax.y);
        }
#endif

        public override void Generate()
        {
            var roomMatrix = FieldController.RoomMatrix;
            var allRoom = FieldController.AllRoom;

            // 部屋を作成
            var horizontal = Random.Range(this.roomMin.x, this.roomMax.x + 1);
            var vertical = Random.Range(this.roomMin.y, this.roomMax.y + 1);
            var horizontalSize = FieldController.XMax / horizontal;
            var verticalSize = FieldController.YMax / vertical;
            for (var y = 0; y < vertical; y++)
            {
                var rooms = new List<Room>();
                roomMatrix.Add(rooms);
                for (var x = 0; x < horizontal; x++)
                {
                    var room = new Room(
                        new Point(x, y),
                        new Point(x * horizontalSize, y * verticalSize),
                        new Point(horizontalSize, verticalSize),
                        this.paddingMin,
                        this.paddingMax,
                        this.offsetSize
                        );
                    rooms.Add(room);
                    allRoom.Add(room);
                }
            }

            // 通路を作成
            this.aisleGenerator.Generate();

            // アイテムを配置
            var randomItemNumber = Random.Range(this.itemNumber.x, this.itemNumber.y + 1);
            for (var i = 0; i < randomItemNumber; i++)
            {
                var rooms = roomMatrix[Random.Range(0, roomMatrix.Count)];
                var room = rooms[Random.Range(0, rooms.Count)];
                var cell = room.Cells[Random.Range(0, room.Cells.Length)];
                var acquireItemEvent = new Item(0);
                cell.RegisterEvent(acquireItemEvent);
            }

            // エネミーを生成
            var actorSpawner = GameController.Instance.ActorSpawner;
            var randomEnemyNumber = Random.Range(this.enemyNumber.x, this.enemyNumber.y + 1);
            for(var i = 0; i < randomEnemyNumber; ++i)
            {
                actorSpawner.SpawnEnemy(ActorSpecDatabase.Get[1], FieldController.RandomPosition);
            }
        }
    }
}
