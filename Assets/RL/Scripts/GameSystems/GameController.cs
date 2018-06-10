using UnityEngine;
using UnityEngine.Assertions;

namespace RL.GameSystems
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class GameController : MonoBehaviour
    {
        [SerializeField]
        private UISizeAdjuster uiSizeAdjuster;

        [SerializeField]
        private FieldController fieldController;

        void Start()
        {
            this.uiSizeAdjuster.Adjust();
            this.fieldController.CreateCells();
        }
    }
}
