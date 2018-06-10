using UnityEngine;
using UnityEngine.Assertions;

namespace RL.GameSystems
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class GameController : MonoBehaviour
    {
        private static GameController instance;
        public static GameController Instance { get { return instance; } }

        [SerializeField]
        private UISizeAdjuster uiSizeAdjuster;

        [SerializeField]
        private FieldController fieldController;
        public FieldController FieldController { get { return this.fieldController; } }

        [SerializeField]
        private PlayerController playerPrefab;
        public PlayerController PlayerController { get; private set; }

        [SerializeField]
        private RectTransform fieldTransform;

        void Awake()
        {
            instance = this;
        }

        void Start()
        {
            this.uiSizeAdjuster.Adjust();
            this.fieldController.CreateCells();
            this.PlayerController = Instantiate(this.playerPrefab, this.fieldTransform);
            this.PlayerController.Initialize(0, 0, this.fieldController.Size);
        }
    }
}
