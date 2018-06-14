using RL.ActorControllers;
using RL.FieldSystems;
using RL.FieldSystems.Generators.Field;
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
        private RectTransform fieldTransform;

        [SerializeField]
        private ScriptableFieldGenerator fieldGenerator;

        [SerializeField]
        private ActorSpawner actorSpawner;
        public ActorSpawner ActorSpawner { get { return this.actorSpawner; } }

        [SerializeField]
        private ActorSpecDatabase actorSpecDatabase;
        public ActorSpecDatabase ActorSpecDatabase { get { return this.actorSpecDatabase; } }

        void Awake()
        {
            instance = this;
        }

        void Start()
        {
            this.uiSizeAdjuster.Adjust();
            this.fieldController.CreateCells();
            this.actorSpawner.SpawnPlayer();
            this.fieldController.GenerateField(this.fieldGenerator);
        }

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.J))
            {
                this.GenerateField();
            }
        }

        private void GenerateField()
        {
            this.fieldController.GenerateField(this.fieldGenerator);
        }
    }
}
