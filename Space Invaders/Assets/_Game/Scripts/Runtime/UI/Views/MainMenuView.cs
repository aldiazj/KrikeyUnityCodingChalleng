using Runtime.DI;
using Runtime.GameState;
using Runtime.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.UI.Views
{
    public class MainMenuView : ViewScript, IRequester
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button highScoresButton;
        [SerializeField] private Button exitButton;
        
        private GameManager gameManager;
        private ViewsController viewsController;
        
        public void Init(DependencyContainer dependencyContainer)
        {
            gameManager     = dependencyContainer.GetComponentDependency<GameManager>();
            viewsController = dependencyContainer.GetComponentDependency<ViewsController>();
        }

        private void Awake()
        {
            playButton.onClick.AddListener(StartGame);
            highScoresButton.onClick.AddListener(OpenHighScores);
            exitButton.onClick.AddListener(GameManager.Exit);
        }

        private void StartGame()
        {
            gameManager.ChangeState(GameState.GameState.LevelSetup);
        }

        private void OpenHighScores()
        {
            viewsController.OpenView<HighScoresView>(true);
        }
    }
}
