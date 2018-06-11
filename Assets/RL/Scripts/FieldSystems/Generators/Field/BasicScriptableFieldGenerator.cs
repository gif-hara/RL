﻿using System.Collections.Generic;
using RL.FieldSystems;
using RL.FieldSystems.Generators.Aisle;
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
            // 部屋を作成
            FieldController.RoomMatrix.Clear();
            var horizontal = Random.Range(this.roomMin.x, this.roomMax.x + 1);
            var vertical = Random.Range(this.roomMin.y, this.roomMax.y + 1);
            var horizontalSize = FieldController.XMax / horizontal;
            var verticalSize = FieldController.YMax / vertical;
            for (var y = 0; y < vertical; y++)
            {
                var rooms = new List<Room>();
                FieldController.RoomMatrix.Add(rooms);
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
                }
            }

            // 通路を作成
            this.aisleGenerator.Generate();
        }
    }
}
