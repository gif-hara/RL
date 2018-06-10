using System.Collections.Generic;
using RL.GameSystems.FieldGenerators;
using UnityEngine;
using UnityEngine.Assertions;

namespace RL.GameSystems.FieldSystems
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class FieldController : MonoBehaviour
    {
        [SerializeField]
        private CellController cellPrefab;

        [SerializeField]
        private int matrixNumber;

        private CellController[,] cells;

        private readonly List<Room> rooms = new List<Room>();

        private float size;

        public void CreateCells()
        {
            if (this.matrixNumber <= 0)
            {
                Assert.IsTrue(false, "matrixNumberが0なためセルを生成できません");
                return;
            }

            this.cells = new CellController[this.matrixNumber, this.matrixNumber];
            var t = (RectTransform)this.transform;
            Assert.IsNotNull(t);
            this.size = (t.rect.width / this.matrixNumber);

            for (int y = 0; y < this.matrixNumber; y++)
            {
                for (int x = 0; x < this.matrixNumber; x++)
                {
                    var cell = Instantiate(this.cellPrefab, t);
                    cell.Initialize(x, y, GetPosition(x, y), this.size);
                    this.cells[y, x] = cell;
                }
            }
        }

        public void GenerateField(IFieldGenerator generator)
        {
            for (int y = 0; y < this.matrixNumber; y++)
            {
                for (int x = 0; x < this.matrixNumber; x++)
                {
                    this.cells[y, x].SetCanMove(false);
                }
            }
            generator.Generate();
        }

        public static Vector2 GetPosition(int x, int y)
        {
            var instance = GameController.Instance.FieldController;
            return new Vector2(x * instance.size, -y * instance.size);
        }

        public static bool CanMove(int x, int y)
        {
            // フィールド外へ侵入しようとしてたら移動できない
            if (x < 0 || x >= XMax || y < 0 || y >= YMax)
            {
                return false;
            }

            return true;
        }

        public static float Size
        {
            get
            {
                return GameController.Instance.FieldController.size;
            }
        }

        public static CellController[,] Cells
        {
            get
            {
                return GameController.Instance.FieldController.cells;
            }
        }

        public static List<Room> Rooms
        {
            get
            {
                return GameController.Instance.FieldController.rooms;
            }
        }

        public static int XMax
        {
            get
            {
                return GameController.Instance.FieldController.matrixNumber;
            }
        }

        public static int YMax
        {
            get
            {
                return GameController.Instance.FieldController.matrixNumber;
            }
        }
    }
}
