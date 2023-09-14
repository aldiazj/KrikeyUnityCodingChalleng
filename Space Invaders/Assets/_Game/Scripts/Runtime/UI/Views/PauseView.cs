using Runtime.DI;
using Runtime.GameState;
using Runtime.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Runtime.UI.Views
{
    public class PauseView : ViewScript, IRequester
    {
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button mainMenuButton;

        private GameManager gameManager;
        
        public void Init(DependencyContainer dependencyContainer)
        {
            gameManager = dependencyContainer.GetComponentDependency<GameManager>();
        }
        
        private void Awake()
        {
            resumeButton.onClick.AddListener(ResumeGame);
            mainMenuButton.onClick.AddListener(OpenMainMenu);
        }

        private void OpenMainMenu()
        {
            gameManager.RestartGame();
        }

        private void ResumeGame()
        {
            gameManager.ChangeState(GameState.GameState.Play);
        }
    }
}