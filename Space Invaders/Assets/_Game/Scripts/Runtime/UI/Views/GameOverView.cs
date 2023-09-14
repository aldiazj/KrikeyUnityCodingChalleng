using Runtime.DI;
using Runtime.GameState;
using Runtime.Interfaces;
using TMPro;
using UnityEngine;

namespace Runtime.UI.Views
{
    public class GameOverView : ViewScript, IRequester
    {
        [SerializeField] private TextMeshProUGUI gameOverTextMesh;

        private GameManager gameManager;
        
        public void Init(DependencyContainer dependencyContainer)
        {
            gameManager = dependencyContainer.GetComponentDependency<GameManager>();
        }

        private void OnEnable()
        {
            gameOverTextMesh.text = gameManager.GetGameOverState();
        }
    }
}