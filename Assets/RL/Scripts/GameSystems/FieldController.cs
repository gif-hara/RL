using UnityEngine;
using UnityEngine.Assertions;

namespace RL.GameSystems
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

        public void CreateCells()
        {
            if (this.matrixNumber <= 0)
            {
                Assert.IsTrue(false, "matrixNumberが0なためセルを生成できません");
                return;
            }

            var t = (RectTransform)this.transform;
            Assert.IsNotNull(t);
            var size = (t.rect.width / this.matrixNumber);

            for (int y = 0; y < this.matrixNumber; y++)
            {
                for (int x = 0; x < this.matrixNumber; x++)
                {
                    var cell = Instantiate(this.cellPrefab, t);
                    cell.Initialize(x, y, new Vector2(size * x, -size * y), size);
                }
            }
        }
    }
}
