using Runtime.DI;
using Runtime.GameState;
using Runtime.Interfaces;
using Runtime.UI.Widgets;
using TMPro;
using UnityEngine;

namespace Runtime.UI.Views
{
    public class GameOverView : ViewScript, IRequester
    {
        [SerializeField] private TextMeshProUGUI gameOverTextMesh;
        [SerializeField] private HighScoresSaveWidget highScoresSaveWidget;

        private GameManager gameManager;
        
        public void Init(DependencyContainer dependencyContainer)
        {
            gameManager = dependencyContainer.GetComponentDependency<GameManager>();
        }

        private void OnEnable()
        {
            gameOverTextMesh.text = gameManager.GetGameOverState();
            
            if (gameManager.GetHighScoringSystem().IsHighScore(gameManager.GetScoringSystem().Score))
            {
                highScoresSaveWidget.Show();
                return;
            }

            highScoresSaveWidget.Hide();
        }
    }
}